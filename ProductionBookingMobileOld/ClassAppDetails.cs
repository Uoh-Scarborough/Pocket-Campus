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

namespace ProductionBookingMobile
{
    public class ClassAppDetails
    {
            
            /// <summary>
            // Global Varibles
            /// </summary>
            public static ClassConnection pbcurrentconnection
            {
                get { return c_SBCurrentConnection; }
                set { c_SBCurrentConnection = value; }
            }

            public static ClassConnection ttcurrentconnection
            {
                get { return c_TTCurrentConnection; }
                set { c_TTCurrentConnection = value; }
            }

            private static ClassConnection c_SBCurrentConnection, c_TTCurrentConnection;

            public static string configname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ConfigName"];
                }
            }

            public static string pbconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["PBConnectionName"];
                }
            }

            public static string ttconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["TTConnectionName"];
                }
            }

            public static string domain
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Domain"];
                }
            }

            public static string group
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Group"];
                }
            }

            public static string admingroup
            {
                get
                {
                    return ConfigurationSettings.AppSettings["AdminGroup"];
                }
            }

            public static string ldapserver
            {
                get
                {
                    return ConfigurationSettings.AppSettings["LDAPServer"];
                }
            }

            public static string uploaddir
            {
                get
                {
                    return ConfigurationSettings.AppSettings["UploadDir"];
                }
            }

            public static string kioskip
            {
                get
                {
                    return ConfigurationSettings.AppSettings["KioskIP"];
                }
            }

            public static string emaildir
            {
                get
                {
                    return ConfigurationSettings.AppSettings["EmailDir"];
                }
            }

        
    }
}
