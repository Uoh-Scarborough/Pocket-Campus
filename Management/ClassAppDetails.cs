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

namespace Management
{
    public class ClassAppDetails
    {
        private static ClassConnection c_ManagementCurrentConnection;
        /// <summary>
        // Global Varibles
        /// </summary>
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

        public static string kioskip1
        {
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

     
    }
}
