﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace IFRS.WEB {
    public static class PageExtensions {
        public static IEnumerable<Control> All(this ControlCollection controls) {
            foreach (Control control in controls) {
                foreach (Control grandChild in control.Controls.All())
                    yield return grandChild;

                yield return control;
            }
        }
    }
}