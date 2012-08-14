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

namespace Mobile
{
    public class ClassCourse
    {

        private int c_ID;
        private string c_Name,c_Code;
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

        public string Code
        {
            get { return c_Code.Trim(); }
            set { c_Code = value.Trim(); }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassCourse()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassCourse(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Courses WHERE Course_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            RQ.RunQuery(Query);

            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Code = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[3]);
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
                returnArray.Add(new string[2]{DR[0].ToString(),DR[1].ToString()});
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
                returnArray.Add(new string[2] { DR[0].ToString(), DR[1].ToString() });
            }

            return returnArray;
        }

        public static string GenerateCourseList(int YearID, int CourseGroupID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQ.RunQuery("SELECT Course_ID_LNK, Course_Name FROM Courses WHERE Course_YearID = " + YearID + " AND Course_GroupID = " + CourseGroupID + " AND Course_Deleted = 0;");

            string returnStr = "<ul>";

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                returnStr += "<li><a href=\"?cid=" + DR[0] + "\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>" + DR[1] + "</a></li>";
            }

            returnStr += "</ul>";

            return returnStr;
        }
    }
}
