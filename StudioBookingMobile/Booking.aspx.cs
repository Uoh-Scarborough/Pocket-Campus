using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using PocketCampusClasses;

namespace StudioBookingMobile
{
    public partial class Booking : System.Web.UI.Page
    {

        ClassConnection SBNC, TTNC;

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

            SBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.bookingcurrentconnection = SBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            if (DELID == "1")
            {
                MultiView.ActiveViewIndex = 2;

                ClassStudioBookings Booking = new ClassStudioBookings(Convert.ToInt16(BID));

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


        protected void setupForm()
        {
            string RID;
            int WID, DID, TID, BID;

            BID = Convert.ToInt16(Request["bid"]);

            if (BID != 0)
            {
                //Edit Booking
                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Bookinglbl.Text = "Edit";

                ClassStudioBookings Booking = new ClassStudioBookings(BID);

                Nametxt.Text = Booking.Name;

                Roomtxt.Text = Booking.Location;

                WeekHidden.Value = Booking.Week.ToString();
                DayHidden.Value = Booking.Day.ToString();
                EditHidden.Value = BID.ToString();
                Datetxt.Text = ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day);

                StartHidden.Value = (Booking.StartTime).ToString();
                StartTimetxt.Text = ClassGeneral.getTime(Booking.StartTime);

                PhoneNumber.Text = Booking.Telephone;

                ArrayList Controls = ControlstoArray();

                Array Names = Booking.GroupMembers.Split(',');

                try
                {

                    for (int i = 0; i <= Names.Length - 1; i++)
                    {

                        ArrayList Control = (ArrayList)Controls[i];

                        TextBox Name = (TextBox)Control[0];
                        DropDownList Type = (DropDownList)Control[1];

                        String nameString = Names.GetValue(i).ToString().Trim();

                        Name.Text = nameString.Substring(0, nameString.LastIndexOf('('));

                        String tmp = nameString.Substring(nameString.LastIndexOf('(') + 1, nameString.LastIndexOf(')') - nameString.LastIndexOf('(') - 1);

                        if (nameString.Substring(nameString.LastIndexOf('(') + 1, nameString.LastIndexOf(')') - nameString.LastIndexOf('(') - 1) == "Student")
                        {
                            Type.SelectedIndex = 0;
                        }
                        else
                        {
                            Type.SelectedIndex = 1;
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Do Nothing
                }

                VehicleRegtxt.Text = Booking.RegNo;

                //Find Next Event

                int NextStart = ClassBooking.FindStartofNext(Booking.Location, ClassUseful.ConvertTo2DigitNumber(Booking.Week), Booking.Day, Booking.StartTime, Booking.Username, BID);

                int WeekBookings = ClassGroupMembers.RemainingUseage(Booking.Username, Booking.Location, ClassUseful.ConvertTo2DigitNumber(Booking.Week),Booking.Day);

                int EndTime = ClassBooking.CalculateEndTime(Convert.ToInt32(StartHidden.Value), WeekBookings, NextStart);

                ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

                //int EndTime;

                /*string sWID;

                if (Convert.ToInt16(Booking.Week) <= 9)
                {
                    sWID = "0" + Booking.Week;
                }
                else
                {
                    sWID = Booking.Week;
                }

                RQ.RunQuery("SELECT Activity_StartTime FROM Activities WHERE Activity_Day = " + Booking.Day + " AND Activity_Location LIKE '%" + Booking.Location + "%' AND Activity_Weeks LIKE '%" + sWID + "%' AND Activity_StartTime >= " + Booking.StartTime + " AND Activity_Deleted = 0 ORDER BY Activity_StartTime;");

                if (RQ.numberofresults > 0)
                {
                    EndTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                }
                else
                {
                    EndTime = 93;
                }

                //Modify End Time

                Boolean Admin = false;

                if (UI.InGroup("ProductionBooking")) { Admin = true; }
                if (UI.InGroup("StudentServicesAdmin")) { Admin = true; }
                if (UI.InGroup("ProductionBookingAdmin")) { Admin = true; }


                if (!Admin)
                {
                    if ((EndTime - Convert.ToInt16(StartHidden.Value)) > 8)
                    {
                        EndTime = Convert.ToInt16(StartHidden.Value) + 8;
                    }
                    MaxBookings = 8;
                }*/

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

                            Errorlbl.Text = "you have already made your full allowance of bookings for this room for the chossen week. Please try another room.";

                            MultiView.SetActiveView(ErrorView);

                        }

                   }

                            ReturnLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";


            /*           Errorlbl.Text = "you have already made you full allowance of bookings for this room for the chossen date. Please try another room.";

                       MultiView.SetActiveView(ErrorView);

                   }

               }
               else
               {

                   //Restricted booking

                   Errorlbl.Text = "your access to this room is restricted. Please try another room.";

                   MultiView.SetActiveView(ErrorView);
               }
           } 
       }

       ReturnLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Return to Calendar</a>";
             * 
             * */
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            ClassStudioBookings Booking;

            //Check Slot Free

            if (EditHidden.Value != "0")
            {
                Booking = new ClassStudioBookings(Convert.ToInt16(EditHidden.Value));
            }
            else
            {
                Booking = new ClassStudioBookings();
            }

            Booking.Title = Nametxt.Text;
            Booking.Day = Convert.ToInt16(DayHidden.Value.ToString());
            Booking.StartTime = Convert.ToInt16(StartHidden.Value.ToString());
            Booking.EndTime = Convert.ToInt16(EndTimecmb.SelectedValue.ToString());
            Booking.Location = Roomtxt.Text;
            Booking.Week = WeekHidden.Value;

            Booking.Telephone = PhoneNumber.Text;

            //Sort out Room Members

            ArrayList Controls = ControlstoArray();

            String Memberstr = "";

            foreach (ArrayList Control in Controls)
            {
                TextBox Name = (TextBox)Control[0];
                DropDownList Typecmb = (DropDownList)Control[1];

                if (Name.Text != "")
                {
                    Memberstr += ", " + Name.Text.Trim() + " (" + Typecmb.SelectedItem.Text + ")";
                }
            }

            if (Memberstr.Length > 2)
            {
                Booking.GroupMembers = Memberstr.Substring(2);
            }
            else
            {
                Booking.GroupMembers = "";
            }

            Booking.RegNo = VehicleRegtxt.Text;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (EditHidden.Value != "0")
            {
                Booking.Save();
                MultiView.ActiveViewIndex = 1;
                SaveEditlbl.Text = "Edited";
                CalendarLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Return to Calendar</a>";

                if (UI.InGroup(ClassAppDetails.admingroup))
                {
                    //Modified Booking by Admin

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

                if ((Booking.Location == "Rehearsal Studio 1" || Booking.Location == "Rehearsal Studio 2") && ((ClassStudioBookings.IsMusicRoomFree(Booking.Day, Booking.Week, Booking.StartTime, Booking.EndTime) || ClassStudioBookings.IsSeminarRoom2Free(Booking.Day,Booking.Week,Booking.StartTime,Booking.EndTime))))
                {
                    //Music Room is in use, therefore cant use.

                    Errorlbl.Text = "you can't book the " + Booking.Location + " becuase the Music Room or Seminar Room 2 is in use. Please try another time, or check the Music Room bookings.";

                    MultiView.SetActiveView(ErrorView);

                }
                else
                {

                    //Check for Overlap

                    ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

                    RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + Booking.Day + " AND Booking_Location LIKE '%" + Booking.Location + "%' AND Booking_Week = '" + Booking.Week + "' AND Booking_StartTime = " + Booking.EndTime + " AND Booking_EndTime = " + Booking.StartTime + " AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

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

                        Booking.Create();
                        MultiView.ActiveViewIndex = 1;
                        SaveEditlbl.Text = "Added";
                        CalendarLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";

                        Booking.SendEmail(0);

                    }

                }
            }
        }

        private ArrayList ControlstoArray()
        {
            ArrayList Controls = new ArrayList();

            ArrayList Member1 = new ArrayList();
            Member1.Add(Member1Nametxt);
            Member1.Add(Member1Typecmb);

            Controls.Add(Member1);

            ArrayList Member2 = new ArrayList();
            Member2.Add(Member2Nametxt);
            Member2.Add(Member2Typecmb);

            Controls.Add(Member2);

            ArrayList Member3 = new ArrayList();
            Member3.Add(Member3Nametxt);
            Member3.Add(Member3Typecmb);

            Controls.Add(Member3);

            ArrayList Member4 = new ArrayList();
            Member4.Add(Member4Nametxt);
            Member4.Add(Member4Typecmb);

            Controls.Add(Member4);

            ArrayList Member5 = new ArrayList();
            Member5.Add(Member5Nametxt);
            Member5.Add(Member5Typecmb);

            Controls.Add(Member5);

            ArrayList Member6 = new ArrayList();
            Member6.Add(Member6Nametxt);
            Member6.Add(Member6Typecmb);

            Controls.Add(Member6);

            ArrayList Member7 = new ArrayList();
            Member7.Add(Member7Nametxt);
            Member7.Add(Member7Typecmb);

            Controls.Add(Member7);

            ArrayList Member8 = new ArrayList();
            Member8.Add(Member8Nametxt);
            Member8.Add(Member8Typecmb);

            Controls.Add(Member8);

            return Controls;
        }

        protected void Deletedcmb_Click(object sender, ImageClickEventArgs e)
        {
            if (BIDHidden.Value != "0")
            {
                ClassStudioBookings Booking = new ClassStudioBookings(Convert.ToInt16(BIDHidden.Value));

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