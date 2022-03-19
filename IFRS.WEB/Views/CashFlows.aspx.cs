using IFRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IFRS.WEB.Views {
    public partial class CashFlows : Models.BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Initialize();
                SetAccess();
            }
        }

        private string GetAccountId() => Request.QueryString["accountId"];
        private string GetCustomerId() => Request.QueryString["customerId"];

        private void Initialize() {
            string accountId = GetAccountId();
            var account = DataContext.GetAccountById(accountId);
            if (account != null) {
                this.LabelAccountNumber.Text = account.AccountNumber.Trim();
                this.LabelAccountCurrency.Text = account.Currency.Trim();
                this.LabelOS.Text = account.CapitalLKR?.ToString("#,###.00");
                //this.LabelInterestOS.Text = $"{account.InterestOSLKR:#,###.00}";
                this.LabelInterestRate.Text = $"{account.InterestRate:#,###.00} %";
                this.Label1CustomerId.Text = account.CustomerId;

                this.AsAtDate = account.AsAtDate.GetValueOrDefault();
                this.RadDatePickerAsAtDate.SelectedDate = account.AsAtDate;
                this.LabelAmortizedAmount.Text = $"{account.AmortizedAmount:n}";
                this.NumericTextBoxAmortizedAmount.Value = Convert.ToDouble(account.AmortizedAmount);

                if (this.NumericTextBoxInterestRate.Value == null) {
                    this.NumericTextBoxInterestRate.Value = Convert.ToDouble(account.InterestRate);
                }

                var cashFlows = this.InitializeCashFlows(account.AccountNumber.Trim());

                if (string.IsNullOrEmpty(this.LabelImpairedAmount.Text)) {
                    this.SetLabelImpairedAmount(account, cashFlows);
                }
                this.RadDatePickerForecastDate.MinDate = account.AsAtDate.GetValueOrDefault().AddDays(1);
            }
        }

        private void SetLabelImpairedAmount(Account account, List<CashFlow> cashFlows) {
            var sumCashFlows = cashFlows.Sum(i => i.PresentValue);
            double diffrence = 0;
            if (account.AmortizedAmount > 0) {
                diffrence = Convert.ToDouble((account.AmortizedAmount - sumCashFlows).GetValueOrDefault());
            }
            else {
                diffrence = Convert.ToDouble((account.AmortizedAmount + sumCashFlows).GetValueOrDefault());
            }

            this.LabelImpairedAmount.Value = diffrence;

            var canEdit = Math.Round(diffrence, 2) > 0d;
            this.RadDatePickerForecastDate.Enabled = canEdit;
            this.NumericTextBoxInterestRate.Enabled = canEdit;
            this.NumericTextBoxCashflowAmount.Enabled = canEdit;
            this.RadiosPeriod.Enabled = canEdit;
            this.ButtonAdd.Enabled = canEdit;


            //var nextAmountForMonth = DataContext.CalculatePresentValue()

            //this.NumericTextBoxCashflowAmount.MaxValue = Math.Abs(diffrence);
        }

        private List<CashFlow> InitializeCashFlows(string accountNumber, bool canRebind = false) {
            var cashFlows = DataContext.GetCashFlowsByAccountId(accountNumber);

            var data = cashFlows.Select(i => new {
                i.Id,
                i.Date,
                ForecastAmount = i.Amount,
                i.PresentValue
            });

            this.GridCashFlows.DataSource = data;

            this.NumericTextBoxCashFlowsAmount.Value = Convert.ToDouble(cashFlows.Select(i => i.Amount).DefaultIfEmpty().Sum());
            this.NumericTextBoxPresentValue.Value = Convert.ToDouble(cashFlows.Select(i => i.PresentValue).DefaultIfEmpty().Sum());

            if (canRebind) {
                this.GridCashFlows.Rebind();
            }
            return cashFlows;
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            var cashFlowAmount = Convert.ToDecimal(this.NumericTextBoxCashflowAmount.Value);
            var forecastInterestRate = Convert.ToDecimal(this.NumericTextBoxInterestRate.Value);

            if (cashFlowAmount <= 0) {
                this.Notification.Text = "Please enter greater than zero amount as cash flow amount !";
                this.Notification.Width = 500;
                this.Notification.Show();
                return;
            }
            if (forecastInterestRate <= 0) {
                this.Notification.Text = "Please enter greater than zero amount as interest rate!";
                this.Notification.Width = 500;
                this.Notification.Show();
                return;
            }

            var forecastDate = this.RadDatePickerForecastDate.SelectedDate;
            var months = Convert.ToInt32(this.RadiosPeriod.SelectedValue);

            string accountId = GetAccountId();
            string customerId = GetCustomerId();

            ///* Formula = CF X  1/(1+Rate/12)^month */
            //var presentValue = Convert.ToDouble(cashFlowAmount) * (1 / Math.Pow(Convert.ToDouble(1 + (forecastInterestRate / 100) / 12), period));

            var flow = new CashFlow() {
                AccountNumber = accountId,
                CustomerId = customerId,
                Date = forecastDate,
                InterestRate = forecastInterestRate,
                Amount = cashFlowAmount,
                EntryUserId = UserId,
                //PresentValue = Convert.ToDecimal(Math.Round(presentValue, 2))
            };

            bool isSuccess = false;
            try {
                isSuccess = DataContext.AddCashFlow(flow, months);
            }
            catch (Exception e1) {
                this.Notification.Text = e1.Message;
                this.Notification.Width = 500;
                this.Notification.Show();
            }

            if (isSuccess) {
                var cashFlows = this.InitializeCashFlows(accountId, true);
                var account = DataContext.GetAccountById(accountId);

                this.SetLabelImpairedAmount(account, cashFlows);
            }



        }

        protected void ButtonNoCashFlows_OnClick(object sender, EventArgs e) {
            string accountId = GetAccountId();
            var isSuccess = DataContext.UpdateNoCashFlows(accountId);
            if (isSuccess) {
                string customerId = GetCustomerId();
                Response.Redirect($"~/Views/Accounts.aspx?customerId={customerId}");
            }
        }

        protected void ButtonCompleteAllCashFlows_OnClick(object sender, EventArgs e) {
            string accountId = GetAccountId();
            try {
                var isSuccess = DataContext.UpdateCompleteAllCashFlows(accountId);
                if (isSuccess) {
                    string customerId = GetCustomerId();
                    Response.Redirect($"~/Views/Accounts.aspx?customerId={customerId}");
                }
            }
            catch (Exception e1) {
                this.Notification.Text = e1.Message;
                this.Notification.Width = 500;
                this.Notification.Show();
            }
        }

        protected void ButtonAdd_OnRowDelete(object sender, EventArgs e) {



        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e) {
            switch (e.CommandName) {
                case "RowClick":
                    var dataItem = e.Item as GridDataItem;
                    this.OnRowClick(source, e, dataItem);
                    break;
                default:
                    break;
            }

        }

        protected void GridCashFlows_DeleteCommand(object source, GridCommandEventArgs e) {
            var dataItem = e.Item as GridDataItem;

            var flowId = Convert.ToInt32(dataItem.GetDataKeyValue("Id").ToString());

            var isSuccess = DataContext.DeleteCashFlowById(flowId);
            if (isSuccess) {
                string accountId = GetAccountId();
                var cashFlows = this.InitializeCashFlows(accountId);
                var account = DataContext.GetAccountById(accountId);

                this.SetLabelImpairedAmount(account, cashFlows);
            }
        }

        private void OnRowClick(object source, GridCommandEventArgs e, GridDataItem dataItem) {


        }

        private void SetAccess() {

        }
    }
}