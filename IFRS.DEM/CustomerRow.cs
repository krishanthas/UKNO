using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM
{
    public class CustomerRow
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? CapitalOSLKR { get; set; }
        public decimal? ImpairmentAmount { get; set; }
        public decimal? CashFlowsAmount { get; set; }
        public decimal? AmortizedAmount { get; set; }
        public decimal? PresentValue { get; set; }
        public string FinalSICR { get; set; }
        public string NPA_PA { get; set; }
        public string MoraStatus { get; set; }
        public decimal? Impairment { get; set; }
        public string LastRejectReason { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string className { get; set; }
        public int SortOrder { get; set; }
        public DateTime? AsAtDate { get; set; }
        public string Remark { get; set; }
    }
}
