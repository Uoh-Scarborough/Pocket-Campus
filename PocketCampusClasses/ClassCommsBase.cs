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
    public class ClassCommsBase : ClassBase
    {

        private int c_CategoryID;
        private string c_Title, c_Notice, c_PostedBy, c_PostedByID, c_PostedByEmail, c_Attachment, c_ValidatedBy, c_InvalidReason;
        private DateTime c_DisplayFrom, c_DisplayTo, c_PostedDate, c_ValidatedDate;
        private Boolean c_Urgent, c_Valid;

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public string Notice
        {
            get { return c_Notice.Trim(); }
            set { c_Notice = value.Trim(); }
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

        public DateTime DisplayFrom
        {
            get { return c_DisplayFrom; }
            set { c_DisplayFrom = value; }
        }

        public DateTime DisplayTo
        {
            get { return c_DisplayTo; }
            set { c_DisplayTo = value; }
        }

        public Boolean Urgent
        {
            get { return c_Urgent; }
            set { c_Urgent = value; }
        }

        public ClassCategory Category
        {
            get { return new ClassCategory(c_CategoryID); }
            set { c_CategoryID = value.ID; }
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

        public ClassCommsBase()
        {
            //Initialise New Class
            ValidatedDate = DateTime.Now;
            Deleted = false;
            Attachment = "";
        }

        public ClassCommsBase(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From CommsBase WHERE CommsBase_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassCommsBase(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            ID = Convert.ToInt32(DR["CommsBase_ID_LNK"].ToString());
            Title = DR["CommsBase_Title"].ToString();
            Notice = DR["CommsBase_Notice"].ToString();
            Attachment = DR["CommsBase_Attachement"].ToString();
            DisplayFrom = Convert.ToDateTime(DR["CommsBase_DisplayFrom"].ToString());
            DisplayTo = Convert.ToDateTime(DR["CommsBase_DisplayTo"].ToString());
            Urgent = Convert.ToBoolean(DR["CommsBase_Urgent"].ToString());
            c_CategoryID = Convert.ToInt16(DR["CommsBase_CategoryIDLNK"].ToString());
            PostedBy = DR["CommsBase_PostedBy"].ToString();
            PostedByID = DR["CommsBase_PostedByID"].ToString();
            PostedByEmail = DR["CommsBase_PostedByEmail"].ToString();
            PostedDate = Convert.ToDateTime(DR["CommsBase_PostedDate"].ToString());
            Valid = Convert.ToBoolean(DR["CommsBase_Valid"].ToString());
            ValidatedBy = DR["CommsBase_ValidatedBy"].ToString();
            try
            {
                ValidatedDate = Convert.ToDateTime(DR["CommsBase_ValidatedDate"].ToString());
            }
            catch
            {
                ValidatedDate = DateTime.Now;
            }
            InvalidReason = DR["CommsBase_InvalidReason"].ToString();
            Deleted = Convert.ToBoolean(DR["CommsBase_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "INSERT INTO CommsBase (CommsBase_Title, CommsBase_Notice, CommsBase_Attachement, CommsBase_DisplayFrom, CommsBase_DisplayTo, CommsBase_Urgent, CommsBase_CategoryIDLNK, CommsBase_PostedBy, CommsBase_PostedByID, CommsBase_PostedByEmail, CommsBase_PostedDate, CommsBase_Valid, CommsBase_ValidatedBy, CommsBase_ValidatedDate, CommsBase_InvalidReason, CommsBase_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "','" + ClassUseful.FormatStringForDB(Notice) + "','" + Attachment + "','" + DisplayFrom.ToShortDateString() + "','" + DisplayTo.ToShortDateString() + "'," + Urgent.GetHashCode() + "," + c_CategoryID + ",'" + PostedBy + "','" + PostedByID + "','" + PostedByEmail + "','" + PostedDate.ToShortDateString() + "'," + Valid.GetHashCode() + ",'" + ValidatedBy + "','" + ValidatedDate.ToString() + "','" + InvalidReason + "',0) SELECT @@IDENTITY;";

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

            string Query = "UPDATE CommsBase SET CommsBase_Title = '" + ClassUseful.FormatStringForDB(Title) + "', CommsBase_Notice = '" + ClassUseful.FormatStringForDB(Notice) + "', CommsBase_Attachement = '" + Attachment + "', CommsBase_DisplayFrom = '" + DisplayFrom.ToShortDateString() + "', CommsBase_DisplayTo = '" + DisplayTo + "', CommsBase_Urgent = " + Urgent.GetHashCode() + ", CommsBase_CategoryIDLNK = " + c_CategoryID + ", CommsBase_PostedBy = '" + PostedBy + "', CommsBase_PostedByID = '" + PostedByID + "', CommsBase_PostedByEmail = '" + PostedByEmail + "', CommsBase_PostedDate = '" + PostedDate + "', CommsBase_Valid = " + Valid.GetHashCode() + ", CommsBase_ValidatedBy = '" + ValidatedBy + "', CommsBase_ValidatedDate = '" + ValidatedDate.ToString() + "', CommsBase_InvalidReason = '" + InvalidReason + "', CommsBase_Deleted = " + Deleted.GetHashCode() + " WHERE CommsBase_ID_LNK = " + ID + ";";
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
            string EmailTitle = "New Notice Added";

            string EmailDetails = "<html><head><title>New Notice Added -" + Title + "</title></head><body>";
            EmailDetails += "<p>Dear Comms Admin,</p>";
            EmailDetails += "<p>A new notice entited " + Title + " has not been added to the system and is awaiting validation.</p>";
            EmailDetails += "<p>You can view the notices at <a href=\"comms.scar.hull.ac.uk/notices.aspx?aid=1&amp;nid=" + ID + "\">comms.scar.hull.ac.uk/notice.aspx?aid=1&amp;nid=" + ID + "</a></p>";
            EmailDetails += "<p>&nbsp;</p>";
            EmailDetails += "<p>Scarborough Communications<br/>Scarborough Campus<br/>01723 362392<br/>comms-scar@hull.ac.uk</p>";

            ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", ClassAppDetails.adminemail, "", "", EmailTitle, EmailDetails);
        }

        public void sendEmail()
        {
            if (!Valid)
            {
                string EmailTitle = "Notice Invalid";

                string EmailDetails = "<html><head><title>Notice Invalid -" + Title + "</title></head><body>";
                EmailDetails += "<p>Dear " + PostedBy + "</p>";
                EmailDetails += "<p>Your notice entited " + Title + " has not been succesfully validated.</p>";

                if (InvalidReason != "")
                {
                    EmailDetails += "<p>The reason given was, " + InvalidReason + "</p>";
                }

                EmailDetails += "<p>You can review the notice at <a href=\"comms.scar.hull.ac.uk/notices.aspx?aid=1&amp;nid=" + ID + "\">comms.scar.hull.ac.uk/notices.aspx?aid=1&amp;nid=" + ID + "</a></p>";
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
                    Query = "SELECT * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_Valid = 1 AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY CommsBase_DisplayFrom";
                }
                else
                {
                    //Show Invalid
                    Query = "SELECT * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_Valid = 0 AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY CommsBase_DisplayFrom";
                }

            }
            else
            {
                Query = "SELECT * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_PostedByID = '" + User.StudentID + "' ORDER BY CommsBase_DisplayFrom";
            }

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static string loadNoticesList()
        {
            string ReturnStr = "";

            //ReturnStr += "<div id=\"noticesarea\">";

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            ReturnStr += "<ul>";

            if (ClassAppDetails.openday == "1")
            {
                //Open Day

                RQ.RunQuery("SELECT TOP 6 * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_Valid = 1 AND CommsBase_CategoryIDLNK = 11 AND CommsBase_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY CommsBase_Urgent DESC, CommsBase_DisplayFrom;");
            }
            else
            {

                RQ.RunQuery("SELECT TOP 6 * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_CategoryIDLNK != 11 AND CommsBase_Valid = 1 AND CommsBase_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY CommsBase_Urgent DESC, CommsBase_DisplayFrom;");

            }

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassCommsBase Notice = new ClassCommsBase(DR);
                ReturnStr += "<li><a href=\"http://campusinfo.scar.hull.ac.uk/notices-feed?id=" + Notice.ID + "\">" + Notice.Title + "</a></li>";
                RQ.connection.connection.Close();
            }

            ReturnStr += "</ul>";

            ReturnStr += "<p><a href=\"http://communications.scar.hull.ac.uk/login.aspx?ReturnUrl=%2fNotices.aspx&amp;aid=1&amp;nid=-1\">Add New Notice</a></p>";

            //ReturnStr += "</div>";

            return ReturnStr;
        }

        public static string loadNoticesList(ClassCategory Category)
        {
            string ReturnStr = "";

            int Counter = 0;

            //ReturnStr += "<div id=\"noticesarea\">";

            ReturnStr += "<h2>Notices</h2>";

            ReturnStr += "Listed below are the current " + Category.Title + " messages. Click the title to view the message.<br/>";

            //Get List
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            ReturnStr += "<ul>";

            RQ.RunQuery("SELECT TOP 4 * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_Valid = 1 AND CommsBase_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_CategoryIDLNK = " + Category.ID + " ORDER BY CommsBase_DisplayFrom, CommsBase_Urgent;");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassCommsBase Notice = new ClassCommsBase(DR);
                ReturnStr += "<li><a href=\"http://pocketcampus.scar.hull.ac.uk/notices.aspx#notice" + Notice.ID + "\">" + Notice.Title + "</a></li>";
                Counter++;
            }

            if (Counter == 0)
            {
                ReturnStr += "<li>No Notices</li>";
            }

            ReturnStr += "</ul>";

            ReturnStr += "<p><a href=\"http://comms.scar.hull.ac.uk/login.aspx?ReturnUrl=%2fnotices.aspx?aid=1&amp;nid=-1\">Add New Notice</a></p>";

            //ReturnStr += "</div>";

            return ReturnStr;
        }

        public static string showAll()
        {
            string ReturnStr = "";

            //Get List
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            ReturnStr += "<dl id=\"fullnotices\">";

            RQ.RunQuery("SELECT * FROM CommsBase WHERE CommsBase_Deleted = 0 AND CommsBase_Valid = 1 AND CommsBase_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + "' AND CommsBase_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY CommsBase_DisplayFrom, CommsBase_Urgent;");

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassCommsBase Notice = new ClassCommsBase(DR);

                ReturnStr += "<dt><a name=\"Notice" + Notice.ID + "\">" + Notice.Title + "</a></dt>";

                ReturnStr += "<dd>" + Notice.Notice + "<br/>" + Notice.GetAttachment;
            }

            ReturnStr += "</dl>";

            return ReturnStr;
        }

       
    }
}
