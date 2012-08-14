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

namespace PocketCampus
{
    public partial class Feed : System.Web.UI.Page
    {
        public void LoadFeed()
        {
            ClassConnection EventsConn,NoticeConn,WelcomeConn,VideoConn;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (XmlWriter writer = XmlWriter.Create(Response.OutputStream, settings))
            {
                int AID = Convert.ToInt32(Request["aid"]);
                int SID = Convert.ToInt32(Request["sid"]);
                string CID = Request["cid"];

                string IncludeStr = "";
                string ExcludeStr = ",11";
                string FinalInc = "";
                string FinalExc = "";
                string Top5 = "Top 5";

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

                if (ClassAppDetails.openday == "1")
                {
                    IncludeStr = ",11";
                    ExcludeStr = ",1,2,3,4,5,6,7,8,9,10";
                    Top5 = "";
                }

                //AID 1 = Notices
                //AID 2 = Events
                //AID 3 = Welcome Screen

                if (AID == 1)
                {

                    

                    if (IncludeStr != "")
                    {
                        FinalInc = " AND Notice_CategoryIDLNK IN (" + IncludeStr.Substring(1) + ") ";
                    }

                    if (ExcludeStr != "")
                    {
                        FinalExc = " AND Notice_CategoryIDLNK NOT IN (" + ExcludeStr.Substring(1) + ") ";
                    }

                    NoticeConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

                    try
                    {

                        ClassReadQuery RQ0 = new ClassReadQuery(NoticeConn);

                        //RQ0.RunQuery("SELECT " + Top5 + " Notice_Title, Notice_Notice FROM Notices WHERE Notice_Deleted = 0 AND Notice_Valid = 1 AND Notice_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Notice_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Notice_Urgent DESC, Notice_DisplayFrom;");

                        RQ0.RunQuery("SELECT " + Top5 + " * FROM Notices WHERE Notice_Deleted = 0 AND Notice_Valid = 1 AND Notice_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Notice_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Notice_Urgent DESC, Notice_DisplayFrom;");

                        if (RQ0.dataset.Tables.Count > 0)
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("notices");

                            foreach (DataRow DR0 in RQ0.dataset.Tables[0].Rows)
                            {
                                ClassNotice Notice = new ClassNotice(DR0);

                                writer.WriteStartElement("notice");
                                writer.WriteAttributeString("title", ClassUseful.FormatString(Notice.Title));
                                writer.WriteElementString("cont", ClassUseful.FormatString(Notice.Notice));
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }

                    }
                    catch (Exception ex)
                    {
                        // Do Nothing
                    }

                    NoticeConn.connection.Close();

                }
                else if (AID == 2)
                {
                    

                    if (IncludeStr != "")
                    {
                        FinalInc = " AND Event_CategoryIDLNK IN (" + IncludeStr.Substring(1) + ") ";
                    }

                    if (ExcludeStr != "")
                    {
                        FinalExc = " AND Event_CategoryIDLNK NOT IN (" + ExcludeStr.Substring(1) + ") ";
                    }

                    EventsConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

                    try
                    {

                        ClassReadQuery RQ1 = new ClassReadQuery(EventsConn);

                        //RQ1.RunQuery("SELECT " + Top5 + " Event_Event, Event_Title, Event_Location, Event_DateTime, Event_Duration FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Event_DateTime");

                        RQ1.RunQuery("SELECT " + Top5 + " * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' " + FinalInc + FinalExc + " ORDER BY Event_DateTime");

                        if (RQ1.dataset.Tables.Count > 0)
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("events");

                            foreach (DataRow DR1 in RQ1.dataset.Tables[0].Rows)
                            {

                                ClassEvent Event = new ClassEvent(DR1);

                                writer.WriteStartElement("eventa");
                                writer.WriteAttributeString("title", ClassUseful.FormatString(Event.Title));
                                DateTime DF = Convert.ToDateTime(DR1["Event_DateTime"].ToString());
                                writer.WriteAttributeString("datetime", DF.ToShortDateString() + " " + DF.ToShortTimeString());
                                writer.WriteAttributeString("location", ClassUseful.FormatString(Event.Location));
                                writer.WriteAttributeString("duration", DR1["Event_Duration"].ToString());
                                writer.WriteElementString("cont", ClassUseful.FormatString(Event.Event));
                                writer.WriteEndElement();

                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();

                        }
                        else
                        {
                            // Do Nothing Yet
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                   

                    EventsConn.connection.Close();
                }
                else if (AID == 3)
                {
                    WelcomeConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

                    //ClassAppDetails.commscurrentconnection = Conn;

                    try
                    {

                        ClassReadQuery RQ2 = new ClassReadQuery(WelcomeConn);

                        RQ2.RunQuery("SELECT Welcome_Message, Welcome_Title FROM Welcomes, WelcomeScreens WHERE Welcome_ID_LNK = WelcomeScreens_WelcomeIDLNK AND WelcomeScreens_ScreenIDLNK = " + SID + " AND WelcomeScreens_Deleted = 0 AND Welcome_Deleted = 0 AND Welcome_ShowFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Welcome_ShowTo >= '" + DateTime.Now.ToShortDateString() + "';");
                        //RQ.connection.connection.Close();

                        if (RQ2.dataset.Tables.Count > 0)
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("welcomes");

                            foreach (DataRow DR2 in RQ2.dataset.Tables[0].Rows)
                            {

                                writer.WriteStartElement("welcome");
                                writer.WriteAttributeString("title", DR2["Welcome_Title"].ToString());
                                writer.WriteElementString("cont", DR2["Welcome_Message"].ToString());
                                writer.WriteEndElement();

                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();

                        }
                        else
                        {
                            //Do Nothing
                        }

                    }
                    catch (Exception ex)
                    {


                        //Do Nothing

                    }

                    WelcomeConn.connection.Close();
                }
                else if (AID == 4)
                {
                    try
                    {

                        VideoConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

                        ClassReadQuery RQ3 = new ClassReadQuery(VideoConn);

                        //ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

                        RQ3.RunQuery("SELECT Video_URL FROM Videos WHERE Video_Deleted = 0 AND Video_ShowFrom <= '" + DateTime.Now.ToShortDateString() + "' AND Video_ShowTo >= '" + DateTime.Now.ToShortDateString() + "';");
                        //RQ3.connection.connection.Close();

                        writer.WriteStartDocument();
                        writer.WriteStartElement("videos");

                        foreach (DataRow DR in RQ3.dataset.Tables[0].Rows)
                        {

                            writer.WriteStartElement("videoad");
                            writer.WriteElementString("url", DR["Video_URL"].ToString());
                            writer.WriteEndElement();

                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                    catch (Exception ex)
                    {

                    }

                   
                }
     
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadFeed();        
        }
    }
}
