using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DAL {
    public static class Constants {

        public static class StatusCodes {
            public const string INITIAL = "00";
            public const string ENTERING = "01";
            
            public const string REJECTED_BY_BM = "02";
            public const string REJECTED_BY_AM = "04";
            public const string REJECTED_BY_AGM = "06";
            public const string REJECTED_BY_FINANCE = "08";
            public const string REJECTED_BY_SYSTEM = "09";

            public const string COMPLETED_ENTERING = "10";
            public const string CORRECTED_TO_BM_BY_ENTERING = "11";
            public const string CORRECTED_TO_AM_BY_ENTERING = "12";
            public const string CORRECTED_TO_AGM_BY_ENTERING = "13";
            public const string APPROVED_BY_BM = "15";
            public const string CORRECTED_APPROVED_TO_BM_BY_BM = "16";
            public const string CORRECTED_APPROVED_TO_AM_BY_BM = "17";
            public const string CORRECTED_APPROVED_TO_AGM_BY_BM = "18";
            public const string APPROVED_BY_AM = "20";
            public const string CORRECTED_APPROVED_TO_AM_BY_AM = "21";
            public const string CORRECTED_APPROVED_TO_AGM_BY_AM = "22";
            public const string APPROVED_BY_AGM = "25";
            public const string CORRECTED_APPROVED_TO_AGM_BY_AGM = "26";
            public const string APPROVED_BY_FINANCE = "50";
            public const string APPROVED_BY_FINANCE_CASHFLOWS = "51";

            public const string SYSTEM_PROCESSING = "99";
        }

        public static class FinanceApproveTypes
        {
            public const string NO_OBJECT_EVI_COMPLETE="1";
            public const string WITH_OBJECT_EVI_NO_CASHFLOW= "2";
            public const string WITH_OBJECT_EVI_WITH_CASHFLOW = "3";
        }
    }
}
