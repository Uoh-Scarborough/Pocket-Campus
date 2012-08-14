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
using System.Collections;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassStudioBookings : ClassBooking
    {

        private string c_Telephone, c_RegNo;

        public string Telephone
        {
            get { return c_Telephone.Trim(); }
            set { c_Telephone = value.Trim(); }
        }

        public string RegNo
        {
            get { return c_RegNo; }
            set { c_RegNo = value; }
        }

        public ClassStudioBookings()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassStudioBookings(int ID)
        {
            //Initialise New Class

            c_ID = ID;
            string Query = "SELECT * From Bookings WHERE Booking_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Day = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString());
            StartTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString());
            EndTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString());
            Location = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            Week = RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString();
            Name = RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString();
            Username = RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString();
            Email = RQ.dataset.Tables[0].Rows[0].ItemArray[9].ToString();
            Number = Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[10].ToString());
            Date = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[11].ToString());
            Telephone = RQ.dataset.Tables[0].Rows[0].ItemArray[12].ToString();
            GroupMembers = RQ.dataset.Tables[0].Rows[0].ItemArray[13].ToString();
            RegNo = RQ.dataset.Tables[0].Rows[0].ItemArray[14].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[15].ToString());
            //Deleted = false;
        }

        public ClassStudioBookings(DataRow DR)
        {
            //Initialise New Class

            c_ID = Convert.ToInt32(DR["Booking_ID_LNK"].ToString());
            Title = DR["Booking_Title"].ToString();
            Day = Convert.ToInt16(DR["Booking_Day"].ToString());
            StartTime = Convert.ToInt16(DR["Booking_StartTime"].ToString());
            EndTime = Convert.ToInt16(DR["Booking_EndTime"].ToString());
            Location = DR["Booking_Location"].ToString();
            Week = DR["Booking_Week"].ToString();
            Name = DR["Booking_Name"].ToString();
            Username = DR["Booking_Username"].ToString();
            Email = DR["Booking_Email"].ToString();
            Number = Convert.ToInt32(DR["Booking_Number"].ToString());
            Date = Convert.ToDateTime(DR["Booking_Date"].ToString());
            Telephone = DR["Booking_Telephone"].ToString();
            GroupMembers = DR["Booking_GroupMember"].ToString();
            RegNo = DR["Booking_RegNo"].ToString();
            Deleted = Convert.ToBoolean(DR["Booking_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Username, Booking_Email, Booking_Number, Booking_Date, Booking_Telephone, Booking_GroupMember, Booking_RegNo, Booking_Deleted) VALUES ('" + FormattedTitle + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Username + "','" + Email + "'," + Number + ",'" + Date + "','" + Telephone + "','" + GroupMembers + "','" + RegNo + "',0) SELECT @@IDENTITY;";
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

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string Query = "INSERT INTO Bookings (Booking_Title, Booking_Day, Booking_StartTime, Booking_EndTime, Booking_Location, Booking_Week, Booking_Name, Booking_Username, Booking_Email, Booking_Number, Booking_Date, Booking_Telephone, Booking_GroupMember, Booking_RegNo, Booking_Deleted) VALUES ('" + FormattedTitle + "', " + Day + ", " + StartTime + ", " + EndTime + ", '" + Location + "', '" + Week + "','" + Name + "','" + Username + "','" + Email + "'," + Number + ",'" + Date + "','" + Telephone + "','" + GroupMembers + "','" + RegNo + "',0) SELECT @@IDENTITY;";
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

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string Query = "UPDATE Bookings SET Booking_Title = '" + FormattedTitle + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Username = '" + Username + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_Telephone = '" + Telephone + "', Booking_GroupMember = '" + GroupMembers + "', Booking_RegNo = '" + RegNo + "', Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;

            string FormattedTitle = Title;

            FormattedTitle = FormattedTitle.Replace("'", "&#39;");
            FormattedTitle = FormattedTitle.Replace("\"", "&#34;");

            string Query = "UPDATE Bookings SET Booking_Title = '" + FormattedTitle + "', Booking_Day = " + Day + ", Booking_StartTime = " + StartTime + ", Booking_EndTime = " + EndTime + ", Booking_Location = '" + Location + "', Booking_Week = '" + Week + "', Booking_Name = '" + Name + "', Booking_Username = '" + Username + "', Booking_Email = '" + Email + "', Booking_Number = '" + Number + "', Booking_Date = '" + Date + "', Booking_Telephone = '" + Telephone + "', Booking_GroupMember = '" + GroupMembers + "', Booking_RegNo = '" + RegNo + "', Booking_Deleted = " + Deleted.GetHashCode() + " WHERE Booking_ID_LNK = " + ID + ";";
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

            email = email.Replace("@RegNo", this.RegNo);

            ClassEmail.SendMailMessage("studio-scar@hull.ac.uk", this.Email, "", "", "Studio Booking Confirmation", email);
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

        public static DataSet GenerateBookingsDS(string UserID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            RQ.RunQuery("SELECT Booking_ID_LNK FROM Bookings WHERE Booking_Username = '" + UserID + "' AND Booking_Deleted = 0 AND Booking_Week >= " + ClassGeneral.getAcademicWeek() + " AND Booking_Day >= " + ClassGeneral.getWeekDay() + " ;");

            return RQ.dataset;
        }

        public static string GenerateBookingsList(string UserID)
        {
            DataSet DS = GenerateBookingsDS(UserID);

            string returnStr = "<ul>";

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                ClassStudioBookings Booking = new ClassStudioBookings(Convert.ToInt16(DR[0]));

                returnStr += "<li>" + Booking.Location + "<a href=\"?bid=" + Booking.ID + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/></a><br/>" + ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day) + " - " + Booking.StartTimeOut + "</li>";
            }

            returnStr += "</ul>";

            return returnStr;
        }

        public static bool IsMusicRoomFree(int Day, string WID, int STID, int ETID)
        {

            bool Overlap = false;

            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);


            RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Day + " AND Booking_Location LIKE '%Music Room%' AND Booking_Week = '" + WID + "' AND ((Booking_StartTime >= " + STID + " AND Booking_StartTime <= " + ETID + ") OR (Booking_EndTime >= " + STID + " AND Booking_EndTime <= " + ETID + ")) AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

            //RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Day + " AND Booking_Location LIKE '%Music Room%' AND Booking_Week = '" + WID + "' AND Booking_StartTime < " + ETID + " AND Booking_EndTime > " + STID + " AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

            if (RQ1.numberofresults > 0)
            {
                Overlap = true;
            }

            RQ2.RunQuery("SELECT Activity_Title FROM Activities WHERE Activity_Day = " + Day + " AND Activity_Location LIKE '%Music Room%' AND Activity_Weeks LIKE '%" + WID + "%' AND ((Activity_StartTime >= " + STID + " OR Activity_StartTime <= " + ETID + ") OR (Activity_EndTime >= " + STID + " AND Activity_EndTime <= " + ETID + ")) AND Activity_Deleted = 0 ORDER BY Activity_StartTime;");

            if (RQ2.numberofresults > 0)
            {
                Overlap = true;
            }

            return Overlap;
        }

        public static bool IsSeminarRoom2Free(int Day, string WID, int STID, int ETID)
        {

            bool Overlap = false;

            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Day + " AND Booking_Location LIKE '%Seminar Room 2%' AND Booking_Week = '" + WID + "' AND ((Booking_StartTime >= " + STID + " AND Booking_StartTime <= " + ETID + ") OR (Booking_EndTime >= " + STID + " AND Booking_EndTime <= " + ETID + ")) AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

            if (RQ1.numberofresults > 0)
            {
                Overlap = true;
            }

            RQ2.RunQuery("SELECT Activity_Title FROM Activities WHERE Activity_Day = " + Day + " AND Activity_Location LIKE '%Seminar Room 2%' AND Activity_Weeks LIKE '%" + WID + "%' AND ((Activity_StartTime >= " + STID + " AND Activity_StartTime <= " + ETID + ") OR (Activity_EndTime >= " + STID + " AND Activity_EndTime <= " + ETID + ")) AND Activity_Deleted = 0 ORDER BY Activity_StartTime;");

            if (RQ2.numberofresults > 0)
            {
                Overlap = true;
            }

            return Overlap;
        }
    }
}
