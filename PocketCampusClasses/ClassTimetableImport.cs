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
using System.Xml;
using StandardClasses;
using System.Data.SqlClient;

namespace PocketCampusClasses
{
    public class ClassTimetableImport
    {
        private static ArrayList Courses;

        public static void Generate(string InputFile)
        {

            //Start Transcation

            Boolean NoErrors = true;

            ClassConnection TransConnection = ClassAppDetails.ttcurrentconnection;

            TransConnection.Connect();

            SqlTransaction Transaction = TransConnection.connection.BeginTransaction(IsolationLevel.ReadCommitted);

            //Delete and Reseed
            ClassWriteQuery DelWQ = new ClassWriteQuery();

            DelWQ.RunQuery("DELETE FROM Activities", Transaction, TransConnection);

            ClassWriteQuery SeedWq = new ClassWriteQuery();

            SeedWq.RunQuery("DBCC CHECKIDENT (Activities, reseed, 0)",Transaction,TransConnection);

            //Load Courses
            Courses = ClassCourse.GenerateKey(Transaction,TransConnection);

            XmlDocument doc = new XmlDocument();
            doc.Load(InputFile);

            XmlNodeList nodes = doc.ChildNodes[1].ChildNodes;

            foreach (XmlNode node in nodes)
            {
                //Get Columns that I need
                string CourseCode = node["CourseName"].InnerText;

                string ModuleCode = CourseCode.Substring(0, 5);

                string CourseName = "";
                try
                {
                    CourseName = node["DescriptionofModule"].InnerText;
                }
                catch
                {
                    CourseName = CourseCode;
                }
                string CourseDay = node["ScheduledStartDay"].InnerText;
                string CourseStart = node["ScheduledStartTime"].InnerText;
                string CourseEnd = node["ScheduledEndTime"].InnerText;

                string CourseLocations = "";
                try
                {
                    CourseLocations = node["Locations"].InnerText;

                    CourseLocations = CourseLocations.Replace("'", "&#39");
                }
                catch
                {
                    CourseLocations = "TBC";
                }

                string CourseStaff = "";
                try
                {
                    CourseStaff = node["Staff"].InnerText;
                }
                catch
                {
                    CourseStaff = "N,A";
                }

                string CourseWeeks = node["Weeks"].InnerText;

                string CourseStudyCode = "";
                try
                {
                    CourseStudyCode = node["POSCode"].InnerText;
                }
                catch
                {
                    CourseStudyCode = "";
                }

                string CourseStudy = "";
                try
                {
                    CourseStudy = node["POS"].InnerText;
                }
                catch
                {
                    CourseStudy = "";
                }

                string tGroup, tType, tExtention;

                try
                {
                    string[] tCourseCode = CourseCode.Split('/');
                    tGroup = ValidateGroup(tCourseCode[1]);
                    
                    //Lines commented out on 03/04/2012 due to timetable output update.
                    //tGroup = ValidateGroup(tCourseCode[2]);
                    //tType = tCourseCode[1];
                    //tExtention = " " + tGroup + " - " + tType;
                    tExtention = " " + tGroup;
                }
                catch
                {
                    tExtention = "";
                }

                string OutTitle = CourseName.Replace(',', ' ') + tExtention;
                string OutDay = CourseDay;
                string OutStartTime = CourseStart;
                string OutEndTime = CourseEnd;
                string OutLocation = CourseLocations;
                string OutStaff = CourseStaff;
                string OutPattern = CourseWeeks;

                string[] tWeeks = OutPattern.Split(',');
                string OutWeeks = "";

                if (OutDay != "" && OutStartTime != "" && OutEndTime != "")
                {

                    

                    foreach (string iWeek in tWeeks)
                    {
                        if (iWeek.Contains('-'))
                        {
                            //Add Weeks
                            string[] fromto = iWeek.Split('-');

                            int WeeksLoop = Convert.ToInt16(fromto[1]) - Convert.ToInt16(fromto[0]);

                            for (int x = 0; x <= WeeksLoop; x++)
                            {

                                int startWeek = Convert.ToInt16(fromto[0]);
                                if (startWeek + x <= 9)
                                {
                                    OutWeeks += ", 0" + Convert.ToString(startWeek + x);
                                }
                                else
                                {
                                    OutWeeks += ", " + Convert.ToString(startWeek + x);
                                }

                            }
                        }
                        else
                        {
                            //Add Week
                            int startWeek = Convert.ToInt16(iWeek.Trim());
                            if (startWeek <= 9)
                            {
                                OutWeeks += ", 0" + startWeek;
                            }
                            else
                            {
                                OutWeeks += ", " + startWeek;
                            }

                        }
                    }

                    OutWeeks = OutWeeks.Substring(2);


                    //Find CID
                    int cid = -1;

                    foreach (object Course in Courses)
                    {
                        //try
                        //{
                            string[] newCourse = (string[])Course;

                            if (newCourse[1].Trim() == CourseStudyCode.Trim())
                            {
                                //Found 
                                cid = Convert.ToInt16(newCourse[0]);
                                break;
                            }
                        //}
                        //catch
                        //{
                            //Do Nothing
                        //}
                    }

                    ClassCourse ActCourse;

                    if (cid > 0)
                    {
                        //Exists
                        ActCourse = new ClassCourse();
                        ActCourse.ID = cid;
                    }
                    else
                    {
                        //Create
                        ActCourse = new ClassCourse();
                        ActCourse.Name = CourseStudy.Trim();
                        ActCourse.Code = CourseStudyCode.Trim();
                        ActCourse.Create(Transaction, TransConnection);
                        //Add to Courses list
                        Courses.Add(new string[2] { ActCourse.ID.ToString(), ActCourse.Code });
                    }

                    ClassActivities Activity = new ClassActivities();

                    Activity.ModuleNumber = ModuleCode.Trim();
                    Activity.Course = ActCourse;
                    Activity.Title = OutTitle;
                    Activity.Day = ClassTimetableImport.covertDay(OutDay);
                    Activity.StartTime = ClassTimetableImport.convertTime(OutStartTime);
                    Activity.EndTime = ClassTimetableImport.convertTime(OutEndTime);
                    Activity.Location = OutLocation;
                    Activity.Staff = OutStaff;
                    Activity.Pattern = OutPattern;
                    Activity.Weeks = OutWeeks;

                    NoErrors = Activity.Create(Transaction, TransConnection);

                }

            }

            if (NoErrors)
            {
           

                Transaction.Commit();
            }
            else
            {
                Transaction.Rollback();
            }

            TransConnection.Disconnect();

        }

        public static string GenerateLocations(string[] Locations)
        {
            string OutLocation = "";
            foreach (string Location in Locations)
            {
                OutLocation += ", " + Location;
            }

            return OutLocation.Substring(2);
        }

        public static string ValidateGroup(string Group)
        {
            //If Group that not just a number then show it.
            string strGroup = "";
            if (Group == "01" || Group == "02" || Group == "03" || Group == "04" || Group == "05" || Group == "06" || Group == "07" || Group == "08" || Group == "09" || Group == "10" || Group == "11" || Group == "12" || Group == "13" || Group == "14" || Group == "15" || Group == "16" || Group == "17" || Group == "18" || Group == "19" || Group == "20")
            {
                strGroup = "";
            }
            else
            {
                strGroup = " " + Group + " ";
            }
            return strGroup;
        }

        public static string FindStaff(string Staff)
        {
            //Split String to get Staff out of it.
            // Firstname, Surname, Firstname, Surname
            try
            {
                string[] tCourseStaff = Staff.Split(',');
                string tStaff = "";

                int staffCounter = 0;

                while (staffCounter <= tCourseStaff.Length - 1)
                {
                    tStaff += tCourseStaff[staffCounter + 1] + " " + tCourseStaff[staffCounter] + " ";
                    tStaff.Trim();
                    staffCounter += 2;
                }

                return tStaff;
            }
            catch
            {
                return "N A";
            }
        }

        private static int covertDay(string Day)
        {
            //Convert Day String Value
            int DayVal = 0;
            if (Day == "Monday")
            {
                DayVal = 0;
            }
            else if (Day == "Tuesday")
            {
                DayVal = 1;
            }
            else if (Day == "Wednesday")
            {
                DayVal = 2;
            }
            else if (Day == "Thursday")
            {
                DayVal = 3;
            }
            else if (Day == "Friday")
            {
                DayVal = 4;
            }
            else if (Day == "Saturday")
            {
                DayVal = 5;
            }
            else if(Day == "Sunday")
            {
                DayVal = 6;
            }

            return DayVal;
        }

        private static int convertTime(string Time)
        {
            //Convert Time String Value
            string[] Times = Time.Split('.');

            int TimeOut = (Convert.ToInt16(Times[0]) * 4) + (Convert.ToInt16(Times[1]) / 15);

            return TimeOut;
        }
    
    }
}
