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

namespace Mobile
{
    public class ClassAppDetails
    {

            private static ClassConnection c_TTCurrentConnection, c_CommsCurrentConnection, c_SSCurrentConnection;

            /// <summary>
            // Global Varibles
            /// </summary>
            
            public static ClassConnection ttcurrentconnection
            {
                get { return c_TTCurrentConnection; }
                set { c_TTCurrentConnection = value; }
            }

            public static ClassConnection commscurrentconnection
            {
                get { return c_CommsCurrentConnection; }
                set { c_CommsCurrentConnection = value; }
            }

            public static ClassConnection sscurrentconnection
            {
                get { return c_SSCurrentConnection; }
                set { c_SSCurrentConnection = value; }
            }

            public static string configname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ConfigName"];
                }
            }

            public static string ttconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["TTConnectionName"];
                }
            }

            public static string commsconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["CommsConnectionName"];
                }
            }

            public static string ssconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["SSConnectionName"];
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
