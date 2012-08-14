using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using StandardClasses;

namespace DirectoryMobile
{
    public class ClassAppDetails
    {

        private static ClassConnection c_DirCurrentConnection;

        /// <summary>
        // Global Varibles
        /// </summary>

        public static ClassConnection dircurrentconnection
        {
            get { return c_DirCurrentConnection; }
            set { c_DirCurrentConnection = value; }
        }

        public static string configname
        {
            get
            {
                return ConfigurationSettings.AppSettings["ConfigName"];
            }
        }

        public static string dirconnectionname
        {
            get
            {
                return ConfigurationSettings.AppSettings["ConnectionName"];
            }
        }

    }
}