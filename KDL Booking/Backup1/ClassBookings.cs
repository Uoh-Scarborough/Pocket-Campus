using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using StandardClasses;

namespace ProductionBooking
{
    public class ClassBookings : IComparable
    {
        private  int c_ID;
        private string c_Title, c_Location, c_Week, c_Name, c_Email, c_AdditionalInfo, c_GroupMembers;
        private int c_Day, c_StartTime, c_EndTime, c_Number;
        private DateTime c_Date;
        private Boolean c_Workshop, c_Deleted;

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

        public int Day
        {
            get { return c_Day; }
            set { c_Day = value; }
        }

        public int StartTime
        {
            get { return c_StartTime; }
            set { c_StartTime = value; }
        }

        public string StartTimeOut
        {
            get
            {
                int ST = StartTime;
                int Hours = ST / 4;
                int Minutes = 15 * (ST - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public int EndTime
        {
            get { return c_EndTime; }
            set { c_EndTime = value; }
        }

        public string EndTimeOut
        {
            get
            {
                int ET = EndTime;
                int Hours = ET / 4;
                int Minutes = 15 * (ET - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public string Location
        {
            get { return c_Location.Trim(); }
            set { c_Location = value.Trim(); }
        }

        public string Week
        {
            get { return c_Week.Trim(); }
            set { c_Week = value.Trim(); }
        }

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string Email
        {
            get { return c_Email.Trim(); }
            set { c_Email = value.Trim(); }
        }

        public int Number
        {
            get { return c_Number; }
            set { c_Number = value; }
        }

        public DateTime Date
        {
            get { return c_Date; }
            set { c_Date = value; }
        }

        public string AdditionalInfo
        {
            get { return c_AdditionalInfo.Trim(); }
            set { c_AdditionalInfo = value.Trim(); }
        }

        public string GroupMembers
        {
            get { return c_GroupMembers.Trim(); }
            set { c_GroupMembers = value.Trim(); }
        }

        public Boolean Workshop
        {
            get { return c_Workshop; }
            set { c_Workshop = value; }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassBookings()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassBookings(int ID)
        {
            //Initialise New Class

            c_ID = ID;
            string Query = "SELECT * From Bookings WHERE Booking_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.pbcurrentconnection);
            RQ.RunQuery(Query);

            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Day = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString());
            StartTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString());
            EndTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
            Location = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            Week = RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            Email = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            Number = Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString());
            Date = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[10].ToString());
            AdditionalInfo = RQ.dataset.Tables[0].Rows[0].ItemArray[11].ToString();
            GroupMembers = RQ.dataset.Tables[0].Rows[0].ItemArray[12].ToString();
            Workshop = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[13].ToString());
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[14].ToString());
            //Deleted = false;
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.pbcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string FormattedAdditionalInfo = AdditionalInfo;

            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("'", "&#39;");
            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("\"", "&#34;");

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Email, Booking_Number, Booking_Date, Booking_AdditionalInfo, Booking_GroupMember, Booking_Workshop, Booking_Deleted) VALUES ('" + FormattedTitle + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Email + "'," + Number + ",'" + Date + "','" + FormattedAdditionalInfo + "','" + GroupMembers + "'," + Workshop.GetHashCode() + ",0) SELECT @@IDENTITY;";
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

        public bool Create(SqlTransaction Trans, ClassConnection TransConn)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.pbcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string FormattedAdditionalInfo = AdditionalInfo;

            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("'", "&#39;");
            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("\"", "&#34;");

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Email, Booking_Number, Booking_Date, Booking_AdditionalInfo, Booking_GroupMember, Booking_Workshop, Booking_Deleted) VALUES ('" + FormattedTitle + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Email + "'," + Number + ",'" + Date + "','" + FormattedAdditionalInfo + "','" + GroupMembers + "'," + Workshop.GetHashCode() + ",0) SELECT @@IDENTITY;";
            try
            {
            RQ.RunQuery(Query,Trans,TransConn);
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.pbcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string FormattedAdditionalInfo = AdditionalInfo;

            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("'", "&#39;");
            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("\"", "&#34;");

            string Query = "UPDATE Bookings SET Booking_Title = '" + FormattedTitle + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_AdditionalInfo = '" + FormattedAdditionalInfo + "', Booking_GroupMember = '" + GroupMembers + "', Booking_Workshop = " + Workshop.GetHashCode() + ", Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query);
                //c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public bool Save(SqlTransaction Trans, ClassConnection TransConn)
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.pbcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string FormattedAdditionalInfo = AdditionalInfo;

            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("'", "&#39;");
            FormattedAdditionalInfo = FormattedAdditionalInfo.Replace("\"", "&#34;");

            string Query = "UPDATE Bookings SET Booking_Title = '" + FormattedTitle + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_AdditionalInfo = '" + FormattedAdditionalInfo + "', Booking_GroupMember = '" + GroupMembers + "', Booking_Workshop = " + Workshop.GetHashCode() + ", Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query,Trans,TransConn);
                //c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public void SendEmail(int Type)
        {
            StreamReader streamReader = new StreamReader(ClassAppDetails.emaildir + "/BookingConfirmation.html");

            //Type 
            // 0 = Normal
            // 1 = Change
            // 2 = Admin Change
            // 3 = Deleted
            // 4 = AdminDeleted

            switch (Type){
                case 0:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/BookingConfirmation.html");
                    break;
                case 1:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/EditBookingConfirmation.html");
                    break;
                case 2:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/AdminEditBookingConfirmation.html");
                    break;
                case 3:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/BookingDeleted.html");
                    break;
                case 4:
                    streamReader = new StreamReader(ClassAppDetails.emaildir + "/AdminBookingDeleted.html");
                    break;
            }

            
            string email = streamReader.ReadToEnd();
            streamReader.Close();

            email = email.Replace("@Name", this.Name);

            email = email.Replace("@Today", DateTime.Now.ToShortDateString());

            email = email.Replace("@ModuleGroup", this.Title);

            //email = email.Replace("@BookingDate", this.Date.ToShortDateString());
            email = email.Replace("@BookingDate", ClassGeneral.getAcademicDate(Convert.ToInt16(this.Week),this.Day));

            email = email.Replace("@Location", this.Location);

            email = email.Replace("@TimeIn", this.StartTimeOut);

            email = email.Replace("@TimeOut", this.EndTimeOut);

            email = email.Replace("@AllParticipants", this.GroupMembers);

            email = email.Replace("@WorkshopAccess", this.Workshop.ToString());

            ClassEmail.SendMailMessage("production-scar@hull.ac.uk", this.Email, "", "", "Production Booking Confirmation", email);
        }

        public override string ToString()
        {
            return StartTime.ToString();
        }

        public int CompareTo(object obj)
        {
            int result = this.ToString().CompareTo(obj.ToString());
            //if (result == 0)
            //    result = this.SSN.CompareTo(Compare.SSN);
            return result;
        }
    }
}
