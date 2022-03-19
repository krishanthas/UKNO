using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IFRS.WEB {
    public partial class SiteMaster : MasterPage {

        public string Skin => "Web20";

        public Telerik.Web.UI.RadNotification Notification => this.NotificationDialog;

        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}