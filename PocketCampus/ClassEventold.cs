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

namespace PocketCampus
{
    public class ClassEvent
    {
        private int c_ID;
        private string c_Title, c_Event, c_Location, c_PostedBy, c_PostedByID, c_PostedByEmail, c_Duration, c_Attachment;
        private DateTime c_EventDateTime, c_PostedDate; 
        private Boolean c_Valid, c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

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

        public Boolean Valid
        {
            get { return c_Valid; }
            set { c_Valid = value; }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassEvent()
        {
             //Initialise New Class
            Deleted = false;
            Attachment = "";
        }

        public ClassEvent(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Events WHERE Event_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.eventscurrentconnection);
            RQ.RunQuery(Query);

            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Event = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Attachment = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            Location = RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString();
            EventDateTime = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString());
            EventDuration = RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            PostedBy = RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            PostedByID = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            PostedByEmail = RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString();
            PostedDate = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[10].ToString());
            Valid = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[11].ToString());
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[12]);
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.eventscurrentconnection);
            bool Result;
            string Query = "INSERT INTO Events (Event_Title, Event_Event, Event_Attachment, Event_Location, Event_DateTime, Event_Duration, Event_PostedBy, Event_PostedByID, Event_PostedByEmail, Event_Posted, Event_Valid, Event_Deleted) VALUES ('" + Title + "','" + Event + "','" + Attachment + "','" + Location + "','" + EventDateTime.ToString() + "','" + EventDuration + "','" + PostedBy + "','" + PostedByID + "','" + PostedByEmail + "','" + PostedDate.ToShortDateString() + "'," + Valid.GetHashCode() + ",0) SELECT @@IDENTITY;";

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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.eventscurrentconnection);
            bool Result;
            string Query = "UPDATE Events SET Event_Title = '" + Title + "', Event_Event = '" + Event + "', Event_Attachment = '" + Attachment + "', Event_DateTime = '" + EventDateTime + "', Event_Duration = '" + EventDuration + "', Event_PostedBy = '" + PostedBy + "', Event_PostedByID = '" + PostedByID + "', Event_PostedByEmail = '" + PostedByEmail + "', Event_Posted = '" + PostedDate + "', Event_Valid = " + Valid.GetHashCode() + ", Event_Deleted = " + Deleted.GetHashCode() + " WHERE Event_ID_LNK = " + ID + ";";
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

        

       
    }
}
