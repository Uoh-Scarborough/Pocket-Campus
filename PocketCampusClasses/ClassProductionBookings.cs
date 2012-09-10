using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassProductionBookings : ClassBooking
    {

        private string c_AdditionalInfo;
        private Boolean c_Workshop;

        public string AdditionalInfo
        {
            get { return c_AdditionalInfo.Trim(); }
            set { c_AdditionalInfo = value.Trim(); }
        }

        public Boolean Workshop
        {
            get { return c_Workshop; }
            set { c_Workshop = value; }
        }

        public ClassProductionBookings()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassProductionBookings(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Bookings WHERE Booking_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            CreateFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassProductionBookings(DataRow DR)
        {
            CreateFromDR(DR);
        }

        private void CreateFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Booking_ID_LNK"].ToString());
            Title = DR["Booking_Title"].ToString();
            Day = Convert.ToInt32(DR["Booking_Day"].ToString());
            StartTime = Convert.ToInt16(DR["Booking_StartTime"].ToString());
            EndTime = Convert.ToInt16(DR["Booking_EndTime"].ToString());
            Location = DR["Booking_Location"].ToString();
            Week = DR["Booking_Week"].ToString();
            Name = DR["Booking_Name"].ToString();
            Username = DR["Booking_Username"].ToString();
            Email = DR["Booking_Email"].ToString();
            Number = Convert.ToInt32(DR["Booking_Number"].ToString());
            Date = Convert.ToDateTime(DR["Booking_Date"].ToString());
            AdditionalInfo = DR["Booking_AdditionalInfo"].ToString();
            GroupMembers = DR["Booking_GroupMember"].ToString();
            Workshop = Convert.ToBoolean(DR["Booking_Workshop"].ToString());
            Deleted = Convert.ToBoolean(DR["Booking_Deleted"].ToString());
            //Deleted = false;
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Username, Booking_Email, Booking_Number, Booking_Date, Booking_AdditionalInfo, Booking_GroupMember, Booking_Workshop, Booking_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Username + "','" + Email + "'," + Number + ",'" + Date + "','" + ClassUseful.FormatStringForDB(AdditionalInfo) + "','" + GroupMembers + "'," + Workshop.GetHashCode() + ",0) SELECT @@IDENTITY;";
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
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Username, Booking_Email, Booking_Number, Booking_Date, Booking_AdditionalInfo, Booking_GroupMember, Booking_Workshop, Booking_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Username + "','" + Email + "'," + Number + ",'" + Date + "','" + ClassUseful.FormatStringForDB(AdditionalInfo) + "','" + GroupMembers + "'," + Workshop.GetHashCode() + ",0) SELECT @@IDENTITY;";
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string Query = "UPDATE Bookings SET Booking_Title = '" + ClassUseful.FormatStringForDB(Title) + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Username = '" + Username + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_AdditionalInfo = '" + ClassUseful.FormatStringForDB(AdditionalInfo) + "', Booking_GroupMember = '" + GroupMembers + "', Booking_Workshop = " + Workshop.GetHashCode() + ", Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
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

        public bool Save(SqlTransaction Trans, ClassConnection TransConn)
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string Query = "UPDATE Bookings SET Booking_Title = '" + ClassUseful.FormatStringForDB(Title) + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Username = '" + Username + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_AdditionalInfo = '" + ClassUseful.FormatStringForDB(AdditionalInfo) + "', Booking_GroupMember = '" + GroupMembers + "', Booking_Workshop = " + Workshop.GetHashCode() + ", Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query,Trans,TransConn);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public static string GenerateBookingsList(string UserID)
        {
            DataSet DS = GenerateBookingsDS(UserID);

            string returnStr = "<ul>";

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                ClassProductionBookings Booking = new ClassProductionBookings(DR);

                returnStr += "<li>" + Booking.Location + "<a href=\"?bid=" + Booking.ID + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/></a><br/>" + ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day) + " - " + Booking.StartTimeOut + "</li>";
            }

            returnStr += "</ul>";

            return returnStr;
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

            email = email.Replace("@BookingDate", ClassGeneral.getAcademicDate(Convert.ToInt16(this.Week),this.Day));

            email = email.Replace("@Location", this.Location);

            email = email.Replace("@TimeIn", this.StartTimeOut);

            email = email.Replace("@TimeOut", this.EndTimeOut);

            email = email.Replace("@AllParticipants", this.GroupMembers);

            email = email.Replace("@WorkshopAccess", this.Workshop.ToString());

            ClassEmail.SendMailMessage("production-scar@hull.ac.uk", this.Email, "", "", "Production Booking Confirmation", email);

            //Find User

            if (ClassGroupMembers.SendAdminEmail(this.Username))
            {
                ClassEmail.SendMailMessage("production-scar@hull.ac.uk", "production-scar@hull.ac.uk", "", "", "Production Booking Confirmation", email);

            }
        }

    }
}
