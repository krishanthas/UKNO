using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IFRS.DAL {
    public static class Services {

        public static bool Authenticate(string username, string password) {
            try {
                var uri = ConfigurationManager.AppSettings["ActiveDirectoryUri"];
                var accessPoint = new Uri(uri);

                var lsi = new LdapDirectoryIdentifier(accessPoint.Host, accessPoint.Port);
                var lcon = new LdapConnection(lsi);
                var nc = new NetworkCredential(username, password, Environment.UserDomainName);
                lcon.Credential = nc;
                lcon.AuthType = AuthType.Negotiate;
                lcon.Bind(nc);

                return false;
            }
            catch (Exception ex) {
                return false;
            }
        }

    }
}
