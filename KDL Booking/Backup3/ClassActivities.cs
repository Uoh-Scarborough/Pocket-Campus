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

namespace ProductionBooking 
{
    public class ClassActivities : IComparable
    {
        private  int c_ID;
        private ClassCourse c_Course;
        private string c_ModuleNumber, c_Title, c_Location, c_Staff, c_Pattern, c_Weeks;
        private int c_Day, c_StartTime, c_EndTime;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public ClassCourse Course
        {
            get { return c_Course; }
            set { c_Course = value; }
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

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassActivities()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassActivities(int ID)
        {
            //Initialise New Class

            c_ID = ID;
            string Query = "SELECT * From Activities WHERE Activity_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            RQ.RunQuery(Query);

            int cid = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[1]);

            Course = new ClassCourse(cid);
            ModuleNumber = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            Day = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
            StartTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString());
            EndTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString());
            Location = RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            Staff = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            Pattern = RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString();
            Weeks = RQ.dataset.Tables[0].Rows[0].ItemArray[10].ToString();
            //'Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString().Trim());
            Deleted = false;
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

        public int CompareTo(object obj)
        {
            int result = this.ToString().CompareTo(obj.ToString());
            //if (result == 0)
            //    result = this.SSN.CompareTo(Compare.SSN);
            return result;
        }

       
    }
}
