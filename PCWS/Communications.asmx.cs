using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using StandardClasses;
using PocketCampusClasses;
using System.Data;

namespace PCWS
{
    /// <summary>
    /// Summary description for Communications1
    /// </summary>
    [WebService(Namespace = "http://pcws.scar.hull.ac.uk/", Description = "PCWS Pocket Campus Web Service pushes out information relating to Communications", Name = "Pocket Campus Web Services - Communications")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Communications1 : System.Web.Services.WebService
    {

        [WebMethod(CacheDuration = 30, Description = "Updates all XML Feed Files")]
        public void Feeds()
        {
            AllNoticeandEvents();
            Menus();
            Tickers();
        }

        [WebMethod(CacheDuration = 30, Description = "Creates XML Feed for All Notices and Events")]
        private void AllNoticeandEvents()
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument OutputAll = new XmlDocument();

            XmlDeclaration decstd = OutputAll.CreateXmlDeclaration("1.0", "utf-8", null);
            OutputAll.AppendChild(decstd);

            XmlElement root = OutputAll.CreateElement("Communications");
            OutputAll.AppendChild(root);

            string Query = string.Format("SELECT * FROM vw_Notices WHERE CommsBase_Valid = 1;");
            string Query1 = string.Format("SELECT * FROM vw_Events WHERE CommsBase_Valid = 1;");

            RQ.RunQuery(Query);
            RQ1.RunQuery(Query1);

            RQ.dataset.Merge(RQ1.dataset);

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement element = OutputAll.CreateElement("NoticeEvents");

                element.SetAttribute("Title", DR["CommsBase_Title"].ToString().Trim());
                element.SetAttribute("Attachement", ClassAppDetails.attachementurl + DR["CommsBase_Attachement"].ToString().Trim());
                element.SetAttribute("AttachementisImage", DR["CommsBase_UseAttachement"].ToString().Trim());
                element.SetAttribute("Category", DR["CommsBase_CategoryIDLNK"].ToString().Trim());
                DateTime Posted = Convert.ToDateTime(DR["CommsBase_PostedDate"]);
                element.SetAttribute("PostedDate", Posted.ToShortDateString());
                element.SetAttribute("Display-From", Convert.ToDateTime(DR["CommsBase_DisplayFrom"]).ToShortDateString().Trim());
                element.SetAttribute("Display-To", Convert.ToDateTime(DR["CommsBase_DisplayTo"]).ToShortDateString().Trim());
                DateTime DisplayFrom = Convert.ToDateTime(DR["CommsBase_DisplayFrom"]);
                element.SetAttribute("Display-From-Feed", DisplayFrom.ToString("MM/dd/yyyy"));
                DateTime DisplayTo = Convert.ToDateTime(DR["CommsBase_DisplayTo"]);
                element.SetAttribute("Display-To-Feed", DisplayTo.ToString("MM/dd/yyyy"));
                element.SetAttribute("High-Priority", DR["CommsBase_Urgent"].ToString().Trim());

                string Content = "<p>" + DR["CommsBase_Notice"].ToString() + "</p>";

                Content = Content.Replace("\n", "</p><p>");

                element.InnerText = Content;
                root.AppendChild(element);

            }

            OutputAll.Save(ClassAppDetails.feedsdir + "/communications.xml");

        }

        [WebMethod(CacheDuration = 30, Description = "Creates XML Feed of Menus")]
        private void Menus()
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument OutputAll = new XmlDocument();

            XmlDeclaration decstd = OutputAll.CreateXmlDeclaration("1.0", "utf-8", null);
            OutputAll.AppendChild(decstd);

            XmlElement root = OutputAll.CreateElement("Menus");
            OutputAll.AppendChild(root);

            string Query = string.Format("SELECT * FROM vw_Menus WHERE CommsBase_Valid = 1;");

            RQ.RunQuery(Query);

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                XmlElement element = OutputAll.CreateElement("Menu");

                element.SetAttribute("Title", DR["CommsBase_Title"].ToString().Trim());
                element.SetAttribute("Attachement", ClassAppDetails.attachementurl + DR["CommsBase_Attachement"].ToString().Trim());
                element.SetAttribute("AttachementisImage", DR["CommsBase_UseAttachement"].ToString().Trim());
                element.SetAttribute("Category", DR["CommsBase_CategoryIDLNK"].ToString().Trim());
                DateTime Posted = Convert.ToDateTime(DR["CommsBase_PostedDate"]);
                element.SetAttribute("PostedDate", Posted.ToShortDateString());
                element.SetAttribute("Display-From", Convert.ToDateTime(DR["CommsBase_DisplayFrom"]).ToShortDateString().Trim());
                element.SetAttribute("Display-To", Convert.ToDateTime(DR["CommsBase_DisplayTo"]).ToShortDateString().Trim());
                DateTime DisplayFrom = Convert.ToDateTime(DR["CommsBase_DisplayFrom"]);
                element.SetAttribute("Display-From-Feed", DisplayFrom.Month.ToString() + "/" + DisplayFrom.Day.ToString() + "/" + DisplayFrom.Year.ToString());
                DateTime DisplayTo = Convert.ToDateTime(DR["CommsBase_DisplayTo"]);
                element.SetAttribute("Display-To-Feed", DisplayTo.Month.ToString() + "/" + DisplayTo.Day.ToString() + "/" + DisplayTo.Year.ToString());
                element.SetAttribute("High-Priority", DR["CommsBase_Urgent"].ToString().Trim());

                element.InnerText = DR["CommsBase_Notice"].ToString();
                root.AppendChild(element);

            }

            OutputAll.Save(ClassAppDetails.feedsdir + "/menus.xml");

        }

        [WebMethod(CacheDuration = 30, Description = "Creates XML Feed of Tickers using Media Format")]
        private void Tickers()
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            XmlDocument OutputAll = new XmlDocument();

            XmlDeclaration decstd = OutputAll.CreateXmlDeclaration("1.0", "utf-8", null);
            OutputAll.AppendChild(decstd);

            XmlElement root = OutputAll.CreateElement("rss");
            root.SetAttribute("version", "2.0");
            root.SetAttribute("xmlns:media", "http://search.yahoo.com/mrss/");
            OutputAll.AppendChild(root);

            XmlElement channel = OutputAll.CreateElement("channel");
            root.AppendChild(channel);

            XmlElement title = OutputAll.CreateElement("title");
            title.InnerXml = "Pocket Campus Ticker Feed";
            channel.AppendChild(title);

            string Query = string.Format("SELECT * FROM vw_Tickers WHERE CommsBase_Valid = 1;");

            RQ.RunQuery(Query);

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {

                XmlElement item = OutputAll.CreateElement("item");
                channel.AppendChild(item);
                XmlElement itemtitle = OutputAll.CreateElement("title");
                itemtitle.InnerXml = DR["CommsBase_Title"].ToString().Trim();
                item.AppendChild(itemtitle);

            }

            OutputAll.Save(ClassAppDetails.feedsdir + "/tickers.xml");

        }
    }
}
