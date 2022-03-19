using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM {
    public static class Extensions {
        public static T2 GetValueOrDefault<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 t2) {
            if (dictionary.ContainsKey(key)) {
                return dictionary[key];
            }
            return t2;
        }
    }
}
