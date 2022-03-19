using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DAL
{
    public class CustomerReportItem : Customer
    {
        public string FinalSICR { get; set; }
        public string NPA_PA { get; set; }
        public string MoraStatus { get; set; }
        public decimal? Impairment { get; set; }
        public string LastRejectReason { get; set; }
        public string Remark { get; set; }
    }
}
