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
    public partial class Default : System.Web.UI.Page
    {
        ClassConnection PBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {
            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.bookingcurrentconnection = PBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            if(Request.Browser["IsMobileDevice"] == "true"){
                Response.Redirect("http://m.studiobooking.scar.hull.ac.uk");
            }

            if (!IsPostBack)
            {
                loadweeks();

                string RID, WID;

                RID = Request["rid"];
                WID = Request["week"];

                Roomcmb.Text = RID;
                Weekscmb.Text = WID;

                if (WID != null)
                {
                    generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.Text));
                }
                else
                {

                    Weekscmb.SelectedValue = ClassUseful.ConvertTo2DigitNumber(ClassGeneral.getAcademicWeek());
                    generategrid(Roomcmb.Text, ClassGeneral.getAcademicWeek());
                }


            }
        }

        private void loadweeks()
        {
            int WeekIndex = Weekscmb.SelectedIndex + 1;

            Weekscmb.Items.Clear();

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            int Weeks = ClassGroupMembers.BookingRange(UI.Username, Roomcmb.Text,ClassUseful.ConvertTo2DigitNumber(ClassGeneral.getAcademicWeek())) - 1;

            Weeks = Weeks + ClassGeneral.getAcademicWeek();

            if (WeekIndex > Weeks)
            {
                WeekIndex = ClassGeneral.getAcademicWeek();
            }


            for (int i = 1; ((i <= Weeks) && i <= 56); i++)
            //for(int i = 1; ((i<= Weeks) && i<=45);i++)
            {
                ListItem LI = new ListItem(ClassGeneral.getAcademicWeekDetails(i), ClassUseful.ConvertTo2DigitNumber(i));

                //int Week = ClassGeneral.getAcademicWeek();

                if (WeekIndex == i)
                {
                    LI.Selected = true;
                }
                Weekscmb.Items.Add(LI); 
            }

        }

        public void generategrid(string Room, int Week)
        {
            Timetable.Dispose();

            string sWeek;
            Boolean showallrooms = false;
            int Dayint = 0;

            if (Room == "All")
            {
                showallrooms = true;
                Dayint = Dayscmb.SelectedIndex;
            }

            if (Week <= 9)
            {
                sWeek = "0" + Week.ToString();
            }
            else
            {
                sWeek = Week.ToString();
            }

            Timetable.Rows.Clear();
            //Timetable.Width = 700;

            Timetable.CellPadding = 0;
            Timetable.CellSpacing = 0;

            //Generate Table Header
            TableRow Header = new TableRow();
            TableRow Footer = new TableRow();
            TableRow TR = new TableRow();

            for (int i = 0; i <= 72; i++)
            {
                TableCell TC = new TableCell();
                TC.Text = "&nbsp;";
                TC.CssClass = "toppad";
                TR.Cells.Add(TC);
            }

            string[] CellsTexts = new string[19];

            CellsTexts[0] = "";
            CellsTexts[1] = "06:15";
            CellsTexts[2] = "07:15";
            CellsTexts[3] = "08:15";
            CellsTexts[4] = "09:15";
            CellsTexts[5] = "10:15";
            CellsTexts[6] = "11:15";
            CellsTexts[7] = "12:15";
            CellsTexts[8] = "13:15";
            CellsTexts[9] = "14:15";
            CellsTexts[10] = "15:15";
            CellsTexts[11] = "16:15";
            CellsTexts[12] = "17:15";
            CellsTexts[13] = "18:15";
            CellsTexts[14] = "19:15";
            CellsTexts[15] = "20:15";
            CellsTexts[16] = "21:15";
            CellsTexts[17] = "22:15";
            CellsTexts[18] = "23:15";

            for (int i = 0; i<= 18; i++)
            {
                TableCell Cell = new TableCell();
                Cell.Font.Size = 8;
                Cell.Text = CellsTexts[i];

                if (i > 0)
                {
                    Cell.ColumnSpan = 4;
                }

                Cell.BorderColor = System.Drawing.Color.Black;
                Cell.BorderStyle = BorderStyle.None;
                Cell.Width = 36;
                Header.Cells.Add(Cell);
            }

            Timetable.Rows.Add(TR);
            Timetable.Rows.Add(Header);
            
            //Loop 7 Times

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            Boolean FullDayClosure = false;

            int loopCounter = 7;

            if (showallrooms)
            {
                loopCounter = 3;
            }

            for (int d = 0; d < loopCounter; d++)
            {

                ArrayList ActivityList;

                if (showallrooms)
                {

                    switch (d)
	                {
		                case 0:
                            Room = "KDL 1";
                            break;
                        case 1:
                            Room = "KDL 2";
                            break;
                        case 2:
                            Room = "KDL 3";
                            break;
	                }

                    ActivityList = ClassKDLBookings.GenerateDaySet(Dayscmb.SelectedIndex, Week, Room, UI.Username, ClassBooking.BookingType.KDLBooking);
      
                }
                else
                {
                    ActivityList = ClassKDLBookings.GenerateDaySet(d, Week, Room, UI.Username, ClassBooking.BookingType.KDLBooking);
                }

                if (!FullDayClosure)
                {

                    TableRow Day = new TableRow();

                    TableRow OriginalDay = Day;

                    Day.BorderStyle = BorderStyle.None;

                    TableCell DayCell = new TableCell();
                    DayCell.BorderColor = System.Drawing.Color.Black;
                    DayCell.BorderStyle = BorderStyle.None;
                    DayCell.BorderWidth = 2;
                    DayCell.Width = 10;
                    //DayCell.Height= 50;

                    if (showallrooms)
                    {

                        switch (d)
                        {
                            case 0:
                                DayCell.Text = "KDL 1";
                                Day.CssClass = "rowone";
                                break;
                            case 1:
                                DayCell.Text = "KDL 2";
                                break;
                            case 2:
                                DayCell.Text = "KDL 3";
                                Day.CssClass = "rowone";
                                break;
                        }

                    }
                    else
                    {

                        if (d == 0)
                        {
                            DayCell.Text = "Mon";
                            Day.CssClass = "rowone";

                        }
                        else if (d == 1)
                        {
                            DayCell.Text = "Tue";
                        }
                        else if (d == 2)
                        {
                            DayCell.Text = "Wed";
                            Day.CssClass = "rowone";
                        }
                        else if (d == 3)
                        {
                            DayCell.Text = "Thu";
                        }
                        else if (d == 4)
                        {
                            DayCell.Text = "Fri";
                            Day.CssClass = "rowone";
                        }
                        else if (d == 5)
                        {
                            DayCell.Text = "Sat";
                        }
                        else
                        {
                            DayCell.Text = "Sun";
                            Day.CssClass = "rowone";
                        }

                    }

                    Day.Cells.Add(DayCell);

                    int LastCol = 26;

                    int RowCounter = 0;

                    int MergeListCounter = 0;

                    foreach (Object Obj in ActivityList)
                    {
                        int StartTime, EndTime;

                        string OutText;

                        string ts = Obj.GetType().ToString();

                        if (Obj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                        {
                            //Booking
                            ClassKDLBookings Booking = (ClassKDLBookings)Obj;
                            StartTime = Booking.StartTime;

                            //Check if closure
                            if (Booking.Title == "Lecture In Progress")
                            {

                                try
                                {

                                    Object nObj = ActivityList[MergeListCounter + 1];

                                    if (nObj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                                    {
                                        ClassKDLBookings nBooking = (ClassKDLBookings)nObj;

                                        if (nBooking.StartTime < Booking.EndTime)
                                        {
                                            Booking.EndTime = nBooking.StartTime;
                                        }
                             
                                    }
                                    else
                                    {
                                        ClassActivities nActivity = (ClassActivities)nObj;

                                        if (nActivity.StartTime < Booking.EndTime)
                                        {
                                            Booking.EndTime = nActivity.StartTime;
                                        }

                                    }

                                }
                                catch
                                {
                                    //Do Nothing
                                }
                            }
                            
                            EndTime = Booking.EndTime;

                            //ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                            if (ClassGroupMembers.IsAdmin(UI.Username))
                            { //UI.InGroup("StudioBookingAdmin")){
                                //Always Edit
                                OutText = "<table class=booking><tr valign=top><td><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td class=cellright><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=2>" + Booking.Title + "</td></tr><tr valign=bottom><td class=bottomleft>" + Booking.StartTimeOut.Trim() + "</td><td class=bottomright>" + Booking.EndTimeOut.Trim() + "</td></tr></table>";
                            }
                            else
                            {
                                //Not in Group
                                if (Booking.Number.ToString() == UI.StudentID)
                                {
                                    //Editable
                                    OutText = "<table class=booking><tr valign=top><td><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td class=cellright><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=2>" + Booking.Title + "</td></tr><tr valign=bottom><td class=bottomleft>" + Booking.StartTimeOut + "</td><td class=bottomright>" + Booking.EndTimeOut + "</td></tr></table>";
                                }
                                else
                                {
                                    //Not Editable
                                    OutText = "<table class=booking><tr valign=top><td>&nbsp;</td><td class=cellright>&nbsp;</td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td class=bottomleft>" + Booking.StartTimeOut.Trim() + "</td><td class=bottomright>" + Booking.EndTimeOut + "</td></tr></table>";
                                }
                            }
                        }
                        else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassActivities")
                        {
                            //Activity
                            ClassActivities Act = (ClassActivities)Obj;

                            StartTime = Act.StartTime;

                            //Check if closure
                            if (Act.Title == "Lecture In Progressd")
                            {
                                try
                                {


                                    Object nObj = ActivityList[MergeListCounter + 1];

                                    if (nObj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                                    {
                                        ClassKDLBookings nBooking = (ClassKDLBookings)nObj;

                                        if (nBooking.StartTime < Act.EndTime)
                                        {
                                            Act.EndTime = nBooking.StartTime;
                                        }

                                    }
                                    else
                                    {
                                        ClassActivities nActivity = (ClassActivities)nObj;

                                        if (nActivity.StartTime < Act.EndTime)
                                        {
                                            Act.EndTime = nActivity.StartTime;
                                        }

                                    }

                                }
                                catch
                                {
                                    //Do Nothing
                                }
                            }

                            EndTime = Act.EndTime;

                            OutText = "<table class=booking><tr valign=top><td>" + Act.ModuleNumber + "</td><td></td></tr><tr><td colspan=2>" + Act.Title + "</td></tr><tr valign=bottom><td class=bottomleft>" + Act.StartTimeOut + "</td><td class=bottomright>" + Act.EndTimeOut + "</td></tr></table>";
                       
                        }
                        else
                        {
                            //Studio Closure

                            ClassConstraint Const = (ClassConstraint)Obj;

                            StartTime = Const.BookableStart;
                            EndTime = Const.BookableEnd;

                            OutText = "<table class=booking><tr valign=top><td></td><td></td></tr><tr><td colspan=2>" + Const.Title + "</td></tr><tr valign=bottom><td class=bottomleft>" + ClassGeneral.getTime(Const.BookableStart) + "</td><td class=bottomright>" + ClassGeneral.getTime(Const.BookableEnd) + "</td></tr></table>";
                        }


                        for (int c = LastCol; c <= (StartTime); c++)
                        {
                            TableCell Cell = new TableCell();
                            Cell.CssClass = "bookable";
                            if (showallrooms)
                            {
                                Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Room + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + Dayint + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                            }
                            else
                            {
                                Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                            }
                            Day.Cells.Add(Cell);
                        }

                        if (StartTime != EndTime)
                        {
                            TableCell ActCel = new TableCell();

                            ActCel.Text = OutText;

                            ActCel.ColumnSpan = EndTime - StartTime;

                            ActCel.CssClass = "bookingcell";

                            Day.Cells.Add(ActCel);

                            
                        }

                        RowCounter += 1;

                        LastCol = EndTime + 1;

                        MergeListCounter += 1;
                    }

                    //Finish Off Row
                    if (LastCol < 97)
                    {
                        //Was 93

                        //Was 73
                        for (int c = LastCol; c <= 97; c++)
                        {
                            TableCell Cell = new TableCell();
                            Cell.CssClass = "bookable";
                            if (showallrooms)
                            {
                                Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Room + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + Dayint + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                            }
                            else
                            {
                                Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                            }
                            Day.Cells.Add(Cell);
                        }
                    }

                    Timetable.Rows.Add(Day);

                }

            }
        }

        protected void Gocmd_Click(object sender, EventArgs e)
        {
            generategrid(Roomcmb.Text,Convert.ToInt16(Weekscmb.SelectedValue));
        }

        protected void Roomcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Roomcmb.SelectedIndex == 3)
            {

                Dayscmb.Visible = true;

            }
            else
            {

                Dayscmb.Visible = false;

                loadweeks();

            }
        }

    }
}
