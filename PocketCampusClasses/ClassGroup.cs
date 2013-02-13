
using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassGroup : ClassBase
    {
        private string c_Name;
        
        private ArrayList c_Members = new ArrayList();
        private ArrayList c_Constraints = new ArrayList();

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public ArrayList Members
        {
            get { return c_Members; }
            set { c_Members = value; }
        }

        public ArrayList Constraints
        {
            get { return c_Constraints; }
            set { c_Constraints = value; }

        }

        public ClassGroup()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassGroup(int ID)
        {
            //Initialise New Class
            
            string Query = String.Format("SELECT * From Groups WHERE Group_ID_LNK = {0}",ID);
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassGroup(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Group_ID_LNK"]);
            Name = DR["Group_Name"].ToString();
            Deleted = Convert.ToBoolean(DR["Group_Deleted"]);


            try
            {
                //Load Constraints
                ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

                string Query = "";
                Query = String.Format("SELECT Constraint_ID_LNK FROM Constraint_Group_View WHERE Constraint_Group_Group_ID_LNK = {0} ORDER BY Constraint_Title;", ID);
                RQ.RunQuery(Query);

                c_Constraints = new ArrayList();

                foreach (DataRow DR1 in RQ.dataset.Tables[0].Rows)
                {
                    c_Constraints.Add(Convert.ToInt32(DR1["Constraint_ID_LNK"]));
                }

                c_Constraints.Sort();
            }
            catch
            {

                //Do Nothing
            }

        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("INSERT INTO Groups (Group_Name, Group_Deleted) VALUES ('{0}',0) SELECT @@IDENTITY; ", Name);

            RQ.RunQuery(Query);
            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);

            foreach(int ConstraintID in c_Constraints){
                string Query2 = String.Format("INSERT INTO Constraint_Groups (Constraint_Group_Group_ID_LNK, Constraint_Group_Constraint_ID_LNK, Constraint_Group_Deleted) VALUES ({0},{1},0);", c_ID, ConstraintID);

                RQ.RunQuery(Query2);
            }

            Result = true;
            
            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("Update GROUPS SET Group_Name = '{0}', Group_Deleted = {1} WHERE Group_ID_LNK = {2};", Name, Deleted.GetHashCode(), ID);

            WQ.RunQuery(Query);

            Result = true;

            try
            {

                string Query2 = String.Format("Update Constraint_Groups SET Constraint_Group_Deleted = 1 WHERE Constraint_Group_Group_ID_LNK = {0};", ID);

                WQ.RunQuery(Query2);

                foreach (object ConstraintID in c_Constraints)
                {
                    string Query3 = String.Format("INSERT INTO Constraint_Groups (Constraint_Group_Group_ID_LNK, Constraint_Group_Constraint_ID_LNK, Constraint_Group_Deleted) VALUES ({0},{1},0);", c_ID, Convert.ToInt32(ConstraintID));

                    WQ.RunQuery(Query3);
                }

                Result = true;

            }
            catch
            {
                //Do Nothing
            }
            
            return Result;
        }

        public void LoadMembers()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT * FROM GroupMembers WHERE GroupMembers_GroupIDLNK = {0} AND GroupMembers_Deleted = 0",ID);

            RQ.RunQuery(Query);

            //Add to Array List

            c_Members = new ArrayList();

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {

                    c_Members.Add(new ClassGroupMembers(DR));
            }

            c_Members.Sort();
            
        }



        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT * FROM Groups WHERE Group_Deleted = 0 ORDER BY Group_Name;");
             
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static Boolean Exists(string GroupName)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT Group_ID_LNK FROM Groups WHERE Group_Name = '{0}' AND Group_Deleted = 0;",GroupName);

            RQ.RunQuery(Query);

            if (RQ.numberofresults >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

       
    }
}
