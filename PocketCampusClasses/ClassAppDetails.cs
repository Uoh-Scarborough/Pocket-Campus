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

namespace PocketCampusClasses
{
    public class ClassAppDetails
    {
            private static ClassConnection c_TTCurrentConnection;
            private static ClassConnection c_BookingCurrentConnection;
            private static ClassConnection c_CommsCurrentConnection;
            private static ClassConnection c_StudentServicesConnection;
            private static ClassConnection c_ManagementCurrentConnection;
            
            /// <summary>
            // Global Varibles
            /// </summary>
            /// 
            public static ClassConnection ttcurrentconnection
            {
                get { return c_TTCurrentConnection; }
                set { c_TTCurrentConnection = value; }
            }

            public static ClassConnection bookingcurrentconnection
            {
                get { return c_BookingCurrentConnection; }
                set { c_BookingCurrentConnection = value; }
            }

            public static ClassConnection commscurrentconnection
            {
                get { return c_CommsCurrentConnection; }
                set { c_CommsCurrentConnection = value; }
            }

            public static ClassConnection studentservicesconnection
            {
                get { return c_StudentServicesConnection; }
                set { c_StudentServicesConnection = value; }
            }

            public static ClassConnection managementcurrentconnection
            {
                get { return c_ManagementCurrentConnection; }
                set { c_ManagementCurrentConnection = value; }
            }

            public static string configname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ConfigName"];
                }
            }

            public static string commsconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["CommsConnectionName"];
                }
            }

            public static string ttconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["TTConnectionName"];
                }
            }

            public static string bookingconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["BookingConnectionName"];
                }
            }

            public static string noticesconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["CommsConnectionName"];
                }
            }

            public static string studentservicesconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["StudentServicesConnectionName"];
                }
            }

            public static string managementconnectionname
            {
                get
                {
                    return ConfigurationSettings.AppSettings["ManagementConnectionName"];
                }
            }

            public static string kioskip
            {
                get
                {
                    return ConfigurationSettings.AppSettings["KioskIP"];
                }
            }

            public static string kioskip1{
                get
                {
                    return ConfigurationSettings.AppSettings["Kiosk1IP"];
                }
            }

            public static string kioskip2
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Kiosk2IP"];
                }
            }

            public static string kioskip3
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Kiosk3IP"];
                }
            }

            public static string kioskip4
            {
                get
                {
                    return ConfigurationSettings.AppSettings["Kiosk4IP"];
                }
            }

            public static string openday
            {
                get
                {
                    return ConfigurationSettings.AppSettings["OpenDay"];
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

            public static string adminemail
            {
                get
                {
                    return ConfigurationSettings.AppSettings["AdminEmail"];
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

            public static string emaildir
            {
                get
                {
                    return ConfigurationSettings.AppSettings["EmailDir"];
                }
            }
        
    }
}
