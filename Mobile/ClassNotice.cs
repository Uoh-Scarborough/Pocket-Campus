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

namespace Mobile
{
    public class ClassNotice
    {

        private int c_ID, c_CategoryID;
        private string c_Title, c_Notice, c_PostedBy, c_PostedByID, c_PostedByEmail, c_Attachment, c_ValidatedBy, c_InvalidReason;
        private DateTime c_DisplayFrom, c_DisplayTo, c_PostedDate, c_ValidatedDate;
        private Boolean c_Urgent, c_Valid, c_Deleted;

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

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassNotice()
        {
             //Initialise New Class
            ValidatedDate = DateTime.Now;
            Deleted = false;
            Attachment = "";
        }

        public ClassNotice(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Notices WHERE Notice_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Notice = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Attachment = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            DisplayFrom = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
            DisplayTo = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString());
            Urgent = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString());
            c_CategoryID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString());
            PostedBy = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            PostedByID = RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString();
            PostedByEmail = RQ.dataset.Tables[0].Rows[0].ItemArray[10].ToString();
            PostedDate = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[11].ToString());
            Valid = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[12].ToString());
            ValidatedBy = RQ.dataset.Tables[0].Rows[0].ItemArray[13].ToString();
            try
            {
                ValidatedDate = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[14].ToString());
            }
            catch
            {
                ValidatedDate = DateTime.Now;
            }
            InvalidReason = RQ.dataset.Tables[0].Rows[0].ItemArray[15].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[16]);
        }

        public static ArrayList loadDataset()
        {
            ArrayList ListofNotices = new ArrayList();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            Query = "SELECT Notice_ID_LNK FROM Notices WHERE Notice_Deleted = 0 AND Notice_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Notice_DisplayFrom";
            
            RQ.RunQuery(Query);

            foreach (DataRow dr in RQ.dataset.Tables[0].Rows)
            {
                ClassNotice Notice = new ClassNotice((int)dr[0]);

                ListofNotices.Add(Notice);
            }

            return ListofNotices;
        }

       
    }
}
