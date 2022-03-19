using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace IFRS.WEB.Models {
    public class BaseMasterPage : System.Web.UI.MasterPage {
        /// <summary>
        /// LabelInstruction control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::Telerik.Web.UI.RadLabel LabelInstruction;
        /// <summary>
        /// LabelAsAtDate control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::Telerik.Web.UI.RadLabel LabelAsAtDate;

        /// <summary>
        /// LabelAsAtDateTitle control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::Telerik.Web.UI.RadLabel LabelAsAtDateTitle;

        public string Skin => (Master as SiteMaster).Skin;

        public string Instruction {
            set => this.LabelInstruction.Text = value;
        }

        public DateTime AsAtDate {
            set {
                this.LabelAsAtDateTitle.Text = "As At Date";
                this.LabelAsAtDate.Text = value.ToString("dd MMMM yyyy");
            }
        }

        public RadNotification Notification => (Master as SiteMaster).Notification;

        public string UserId {
            get {
                var value = Session["UserId"];
                if (value != null) {
                    return Convert.ToString(value);
                }
                else {
                    return null;
                }
            }
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

        protected void LogOut() {
            var value = Session["UserId"];
            if (value != null) {
                Session.Remove("UserId");
            }
            Response.Redirect("~/Views/Login.aspx");
        }

    }
}