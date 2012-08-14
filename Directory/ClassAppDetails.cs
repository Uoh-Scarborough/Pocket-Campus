using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;

namespace Directory
{
    public class ClassAppDetails
    {
            private static ClassConnection c_DirectoryCurrentConnection;
            /// <summary>
            // Global Varibles
            /// </summary>
            public static ClassConnection directorycurrentconnection
            {
                get { return c_DirectoryCurrentConnection; }
                set { c_DirectoryCurrentConnection = value; }
            }

            public static string configname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ConfigName"];
                }
            }

            public static string directoryconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ConnectionName"];
                }
            }

            public static string kioskip
            {
                get
                {
                    return ConfigurationSettings.AppSettings["KioskIP"];
                }
            }

            public static string domain
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Domain"];
                }
            }

            public static string ldapserver
            {
                get
                {
                    return ConfigurationSettings.AppSettings["LDAPServer"];
                }
            }

        
    }
}
