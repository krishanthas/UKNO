using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM {
    public enum UserRole {
        ENTERING,
        VERIFY_1,
        VERIFY_2,
        VERIFY_3,
        VERIFY_4,
        UNAUTHORIZED
    }
}
