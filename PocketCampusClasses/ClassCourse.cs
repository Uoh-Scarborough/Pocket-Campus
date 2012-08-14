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
    public class ClassCourse : ClassBase
    {

        private string c_Name,c_Code;

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string Code
        {
            get { return c_Code.Trim(); }
            set { c_Code = value.Trim(); }
        }

        public ClassCourse()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassCourse(int ID)
        {
            //Initialise New Class
            string Query = "SELECT Course_Code, Course_Name, Course_Deleted From Courses WHERE Course_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        private void LoadFromDR(DataRow DR)
        {
            if (DR.Table.Columns.Contains("Course_ID_LNK"))
            {
                c_ID = Convert.ToInt32(DR["Course_ID_LNK"]);
            }

            Name = DR["Course_Name"].ToString();
            Code = DR["Course_Code"].ToString();
            Deleted = Convert.ToBoolean(DR["Course_Deleted"]);
        }


        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Courses (Course_Name, Course_Code, Course_Deleted) VALUES ('" + Name + "','" + Code + "',0) SELECT @@IDENTITY;";
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

        public bool Create(SqlTransaction Trans, ClassConnection TransConn)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Courses (Course_Name, Course_Code, Course_Deleted) VALUES ('" + Name + "','" + Code + "',0) SELECT @@IDENTITY;";
            try
            {
                RQ.RunQuery(Query,Trans,TransConn);
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.ttcurrentconnection);
            bool Result;
            string Query = "UPDATE Courses SET Course_Name = '" + Name + "', Course_Code = '" + Code + "', Course_Deleted = " + Deleted.GetHashCode() + " WHERE Course_ID_LNK = " + ID + ";";
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

        public static int Exists(string Code)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            string Query = "SELECT Course_ID_LNK FROM Courses WHERE Course_Code = '" + Code + "' AND Course_Deleted = 0;";

            RQ.RunQuery(Query);

            if(RQ.numberofresults > 0)
            {
                return Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
            } else {
                return 0;
            }
        }

        public static ArrayList GenerateKey()
        {
            
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            string Query = "SELECT Course_ID_LNK, Course_Code From Courses WHERE Course_Deleted = 0;";

            RQ.RunQuery(Query);

            ArrayList returnArray = new ArrayList();

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                returnArray.Add(new string[2]{DR["Course_ID_LNK"].ToString(),DR["Course_Code"].ToString()});
            }

            return returnArray;
        }

        public static ArrayList GenerateKey(SqlTransaction Trans, ClassConnection TransConn)
        {

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            string Query = "SELECT Course_ID_LNK, Course_Code From Courses WHERE Course_Deleted = 0;";

            RQ.RunQuery(Query,Trans,TransConn);

            ArrayList returnArray = new ArrayList();

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                returnArray.Add(new string[2] { DR["Course_ID_LNK"].ToString(), DR["Course_Code"].ToString() });
            }

            return returnArray;
        }
    }
}
