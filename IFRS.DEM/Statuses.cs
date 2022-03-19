using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM {
    public class AccessEntry {
        public bool Enabled { get; set; }
        public string State { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }

    public static class Constants {


        public static readonly DualKeyDictionary<UserRole, string, AccessEntry> AccessRules = new DualKeyDictionary<UserRole, string, AccessEntry>() {

            { UserRole.ENTERING,  "00", new AccessEntry() { State = "00", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.ENTERING,  "01", new AccessEntry() { State = "01", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.ENTERING,  "02", new AccessEntry() { State = "02", Enabled = true, SortOrder = 1, Description = "Rejected by Branch Manager", Class = "customer-pending-rejected-for-me" } },
            { UserRole.ENTERING,  "04", new AccessEntry() { State = "04", Enabled = true, SortOrder = 1, Description = "Rejected by Area Manager", Class = "customer-pending-rejected-for-me" } },
            { UserRole.ENTERING,  "06", new AccessEntry() { State = "06", Enabled = true, SortOrder = 1, Description = "Rejected by AGM", Class = "customer-pending-rejected-for-me" } },
            { UserRole.ENTERING,  "08", new AccessEntry() { State = "08", Enabled = true, SortOrder = 2, Description = "Pending", Class = "customer-pending-rejected-for-me" } },           //08 not in use
            { UserRole.ENTERING,  "09", new AccessEntry() { State = "09", Enabled = true, SortOrder = 2, Description = "Pending", Class = "customer-pending-rejected-for-me" } },           //09 not in use
            { UserRole.ENTERING,  "10", new AccessEntry() { State = "10", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified"} },
            { UserRole.ENTERING,  "11", new AccessEntry() { State = "11", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified"} },
            { UserRole.ENTERING,  "12", new AccessEntry() { State = "12", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified"} },
            { UserRole.ENTERING,  "13", new AccessEntry() { State = "13", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified"} },
            { UserRole.ENTERING,  "15", new AccessEntry() { State = "15", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "16", new AccessEntry() { State = "16", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "17", new AccessEntry() { State = "17", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "18", new AccessEntry() { State = "18", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "20", new AccessEntry() { State = "20", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "21", new AccessEntry() { State = "21", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "22", new AccessEntry() { State = "22", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "25", new AccessEntry() { State = "25", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "26", new AccessEntry() { State = "26", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "50", new AccessEntry() { State = "50", Enabled = false, SortOrder = 3, Description = "Completed", Class = "customer-verified" } },
            { UserRole.ENTERING,  "51", new AccessEntry() { State = "51", Enabled = true, SortOrder = 3, Description = "Pending Cashflows", Class = "customer-pending" } },
            { UserRole.ENTERING,  "99", new AccessEntry() { State = "99", Enabled = false, SortOrder = 4, Description = "System Processing", } },


            { UserRole.VERIFY_1,  "00", new AccessEntry() { State = "00", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "01", new AccessEntry() { State = "01", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "02", new AccessEntry() { State = "02", Enabled = false ,SortOrder = 2, Description = "Rejected", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "04", new AccessEntry() { State = "04", Enabled = false ,SortOrder = 2, Description = "Rejected by Area Manager", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "06", new AccessEntry() { State = "06", Enabled = false ,SortOrder = 2, Description = "Rejected by AGM", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "08", new AccessEntry() { State = "08", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "09", new AccessEntry() { State = "09", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_1,  "10", new AccessEntry() { State = "10", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.VERIFY_1,  "11", new AccessEntry() { State = "11", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending"} },
            { UserRole.VERIFY_1,  "12", new AccessEntry() { State = "12", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending"} },
            { UserRole.VERIFY_1,  "13", new AccessEntry() { State = "13", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending"} },
            { UserRole.VERIFY_1,  "15", new AccessEntry() { State = "15", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "16", new AccessEntry() { State = "16", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "17", new AccessEntry() { State = "17", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "18", new AccessEntry() { State = "18", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "20", new AccessEntry() { State = "20", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "21", new AccessEntry() { State = "21", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "22", new AccessEntry() { State = "22", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "25", new AccessEntry() { State = "25", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "26", new AccessEntry() { State = "26", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "50", new AccessEntry() { State = "50", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "51", new AccessEntry() { State = "51", Enabled = false, SortOrder = 3, Description = "Pending Cashflows", Class = "customer-verified" } },
            { UserRole.VERIFY_1,  "99", new AccessEntry() { State = "99", Enabled = false, SortOrder = 4, Description = "System Processing", } },


            { UserRole.VERIFY_2,  "00", new AccessEntry() { State = "00", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "01", new AccessEntry() { State = "01", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "02", new AccessEntry() { State = "02", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "04", new AccessEntry() { State = "04", Enabled = false ,SortOrder = 2, Description = "Rejected", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "06", new AccessEntry() { State = "06", Enabled = false ,SortOrder = 2, Description = "Rejected by AGM", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "08", new AccessEntry() { State = "08", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "09", new AccessEntry() { State = "09", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "10", new AccessEntry() { State = "10", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_2,  "11", new AccessEntry() { State = "11", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-verified"} },
            { UserRole.VERIFY_2,  "12", new AccessEntry() { State = "12", Enabled = false, SortOrder = 2, Description = "Rejected", Class = "customer-verified"} },
            { UserRole.VERIFY_2,  "13", new AccessEntry() { State = "13", Enabled = false, SortOrder = 2, Description = "Rejected by AGM", Class = "customer-verified"} },
            { UserRole.VERIFY_2,  "15", new AccessEntry() { State = "15", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.VERIFY_2,  "16", new AccessEntry() { State = "16", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.VERIFY_2,  "17", new AccessEntry() { State = "17", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending" } },
            { UserRole.VERIFY_2,  "18", new AccessEntry() { State = "18", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending" } },
            { UserRole.VERIFY_2,  "20", new AccessEntry() { State = "20", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "21", new AccessEntry() { State = "21", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "22", new AccessEntry() { State = "22", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "25", new AccessEntry() { State = "25", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "26", new AccessEntry() { State = "26", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "50", new AccessEntry() { State = "50", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "51", new AccessEntry() { State = "51", Enabled = false, SortOrder = 3, Description = "Pending Cashflows", Class = "customer-verified" } },
            { UserRole.VERIFY_2,  "99", new AccessEntry() { State = "99", Enabled = false, SortOrder = 4, Description = "System Processing", } },


            { UserRole.VERIFY_3,  "00", new AccessEntry() { State = "00", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_3,  "01", new AccessEntry() { State = "01", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_3,  "02", new AccessEntry() { State = "02", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "04", new AccessEntry() { State = "04", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "06", new AccessEntry() { State = "06", Enabled = false ,SortOrder = 2, Description = "Rejected", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "08", new AccessEntry() { State = "08", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "09", new AccessEntry() { State = "09", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "10", new AccessEntry() { State = "10", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "11", new AccessEntry() { State = "11", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-verified"} },
            { UserRole.VERIFY_3,  "12", new AccessEntry() { State = "12", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-verified"} },
            { UserRole.VERIFY_3,  "13", new AccessEntry() { State = "13", Enabled = false, SortOrder = 2, Description = "Rejected", Class = "customer-verified"} },
            { UserRole.VERIFY_3,  "15", new AccessEntry() { State = "15", Enabled = false ,SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_3,  "16", new AccessEntry() { State = "16", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "17", new AccessEntry() { State = "17", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "18", new AccessEntry() { State = "18", Enabled = false, SortOrder = 2, Description = "Rejected", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "20", new AccessEntry() { State = "20", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.VERIFY_3,  "21", new AccessEntry() { State = "21", Enabled = true, SortOrder = 0, Description = "Pending to me", Class = "customer-pending" } },
            { UserRole.VERIFY_3,  "22", new AccessEntry() { State = "22", Enabled = true, SortOrder = 1, Description = "Pending to me - Previously rejected", Class = "customer-pending" } },
            { UserRole.VERIFY_3,  "25", new AccessEntry() { State = "25", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "26", new AccessEntry() { State = "26", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "50", new AccessEntry() { State = "50", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "51", new AccessEntry() { State = "51", Enabled = false, SortOrder = 3, Description = "Pending Cashflows", Class = "customer-verified" } },
            { UserRole.VERIFY_3,  "99", new AccessEntry() { State = "99", Enabled = false, SortOrder = 4, Description = "System Processing", } },


            { UserRole.VERIFY_4,  "00", new AccessEntry() { State = "00", Enabled = false , SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "01", new AccessEntry() { State = "01", Enabled = false, SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_4,  "02", new AccessEntry() { State = "02", Enabled = false , SortOrder = 2, Description = "Rejected by Branch Manager", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "04", new AccessEntry() { State = "04", Enabled = false, SortOrder = 2, Description = "Rejected by Area Manager", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_4,  "06", new AccessEntry() { State = "06", Enabled = false, SortOrder = 2, Description = "Rejected by AGM", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_4,  "08", new AccessEntry() { State = "08", Enabled = false , SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "09", new AccessEntry() { State = "09", Enabled = false , SortOrder = 4, Description = "Pending", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "10", new AccessEntry() { State = "10", Enabled = false, SortOrder = 3, Description = "In Branch Manager Level", Class = "customer-pending-rejected" } },
            { UserRole.VERIFY_4,  "11", new AccessEntry() { State = "11", Enabled = false, SortOrder = 3, Description = "In Branch Manager Level - Previously rejected", Class = "customer-verified"} },
            { UserRole.VERIFY_4,  "12", new AccessEntry() { State = "12", Enabled = false, SortOrder = 3, Description = "In Branch Manager Level - Previously rejected", Class = "customer-verified"} },
            { UserRole.VERIFY_4,  "13", new AccessEntry() { State = "13", Enabled = false, SortOrder = 3, Description = "In Branch Manager Level - Previously rejected", Class = "customer-verified"} },
            { UserRole.VERIFY_4,  "15", new AccessEntry() { State = "15", Enabled = false , SortOrder = 2, Description = "In Area Manager Level", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "16", new AccessEntry() { State = "16", Enabled = false, SortOrder = 2, Description = "In Area Manager Level", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "17", new AccessEntry() { State = "17", Enabled = false, SortOrder = 2, Description = "In Area Manager Level - Previously rejected", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "18", new AccessEntry() { State = "18", Enabled = false, SortOrder = 2, Description = "In Area Manager Level - Previously rejected", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "20", new AccessEntry() { State = "20", Enabled = false , SortOrder = 1, Description = "In AGM Level", Class = "customer-pending-rejected"} },
            { UserRole.VERIFY_4,  "21", new AccessEntry() { State = "21", Enabled = false, SortOrder = 1, Description = "In AGM Level - Previously rejected", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "22", new AccessEntry() { State = "22", Enabled = false, SortOrder = 1, Description = "In AGM Level - Previously rejected", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "25", new AccessEntry() { State = "25", Enabled = true, SortOrder = 0, Description = "Approved", Class = "customer-pending" } },
            { UserRole.VERIFY_4,  "26", new AccessEntry() { State = "26", Enabled = true, SortOrder = 0, Description = "Approved", Class = "customer-pending" } },
            { UserRole.VERIFY_4,  "50", new AccessEntry() { State = "50", Enabled = false, SortOrder = 3, Description = "Approved", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "51", new AccessEntry() { State = "51", Enabled = false, SortOrder = 3, Description = "Pending Cashflows", Class = "customer-verified" } },
            { UserRole.VERIFY_4,  "99", new AccessEntry() { State = "99", Enabled = false, SortOrder = 4, Description = "System Processing", } },


        };
    }
}