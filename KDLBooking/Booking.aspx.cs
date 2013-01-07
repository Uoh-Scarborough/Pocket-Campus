using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;
using PocketCampusClasses;

namespace KDLBooking
{
    public partial class Booking : System.Web.UI.Page
    {
        ClassConnection PBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {
            string RID, BID, DID, DELID;

            RID = Request["rid"];
            BID = Request["bid"];
            DID = Request["did"];
            DELID = Request["delid"];

            if (RID == null && BID == null)
            {
                Response.Redirect("Default.aspx");
            }

            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.bookingcurrentconnection = PBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            if (DELID == "1")
            {
                MultiView.ActiveViewIndex = 2;

                ClassKDLBookings Booking = new ClassKDLBookings(Convert.ToInt16(BID));

                BIDHidden.Value = BID;

                DeleteInfolbl.Text = Booking.Title + " on " + Booking.Date.ToShortDateString() + " between " + Booking.StartTimeOut + " - " + Booking.EndTimeOut;
            }
            else
            {
                MultiView.ActiveViewIndex = 0;

                if (!IsPostBack)
                {

                    setupForm();

                }
            }
        }

        public void setupForm()
        {
            string RID;
            int WID, DID, TID, BID;
            int MaxBookings = 1000;
            //int MaxBookings = 100000;

            BID = Convert.ToInt16(Request["bid"]);

            if (BID != 0)
            {
                //Edit Booking
                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Bookinglbl.Text = "Edit";

                ClassKDLBookings Booking = new ClassKDLBookings(BID);

                Nametxt.Text = Booking.Name;

                Roomtxt.Text = Booking.Location;

                WeekHidden.Value = Booking.Week.ToString();
                DayHidden.Value = Booking.Day.ToString();
                EditHidden.Value = BID.ToString();
                Datetxt.Text = ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day);

                StartHidden.Value = (Booking.StartTime).ToString();
                StartTimetxt.Text = ClassGeneral.getTime(Booking.StartTime);

                //Find Next Event

                int NextStart = ClassBooking.FindStartofNext(Booking.Location, ClassUseful.ConvertTo2DigitNumber(Booking.Week), Booking.Day, Booking.StartTime, Booking.Username,BID);

                int WeekBookings = ClassGroupMembers.RemainingUseage(Booking.Username, Booking.Location, ClassUseful.ConvertTo2DigitNumber(Booking.Week), Booking.Day);
                WeekBookings += (Booking.EndTime - Booking.StartTime) * 15;

                //Increment WeekBookings to include the lenght of this booking. 
                int EndTime = ClassBooking.CalculateEndTime(Convert.ToInt32(StartHidden.Value), WeekBookings, NextStart);

                EndTimecmb.Items.Clear();

                for (int i = 0; i <= EndTime - Booking.StartTime - 1; i++)
                {
                    int nTID = Booking.StartTime + i + 1;
                    ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                    EndTimecmb.Items.Add(LI);
                }

                EndTimecmb.Text = Booking.EndTime.ToString();
            }
            else
            {
                //New Booking

                Bookinglbl.Text = "New";

                RID = Request["rid"];
                WID = Convert.ToInt32(Request["wid"]);
                DID = Convert.ToInt32(Request["did"]);
                TID = Convert.ToInt32(Request["tid"]);

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);


                int NextStart = ClassBooking.FindStartofNext(RID, ClassUseful.ConvertTo2DigitNumber(WID), DID, TID, UI.Username,0);

                int WeekBookings = ClassGroupMembers.RemainingUseage(UI.Username, RID, ClassUseful.ConvertTo2DigitNumber(WID),DID);

                int EndTime = ClassBooking.CalculateEndTime(TID, WeekBookings, NextStart);

                if (WeekBookings > 0)
                {

                    Nametxt.Text = UI.DisplayName;

                    Roomtxt.Text = RID;

                    WeekHidden.Value = WID.ToString();
                    DayHidden.Value = DID.ToString();
                    EditHidden.Value = "0";
                    Datetxt.Text = ClassGeneral.getAcademicDate(WID, DID);

                    StartHidden.Value = (TID - 1).ToString();
                    StartTimetxt.Text = ClassGeneral.getTime(TID - 1);

                    //Find Next Event

                    ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
                    ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

                    EndTimecmb.Items.Clear();

                    for (int i = 0; i <= EndTime - TID; i++)
                    {
                        int nTID = TID + i;
                        ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                        EndTimecmb.Items.Add(LI);
                    }

                }
                else
                {

                    //More than allowed

                    Errorlbl.Text = "you have already made your full allowance of bookings for this room for the chosen day. Please try room bookings if you would like to book another room.";

                    MultiView.SetActiveView(ErrorView);

                }

                }

            ReturnLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            ClassKDLBookings Booking;

            //Check Slot Still Free
            
            if (EditHidden.Value != "0")
            {
                Booking = new ClassKDLBookings(Convert.ToInt16(EditHidden.Value));
            }
            else
            {
                Booking = new ClassKDLBookings();
            }

            Booking.Title = Nametxt.Text;

            //Booking.Title = ModuleGrouptxt.Text;

            Booking.Day = Convert.ToInt16(DayHidden.Value.ToString());
            Booking.StartTime = Convert.ToInt16(StartHidden.Value.ToString());
            Booking.EndTime = Convert.ToInt16(EndTimecmb.SelectedValue.ToString());
            Booking.Location = Roomtxt.Text;
            Booking.Week = WeekHidden.Value;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (EditHidden.Value != "0")
            {
                Booking.Save();
                MultiView.ActiveViewIndex = 1;
                SaveEditlbl.Text = "Edited";
                CalendarLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";

                if (ClassGroupMembers.IsAdmin(UI.Username)) 
                {
                    //Modified Booking by Admim

                    Booking.SendEmail(2);

                }
                else
                {
                    //Modified Booking by Booker

                    Booking.SendEmail(1);
                }
            }
            else
            {

                //if ((Booking.Location == "Rehearsal Studio 1" || Booking.Location == "Rehearsal Studio 2") && ((ClassKDLBookings.IsMusicRoomFree(Booking.Day, Booking.Week, Booking.StartTime, Booking.EndTime) || ClassKDLBookings.IsSeminarRoom2Free(Booking.Day, Booking.Week, Booking.StartTime, Booking.EndTime))))
                //{
                    //Music Room is in use, therefore cant use.

                //    Errorlbl.Text = "you can't book the " + Booking.Location + " becuase the Music Room or Seminar Room 2 is in use. Please try another time, or check the Music Room bookings.";

                //    MultiView.SetActiveView(ErrorView);

                //}
                //else
                //{

                    //Check for Overlap

                    ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

                    //RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Booking.Day + " AND Booking_Location LIKE '%" + Booking.Location + "%' AND Booking_Week = '" + Booking.Week + "' AND Booking_StartTime <= " + Booking.EndTime + " AND Booking_EndTime >= " + Booking.StartTime + " AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

                    RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Booking.Day + " AND Booking_Location LIKE '%" + Booking.Location + "%' AND Booking_Week = '" + Booking.Week + "' AND Booking_StartTime < " + Booking.EndTime + " AND Booking_EndTime > " + Booking.StartTime + " AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

                    if (RQ1.numberofresults > 0)
                    {
                        //Booking Overlap

                        Errorlbl.Text = "another booking which overlaps has been made while you have been making this booking. Return to the <a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a> to try again.";

                        MultiView.SetActiveView(ErrorView);

                    }
                    else
                    {


                        Booking.Name = UI.DisplayName;
                        Booking.Username = UI.Username;
                        try
                        {
                            Booking.Number = Convert.ToInt32(UI.StudentID.ToString());
                        }
                        catch
                        {
                            Booking.Number = 0;
                        }
                        Booking.Email = UI.Mail;
                        Booking.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        Booking.AdditionalInfo = "";

                        Booking.Create();
                        MultiView.ActiveViewIndex = 1;
                        SaveEditlbl.Text = "Added";
                        CalendarLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";

                        Booking.SendEmail(0);
                    }

                //}
            }

        }

        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            if (BIDHidden.Value != "0")
            {
                ClassKDLBookings Booking = new ClassKDLBookings(Convert.ToInt16(BIDHidden.Value));

                Booking.Deleted = true;

                Booking.Save();

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                if (UI.InGroup(ClassAppDetails.admingroup))
                {
                    //Admin Delete
                    Booking.SendEmail(4);
                }
                else
                {
                    //Normal Delete
                    Booking.SendEmail(4);
                }

                MultiView.ActiveViewIndex = 3;
            }
        }

    }
}
