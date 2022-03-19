using IFRS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IFRS.WEB.Views.Masters {
    public partial class HomeMasterPage : Models.BaseMasterPage {


        protected void Page_Load(object sender, EventArgs e) {
            Initialize();
        }

        private void Initialize() {
            var user = DataContext.GetUserById(base.UserId);
            if (user != null) {
                this.LabelUserId.Text = user.Id;
                this.LabelUsername.Text = user.Name;
                this.LabelBranchName.Text = user.DepartmentName;
            }
            var isNotHome = !this.IsHome();

            this.ButtonBack.Visible = isNotHome;
            this.ButtonHome.Visible = isNotHome;
            this.ButtonAdmin.Visible = this.IsAdmin;

        }

        protected void ButtonLogout_OnClick(object sender, EventArgs e) {
            base.LogOut();
        }
        protected void ButtonBack_OnClick(object sender, EventArgs e) {
            var url = this.GetNextBackUrl();
            Response.Redirect(url);
        }
        protected void ButtonHome_OnClick(object sender, EventArgs e) {
            Response.Redirect("~/Views/Home.aspx");
        }
        protected void ButtonReports_OnClick(object sender, EventArgs e) {
            Response.Redirect("~/Views/Reports.aspx");
        }
        protected void ButtonAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Admin.aspx");
        }

        private bool IsHome() {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            return path.ToUpperInvariant().Contains("HOME");
        }

        private string GetNextBackUrl() {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string query = HttpContext.Current.Request.Url.Query;
            string url;
            var upperPath = path.ToUpperInvariant();
            if (upperPath.Contains("Home".ToUpperInvariant())) {
                url = path;
            }
            else if (upperPath.Contains("Questions".ToUpperInvariant())) {
                url = path.Replace("Questions", "Home");
            }
            else if (upperPath.Contains("Accounts".ToUpperInvariant())) {
                url = path.Replace("Accounts", "Questions");
            }
            else if (upperPath.Contains("CashFlowsReadonly".ToUpperInvariant())) {
                url = path.Replace("CashFlowsReadonly", "Accounts");
            }
            else if (upperPath.Contains("Cashflows".ToUpperInvariant())) {
                url = path.Replace("CashFlows", "Accounts");
            }
            else if (upperPath.Contains("Reports".ToUpperInvariant())) {
                url = path.Replace("Reports", "Home");
            }
            else {
                url = path;
            }
            return $"~{url}{query}";
        }

    }
}