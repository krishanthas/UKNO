using IFRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IFRS.WEB.Views {
    public partial class CashFlowsReadonly : Models.BasePage {
        private string GetAccountId() => Request.QueryString["accountId"];
        private string GetCustomerId() => Request.QueryString["customerId"];
        protected void Page_Load(object sender, EventArgs e) {
            Initialize();
        }

        private void Initialize() {
            string accountId = GetAccountId();
            var account = DataContext.GetAccountById(accountId);
            if(account != null) {

                var cashFlows = this.InitializeCashFlows(account.AccountNumber.Trim());

            }
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
            if (canRebind) {
                this.GridCashFlows.Rebind();
            }
            return cashFlows;
        }

    }
}