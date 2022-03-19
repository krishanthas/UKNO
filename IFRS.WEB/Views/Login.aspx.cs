using IFRS.DAL;
using IFRS.DEM;
using System;
using System.Configuration;

namespace IFRS.WEB.Views {
    public partial class Login : Models.BasePage {
        protected void Page_Load(object sender, EventArgs e) {

        }
        protected void ButtonLogin_OnClick(object sender, EventArgs e) {
            var userId = this.TextBoxUserId.Text;
            var password = this.TextBoxPassword.Text;
            bool isAdmin = false;

            if (userId.StartsWith("A-"))
            {
                userId = userId.Substring(2);
                isAdmin = true;
            }

            var isAuthenticated = this.IsAuthenticated(userId, password);

            if (isAuthenticated) {
                var user = DataContext.GetUserById(userId);
                if (user != null) {
                    var userRoleKey = DataContext.GetUserRoleById(user.Id, user.DepartmentId);

                    if (!userRoleKey.Equals(DEM.UserRole.UNAUTHORIZED)) {
                        this.UserId = user.Id;
                        this.UserBranchId = user.DepartmentId.Trim();
                        this.UserRole = userRoleKey;
                        this.IsAdmin = isAdmin;
                        //Session["UserId"] = user.Id;
                        //Session["UserRoleKey"] = userRoleKey;
                        this.LabelError.Text = string.Empty;
                        this.LabelError.Visible = false;

                        if(isAdmin)
                            Response.Redirect("~/Views/Admin.aspx");
                        else
                            Response.Redirect("~/Views/Home.aspx");
                    }
                    else {

                        this.LabelError.Text = "Unauthorized!";
                        this.LabelError.Visible = true;
                    }
                }
                else {
                    this.LabelError.Text = "User Id or Password incorrect!";
                    this.LabelError.Visible = true;
                }
            }
            else {
                this.LabelError.Text = "User Id or Password incorrect!";
                this.LabelError.Visible = true;
            }
        }

        private bool IsAuthenticated(string userId, string password) {
            try {
                var canAuthenticateString = ConfigurationManager.AppSettings["Authentication"];
                if (bool.TryParse(canAuthenticateString, out var canAuthenticate)) {
                    if (canAuthenticate) {
                        return IFRS.DAL.Services.Authenticate(userId, password);
                    }
                    else {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception e) {
                return false;
            }
        }
    }
}