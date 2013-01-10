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
                    Weekscmb.SelectedValue = ClassUseful.ConvertTo2DigitNumber(WID);
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
            {
                ListItem LI = new ListItem(ClassGeneral.getAcademicWeekDetails(i), ClassUseful.ConvertTo2DigitNumber(i));

                if (WeekIndex == i)
                {
                    LI.Selected = true;
                }
                Weekscmb.Items.Add(LI); 
            }

        }

        public void generategrid(string Room, int Week)
        {
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

            Timetable.CssClass = "timetable";

            Timetable.Dispose();

            Timetable.Rows.Clear();

            while (Timetable.Rows.Count > 1)
            {
                Timetable.Rows.RemoveAt(0);
            }

            Timetable.Rows.Clear();

            while (Timetable.Rows.Count > 1)
            {
                Timetable.Rows.RemoveAt(0);
            }

            Timetable.CellPadding = 0;
            Timetable.CellSpacing = 0;

            //Generate Table Header
            TableHeaderRow Header = new TableHeaderRow();
            Header.CssClass = "headertimes";
            TableRow Footer = new TableRow();
            TableRow TR = new TableRow();

            string[] CellsTexts = new string[18];

            CellsTexts[0] = "";
            CellsTexts[1] = "07:15";
            CellsTexts[2] = "08:15";
            CellsTexts[3] = "09:15";
            CellsTexts[4] = "10:15";
            CellsTexts[5] = "11:15";
            CellsTexts[6] = "12:15";
            CellsTexts[7] = "13:15";
            CellsTexts[8] = "14:15";
            CellsTexts[9] = "15:15";
            CellsTexts[10] = "16:15";
            CellsTexts[11] = "17:15";
            CellsTexts[12] = "18:15";
            CellsTexts[13] = "19:15";
            CellsTexts[14] = "20:15";
            CellsTexts[15] = "21:15";
            CellsTexts[16] = "22:15";
            CellsTexts[17] = "23:15";

            for (int i1 = 0; i1<= 17; i1++)
            {
                TableCell Cell = new TableCell();
                Cell.Font.Size = 8;
                Cell.Text = CellsTexts[i1];

                if (i1 > 0)
                {
                    Cell.ColumnSpan = 4;
                }

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
                    Day.CssClass = "headerday firstrow";

                    TableRow OriginalDay = Day;

                    TableCell DayCell = new TableCell();
                    DayCell.CssClass = "headerdays";

                    if (showallrooms)
                    {

                        switch (d)
                        {
                            case 0:
                                DayCell.Text = "<span>KDL 1</span>";
                                break;
                            case 1:
                                DayCell.Text = "<span>KDL 2</span>";
                                break;
                            case 2:
                                DayCell.Text = "<span>KDL 3</span>";
                                break;
                        }

                    }
                    else
                    {

                        if (d == 0)
                        {
                            DayCell.Text = "<span>Mon</span>";
                        }
                        else if (d == 1)
                        {
                            DayCell.Text = "<span>Tue</span>";
                        }
                        else if (d == 2)
                        {
                            DayCell.Text = "<span>Wed</span>";
                        }
                        else if (d == 3)
                        {
                            DayCell.Text = "<span>Thu</span>";
                        }
                        else if (d == 4)
                        {
                            DayCell.Text = "<span>Fri</span>";
                        }
                        else if (d == 5)
                        {
                            DayCell.Text = "<span>Sat</span>";
                        }
                        else
                        {
                            DayCell.Text = "<span>Sun</span>";
                        }

                    }

                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;

                    Day.Cells.Add(DayCell);

                    int LastCol = 30;
                    int lastActivityEnd = LastCol-1;

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

                            //Edit and Delete Buttons
                            String EditButton = "<a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a>";
                            String DeleteButton = "<a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a>";

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
                            { 
                                //Always Edit
                                OutText = ClassBooking.ActivityTable(EditButton, DeleteButton, Booking.Title, Booking.StartTime, Booking.EndTime, "", lastActivityEnd, Booking.Location, Booking.Week, Booking.Day);
                                lastActivityEnd = Booking.EndTime;
                            }
                            else
                            {
                                //Not in Group
                                if (Booking.Number.ToString() == UI.StudentID)
                                {
                                    //Editable
                                    OutText = ClassBooking.ActivityTable(EditButton, DeleteButton, Booking.Title, Booking.StartTime, Booking.EndTime, "", lastActivityEnd, Booking.Location, Booking.Week, Booking.Day);
                                    lastActivityEnd = Booking.EndTime;
                                }
                                else
                                {
                                    //Not Editable
                                    OutText = ClassBooking.ActivityTable("", "", "Room Booked", Booking.StartTime, Booking.EndTime, "", lastActivityEnd, Booking.Location, Booking.Week, Booking.Day);
                                    lastActivityEnd = Booking.EndTime;
                                }
                            }
                        }
                        else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassActivities")
                        {
                            //Activity
                            ClassActivities Act = (ClassActivities)Obj;

                            StartTime = Act.StartTime;

                            //Check if closure
                            if (Act.Title == "Lecture In Progress")
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

                            OutText = ClassBooking.ActivityTable(Act.ModuleNumber, "", Act.Title, Act.StartTime, Act.EndTime, "", lastActivityEnd, Room,Weekscmb.SelectedValue,d);
                            lastActivityEnd = Act.EndTime;
                       
                        }
                        else
                        {
                            //Studio Closure

                            ClassConstraint Const = (ClassConstraint)Obj;

                            StartTime = Const.BookableStart;
                            EndTime = Const.BookableEnd;

                            OutText = ClassBooking.ActivityTable("", "", Const.Title, Const.BookableStart,Const.BookableEnd, "", lastActivityEnd, Room, Weekscmb.SelectedValue,d);
                            lastActivityEnd = Const.BookableEnd;
                        }


                        for (int c = LastCol; c <= (StartTime); c++)
                        {
                            TableCell Cell = new TableCell();
                            Cell.CssClass = "blankcell";
                            if (showallrooms)
                            {
                                Cell.Text = ClassBooking.BookableCell(Room,Weekscmb.SelectedValue,Dayint,c);
                            }
                            else
                            {
                                Cell.Text = ClassBooking.BookableCell(Roomcmb.Text,Weekscmb.SelectedValue,d,c);
                            }
                            Day.Cells.Add(Cell);
                        }

                        if (StartTime != EndTime)
                        {
                            TableCell ActCel = new TableCell();

                            ActCel.Text = OutText;

                            ActCel.ColumnSpan = EndTime - StartTime;

                            ActCel.CssClass = "actcell spancols " + ActCel.ColumnSpan;

                            Day.Cells.Add(ActCel);

                            
                        }

                        RowCounter += 1;

                        LastCol = EndTime + 1;

                        MergeListCounter += 1;
                    }

                    //Finish Off Row
                    if (LastCol < 97)
                    {

                        for (int c = LastCol; c <= 96; c++)
                        {
                            TableCell Cell = new TableCell();
                            Cell.CssClass = "blankcell";
                            if (showallrooms)
                            {
                                Cell.Text = ClassBooking.BookableCell(Room,Weekscmb.SelectedValue,Dayint,c);
                            }
                            else
                            {
                                Cell.Text = ClassBooking.BookableCell(Roomcmb.Text,Weekscmb.SelectedValue,d,c);
                            }

                            if (c == 96){
                                Cell.CssClass = "lastcell";
                                Cell.Text += ClassBooking.FreeSpaceTable(Room, Weekscmb.SelectedValue,d, lastActivityEnd, 96);

                            }

                            Day.Cells.Add(Cell);
                        }
                    }

                    Timetable.Rows.Add(Day);

                }

            }
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

            generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.SelectedValue));

        }

        protected void Weekscmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.SelectedValue));
        }

        protected void Dayscmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.SelectedValue));
        }

        protected void Dayscmb_SelectedIndexChanged1(object sender, EventArgs e)
        {
            generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.SelectedValue));
        }

    }
}
