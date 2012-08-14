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

namespace ProductionBooking
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

            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.pbconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.pbcurrentconnection = PBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            if (DELID == "1")
            {
                MultiView.ActiveViewIndex = 2;

                ClassBookings Booking = new ClassBookings(Convert.ToInt16(BID));

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

            BID = Convert.ToInt16(Request["bid"]);

            if (BID != 0)
            {
                //Edit Booking
                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Bookinglbl.Text = "Edit";

                ClassBookings Booking = new ClassBookings(BID);

                Nametxt.Text = Booking.Name;

                Roomtxt.Text = Booking.Location;

                WeekHidden.Value = Booking.Week.ToString();
                DayHidden.Value = Booking.Day.ToString();
                EditHidden.Value = BID.ToString();
                Datetxt.Text = ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day);

                StartHidden.Value = (Booking.StartTime).ToString();
                StartTimetxt.Text = ClassGeneral.getTime(Booking.StartTime);

                //Find Next Event

                ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

                int EndTime;

                string sWID;

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
                }

                EndTimecmb.Items.Clear();

                for (int i = 0; i <= EndTime - Booking.StartTime - 1; i++)
                {
                    int nTID = Booking.StartTime + i + 1;
                    ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                    EndTimecmb.Items.Add(LI);
                }

                EndTimecmb.Text = Booking.EndTime.ToString();

                ModuleGrouptxt.Text = Booking.Title;

                //AdditionalInfotxt.Text = Booking.AdditionalInfo;

                GroupMemberstxt.Text = Booking.GroupMembers;
            }
            else
            {
                //New Booking

                Bookinglbl.Text = "New";

                RID = Request["rid"];
                WID = Convert.ToInt32(Request["wid"]);

                string sWID;

                if (WID <= 9)
                {
                    sWID = "0" + WID.ToString();
                }
                else
                {
                    sWID = WID.ToString();
                }
                DID = Convert.ToInt32(Request["did"]);
                TID = Convert.ToInt32(Request["tid"]);

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

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
                ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.pbcurrentconnection);

                int EndTime;

                RQ.RunQuery("SELECT Activity_StartTime FROM Activities WHERE Activity_Day = " + DID + " AND Activity_Location LIKE '%" + RID + "%' AND Activity_Weeks LIKE '%" + sWID + "%' AND Activity_StartTime >= " + TID + " ORDER BY Activity_StartTime;");
                
                RQ1.RunQuery("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = " + DID + " AND Booking_Location LIKE '%" + RID + "%' AND Booking_Week LIKE '%" + WID + "%' AND Booking_StartTime >= " + TID + " ORDER BY Booking_StartTime;");

                if (RQ.numberofresults > 0 && RQ1.numberofresults > 0)
                {
                    //Both are there
                    int eT1, eT2;

                    eT1 = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                    eT2 = Convert.ToInt16(RQ1.dataset.Tables[0].Rows[0].ItemArray[0]);

                    if (eT1 < eT2)
                    {
                        EndTime = eT1;
                    }
                    else
                    {
                        EndTime = eT2;
                    }
                }
                else if (RQ.numberofresults > 0)
                {
                    EndTime = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                }
                else if (RQ1.numberofresults > 0)
                {
                    EndTime = Convert.ToInt16(RQ1.dataset.Tables[0].Rows[0].ItemArray[0]);
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
                    if ((EndTime - Convert.ToInt16(StartHidden.Value)) > 8) {
                        EndTime = Convert.ToInt16(StartHidden.Value) + 8;
                    }
                }

                EndTimecmb.Items.Clear();

                for (int i = 0; i <= EndTime - TID; i++)
                {
                    int nTID = TID + i;
                    ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                    EndTimecmb.Items.Add(LI);
                }
            }

            ReturnLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            ClassBookings Booking;

            if (EditHidden.Value != "0")
            {
                Booking = new ClassBookings(Convert.ToInt16(EditHidden.Value));
            }
            else
            {
                Booking = new ClassBookings();
            }
            

            Booking.Title = ModuleGrouptxt.Text;

            Booking.Day = Convert.ToInt16(DayHidden.Value.ToString());
            Booking.StartTime = Convert.ToInt16(StartHidden.Value.ToString());
            Booking.EndTime = Convert.ToInt16(EndTimecmb.SelectedValue.ToString());
            Booking.Location = Roomtxt.Text;
            Booking.Week = WeekHidden.Value;

            Booking.AdditionalInfo = "";
            Booking.GroupMembers = GroupMemberstxt.Text;
            Booking.Workshop = Workshopchk.Checked;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (EditHidden.Value != "0")
            {
                Booking.Save();
                MultiView.ActiveViewIndex = 1;
                SaveEditlbl.Text = "Edited";
                CalendarLinklbl.Text = "<a href='default.aspx?rid=" + Roomtxt.Text + "&amp;week=" + WeekHidden.Value + "'>Calendar</a>";

                if (UI.InGroup(ClassAppDetails.admingroup))
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
                Booking.Name = UI.DisplayName;
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

        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            if (BIDHidden.Value != "0")
            {
                ClassBookings Booking = new ClassBookings(Convert.ToInt16(BIDHidden.Value));

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
