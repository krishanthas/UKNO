//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IFRS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public decimal Id { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string BI_Type { get; set; }
        public Nullable<System.DateTime> AsAtDate { get; set; }
        public decimal No { get; set; }
    }
}
