using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM {
    public class User {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Name With Initials")]
        public string NameWithInitials { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Department Id")]
        public string DepartmentId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Reporting Id")]
        public string ReportingId { get; set; }
    }
}
