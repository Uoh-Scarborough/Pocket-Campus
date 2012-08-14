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
    public class ClassDepartment
    {

        private int c_ID;
        private string c_Name, c_Email, c_Phone, c_Fax, c_Office, c_Opening;
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

        public string Email
        {
            get { return c_Email.Trim(); }
            set { c_Email = value.Trim(); }
        }

        public string Phone
        {
            get { return c_Phone.Trim(); }
            set { c_Phone = value.Trim(); }
        }

        public string Fax
        {
            get { return c_Fax.Trim(); }
            set { c_Fax = value.Trim(); }
        }

        public string Office
        {
            get { return c_Office.Trim(); }
            set { c_Office = value.Trim(); }
        }

        public string Opening
        {
            get { return c_Opening.Trim(); }
            set { c_Opening = value.Trim(); }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassDepartment()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassDepartment(int ID)
        {
            //Initialise New Class

            c_ID = ID;
            string Query = "SELECT * From Department WHERE Department_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);
            RQ.RunQuery(Query);

            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Email = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Phone = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            Fax = RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString();
            Office = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            Opening = RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            Deleted = false;
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);
            bool Result;
            string Query = "INSERT INTO Department (Department_Name,Department_Email,Department_Phone,Department_Fax,Department_Office,Department_Office,Department_Opening,Department_Deleted) VALUES ('" + Name + "','" + Email + "','" + Phone + "','" + Fax + "','" + Office + "','" + Opening + "',0) SELECT @@IDENTITY;";
            try
            {
                RQ.RunQuery(Query);
                c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.directorycurrentconnection);
            bool Result;
            string Query = "UPDATE Department SET Department_Name = '" + Name + "', Department_Email = '" + Email + "' , Department_Phone = '" + Phone + "', Department_Fax = '" + Fax + "', Department_Office = '" + Office + "', Department_Opening = '" + Opening + "', Department_Deleted = " + Deleted.GetHashCode() + " WHERE Department_IDLNK = " + ID + ";";

            try
            {
                WQ.RunQuery(Query);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }

            return Result;
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            string Query = "";
    
            //Show Valid
            Query = "SELECT * FROM Department WHERE Department_Deleted = 0 ORDER BY Department_Name";
    
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static ListItem[] loadList()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            RQ.RunQuery("SELECT * FROM Department WHERE Department_Deleted = 0 ORDER BY Department_Name");

            ListItem[] List = new ListItem[RQ.numberofresults];

            int Counter = 0;

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ListItem LI = new ListItem(DR[1].ToString(), DR[0].ToString());

                List[Counter] = LI;

                Counter += 1;
            }

            return List;
        }

        public static string MobileList()
        {
            string List = "<ul>";

            DataSet DS = loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                List += "<li><a href=\"?did=" + DR[0] + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/>" + DR[1] +  "</a></li>";
            }

            List += "</ul>";

            return List;
        }

    }
}
