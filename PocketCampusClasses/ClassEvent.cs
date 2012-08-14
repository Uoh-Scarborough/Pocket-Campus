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
    public class ClassEvent : ClassBase
    {
        private int c_CategoryID;
        private string c_Title, c_Event, c_Location, c_PostedBy, c_PostedByID, c_PostedByEmail, c_Duration, c_Attachment, c_ValidatedBy, c_InvalidReason;
        private DateTime c_EventDateTime, c_PostedDate, c_ValidatedDate;
        private Boolean c_Valid;

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public string Event
        {
            get { return c_Event.Trim(); }
            set { c_Event = value.Trim(); }
        }

        public string Attachment
        {
            get { return c_Attachment.Trim(); }
            set { c_Attachment = value.Trim(); }
        }

        public string GetAttachment
        {
            get
            {
                string returnStr = "";

                if (Attachment != "")
                {
                    returnStr += "<a href=\"http://communications.scar.hull.ac.uk/" + Attachment + "\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/PaperClip.png\" alt=\"" + Title + " Attachment\"/> Attachment</a>";
                }

                return returnStr;
            }
        }

        public string Location
        {
            get { return c_Location.Trim(); }
            set { c_Location = value.Trim(); }
        }

        public DateTime EventDateTime
        {
            get { return c_EventDateTime; }
            set { c_EventDateTime = value; }
        }

        public String EventDuration
        {
            get { return c_Duration; }
            set { c_Duration = value; }
        }

        public string PostedBy
        {
            get { return c_PostedBy.Trim(); }
            set { c_PostedBy = value.Trim(); }
        }

        public string PostedByID
        {
            get { return c_PostedByID.Trim(); }
            set { c_PostedByID = value.Trim(); }
        }

        public string PostedByEmail
        {
            get { return c_PostedByEmail.Trim(); }
            set { c_PostedByEmail = value.Trim(); }
        }

        public DateTime PostedDate
        {
            get { return c_PostedDate; }
            set { c_PostedDate = value; }
        }

        public ClassCategory Category
        {
            get { return new ClassCategory(c_CategoryID); }
            set { c_CategoryID = value.ID; }
        }

        public Boolean Valid
        {
            get { return c_Valid; }
            set { c_Valid = value; }
        }

        public string ValidatedBy
        {
            get { return c_ValidatedBy; }
            set { c_ValidatedBy = value; }
        }

        public DateTime ValidatedDate
        {
            get { return c_ValidatedDate; }
            set { c_ValidatedDate = value; }
        }

        public string InvalidReason
        {
            get { return c_InvalidReason; }
            set { c_InvalidReason = value; }
        }

        public ClassEvent()
        {
            //Initialise New Class
            ValidatedDate = DateTime.Now;
            Deleted = false;
            Attachment = "";
        }

        public ClassEvent(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Events WHERE Event_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassEvent(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Event_ID_LNK"].ToString());
            Title = ClassUseful.FormatString(DR["Event_Title"].ToString());
            Event = ClassUseful.FormatString(DR["Event_Event"].ToString());
            Attachment = DR["Event_Attachment"].ToString();
            Location = ClassUseful.FormatString(DR["Event_Location"].ToString());
            EventDateTime = Convert.ToDateTime(DR["Event_DateTime"].ToString());
            EventDuration = DR["Event_Duration"].ToString();
            PostedBy = DR["Event_PostedBy"].ToString();
            PostedByID = DR["Event_PostedByID"].ToString();
            PostedByEmail = DR["Event_PostedByEmail"].ToString();
            PostedDate = Convert.ToDateTime(DR["Event_Posted"].ToString());
            c_CategoryID = Convert.ToInt16(DR["Event_CategoryIDLNK"].ToString());
            Valid = Convert.ToBoolean(DR["Event_Valid"].ToString());
            ValidatedBy = DR["Event_ValidatedBy"].ToString();
            try
            {
                ValidatedDate = Convert.ToDateTime(DR["Event_ValidatedDate"].ToString());
            }
            catch
            {
                ValidatedDate = DateTime.Now;
            }
            InvalidReason = DR["Event_InvalidReason"].ToString();
            Deleted = Convert.ToBoolean(DR["Event_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "INSERT INTO Events (Event_Title, Event_Event, Event_Attachment, Event_Location, Event_DateTime, Event_Duration, Event_PostedBy, Event_PostedByID, Event_PostedByEmail, Event_Posted, Event_CategoryIDLNK, Event_Valid, Event_ValidatedBy, Event_ValidatedDate, Event_InvalidReason, Event_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "','" + ClassUseful.FormatStringForDB(Event) + "','" + Attachment + "','" + ClassUseful.FormatStringForDB(Location) + "','" + EventDateTime.ToString() + "','" + EventDuration + "','" + PostedBy + "','" + PostedByID + "','" + PostedByEmail + "','" + PostedDate.ToShortDateString() + "'," + c_CategoryID + "," + Valid.GetHashCode() + ",'" + ValidatedBy + "','" + ValidatedDate.ToString() + "','" + InvalidReason + "',0) SELECT @@IDENTITY;";

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

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "UPDATE Events SET Event_Title = '" + ClassUseful.FormatStringForDB(Title) + "', Event_Event = '" + ClassUseful.FormatStringForDB(Event) + "', Event_Attachment = '" + Attachment + "', Event_Location = '" + ClassUseful.FormatStringForDB(Location) + "', Event_DateTime = '" + EventDateTime + "', Event_Duration = '" + EventDuration + "', Event_PostedBy = '" + PostedBy + "', Event_PostedByID = '" + PostedByID + "', Event_PostedByEmail = '" + PostedByEmail + "', Event_Posted = '" + PostedDate + "', Event_CategoryIDLNK = " + c_CategoryID + ", Event_Valid = " + Valid.GetHashCode() + ", Event_ValidatedBy = '" + ValidatedBy + "', Event_ValidatedDate = '" + ValidatedDate.ToString() + "', Event_InvalidReason = '" + InvalidReason + "', Event_Deleted = " + Deleted.GetHashCode() + " WHERE Event_ID_LNK = " + ID + ";";
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

        public void sendAdminEmail()
        {
            string EmailTitle = "New Event Added";

            string EmailDetails = "<html><head><title>New Event Added -" + Title + "</title></head><body>";
            EmailDetails += "<p>Dear Comms Admin,</p>";
            EmailDetails += "<p>A new event entited " + Title + " has not been added to the system and is awaiting validation.</p>";
            EmailDetails += "<p>You can view the event at <a href=\"comms.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=" + ID + "\">comms.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=" + ID + "</a></p>";
            EmailDetails += "<p>&nbsp;</p>";
            EmailDetails += "<p>Scarborough Communications<br/>Scarborough Campus<br/>01723 362392<br/>comms-scar@hull.ac.uk</p>";

            ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", ClassAppDetails.adminemail, "", "", EmailTitle, EmailDetails);
        }

        public void sendEmail()
        {
            if (!Valid)
            {
                string EmailTitle = "Event Invalid";

                string EmailDetails = "<html><head><title>Event Invalid -" + Title + "</title></head><body>";
                EmailDetails += "<p>Dear " + PostedBy + "</p>";
                EmailDetails += "<p>Your event entited " + Title + " has not been succesfully validated.</p>";

                if (InvalidReason != "")
                {
                    EmailDetails += "<p>The reason given was, " + InvalidReason + "</p>";
                }

                EmailDetails += "<p>You can review the event at <a href=\"comms.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=" + ID + "\">comms.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=" + ID + "</a></p>";
                EmailDetails += "<p>&nbsp;</p>";
                EmailDetails += "<p>Scarborough Communications<br/>Scarborough Campus<br/>01723 362392<br/>comms-scar@hull.ac.uk</p>";

                ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", PostedByEmail, "", "", EmailTitle, EmailDetails);
            }
        }

        public static DataSet loadDataset(ClassUserInfo User, int View)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            if (User.InGroup(ClassAppDetails.admingroup))
            {
                //Admin therefore show all
                if (View == 1)
                {
                    //Show Valid
                    Query = "SELECT * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "'  ORDER BY Event_DateTime";
                }
                else
                {
                    //Show Invalid
                    Query = "SELECT * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 0 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Event_DateTime";
                }

            }
            else
            {
                Query = "SELECT * FROM Events WHERE Event_Deleted = 0 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' AND Event_PostedByID = '" + User.StudentID + "' ORDER BY Event_DateTime";
            }

            RQ.RunQuery(Query);

            return RQ.dataset;
        }


        public static string loadEventsList()
        {
            string ReturnStr = "";

            int EventCounter = 0;

            ReturnStr += "<div id=\"eventsarea\" class=\"events\">";

            ReturnStr += "<table><tr><td>";

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            try
            {

                ReturnStr += "<dl>";

                if (ClassAppDetails.openday == "1")
                {
                    //Open Day
                    
                    RQ.RunQuery("SELECT Top 6 * FROM Events WHERE Event_Deleted = 0 AND Event_CategoryIDLNK = 11 AND Event_Valid = 1 AND Event_DateTime > '" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "' ORDER BY Event_DateTime");
                    RQ.connection.connection.Close();
                   
                }
                else
                {

                    RQ.RunQuery("SELECT TOP 6 * FROM Events WHERE Event_CategoryIDLNK != 11 AND Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' AND Event_DateTime <= '" + DateTime.Now.AddDays(7).ToShortDateString() + "' ORDER BY Event_DateTime");
                    RQ.connection.connection.Close();

                    if (RQ.numberofresults < 6)
                    {
                        RQ.RunQuery("SELECT Top 6 * FROM Events WHERE Event_CategoryIDLNK != 11 AND Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Event_DateTime");
                        RQ.connection.connection.Close();
                    }

                }

                foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
                {

                    ClassEvent Event = new ClassEvent(DR);
    
                    ReturnStr += "<dt><a href=\"events.aspx#event" + Event.ID + "\">" + Event.Title + "</a></dt>";

                    DateTime WeeksTime = DateTime.Now.AddDays(7);

                    if (Event.EventDateTime.CompareTo(WeeksTime) > 0)
                    {
                        ReturnStr += "<dd>" + Event.EventDateTime.ToShortDateString() + " " + Event.EventDateTime.ToShortTimeString() + " " + Event.Location + "</dd>";
                    }
                    else
                    {
                        ReturnStr += "<dd>" + Event.EventDateTime.DayOfWeek + " " + Event.EventDateTime.ToShortTimeString() + " " + Event.Location + "</dd>";
                    }

                    EventCounter += 1;
                }

                ReturnStr += "</dl>";

            }
            catch
            {

            }

            ReturnStr += "<p><a href=\"http://communications.scar.hull.ac.uk/login.aspx?ReturnUrl=%2fEvents.aspx%3faid%3d1%26eid%3d-1&aid=1&eid=-1\">Add New Event</a></p>";

            ReturnStr += "</td></tr></table>";

            ReturnStr += "</div>";

            return ReturnStr;
        }

        public static string loadEventsList(ClassCategory Category)
        {
            string ReturnStr = "";

            int Counter = 0;

            ReturnStr += "<div id=\"eventsarea\">";

            ReturnStr += "<h2>Events</h2>";

            ReturnStr += "Listed below are the most current " + Category.Title + " events. Click the title to view the message.<br/>";

            //Get List
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            ReturnStr += "<dl>";

            RQ.RunQuery("SELECT TOP 4 * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' AND Event_CategoryIDLNK = " + Category.ID + " ORDER BY Event_DateTime;");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassEvent Event = new ClassEvent(DR);
                ReturnStr += "<dt><a href=\"http://pocketcampus.scar.hull.ac.uk/events.aspx#event" + Event.ID + "\">" + Event.Title + "</a></dt>";
                ReturnStr += "<dd>" + Event.EventDateTime.ToShortDateString() + " " + Event.EventDateTime.ToShortTimeString() + " " + Event.Location + "</dd>";

                Counter++;
            }

            if (Counter == 0)
            {
                ReturnStr += "<dt>No Events</dt>";
            }

            ReturnStr += "</dl>";

            ReturnStr += "<p><a href=\"http://communications.scar.hull.ac.uk/login.aspx?ReturnUrl=%2fEvents.aspx%3faid%3d1%26eid%3d-1&aid=1&eid=-1\">Add New Event</a></p>";

            ReturnStr += "</div>";

            return ReturnStr;
        }

        public static string showAll()
        {
            string ReturnStr = "";

            //Get List
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            ReturnStr += "<dl id=\"fullevents\">";

            RQ.RunQuery("SELECT * FROM Events WHERE Event_Deleted = 0 AND Event_Valid = 1 AND Event_DateTime >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Event_DateTime");
            RQ.connection.connection.Close();

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassEvent Event = new ClassEvent(DR);

                ReturnStr += "<dt><a name=\"Event" + Event.ID + "\">" + Event.Title + "</a></dt>";

                ReturnStr += "<dd>" + Event.Event + "<br/>" + Event.Location + " at " + Event.EventDateTime.ToString() + " for " + Event.EventDuration + "<br/>" + Event.GetAttachment;
            }

            ReturnStr += "</dl>";

            return ReturnStr;
        }


    }
}
