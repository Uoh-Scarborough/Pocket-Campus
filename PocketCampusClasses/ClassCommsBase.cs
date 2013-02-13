using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
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
    public class ClassCommsBase
    {

        public enum CommsType
        {
            Notice,
            Event,
            Menu,
            Ticker
        }

        public enum EmailType
        {
            Add,
            Valid,
            Invalid
        }
        
        protected int c_BaseID;
        private int c_CategoryID;
        private string c_Title, c_Content, c_PostedBy, c_PostedByID, c_PostedByEmail, c_Attachment, c_ValidatedBy, c_InvalidReason;
        private DateTime c_DisplayFrom, c_DisplayTo, c_PostedDate, c_ValidatedDate;
        private Boolean c_Urgent, c_Valid, c_UseAttachment;
        protected Boolean c_Deleted;

        public int BaseID
        {
            get { return c_BaseID; }
            set { c_BaseID = value; }
        }

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public string Content
        {
            get { return c_Content.Trim(); }
            set { c_Content = value.Trim(); }
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

        public Boolean UseAttachement
        {
            get
            {
                return c_UseAttachment;
            }
            set
            {
                c_UseAttachment = value;
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

        public virtual ClassCategory Category
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

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }


        public ClassCommsBase()
        {
            //Initialise New Class
            ValidatedDate = DateTime.Now;
            Deleted = false;
            Attachment = "";
        }

        public void LoadBaseFromID(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From CommsBase WHERE CommsBase_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            LoadBaseFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        protected void LoadBaseFromDR(DataRow DR)
        {
            BaseID = Convert.ToInt32(DR["CommsBase_ID_LNK"].ToString());
            Title = DR["CommsBase_Title"].ToString();
            Content = DR["CommsBase_Notice"].ToString();
            Attachment = DR["CommsBase_Attachement"].ToString();
            UseAttachement = Convert.ToBoolean(DR["CommsBase_UseAttachement"]);
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

        protected void LoadBaseFromBase(ClassCommsBase Base)
        {
            BaseID = Base.BaseID;
            Title = Base.Title;
            Content = Base.Content;
            Attachment = Base.Attachment;
            UseAttachement = Base.UseAttachement;
            DisplayFrom = Base.DisplayFrom;
            DisplayTo = Base.DisplayTo;
            Urgent = Base.Urgent;
            Category = Base.Category;
            PostedBy = Base.PostedBy;
            PostedByID = Base.PostedByID;
            PostedByEmail = Base.PostedByEmail;
            PostedDate = Base.PostedDate;
            Valid = Base.Valid;
            ValidatedBy = Base.ValidatedBy;
            ValidatedDate = Base.ValidatedDate;
            InvalidReason = Base.InvalidReason;
            Deleted = Base.Deleted;
        }

        public ClassCommsBase GetBase()
        {
            return this;
        }

        public bool CreateBase()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "INSERT INTO CommsBase (CommsBase_Title, CommsBase_Notice, CommsBase_Attachement, CommsBase_UseAttachement, CommsBase_DisplayFrom, CommsBase_DisplayTo, CommsBase_Urgent, CommsBase_CategoryIDLNK, CommsBase_PostedBy, CommsBase_PostedByID, CommsBase_PostedByEmail, CommsBase_PostedDate, CommsBase_Valid, CommsBase_ValidatedBy, CommsBase_ValidatedDate, CommsBase_InvalidReason, CommsBase_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "','" + ClassUseful.FormatStringForDB(Content) + "','" + Attachment + "'," + UseAttachement.GetHashCode() + ",'" + DisplayFrom.ToShortDateString() + "','" + DisplayTo.ToShortDateString() + "'," + Urgent.GetHashCode() + "," + c_CategoryID + ",'" + PostedBy + "','" + PostedByID + "','" + PostedByEmail + "','" + PostedDate.ToShortDateString() + "'," + Valid.GetHashCode() + ",'" + ValidatedBy + "','" + ValidatedDate.ToString() + "','" + InvalidReason + "',0) SELECT @@IDENTITY;";

            try
            {
                RQ.RunQuery(Query);
                c_BaseID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }

            return Result;
        }

        public bool SaveBase()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "UPDATE CommsBase SET CommsBase_Title = '" + ClassUseful.FormatStringForDB(Title) + "', CommsBase_Notice = '" + ClassUseful.FormatStringForDB(Content) + "', CommsBase_Attachement = '" + Attachment + "', CommsBase_UseAttachement = " + UseAttachement.GetHashCode() + ", CommsBase_DisplayFrom = '" + DisplayFrom.ToShortDateString() + "', CommsBase_DisplayTo = '" + DisplayTo + "', CommsBase_Urgent = " + Urgent.GetHashCode() + ", CommsBase_CategoryIDLNK = " + c_CategoryID + ", CommsBase_PostedBy = '" + PostedBy + "', CommsBase_PostedByID = '" + PostedByID + "', CommsBase_PostedByEmail = '" + PostedByEmail + "', CommsBase_PostedDate = '" + PostedDate + "', CommsBase_Valid = " + Valid.GetHashCode() + ", CommsBase_ValidatedBy = '" + ValidatedBy + "', CommsBase_ValidatedDate = '" + ValidatedDate.ToString() + "', CommsBase_InvalidReason = '" + InvalidReason + "', CommsBase_Deleted = " + Deleted.GetHashCode() + " WHERE CommsBase_ID_LNK = " + BaseID + ";";
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

        public void sendEmail(CommsType Type, EmailType Email, int ParentID, Boolean Admin, String To)
        {
            StreamReader streamReader = new StreamReader(ClassAppDetails.emaildir + "/CommunicationAddedAdmin.html");
            string emailTitle = "";

            switch(Email){
                case EmailType.Add:
                    if (Admin)
                    {
                        streamReader = new StreamReader(ClassAppDetails.emaildir + "/CommunicationAddedAdmin.html");
                    }
                    else
                    {
                        streamReader = new StreamReader(ClassAppDetails.emaildir + "/CommunicationAdded.html");
                    }
                    emailTitle = "New " + Type.ToString() + " Added";

                    break;

                case EmailType.Invalid:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/CommunicationInvalid.html");
                    emailTitle = Type.ToString() + " Invalid";

                    break;

                case EmailType.Valid:

                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/CommunicationValid.html");
                    emailTitle = Type.ToString() + " Valid";

                    break;
            }

            string email = streamReader.ReadToEnd();
            streamReader.Close();

            email = email.Replace("@CommType", Type.ToString());

            email = email.Replace("@Name", this.PostedBy);

            email = email.Replace("@Title", this.Title);

            email = email.Replace("@CommURL", "http://communications.scar.hull.ac.uk?cmd=" + Type.ToString() + "&id=" + ParentID);

            email = email.Replace("@Reason", this.InvalidReason);

            ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", To, "", "", emailTitle, email);
            
        }
       
    }
}
