using IFRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IFRS.WEB.Views {
    public partial class Accounts : Models.BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            Initialize();
        }

        private void Initialize() {
            var user = DataContext.GetUserById(UserId);
            if (user != null) {
                //this.LabelUserId.Text = user.Id;
                //this.LabelUsername.Text = user.Name;
                //this.LabelBranchId.Text = user.DepartmentId;

                string customerId = Request.QueryString["customerId"];

                var customer = DataContext.GetCustomerById(customerId);
                if (customer != null) {
                    this.AsAtDate = customer.AsAtDate.GetValueOrDefault();

                    var accounts = DataContext.GetAccountsByCustomerId(customer.Id.Trim());
                    if (accounts != null)
                    {
                        this.GridAccounts.DataSource = accounts.Select(i => new
                        {
                            i.AccountNumber,
                            i.CustomerId,
                            i.Name,
                            i.AmortizedAmount,
                            PresentValue = i.PresentValue,
                            ImpairmentAmount = i.ImpairmentAmount,
                            CashFlowsAmount = i.TotalCashFlowAmount,
                            i.Product,
                            Status = i.ImpairmentStatus,
                            className = this.GetClassName(i.ImpairmentStatus)
                        });

                        this.Instruction = $"PLEASE ENTER CASHFLOWS FOR THE ACCOUNTS OF CUSTOMER {customer.Id} - {customer.Name} ({customer.CapitalOSLKR:n})";
                    }
                    else
                    {
                        this.Notification.Text = $"No ACCOUNT is found for the customer{customer.Id} - {customer.Name}";
                        this.Notification.Show();
                    }
                    
                }
                else
                {
                    this.Notification.Text = $"Customer {customer.Id} - {customer.Name} is not found";
                    this.Notification.Show();
                }
            }
        }

        private string GetClassName(string status) {
            var ret = string.Empty;
            switch (status) {
                case DataContext.Constants.CashFlowsImpairedNoCashFlows:
                case DataContext.Constants.CashFlowsImpairedWithCashFlows:
                case DataContext.Constants.CashFlowsNotImpairedNoCashFlows:
                case DataContext.Constants.CashFlowsNotImpairedWithCashFlows:
                    ret = Constants.ClassNames.AccountComplete;
                    break;
                case DataContext.Constants.CashFlowsImpairmentStatusInProgress:
                    ret = Constants.ClassNames.AccountInProgress;
                    break;
                default:
                    ret = Constants.ClassNames.AccountPending;
                    break;
            }
            return ret;
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            string customerId = Request.QueryString["customerId"];

            try {
                var result = DataContext.SubmitAllAccountCompleteByCustomerId(customerId, UserId, UserRole);
                if (result) {
                    Response.Redirect($"~/Views/Home.aspx");
                }
            }
            catch (Exception e1) {
                this.Notification.Text = e1.Message;
                this.Notification.Show();
            }



            //var account = DataContext.GetAccountsByCustomerId(customerId);
            //var hasNotCompletedAny = account.Select(i => i.ImpairementStatus).Distinct().Any(i => i != DataContext.Constants.CashFlowsImpairementStatusNoCashFlows && i != DataContext.Constants.CashFlowsImpairementStatusWithCashFlows);

            //if (!hasNotCompletedAny) {
            //    var result = DataContext.SubmitAllAccountCompleteByCustomerId(customerId);
            //    if (result) {
            //        Response.Redirect($"~/Views/Home.aspx");
            //    }
            //}
            //else {
            //    this.Notification.Text = "Please complete all accounts before submit!";
            //    this.Notification.Show();
            //}

            //var customerId = Request.QueryString["customerId"];
            //var accountId = "null";
            //Response.Redirect($"~/Views/CashFLows.aspx?customerId={customerId}&accountId={accountId}");
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e) {
            // OnItemDataBound="RadGrid1_ItemDataBound"
            if (e.Item is GridDataItem dataItem) {
                var className = dataItem["className"].Text;

                dataItem["Status"].CssClass = className;

                //e.Item.CssClass = className;
            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e) {
            switch (e.CommandName) {
                case "RowClick":
                    GridDataItem dataItem = e.Item as GridDataItem;
                    this.OnRowClick(source, e, dataItem);
                    break;
                default:
                    break;
            }

        }

        protected void OnRowClick(object source, GridCommandEventArgs e, GridDataItem gridDataItem) {
            var customerId = gridDataItem["CustomerId"].Text.Trim();
            var accountId = gridDataItem["AccountNumber"].Text.Trim();
            if (UserRole == DEM.UserRole.ENTERING) {
                Response.Redirect($"~/Views/CashFlows.aspx?customerId={customerId}&accountId={accountId}");
            }
            else {
                Response.Redirect($"~/Views/CashFlowsReadonly.aspx?customerId={customerId}&accountId={accountId}");
            }
        }

        private void SetAccess() {

        }

    }
}