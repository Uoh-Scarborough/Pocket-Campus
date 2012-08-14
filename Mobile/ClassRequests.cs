using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;

namespace Mobile
{
    public class ClassRequest
    {

        private int c_ID;
        private string c_Name, c_Number, c_Email, c_CompletedBy;
        private ClassRequestForms c_Form;
        private Boolean c_Completed, c_EmailSent, c_Deleted;
        private DateTime c_RequestDate, c_CompletedDate;

        public int ID
        {
            get { return  c_ID; }
        }

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string Number
        {
            get { return c_Number.Trim(); }
            set { c_Number = value.Trim(); }
        }

        public string Email
        {
            get { return c_Email.Trim(); }
            set { c_Email = value.Trim(); }
        }

        public ClassRequestForms Form
        {
            get { return c_Form; }
            set { c_Form = value; }
        }

        public DateTime RequestDate
        {
            get { return  c_RequestDate; }
            set { c_RequestDate = value; }
        }

        public Boolean Completed
        {
            get { return c_Completed; }
            set { c_Completed = value; }
        }

        public DateTime CompletedDate
        {
            get { return c_CompletedDate; }
            set { c_CompletedDate = value; }
        }

        public string CompletedBy
        {
            get { return c_CompletedBy.Trim(); }
            set { c_CompletedBy = value.Trim(); }
        }

        public Boolean EmailSent
        {
            get { return c_EmailSent; }
            set { c_EmailSent = value; }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public void CreateEmails()
        {
            //Check If Request Completed
            if (Completed && !EmailSent)
            {
                //Send Completed Email
                string Title = "Student Services - " + Form.FormName;

                string EmailDetails = "<html><head><title>Student Support Request -" + Form.FormName + "</title></head><body>";
                EmailDetails += "<p>Dear " + Name + "</p>";
                EmailDetails += "<p>Thankyou you request has now been completed and is ready to collect from Student Services in Quad 3.</p>";
                EmailDetails += "<p>If you require any further information please don't hessitate to get in touch with Student Services.</p>";
                EmailDetails += "<p>&nbsp;</p><p>&nbsp;</p>";
                EmailDetails += "<p>Scarborough Student Services<br/>Quad 3, Scarborough Campus<br/>01723 35XXXX<br/>student-help@hull.ac.uk</p>";

                ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", Email, "", "", Title, EmailDetails);

                EmailSent = true;
            }
            
            if(!Completed)
            {
                //Send Standard
                string Title = "Student Services - " + Form.FormName;

                string EmailDetails = "<html><head><title>Student Support Request -" + Form.FormName + "</title></head><body>";
                EmailDetails += "<p>Dear " + Name + "</p>";
                EmailDetails += "<p>Thankyou you request it is now being processed, you will recive an email to this informing you that you letter is ready to collect from Student Services in Quad 3.</p>";
                EmailDetails += "<p>If you require any further information please don't hessitate to get in touch with Student Services.</p>";
                EmailDetails += "<p>nbsp;</p><p>nbsp;</p>";
                EmailDetails += "<p>Scarborough Student Services<br/>Quad 3, Scarborough Campus<br/>01723 35XXXX<br/>student-help@hull.ac.uk</p>";

                string EmailDetails2 = "<html><head><title>Student Support Request -" + Form.FormName + "</title></head><body>";
                EmailDetails2 += "<p>Dear " + Name + "</p>";
                EmailDetails2 += "<p>A new request has been made for a " + Form.FormName + " from " + Name + " (" + Number + ").</p>";
                EmailDetails2 += "<p>You can view the request at <a href='http://studentservices.scar.hull.ac.uk/default.aspx?aid=1&amp;rid=" + ID + "'>Student Services</a>.</p>";
                EmailDetails2 += "<p>nbsp;</p><p>nbsp;</p>";
                EmailDetails2 += "<p>Scarborough Student Services<br/>Quad 3, Scarborough Campus<br/>01723 35XXXX<br/>student-help@hull.ac.uk</p>";

                ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", Email, "", "", Title, EmailDetails);

                ClassEmail.SendMailMessage("pocketcampus@hull.ac.uk", "studenthelp-scar@hull.ac.uk", "", "", Title, EmailDetails2);
            }
        }

        public ClassRequest(int ID)
        {
            //Pull of ID
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.sscurrentconnection);
            RQ.RunQuery("SELECT * FROM StudentRequest WHERE StudentRequest_ID_LNK = " + ID + " AND StudentRequest_Deleted = 0;");
            //Response.Write(RQ.query);
            this.c_ID = ID;
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Number = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Email = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            Form = new ClassRequestForms(Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[4]));
            RequestDate = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[5]);
            Completed = (Boolean)RQ.dataset.Tables[0].Rows[0].ItemArray[6];
            CompletedDate = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[7]);
            CompletedBy = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            EmailSent = (Boolean)RQ.dataset.Tables[0].Rows[0].ItemArray[9];
            Deleted = (Boolean)RQ.dataset.Tables[0].Rows[0].ItemArray[10];

        }

        public ClassRequest(string sName, string sNumber, string sEmail, ClassRequestForms fForm)
        {
             //Create new Request
            Name = sName;
            Number = sNumber;
            Email = sEmail;
            Form = fForm;

            //Setup Request Details
            RequestDate = DateTime.Now;
            CompletedBy = "";
            CompletedDate = DateTime.Now;
            Completed = false;
            EmailSent = false;
            Deleted = false;

            //Create
            Create();

            CreateEmails();
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.sscurrentconnection);
            bool Result;
            string Query = "INSERT INTO StudentRequest (StudentRequest_Name, StudentRequest_Number, StudentRequest_Email, StudentRequest_FormID_LNK, StudentRequest_RequestDate, StudentRequest_Completed, StudentRequest_CompletedDate, StudentRequest_CompletedBy, StudentRequest_EmailSent, StudentRequest_Deleted) VALUES ('" + Name + "','" + Number + "','" + Email + "'," + Form.ID + ",'" + RequestDate.ToShortDateString() + "'," + Completed.GetHashCode() + ",'" + CompletedDate.ToShortDateString() + "','" + CompletedBy + "', 0, 0) SELECT @@IDENTITY;";
            
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.sscurrentconnection);
            bool Result;
            string Query = "UPDATE StudentRequest SET StudentRequest_Name = '" + Name + "', StudentRequest_Email = '" + Email + "', StudentRequest_FormID_LNK = " + Form.ID + ", StudentRequest_RequestDate = '" + RequestDate.ToShortDateString() + "', StudentRequest_Completed = " + Completed.GetHashCode() + ", StudentRequest_CompletedDate = '" + CompletedDate.ToShortDateString() + "', StudentRequest_CompletedBy = '" + CompletedBy + "', StudentRequest_EmailSent = " + EmailSent.GetHashCode() + ", StudentRequest_Deleted = " + Deleted.GetHashCode() + " WHERE StudentRequest_ID_LNK = " + ID + ";";
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

        public static DataSet loadDataset(Boolean Completed)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.sscurrentconnection);

            string Query = "";

            if (Completed)
            {
                Query = "SELECT * FROM Student_Request_Completed_View ORDER BY StudentRequest_Number";
            }
            else
            {
                Query = "SELECT * FROM Student_Request_View ORDER BY StudentRequest_Number";
            }
            
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static Boolean requestExists(string sNumber, int fForm)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.sscurrentconnection);

            RQ.RunQuery("SELECT * FROM StudentRequest WHERE StudentRequest_Number = " + sNumber + " AND StudentRequest_FormID_LNK = " + fForm + " AND StudentRequest_Deleted = 0;");

            if (RQ.numberofresults > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
