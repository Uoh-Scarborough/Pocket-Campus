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
    /// PCWS Pocket Campus Web Service Pushs out informtion relating to Campus Communications
    /// </summary>
    [WebService(Namespace = "http://pcws.scar.hull.ac.uk/", Description="PCWS Pocket Campus Web Service pushes out information relating to Campus Notices and Events",Name="Pocket Campus Web Services - Notices & Events")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod(CacheDuration = 1, Description = "Returns Notice and Event Categorys")]
        public XmlDocument ListCategorys()
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Categorys");
            Output.AppendChild(root);

            RQ.RunQuery("SELECT * FROM Categories WHERE Category_Deleted = 0 ORDER BY Category_Title");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement category = Output.CreateElement("Category");
                category.SetAttribute("ID", Convert.ToInt32(DR["Category_ID_LNK"]).ToString());
                category.InnerText = DR["Category_Title"].ToString();

                root.AppendChild(category);
            }

            return Output;
        }


        [WebMethod(CacheDuration = 1, Description = "Returns Notices as XML File Limited by Number (0 = All) and Category (0 = All)")]
        public XmlDocument ListNotices(int Max = 0, int Category = 0)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Notices");
            Output.AppendChild(root);

            string CatStr = "";
            string TopStr = "";

            if (Max > 0)
            {
                TopStr = "Top " + Max;
            }

            if (Category > 0)
            {
                CatStr = " AND Notice_CategoryIDLNK = " + Category;
            }

            RQ.RunQuery("SELECT " + TopStr + " Notice_ID_LNK, Notice_Title, Notice_Notice FROM Notices WHERE Notice_Deleted = 0 AND Notice_Valid = 1 " + CatStr + " AND Notice_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Notice_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Notice_DisplayFrom, Notice_Urgent;");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement notice = Output.CreateElement("Notice");
                notice.SetAttribute("Title", DR["Notice_Title"].ToString());
                notice.SetAttribute("ID", Convert.ToInt32(DR["Notice_ID_LNK"]).ToString());
                notice.InnerText = DR["Notice_Notice"].ToString();

                root.AppendChild(notice);
            }


            return Output;
        }

       

        [WebMethod(CacheDuration = 1, Description = "Returns Notice by ID")]
        public XmlDocument Notice(int ID)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            string Query = string.Format("SELECT * FROM Notices WHERE Notice_ID_LNK = {0} AND Notice_Valid = 1 AND Notice_Deleted = 0", ID);

            RQ.RunQuery(Query);

            DataRow DR = RQ.dataset.Tables[0].Rows[0];

            XmlElement root = Output.CreateElement("Notice");

            root.SetAttribute("Title",DR["Notice_Title"].ToString());
            root.SetAttribute("Attachement", DR["Notice_Attachement"].ToString());
            root.SetAttribute("Display-From", Convert.ToDateTime(DR["Notice_DisplayFrom"]).ToShortDateString());
            root.SetAttribute("Display-To", Convert.ToDateTime(DR["Notice_DisplayTo"]).ToShortDateString());
            root.SetAttribute("High-Priority", DR["Notice_Urgent"].ToString());

            root.InnerText = DR["Notice_Notice"].ToString();
            Output.AppendChild(root);

            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Returns Events as XML File Limited by Number (0 = All) and Category (0 = All)")]
        public XmlDocument ListEvents(int Max = 0, int Category = 0)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            XmlElement root = Output.CreateElement("Events");
            Output.AppendChild(root);

            string CatStr = "";
            string TopStr = "";

            if (Max > 0)
            {
                TopStr = "Top " + Max;
            }

            if (Category > 0)
            {
                CatStr = " AND Event_CategoryIDLNK = " + Category;
            }

            RQ.RunQuery("SELECT " + TopStr + " Event_ID_LNK, Event_Title, Event_Event, Event_Location, Event_DateTime, Event_Duration  FROM Events WHERE Event_CategoryIDLNK != 11 " + CatStr + " AND Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Event_DateTime");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement eventn = Output.CreateElement("Event");
                eventn.SetAttribute("Title", DR["Event_Title"].ToString());
                eventn.SetAttribute("Location", DR["Event_Location"].ToString());
                eventn.SetAttribute("DateTime", Convert.ToDateTime(DR["Event_DateTime"]).ToShortDateString());
                eventn.SetAttribute("Duration", DR["Event_Duration"].ToString());
                eventn.SetAttribute("ID", Convert.ToInt32(DR["Event_ID_LNK"]).ToString());
                eventn.InnerText = DR["Event_Event"].ToString();

                root.AppendChild(eventn);
            }


            return Output;
        }

        [WebMethod(CacheDuration = 1, Description = "Returns Events by ID")]
        public XmlDocument Event(int ID)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument Output = new XmlDocument();

            XmlDeclaration dec = Output.CreateXmlDeclaration("1.0", null, null);
            Output.AppendChild(dec);

            string Query = string.Format("SELECT * FROM Events WHERE Event_ID_LNK = {0} AND Event_Valid = 1 AND Event_Deleted = 0", ID);

            RQ.RunQuery(Query);

            DataRow DR = RQ.dataset.Tables[0].Rows[0];

            XmlElement root = Output.CreateElement("Event");
            

            root.SetAttribute("Title", DR["Event_Title"].ToString());
            root.SetAttribute("DateTime", Convert.ToDateTime(DR["Event_DateTime"]).ToShortDateString());
            root.SetAttribute("Duration", DR["Event_Duration"].ToString());
            root.SetAttribute("ID", Convert.ToInt32(DR["Event_ID_LNK"]).ToString());
            root.SetAttribute("Attachment", DR["Event_Attachment"].ToString());
            root.InnerText = DR["Event_Event"].ToString();

            Output.AppendChild(root);            

            return Output;
        }
        
    }
}