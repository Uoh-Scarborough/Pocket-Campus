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
    public class ClassKiosks
    {

        private int c_ID;
        private string c_Name;
        private string c_IP;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string IP
        {
            get { return c_IP.Trim(); }
            set { c_IP = value.Trim(); }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassKiosks()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassKiosks(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Kiosks WHERE Kiosk_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);
            RQ.RunQuery(Query);

            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            IP = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[3]);
        }

        public ClassKiosks(string IP)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Kiosks WHERE Kiosk_IPAddress = '" + IP + "';";
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);
            RQ.RunQuery(Query);

            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            IP = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[3]);
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);

            string Query = "";

            Query = "SELECT * FROM Kiosks WHERE Kiosk_Deleted = 0 ORDER BY Kiosk_Name";
           
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

    }
}
