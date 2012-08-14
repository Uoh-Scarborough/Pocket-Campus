using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml;
using StandardClasses;
using PocketCampusClasses;
using System.Data;

namespace PCWS
{
    /// <summary>
    /// Summary description for Timetables
    /// </summary>
    [WebService(Namespace = "http://pcws.scar.hull.ac.uk", Description="PCWS Pocket Campus Web Service pushes out information relating to Campus Timetables",Name="Pocket Campus Web Services - Timetables")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Timetables : System.Web.Services.WebService
    {

        [WebMethod(CacheDuration = 1, Description = "Returns All Programs of Study")]
        public XmlDocument ListPoS()
        {
            ClassAppDetails.ttcurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("PoS");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Courses WHERE Course_Deleted = 0 ORDER BY Course_Name");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Program");
                category.SetAttribute("ID", Convert.ToInt32(DR["Course_ID_LNK"]).ToString());
                category.SetAttribute("Code", DR["Course_Code"].ToString());
                category.SetAttribute("YearID", DR["Course_YearID"].ToString());
                category.InnerText = DR["Course_Name"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Returns Day Activities")]
        public XmlDocument DayAct(int Week, int Day, int PoSID)
        {
            ClassAppDetails.ttcurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Timetable");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Activities WHERE Activity_Deleted = 0 AND Activity_Course_ID_LNK = " + PoSID + " AND Activity_Day = " + Day + " AND Activity_Weeks LIKE '%" + ClassUseful.ConvertTo2DigitNumber(Week) + "%' ORDER BY Activity_StartTime");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Activity");
                category.SetAttribute("ID", DR["Activity_ID_LNK"].ToString());
                category.SetAttribute("Module", DR["Activity_Module"].ToString());
                category.SetAttribute("Room", DR["Activity_Location"].ToString());
                category.SetAttribute("StartTime", ClassGeneral.getTime(Convert.ToInt16(DR["Activity_StartTime"])));
                category.SetAttribute("FinishTime", ClassGeneral.getTime(Convert.ToInt16(DR["Activity_EndTime"])));
                category.SetAttribute("Academic", ClassActivities.StaticOutputStaff(DR["Activity_Academic"].ToString()));
                category.SetAttribute("Pattern", DR["Activity_Pattern"].ToString());
                category.InnerText = DR["Activity_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Returns Activity Details")]
        public XmlDocument Act(int ActID)
        {
            ClassAppDetails.ttcurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Timetable");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Activities WHERE Activity_Deleted = 0 AND Activity_ID_LNK = " + ActID + ";");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Activity");
                category.SetAttribute("ID", DR["Activity_ID_LNK"].ToString());
                category.SetAttribute("Module", DR["Activity_Module"].ToString());
                category.SetAttribute("Room", DR["Activity_Location"].ToString());
                category.SetAttribute("StartTime", ClassGeneral.getTime(Convert.ToInt16(DR["Activity_StartTime"])));
                category.SetAttribute("FinishTime", ClassGeneral.getTime(Convert.ToInt16(DR["Activity_EndTime"])));
                category.SetAttribute("Academic",  ClassActivities.StaticOutputStaff(DR["Activity_Academic"].ToString()));
                category.SetAttribute("Pattern", DR["Activity_Pattern"].ToString());
                category.InnerText = DR["Activity_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Returns Current Week")]
        public XmlDocument CurrentWeek()
        {

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("AcademicWeek");
            Output.AppendChild(root);

            XmlElement category = Output.CreateElement("CurrentWeek");

            int AcWk = ClassGeneral.getAcademicWeek();

            category.SetAttribute("ID", AcWk.ToString());
            category.SetAttribute("Monday", ClassGeneral.getAcademicDate(AcWk, 0));
            category.SetAttribute("Tuesday", ClassGeneral.getAcademicDate(AcWk, 1));
            category.SetAttribute("Wednesday", ClassGeneral.getAcademicDate(AcWk, 2));
            category.SetAttribute("Thursday", ClassGeneral.getAcademicDate(AcWk, 3));
            category.SetAttribute("Friday", ClassGeneral.getAcademicDate(AcWk, 4));
            category.SetAttribute("Saturday", ClassGeneral.getAcademicDate(AcWk, 5));
            category.SetAttribute("Sunday", ClassGeneral.getAcademicDate(AcWk, 6));
            category.InnerText = ClassGeneral.getAcademicWeekDetails(ClassGeneral.getAcademicWeek()).ToString();

            root.AppendChild(category);

            return Output;

        }

        [WebMethod(CacheDuration = 1, Description = "Returns all Weeks")]
        public XmlDocument Weeks()
        {

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("AcademicWeeks");
            Output.AppendChild(root);

            int AcWk = 1;

            while (AcWk <= 52)
            {
                XmlElement category = Output.CreateElement("Week");

                category.SetAttribute("ID", AcWk.ToString());
                category.InnerText = ClassGeneral.getAcademicWeekDetails(AcWk).ToString();

                root.AppendChild(category);

                AcWk += 1;

            }

            return Output;

        }
    }
}
