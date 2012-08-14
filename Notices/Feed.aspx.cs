using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using StandardClasses;
using PocketCampusClasses;

namespace Comms
{
    public partial class Feed : System.Web.UI.Page
    {
        ClassConnection Conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            Conn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = Conn;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (XmlWriter writer = XmlWriter.Create(Response.OutputStream, settings))
            {
                int AID = Convert.ToInt32(Request["aid"]);
                int SID = Convert.ToInt32(Request["sid"]);
                string CID = Request["cid"];

                string IncludeStr = "";
                string ExcludeStr = "";
                string FinalInc = "";
                string FinalExc = "";

                if (CID != null)
                {
                    string[] Cats = CID.Split(',');

                    foreach (string strCat in Cats)
                    {
                        int intCat = Convert.ToInt16(strCat);

                        if (intCat > 0)
                       {
                            //Include
                            IncludeStr += "," + intCat;
                        }
                        else
                       {
                            //Exclude
                            ExcludeStr += "," + (intCat * -1);
                        }
                    }
                }
                
                //AID 1 = Notices
                //AID 2 = Events
                //AID 3 = Welcome Screen
                //AID 4 = Video

                if (AID == 1)
                {

                    writer.WriteStartDocument();
                    writer.WriteStartElement("notices");

                    
                    //RQ.connection.connection.Close();

                    if (IncludeStr != "")
                    {
                        FinalInc = " AND Notice_CategoryIDLNK IN (" + IncludeStr.Substring(1) + ") ";
                    }

                    if (ExcludeStr != "")
                    {
                        FinalExc = " AND Notice_CategoryIDLNK NOT IN (" + ExcludeStr.Substring(1) + ") ";
                    }

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

                    RQ.RunQuery("SELECT * FROM Notices WHERE Notice_Deleted = 0 AND Notice_Valid = 1 AND Notice_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Notice_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Notice_DisplayFrom, Notice_Urgent;");
                    //RQ.connection.connection.Close();

                    foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                    {

                        ClassNotice Notice = new ClassNotice(DR);

                        writer.WriteStartElement("notice");
                        writer.WriteAttributeString("title", Notice.Title);
                        writer.WriteAttributeString("displayfrom", Notice.DisplayFrom.ToShortDateString());
                        writer.WriteAttributeString("displayto", Notice.DisplayTo.ToShortDateString());
                        writer.WriteElementString("cont", Notice.Notice);
                        writer.WriteEndElement();

                    }


                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                }
                else if(AID == 2)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("events");

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
                    //RQ.connection.connection.Close();

                    if (IncludeStr != "")
                    {
                        FinalInc = " AND Event_CategoryIDLNK IN (" + IncludeStr.Substring(1) + ") ";
                    }

                    if (ExcludeStr != "")
                    {
                        FinalExc = " AND Event_CategoryIDLNK NOT IN (" + ExcludeStr.Substring(1) + ") ";
                    }

                    RQ.RunQuery("SELECT * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Event_DateTime");
                    RQ.connection.connection.Close();

                    foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                    {

                        ClassEvent Event = new ClassEvent(DR);

                        writer.WriteStartElement("eventa");
                        writer.WriteAttributeString("title", Event.Title);
                        writer.WriteAttributeString("datetime", Event.EventDateTime.ToShortDateString() + " " + Event.EventDateTime.ToShortTimeString());
                        writer.WriteAttributeString("location", Event.Location);
                        writer.WriteAttributeString("duration", Event.EventDuration);
                        writer.WriteElementString("cont", Event.Event);
                        writer.WriteEndElement();

                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                else if (AID == 3)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("welcomes");

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

                    RQ.RunQuery("SELECT * FROM Welcomes, WelcomeScreens WHERE Welcome_ID_LNK = WelcomeScreens_WelcomeIDLNK AND WelcomeScreens_ScreenIDLNK = " + SID + " AND WelcomeScreens_Deleted = 0 AND Welcome_Deleted = 0 AND Welcome_ShowFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Welcome_ShowTo >= '" + DateTime.Now.ToShortDateString() + "';");
                    //RQ.connection.connection.Close();

                    foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                    {

                        ClassWelcome Welcome = new ClassWelcome(DR);

                        writer.WriteStartElement("welcome");
                        writer.WriteAttributeString("title", Welcome.Title);
                        writer.WriteElementString("cont", Welcome.Message);
                        writer.WriteEndElement();

                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                else if (AID == 4)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("videos");

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

                    RQ.RunQuery("SELECT Video_URL FROM Videos WHERE Video_Deleted = 0 AND Video_ShowFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Video_ShowTo >= '" + DateTime.Now.ToShortDateString() + "';");
                    //RQ.connection.connection.Close();

                    foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                    {

                        writer.WriteStartElement("videoad");
                        writer.WriteAttributeString("url", DR["Video_URL"].ToString());
                        writer.WriteEndElement();

                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                else if (AID == 5)
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("adverts");

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

                    RQ.RunQuery("SELECT Event_Attachment FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' AND Event_CategoryIDLNK = 7 AND Event_Attachment != '' ORDER BY Event_DateTime");

                    RQ.connection.connection.Close();

                    foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                    {

                        writer.WriteStartElement("advert");
                        //writer.WriteAttributeString("url", "http://172.16.253.128/Notices/" + DR["Event_Attachment"].ToString());
                        writer.WriteAttributeString("url", "http://comms.scar.hull.ac.uk/" + DR["Event_Attachment"].ToString());
                        writer.WriteEndElement();

                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }
    }
}
