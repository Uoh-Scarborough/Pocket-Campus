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

namespace Timetables
{
    public partial class _Default : System.Web.UI.Page
    {
        ClassConnection NC;

        

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.ttcurrentconnection = NC;

            int TID = Convert.ToInt32(Request["tid"]);

            if (!IsPostBack)
            {

                loadsubjects();

                loadweeks();

                if (TID != 0)
                {
                    generategrid(TID, 0);
                }
                else
                {
                    NoTimetablelbl.Text = "No Timetable Selected, please select a Timetable from the drop down above.";

                    NoTimetablePanel.Visible = true;

                    WeekPanel.Visible = false;

                    TIDPanel.Visible = false;

                }
            }
            else
            {
                NoTimetablelbl.Text = "";
            }

        }

        public void loadsubjects()
        {
            CourseList.Items.Clear();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQ.RunQuery("SELECT Course_ID_LNK, Course_Name FROM Courses WHERE Course_Deleted = 0 ORDER BY Course_Name");

            foreach(DataRow DR in RQ.dataset.Tables[0].Rows)
            {

                ListItem LI = new ListItem(DR["Course_Name"].ToString(), DR["Course_ID_LNK"].ToString());

                CourseList.Items.Add(LI);
            }

        }

        public void loadweeks()
        {
             WeekList.Items.Clear();

             WeekList.Items.Add(new ListItem("Show All Weeks", "0"));

             for (int i = ClassGeneral.getAcademicWeek(); i <= 52; i++)
            {
                ListItem LI = new ListItem(ClassGeneral.getAcademicWeekDetails(i), i.ToString());
                WeekList.Items.Add(LI);
            }
        
        }

        public void generategrid(int ActID, int ActWeek)
        {
            int Week = ClassGeneral.getAcademicWeek();

            string sWeek = "";

            if (ActWeek <= 9)
            {
                sWeek = "0" + ActWeek;
            }
            else
            {
                sWeek = ActWeek.ToString();
            }

            int EndCol = 76;
            int MasterLastCol = 37;

            //int Week = 5;

            Weeklbl.Text = "We are in Academic Week " + Week;

            NoTimetablePanel.Visible = false;

            WeekPanel.Visible = true;

            Timetable.Dispose();

            Timetable.Rows.Clear();

            while (Timetable.Rows.Count > 1)
            {
                Timetable.Rows.RemoveAt(0);
            }
           
            Timetable.CellPadding = 0;
            Timetable.CellSpacing = 0;

            //Generate Table Header

            TableRow Header = new TableRow();
            TableRow Footer = new TableRow();

            //Loop Through Time

            string[] CellTexts = new string[21];

            ClassReadQuery RQCheck = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQCheck.RunQuery("SELECT Activity_ID_LNK FROM Activities WHERE Activity_Course_ID_LNK = " + ActID + " AND Activity_StartTime <= 48;");

            if (RQCheck.numberofresults == 0)
            {
                //Start = 48 (12:00)
                CellTexts[0] = "";
                CellTexts[1] = "12:00";
                CellTexts[2] = "12:30";
                CellTexts[3] = "13:00";
                CellTexts[4] = "13:30";
                CellTexts[5] = "14:00";
                CellTexts[6] = "14:30";
                CellTexts[7] = "15:00";
                CellTexts[8] = "15:30";
                CellTexts[9] = "16:00";
                CellTexts[10] = "16:30";
                CellTexts[11] = "17:00";
                CellTexts[12] = "17:30";
                CellTexts[13] = "18:00";
                CellTexts[14] = "18:30";
                CellTexts[15] = "19:00";
                CellTexts[16] = "19:30";
                CellTexts[17] = "20:00";
                CellTexts[18] = "20:30";
                CellTexts[19] = "21:00";
                CellTexts[20] = "21:30";

                MasterLastCol = 49;
                EndCol = 88;
            }
            else
            {

                //Start = 36 (9:00)

                CellTexts[0] = "";
                CellTexts[1] = "9:00";
                CellTexts[2] = "9:30";
                CellTexts[3] = "10:00";
                CellTexts[4] = "10:30";
                CellTexts[5] = "11:00";
                CellTexts[6] = "11:30";
                CellTexts[7] = "12:00";
                CellTexts[8] = "12:30";
                CellTexts[9] = "13:00";
                CellTexts[10] = "13:30";
                CellTexts[11] = "14:00";
                CellTexts[12] = "14:30";
                CellTexts[13] = "15:00";
                CellTexts[14] = "15:30";
                CellTexts[15] = "16:00";
                CellTexts[16] = "16:30";
                CellTexts[17] = "17:00";
                CellTexts[18] = "17:30";
                CellTexts[19] = "18:00";
                CellTexts[20] = "18:30";

                MasterLastCol = 37;
                EndCol = 76;

            }

            for (int i = 0; i <= 20; i++)
            {
                TableCell Cell = new TableCell();
                Cell.Font.Size = 8;
                Cell.Text = CellTexts[i];

                if (i > 0)
                {
                    Cell.ColumnSpan = 2;
                }
                Cell.BorderStyle = BorderStyle.None;
                Cell.Width = 20;
                Header.Cells.Add(Cell);
                //Footer.Cells.Add(Cell);
            }

            Timetable.Rows.Add(Header);

            //Read All TT Information
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            if (ActWeek == 0)
            {
                //Show All
                RQ.RunQuery("SELECT * FROM Activity_View WHERE Activity_Course_ID_LNK = " + ActID + " ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC");
            }
            else
            {
                //Show Week Only
                RQ.RunQuery("SELECT * FROM Activity_View WHERE Activity_Course_ID_LNK = " + ActID + " AND Activity_Weeks LIKE '%" + sWeek + "%' ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC");
            }

            //Loop 5 Times
            for (int d = 0; d < 5; d++)
            {
                DataRow[] TTActs = RQ.dataset.Tables[0].Select("Activity_Day = " + d);
               
                TableRow Day = new TableRow();

                TableRow OriginalDay = Day;

                Day.BorderStyle = BorderStyle.None;

                TableCell DayCell = new TableCell();
                DayCell.BorderStyle = BorderStyle.None;
                DayCell.BorderWidth = 1;
                DayCell.BorderColor = System.Drawing.Color.Black;
                //DayCell.Height= 50;

                if (d == 0)
                {
                    DayCell.Text = "Mon";
                    //Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    Day.BackColor = System.Drawing.Color.FromArgb(255, 232, 127);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 1)
                {
                    DayCell.Text = "Tue";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 2)
                {
                    DayCell.Text = "Wed";
                    //Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    Day.BackColor = System.Drawing.Color.FromArgb(255, 232, 127);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 3)
                {
                    DayCell.Text = "Thu";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else
                {
                    DayCell.Text = "Fri";
                    //Day.BackColor = System.Drawing.Color.FromArgb(183, 204, 219);
                    Day.BackColor = System.Drawing.Color.FromArgb(255, 232, 127);
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }

                Day.Cells.Add(DayCell);

                int LastCol = MasterLastCol;

                int RowCounter = 0;

                foreach (DataRow DR in TTActs)
                {
                    ClassActivities Act = new ClassActivities(DR);

                    //Fill in Last Col to Start Col
                    //if (Act.StartTime < LastCol - 1)
                    if (Act.StartTime < LastCol - 1)
                    {
                        //New Row

                        System.Drawing.Color CellCol = Day.BackColor;

                        //Finish Off Row
                        //if (LastCol < 76)
                        if (LastCol < EndCol)
                        {
                            //Was 73
                            //for (int c = LastCol; c <= 76; c++)
                            for (int c = LastCol; c <= EndCol; c++)
                            {
                                TableCell Cell = new TableCell();
                                Cell.BorderWidth = 1;
                                Cell.BorderColor = System.Drawing.Color.Black;
                                Cell.Text = "";
                                Day.Cells.Add(Cell);
                            }
                        }

                        Timetable.Rows.Add(Day);

                        OriginalDay.Cells[0].RowSpan = OriginalDay.Cells[0].RowSpan + 1;

                        if (OriginalDay.Cells[0].RowSpan == 1) { OriginalDay.Cells[0].RowSpan = 2; }

                        Day = new TableRow();

                        Day.BackColor = CellCol;

                        //LastCol = 37;
                        LastCol = MasterLastCol;
                    }

                    for (int c = LastCol; c <= Act.StartTime; c++)
                    {
                        TableCell Cell = new TableCell();
                        Cell.BorderColor = System.Drawing.Color.Black;
                        Cell.BorderWidth = 1;
                        Cell.Text = "";
                        Cell.Width = 20;
                        Day.Cells.Add(Cell);

                    }

                    TableCell ActCel = new TableCell();

                    ActCel.BorderWidth = 2;

                    ActCel.BorderColor = System.Drawing.Color.Black; 

                    if (Act.InWeek(Week.ToString()))
                    {
                        ActCel.BorderColor = System.Drawing.Color.FromArgb(174, 43, 48);
                    }
                    //ActCel.Text = Act.Title + "<br/>" + Act.Staff;

                    ActCel.Font.Size = 7;

                    ActCel.BackColor = System.Drawing.Color.White;

                    //ActCel.Text = "<table width=100%><tr valign=top><td colspan=3>" + Act.OutputStaff + "</td><td style=\"text-align:right;\">" + Act.Location + "</td></tr><tr><td colspan=4>" + Act.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Act.StartTimeOut + "<td colspan=2 style=\"text-align:center\">" + Act.Pattern + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Act.EndTimeOut + "</td></tr></table>";

                    ActCel.Text = "<table width=100%><tr valign=top><td colspan=3>" + Act.ModuleNumber + "</td><td style=\"text-align:right;\">" + Act.Location + "</td></tr><tr><td colspan=4>" + Act.Title + "</td></tr><tr valign=bottom><td style=\"background:#7D7D7D;\">" + Act.StartTimeOut + "</td><td colspan=2 style=\"text-align:center\">" + Act.Pattern + "</td><td style=\"text-align:right;background:#7D7D7D;\">" + Act.EndTimeOut + "</td></tr></table>";

                    ActCel.ColumnSpan = Act.EndTime - Act.StartTime;

                    ActCel.Width = ActCel.ColumnSpan * 25;

                    Day.Cells.Add(ActCel);

                    RowCounter += 1;

                    LastCol = Act.EndTime + 1;
                }

                //Finish Off Row
                //if (LastCol < 76)
                if(LastCol < EndCol)
                {
                    //Was 73
                    //for (int c = LastCol; c <= 76; c++)
                    for (int c = LastCol; c <= EndCol; c++)
                    {
                        TableCell Cell = new TableCell();
                        Cell.BorderColor = System.Drawing.Color.Black;
                        Cell.BorderWidth = 1;
                        Cell.Text = "";
                        Cell.Width = 20;
                        Day.Cells.Add(Cell);
                    }
                }

                Timetable.Rows.Add(Day);

            }

            //Set Drop Down

            ClassCourse Course = new ClassCourse(ActID);

            CourseList.SelectedValue = ActID.ToString();

            TIDPanel.Visible = true;

            TIDlbl.Text = ActID.ToString();
        }

        protected void CourseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadweeks();

            generategrid(Convert.ToInt16(CourseList.SelectedValue),0);
        }

        protected void CourseButton_Click(object sender, EventArgs e)
        {
            loadweeks();

            generategrid(Convert.ToInt16(CourseList.SelectedValue),0);
        }

        protected void WeekButton_Click(object sender, EventArgs e)
        {
            generategrid(Convert.ToInt16(CourseList.SelectedValue),Convert.ToInt16(WeekList.SelectedValue));
        }


    }
}
