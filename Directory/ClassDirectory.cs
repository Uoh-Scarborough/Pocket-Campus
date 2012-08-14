using System;
using System.Collections;
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
    public class ClassDirectory
    {

        private int c_ID;
        private string c_Username, c_Name, c_Surname, c_Telephone, c_Email, c_Room, c_OfficeHours;
        private ClassDepartment c_Department;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public string Username
        {
            get { return c_Username.Trim(); }
            set { c_Username = value.Trim(); }
        }

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string Surname
        {
            get { return c_Surname.Trim(); }
            set { c_Surname = value.Trim(); }
        }

        public ClassDepartment Department
        {
            get { return c_Department; }
            set { c_Department = value; }
        }

        public string Telephone
        {
            get { return c_Telephone.Trim(); }
            set { c_Telephone = value.Trim(); }
        }

        public string Email
        {
            get { return c_Email.Trim(); }
            set { c_Email = value.Trim(); }
        }

        public string Room
        {
            get { return c_Room.Trim(); }
            set { c_Room = value.Trim(); }
        }

        public string OfficeHours
        {
            get { return c_OfficeHours.Trim(); }
            set { c_OfficeHours = value.Trim(); }
        }

         public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassDirectory()
        {
             //Initialise New Class
            Telephone = "";
            Room = "";
            OfficeHours = "";
            Deleted = false;
        }

        public ClassDirectory(string Username)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            string Query = "SELECT People_IDLNK FROM People WHERE People_Username = '" + Username + "' AND People_Deleted = 0;";

            RQ.RunQuery(Query);

            c_ID = Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);

            //c_ID = ID;
            string Query2 = "SELECT * From People WHERE People_IDLNK = " + c_ID;
            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);
            RQ2.RunQuery(Query2);

            c_ID = Convert.ToInt16(RQ2.dataset.Tables[0].Rows[0].ItemArray[0]);
            Username = RQ2.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Name = RQ2.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Surname = RQ2.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            int DID = Convert.ToInt16(RQ2.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
            Department = new ClassDepartment(DID);
            Telephone = RQ2.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            Email = RQ2.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            Room = RQ2.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            OfficeHours = RQ2.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            Deleted = false;
        }

        public ClassDirectory(int ID)
        {
            //Initialise New Class

            c_ID = ID;
            string Query = "SELECT * From People WHERE People_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);
            RQ.RunQuery(Query);

            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
            Username = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Surname = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();

            try
            {
                int DID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
                Department = new ClassDepartment(DID);
            }
            catch
            {
                Department = new ClassDepartment();
            }
            Telephone = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            Email = RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            Room = RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            OfficeHours = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            Deleted = false;
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);
            bool Result;

            int DID = 0;

            try
            {
                DID = Department.ID;
            }
            catch
            {
                DID = 0;
            }

            string Query = "INSERT INTO People (People_Username, People_Name, People_Surname, People_DepartmentIDLNK, People_Telephone, People_Email, People_Room, People_OfficeHours, People_Deleted) VALUES ('" + Username + "','" + Name + "','" + Surname + "'," + DID + ", '" + Telephone + "', '" + Email + "', '" + Room + "', '" + OfficeHours + "', 0) SELECT @@IDENTITY;";
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
            string Query = "UPDATE People SET People_Username = '" + Username + "', People_Name = '" + Name + "', People_Surname = '" + Surname + "', People_DepartmentIDLNK = " + Department.ID + ", People_Telephone = '" + Telephone + "', People_Email = '" + Email + "', People_Room = '" + Room + "', People_OfficeHours = '" + OfficeHours + "', People_Deleted = " + Deleted.GetHashCode() +" WHERE People_IDLNK = " + ID + ";";

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

            RQ.RunQuery("SELECT * FROM PeopleView ORDER BY People_Surname, People_Name");

            return RQ.dataset;
        }

        public static DataSet loadDataset(int DeptID)
        {

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            RQ.RunQuery("SELECT * FROM PeopleView WHERE Department_IDLNK = " + DeptID + " ORDER BY People_Surname, People_Name");

            return RQ.dataset;
        }

        public static DataSet loadDataset(string Search)
        {
            //Search the Names
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            RQ.RunQuery("SELECT * FROM PeopleView WHERE People_Name LIKE '%" + Search + "%' ORDER BY People_Surname, People_Name");

            return RQ.dataset;
        }

        public static bool Exists(string Username)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.directorycurrentconnection);

            string Query = "SELECT People_IDLNK FROM People WHERE People_Username = '" + Username + "';";

            RQ.RunQuery(Query);

            if(RQ.numberofresults >= 1)
            {
                return true;
            } else {
                return false;
            }
        }

        public static string MobileList(int DeptID)
        {
            string List = "<ul>";

            DataSet DS = loadDataset(DeptID);

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                List += "<li><a href=\"?pid=" + DR[0] + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/>" + DR[1] + "</a></li>";
            }

            List += "</ul>";

            return List;
        }

        public static string MobileList(string Search)
        {
            string List = "<ul>";

            DataSet DS = loadDataset(Search);

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                List += "<li><a href=\"?pid=" + DR[0] + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/>" + DR[1] + "</a></li>";
            }

            List += "</ul>";

            return List;
        }
    }
}
