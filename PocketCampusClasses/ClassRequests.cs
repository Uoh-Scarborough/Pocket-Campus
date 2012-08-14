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

namespace PocketCampusClasses 
{
    public class ClassRequest : ClassBase
    {

        private string c_Name, c_Number, c_Email, c_CompletedBy;
        private ClassRequestForms c_Form;
        private Boolean c_Completed, c_EmailSent;
        private DateTime c_RequestDate, c_CompletedDate;

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

        public void CreateEmails()
        {
            //Check If Request Completed
            if (Completed && !EmailSent)
            {
                //Send Completed Email
                string Title = "Student Services - " + Form.FormName + " ready for collection";

                string EmailDetails = "<html><head><title>Student Support Request -" + Form.FormName + " ready for collection</title></head><body>";
                EmailDetails += "<p>Dear " + Name + "</p>";
                EmailDetails += "<p>I am pleased to advise that your status letter is now ready for collection from Campus Connect located on the ground floor within the Keith Donaldson Library. </p>";
                EmailDetails += "<p>If you require any further information please do not hesitate to contact Scarborough Student Services by telephone on 01723 357236 or by email <a href='student-help@hull.ac.uk'>student-help@hull.ac.uk</a></p>";
                EmailDetails += "<p>Kind regards,</p>";
                EmailDetails += "<p>Scarborough Student Services</p>";

                ClassEmail.SendMailMessage("studenthelp-scar@hull.ac.uk", Email, "", "", Title, EmailDetails);

                EmailSent = true;
            }
            
            if(!Completed)
            {
                //Send Standard
                string Title = "Student Services - " + Form.FormName + " request receipt";

                string EmailDetails = "<html><head><title>Student Support Request -" + Form.FormName + "</title></head><body>";
                EmailDetails += "<p>Dear " + Name + "</p>";
                EmailDetails += "<p>We are pleased to acknowledge safe receipt of your request for a status letter.</p>";
                EmailDetails += "<p>You will receive a second email when your letter is ready including details of where this can be collected from.</p>";
                EmailDetails += "<p>If you require any further information in advance of receiving this second email please do not hesitate to contact Scarborough Student Services by telephone on 01723 357236 or by email <a href='student-help@hull.ac.uk'>student-help@hull.ac.uk</a>.</p>";
                EmailDetails += "<p>Kind Regards,</p>";
                EmailDetails += "<p>Scarborough Student Services</p>";

                string EmailDetails2 = "<html><head><title>Student Support Request -" + Form.FormName + "</title></head><body>";
                EmailDetails2 += "<p>Dear " + Name + "</p>";
                EmailDetails2 += "<p>A new request has been made for a " + Form.FormName + " from " + Name + " (" + Number + ").</p>";
                EmailDetails2 += "<p>You can view the request at <a href='http://studentservices.scar.hull.ac.uk/default.aspx?aid=1&amp;rid=" + ID + "'>Student Services</a>.</p>";
                EmailDetails2 += "<p>Scarborough Student Services</p>";

                ClassEmail.SendMailMessage("studenthelp-scar@hull.ac.uk", Email, "", "", Title, EmailDetails);

                ClassEmail.SendMailMessage("studenthelp-scar@hull.ac.uk", "studenthelp-scar@hull.ac.uk", "", "", Title, EmailDetails2);
            }
        }

        public ClassRequest(int ID)
        {
            //Pull of ID
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);
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
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.studentservicesconnection);
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
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);

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
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.studentservicesconnection);

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
