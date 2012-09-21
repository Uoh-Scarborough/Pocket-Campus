using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using StandardClasses;
using PocketCampusClasses;

namespace ProductionBooking
{
    public partial class _Default : System.Web.UI.Page
    {
        ClassConnection PBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {
            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.bookingcurrentconnection = PBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            string strClientIP;
            strClientIP = Request.ServerVariables["REMOTE_ADDR"];

            if (ClassAppDetails.kioskip.Contains(strClientIP))
            {
                //sStyleSheet = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                HtmlLink newStyleSheet = new HtmlLink();
                newStyleSheet.Href = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                newStyleSheet.Attributes.Add("type", "text/css");
                newStyleSheet.Attributes.Add("rel", "stylesheet");
                Page.Header.Controls.Add(newStyleSheet);
            }

            if (Request.Browser["IsMobileDevice"] == "true")
            {
                Response.Redirect("http://m.productionbooking.scar.hull.ac.uk");
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

            int Weeks = ClassGroupMembers.BookingRange(UI.Username, Roomcmb.Text, ClassUseful.ConvertTo2DigitNumber(ClassGeneral.getAcademicWeek())) - 1;

            Weeks = Weeks + ClassGeneral.getAcademicWeek();

            if (WeekIndex > Weeks)
            {
                WeekIndex = ClassGeneral.getAcademicWeek();
            }

            for (int i = 1; ((i <= Weeks) && i <= 45); i++)
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

            if (Week <= 9)
            {
                sWeek = "0" + Week.ToString();
            }
            else
            {
                sWeek = Week.ToString();
            }

            Timetable.Rows.Clear();

            Timetable.CellPadding = 0;
            Timetable.CellSpacing = 0;

            //Generate Table Header
            TableRow Header = new TableRow();
            TableRow Footer = new TableRow();
            TableRow TR = new TableRow();

            for (int i = 0; i <= 59; i++)
            {
                TableCell TC = new TableCell();
                TC.Text = "&nbsp;";
                TC.CssClass = "toppad";
                TR.Cells.Add(TC);
            }

            string[] CellsTexts = new string[17];

            CellsTexts[0] = "";
            CellsTexts[1] = "08:15";
            CellsTexts[2] = "09:15";
            CellsTexts[3] = "10:15";
            CellsTexts[4] = "11:15";
            CellsTexts[5] = "12:15";
            CellsTexts[6] = "13:15";
            CellsTexts[7] = "14:15";
            CellsTexts[8] = "15:15";
            CellsTexts[9] = "16:15";
            CellsTexts[10] = "17:15";
            CellsTexts[11] = "18:15";
            CellsTexts[12] = "19:15";
            CellsTexts[13] = "20:15";
            CellsTexts[14] = "21:15";
            CellsTexts[15] = "22:15";
            CellsTexts[16] = "23:15";

            for (int i = 0; i<= 15; i++)
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
                Cell.Width = 20;
                Header.Cells.Add(Cell);
            }

            Timetable.Rows.Add(TR);
            Timetable.Rows.Add(Header);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            //Find Studio Closures for User

            //string Query1 = string.Format("SELECT * FROM Studio_Closure_View WHERE GroupMembers_UserID = '{0}' AND Constraint_room = '{1}' AND Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}'", Context.User.Identity.Name, Room, ClassGeneral.getAcademicDate(Week, 0), ClassGeneral.getAcademicDate(Week, 6));

            string Query1 = string.Format("SELECT * FROM Studio_Closure_View WHERE (GroupMembers_UserID = '{0}' AND Constraint_room = '{1}') AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_BookableStart;", Context.User.Identity.Name, Room, ClassGeneral.getAcademicDate(Week, 0), ClassGeneral.getAcademicDate(Week, 6));

            RQ2.RunQuery(Query1);

            RQ.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS Activity_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks FROM Activities WHERE (Activity_Location LIKE '%" + Room + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

            RQ1.RunQuery("SELECT * FROM Bookings WHERE (Booking_Location LIKE '%" + Room + "%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");

            //Loop 7 Times

            Boolean FullDayClosure = false;

            for (int d = 0; d < 7; d++)
            {

                FullDayClosure = false;

                int DayStartBooking = 33;
                int DayEndBooking = 93;

                DataRow[] dayRestricts = RQ2.dataset.Tables[0].Select("Constraint_StartDate <= '" + ClassGeneral.getAcademicDate(Week, d) + "' AND Constraint_EndDate >= '" + ClassGeneral.getAcademicDate(Week, d) + "'");

                //Merge Together

                ArrayList MergeList = new ArrayList();

                foreach (DataRow DR in dayRestricts)
                {
                    if (!FullDayClosure)
                    {

                        int Start1, End1, Start2, End2;

                        //Start1 = 24;
                        //End2 = 96; 
                        Start1 = DayStartBooking;
                        End2 = DayEndBooking;

                        End1 = Convert.ToInt32(DR["Constraint_BookableStart"]);
                        Start2 = Convert.ToInt32(DR["Constraint_BookableEnd"]);

                        if (End1 == Start2)
                        {
                            //Full Day
                            Object Obj = new Object();
                            Obj = new ClassConstraint(DR, Start1, End2);
                            MergeList.Add(Obj);
                            FullDayClosure = true;
                        }
                        else
                        {

                            if (End1 > DayStartBooking)
                            {

                                Object Obj = new Object();
                                Obj = new ClassConstraint(DR, Start1, End1);
                                MergeList.Add(Obj);

                                DayStartBooking = End1;


                            }

                            if (Start2 < DayEndBooking)
                            {
                                Object Obj2 = new Object();
                                Obj2 = new ClassConstraint(DR, Start2, End2);
                                MergeList.Add(Obj2);

                                DayEndBooking = Start2;

                            }
                        }

                    }
                }

                if (!FullDayClosure)
                {
                    DataRow[] dayActivities = RQ.dataset.Tables[0].Select("Activity_Day = " + d);
                    DataRow[] dayBookings = RQ1.dataset.Tables[0].Select("Booking_Day = " + d);

                    foreach (DataRow DR in dayActivities)
                    {
                        Object Obj = new Object();

                        if (Convert.ToInt32(DR["Activity_StartTime"]) < DayStartBooking)
                        {
                            DR["Activity_StartTime"] = DayStartBooking;
                        }

                        if (Convert.ToInt32(DR["Activity_EndTime"]) > DayEndBooking)
                        {
                            DR["Activity_EndTime"] = DayEndBooking;
                        }

                        if (Convert.ToInt32(DR["Activity_StartTime"]) < Convert.ToInt32(DR["Activity_EndTime"]))
                        {
                            Obj = new ClassActivities(DR);
                            MergeList.Add(Obj);
                        }

                    }

                    foreach (DataRow DR in dayBookings)
                    {
                        Object Obj = new Object();

                        if (Convert.ToInt32(DR["Booking_StartTime"]) < DayStartBooking)
                        {
                            DR["Booking_StartTime"] = DayStartBooking;
                        }

                        if (Convert.ToInt32(DR["Booking_EndTime"]) > DayEndBooking)
                        {
                            DR["Booking_EndTime"] = DayEndBooking;
                        }

                        if (Convert.ToInt32(DR["Booking_StartTime"]) < Convert.ToInt32(DR["Booking_EndTime"]))
                        {
                            Obj = new ClassProductionBookings(DR);
                            MergeList.Add(Obj);
                        }
                    }

                }

                MergeList.Sort();

                TableRow Day = new TableRow();

                TableRow OriginalDay = Day;

                Day.BorderStyle = BorderStyle.None;

                TableCell DayCell = new TableCell();
                DayCell.BorderColor = System.Drawing.Color.Black;
                DayCell.BorderStyle = BorderStyle.None;
                DayCell.BorderWidth = 2;
                DayCell.Width = 10;
                //DayCell.Height= 50;

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

                Day.Cells.Add(DayCell);

                int LastCol = 34;

                int RowCounter = 0;

                foreach (Object Obj in MergeList)

                {
                    int StartTime, EndTime;

                    string OutText;

                    string ts = Obj.GetType().ToString();

                    if (Obj.GetType().ToString() == "PocketCampusClasses.ClassProductionBookings")
                    {
                        //Booking
                        ClassProductionBookings Booking = (ClassProductionBookings)Obj;
                        StartTime = Booking.StartTime;
                        EndTime = Booking.EndTime;

                        ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                        if(ClassGroupMembers.IsAdmin(UI.Username)){
                            //Always Edit
                            OutText = "<table width=100%><tr valign=top><td colspan=3><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td style=\"text-align:right;\"><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "</td><td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                        } else {
                            //Not in Group
                            if(Booking.Number.ToString() == UI.StudentID){
                                //Editable
                                OutText = "<table width=100%><tr valign=top><td colspan=3><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td style=\"text-align:right;\"><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "</td><td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                            } else {
                                //Not Editable
                                OutText = "<table width=100%><tr valign=top><td colspan=3>&nbsp;</td><td style=\"text-align:right;\">&nbsp;</td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "</td><td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                            }
                        }
                    }
                    else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassActivities")
                    {
                        //Activity
                        ClassActivities Act = (ClassActivities)Obj;

                        StartTime = Act.StartTime;
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
                        Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                        Day.Cells.Add(Cell);
                    }

                    TableCell ActCel = new TableCell();

                    ActCel.Text = OutText;

                    ActCel.ColumnSpan = EndTime - StartTime;

                    ActCel.CssClass = "bookingcell";

                    Day.Cells.Add(ActCel);

                    RowCounter += 1;

                    LastCol = EndTime + 1;

                }

                //Finish Off Row
                if (LastCol < 93)
                {
                    //Was 73
                    for (int c = LastCol; c <= 93; c++)
                    {
                        TableCell Cell = new TableCell();
                        Cell.CssClass = "bookable";
                        Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                        Day.Cells.Add(Cell);
                    }
                }

                Timetable.Rows.Add(Day);

            }
        }

        protected void Gocmd_Click(object sender, EventArgs e)
        {
            generategrid(Roomcmb.Text,Convert.ToInt16(Weekscmb.SelectedValue));
        }

        protected void Roomcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadweeks();
        }

    


    }
}
