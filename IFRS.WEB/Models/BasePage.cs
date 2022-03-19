using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace IFRS.WEB.Models {
    public class BasePage : System.Web.UI.Page {
        public string Skin => (Master as BaseMasterPage).Skin;

        public string Instruction {
            set => (Master as BaseMasterPage).Instruction = value;
        }
        public DateTime AsAtDate {
            set => (Master as BaseMasterPage).AsAtDate = value;
        }

        public RadNotification Notification => (Master as BaseMasterPage).Notification;

        public string UserId {
            get {
                var value = Session["UserId"];
                if (value != null) {
                    return Convert.ToString(value);
                }
                else {
                    Response.Redirect("~/Views/Login.aspx");
                    return null;
                }
            }
            set => Session["UserId"] = value;
        }
        public string UserBranchId {
            get {
                var value = Session["UserBranchId"];
                if (value != null) {
                    return Convert.ToString(value).PadLeft( 3, '0');
                }
                else {
                    return null;
                }
            }
            set => Session["UserBranchId"] = value;
        }

        public DEM.UserRole UserRole {
            get {
                var value = Session["UserRoleKey"];
                if (value != null) {
                    return (DEM.UserRole)Enum.Parse(typeof(DEM.UserRole), Convert.ToString(value), true);
                }

                return DEM.UserRole.UNAUTHORIZED;
            }
            set => Session["UserRoleKey"] = value.ToString();
        }

        public bool IsAdmin
        {
            get
            {
                var value = Session["IsAdmin"];
                if (value != null)
                {
                    return Convert.ToBoolean(value);
                }
                else
                {
                    return false;
                }
            }
            set => Session["IsAdmin"] = value;
        }

        protected void GoToHome() {
            Response.Redirect($"~/Views/Home.aspx");
        }

    }
}