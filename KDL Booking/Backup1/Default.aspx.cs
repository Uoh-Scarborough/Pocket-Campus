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
    public partial class _Default : System.Web.UI.Page
    {
        ClassConnection PBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {
            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.pbconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.pbcurrentconnection = PBNC;
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
                    generategrid(Roomcmb.Text, Convert.ToInt16(Weekscmb.Text));
                }
                else
                {
                    Weekscmb.SelectedValue = ClassGeneral.getAcademicWeek().ToString();
                    generategrid(Roomcmb.Text, ClassGeneral.getAcademicWeek());
                }


            }
        }

        private void loadweeks()
        {
            Weekscmb.Items.Clear();

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            //Setup Group In

            Boolean Admin = false;

            if (UI.InGroup("ProductionBooking")) { Admin = true; }
            if (UI.InGroup("StudentServicesAdmin")) { Admin = true; }
            if (UI.InGroup("ProductionBookingAdmin")) { Admin = true; }

            if(!Admin)
            //if (!UI.InGroup("ProductionBooking") || !UI.InGroup("StudentServicesAdmin") ||!UI.InGroup("ProductionBookingAdmin"))
            {
                for (int i = 1; ((i <= ClassGeneral.getAcademicWeek() + 3) && i <= 37); i++)
                {
                    ListItem LI = new ListItem(ClassGeneral.getAcademicWeekDetails(i), i.ToString());
                    Weekscmb.Items.Add(LI);
                }
            }
            else
            {
                for (int i = 1; i <= 37; i++)
                {
                    ListItem LI = new ListItem(ClassGeneral.getAcademicWeekDetails(i), i.ToString());
                    Weekscmb.Items.Add(LI);
                }
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

            Timetable.Rows.Add(Header);

            //Loop 7 Times

            for (int d = 0; d < 7; d++)
            {
                ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

                RQ.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS ID, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime FROM Activities WHERE (Activity_Day = " + d + ") AND (Activity_Location LIKE '%" + Room + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

                ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.pbcurrentconnection);

                RQ1.RunQuery("SELECT Booking_ID_LNK FROM Bookings WHERE (Booking_Day = " + d + ") AND (Booking_Location LIKE '%" + Room + "%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");

                //Merge Together

                ArrayList MergeList = new ArrayList();

                foreach (DataRow DR in RQ.dataset.Tables[0].Rows){
                    Object Obj = new Object();
                    Obj = new ClassActivities(Convert.ToInt16(DR[0]));
                    MergeList.Add(Obj);
                }

                foreach (DataRow DR in RQ1.dataset.Tables[0].Rows)
                {
                    Object Obj = new Object();
                    Obj = new ClassBookings(Convert.ToInt16(DR[0]));
                    MergeList.Add(Obj);
                }

                MergeList.Sort();

                TableRow Day = new TableRow();

                TableRow OriginalDay = Day;

                Day.BorderStyle = BorderStyle.None;

                TableCell DayCell = new TableCell();
                DayCell.BorderColor = System.Drawing.Color.Black;
                DayCell.BorderStyle = BorderStyle.None;
                DayCell.BorderWidth = 1;
                //DayCell.Height= 50;

                if (d == 0)
                {
                    DayCell.Text = "Mon";
                    Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else if (d == 1)
                {
                    DayCell.Text = "Tue";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else if (d == 2)
                {
                    DayCell.Text = "Wed";
                    Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else if (d == 3)
                {
                    DayCell.Text = "Thu";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else if (d == 4)
                {
                    DayCell.Text = "Fri";
                    Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else if (d == 5)
                {
                    DayCell.Text = "Sat";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 30;
                }
                else
                {
                    DayCell.Text = "Sun";
                    Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                    DayCell.Width = 03;
                }

                Day.Cells.Add(DayCell);

                int LastCol = 34;

                int RowCounter = 0;

                foreach (Object Obj in MergeList)

                {
                    int StartTime, EndTime;

                    string OutText;

                    string ts = Obj.GetType().ToString();

                    if (Obj.GetType().ToString() == "ProductionBooking.ClassBookings")
                    {
                        //Booking
                        ClassBookings Booking = (ClassBookings)Obj;
                        StartTime = Booking.StartTime;
                        EndTime = Booking.EndTime;

                        ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                        if(UI.InGroup("ProductionBookingAdmin")){
                            //Always Edit
                            OutText = "<table width=100%><tr valign=top><td colspan=3><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td style=\"text-align:right;\"><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "<td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                        } else {
                            //Not in Group
                            if(Booking.Number.ToString() == UI.StudentID){
                                //Editable
                                OutText = "<table width=100%><tr valign=top><td colspan=3><a href=\"Booking.aspx?bid=" + Booking.ID + "\">Edit</a></td><td style=\"text-align:right;\"><a href=\"Booking.aspx?bid=" + Booking.ID + "&amp;delid=1\">Delete</a></td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "<td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                            } else {
                                //Not Editable
                                OutText = "<table width=100%><tr valign=top><td colspan=3>&nbsp;</td><td style=\"text-align:right;\">&nbsp;</td></tr><tr><td colspan=4>" + Booking.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Booking.StartTimeOut + "<td colspan=2 style=\"text-align:center\">" + Booking.Week + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Booking.EndTimeOut + "</td></tr></table>";
                            }
                        }
                    }
                    else
                    {
                        //Activity
                        ClassActivities Act = (ClassActivities)Obj;

                        StartTime = Act.StartTime;
                        EndTime = Act.EndTime;

                        OutText = "<table width=100%><tr valign=top><td colspan=3>" + Act.ModuleNumber + "</td><td style=\"text-align:right;\">" + Act.Location + "</td></tr><tr><td colspan=4>" + Act.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Act.StartTimeOut + "<td colspan=2 style=\"text-align:center\">" + Act.Pattern + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Act.EndTimeOut + "</td></tr></table>";
                    }
                    
                    
                    for (int c = LastCol; c <= (StartTime); c++)
                    {
                        TableCell Cell = new TableCell();
                        Cell.BorderColor = System.Drawing.Color.Black;
                        Cell.BorderWidth = 1;
                        Cell.Text = Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                        //Cell.Width = 20;
                        Day.Cells.Add(Cell);
                    }

                    TableCell ActCel = new TableCell();
                    ActCel.BorderColor = System.Drawing.Color.Black;
                    ActCel.BorderWidth = 2;

                    ActCel.Font.Size = 7;

                    ActCel.BackColor = System.Drawing.Color.White;

                    ActCel.Text = OutText;

                    ActCel.ColumnSpan = EndTime - StartTime;

                    ActCel.Width = ActCel.ColumnSpan * 10;

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
                        Cell.BorderColor = System.Drawing.Color.Black;
                        Cell.BorderWidth = 1;
                        Cell.Text = "<a href='Booking.aspx?rid=" + Roomcmb.Text + "&amp;wid=" + Weekscmb.SelectedValue + "&amp;did=" + d + "&amp;tid=" + c + "' class='fullcell'>&nbsp;</a>";
                        Cell.Width = 10;
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


    }
}
