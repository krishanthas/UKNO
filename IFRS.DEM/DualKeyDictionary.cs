using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DEM {
    public class KeyPair<Tkey1, Tkey2> {
        public KeyPair(Tkey1 key1, Tkey2 key2) {
            Key1 = key1;
            Key2 = key2;
        }

        public Tkey1 Key1 { get; set; }
        public Tkey2 Key2 { get; set; }

        public override int GetHashCode() {
            return Key1.GetHashCode() ^ Key2.GetHashCode();
        }
        public override bool Equals(object obj) {
            KeyPair<Tkey1, Tkey2> o = obj as KeyPair<Tkey1, Tkey2>;
            if (o == null)
                return false;
            else
                return Key1.Equals(o.Key1) && Key2.Equals(o.Key2);
        }
    }
    public class DualKeyDictionary<Tkey1, Tkey2, Tvalue>
        : Dictionary<KeyPair<Tkey1, Tkey2>, Tvalue> {
        public Tvalue this[Tkey1 key1, Tkey2 key2] {
            get {
                return this[new KeyPair<Tkey1, Tkey2>(key1, key2)];
            }
            set {
                this[new KeyPair<Tkey1, Tkey2>(key1, key2)] = value;
            }
        }

        public void Add(Tkey1 tkey1, Tkey2 tkey2, Tvalue tvalue) {
            this[tkey1, tkey2] = tvalue;
        }

    }
}
