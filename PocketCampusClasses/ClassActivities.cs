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
using System.Data.SqlClient;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassActivities : ClassBase
    {
        private ClassCourse c_Course;
        private string c_ModuleNumber, c_Title, c_Location, c_Staff, c_Pattern, c_Weeks;
        private int c_CourseID, c_Day, c_StartTime, c_EndTime;

        public int CourseID{
            get {
                if (c_Course == new ClassCourse()){
                    return c_CourseID;
                }    else{
                    return  c_Course.ID;
                }
            }
            set{
                c_CourseID = value; 
            }
        }

        public ClassCourse Course
        {
            get { 
                    if (c_Course == new ClassCourse()){
                        c_Course = new ClassCourse(c_CourseID);
                    }
                    return c_Course; 
                }
            set { 
                    c_Course = value;
                    c_CourseID = value.ID;
            }
        }

        public string ModuleNumber
        {
            get { return c_ModuleNumber.Trim(); }
            set { c_ModuleNumber = value.Trim(); }
        }

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public int Day
        {
            get { return c_Day; }
            set { c_Day = value; }
        }

        public int StartTime
        {
            get { return c_StartTime; }
            set { c_StartTime = value; }
        }

        public string StartTimeOut
        {
            get
            {
                int ST = StartTime;
                int Hours = ST / 4;
                int Minutes = 15 * (ST - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public int EndTime
        {
            get { return c_EndTime; }
            set { c_EndTime = value; }
        }

        public string EndTimeOut
        {
            get
            {
                int ET = EndTime;
                int Hours = ET / 4;
                int Minutes = 15 * (ET - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public string Location
        {
            get { return c_Location.Trim(); }
            set { c_Location = value.Trim(); }
        }

        public string Staff
        {
            get { return c_Staff; }
            set { c_Staff = value; }
        }

        public string OutputStaff
        {
            get
            {
                string[] staffs = this.Staff.Split(',');

                string output = "";

                if (staffs.Length > 1)
                {

                    for (int i = 0; i < staffs.Length; i += 2)
                    {
                        output += "<br/>" + staffs[i] + ", " + staffs[i + 1];
                    }

                    output = output.Substring(5);

                }
                else
                {
                    output = staffs[0].Trim();
                }

                return output;

            }
        }

        public string Pattern
        {
            get { return c_Pattern; }
            set { c_Pattern = value; }
        }

        public string Weeks
        {
            get { return c_Weeks; }
            set { c_Weeks = value; }
        }

        public ClassActivities()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassActivities(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Activities WHERE Activity_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            RQ.RunQuery(Query);
            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassActivities(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt16(DR["Activity_ID_LNK"]);
            if(DR.Table.Columns.Contains("Activity_Course_ID_LNK")){
                c_CourseID = Convert.ToInt32(DR["Activity_Course_ID_LNK"]);
            }
            c_ModuleNumber = DR["Activity_Module"].ToString();
            c_Title = DR["Activity_Title"].ToString();
            c_Day = Convert.ToInt32(DR["Activity_Day"]);
            c_StartTime = Convert.ToInt32(DR["Activity_StartTime"]);
            c_EndTime = Convert.ToInt32(DR["Activity_EndTime"]);
            c_Location = DR["Activity_Location"].ToString();
            c_Staff = DR["Activity_Academic"].ToString();
            c_Pattern = DR["Activity_Pattern"].ToString();
            c_Weeks = DR["Activity_Weeks"].ToString();
            c_Deleted = false;

            if (DR.Table.Columns.Contains("Course_Code") && DR.Table.Columns.Contains("Course_Name"))
            {
                Course = new ClassCourse();
                Course.ID = Convert.ToInt32(DR["Activity_Course_ID_LNK"]);
                Course.Code = DR["Course_Code"].ToString();
                Course.Name = DR["Course_Name"].ToString();
                Course.Deleted = false;
            }
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Activities (Activity_Course_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks, Activity_Deleted) VALUES (" + Course.ID + ",'" + ModuleNumber + "','" + Title.Replace('\'', ' ') + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Staff + "', '" + Pattern + "', '" + Weeks + "',0) SELECT @@IDENTITY;";
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
            string Query = "INSERT INTO Activities (Activity_Course_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks, Activity_Deleted) VALUES (" + Course.ID + ",'" + ModuleNumber + "','" + Title.Replace('\'', ' ') + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Staff + "', '" + Pattern + "', '" + Weeks + "',0) SELECT @@IDENTITY;";
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

        public Boolean InWeek(string Week)
        {
            int tWeek = Convert.ToInt16(Week);
            string sWeek = "";

            if (tWeek <= 9)
            {
                sWeek = "0" + tWeek.ToString();
            }
            else
            {
                sWeek = tWeek.ToString();
            }

            if (Weeks.Contains(sWeek))
            {
                return true;
            }
            else
            {
                return false;
            }
         }

        public override string ToString()
        {
            return StartTime.ToString();
        }

        public static string StaticOutputStaff(String Staff)
        {
                string[] staffs = Staff.Split(',');

                string output = "";

                if (staffs.Length > 1)
                {

                    for (int i = 0; i < staffs.Length; i += 2)
                    {
                        output += "<br/>" + staffs[i] + ", " + staffs[i + 1];
                    }

                    output = output.Substring(5);

                }
                else
                {
                    output = staffs[0].Trim();
                }

                return output;
        }
       
    }
}
