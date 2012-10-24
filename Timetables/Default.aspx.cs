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

            string strClientIP;
            strClientIP = Request.ServerVariables["REMOTE_ADDR"];

            //Label1.Text = strClientIP;

            if (ClassAppDetails.kioskip.Contains(strClientIP))
            {
                //sStyleSheet = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                HtmlLink newStyleSheet = new HtmlLink();
                newStyleSheet.Href = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                newStyleSheet.Attributes.Add("type", "text/css");
                newStyleSheet.Attributes.Add("rel", "stylesheet");
                //Page.Header.Controls.Add(newStyleSheet);
            }

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
                    //NoTimetablelbl.Text = "No Timetable Selected, please select a Timetable from the drop down above.";

                    //NoTimetablePanel.Visible = true;

                    //WeekPanel.Visible = false;

                    TIDPanel.Visible = false;

                }
            }
            else
            {
                //NoTimetablelbl.Text = "";
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

            Courselbl.Text = CourseList.SelectedItem.Text;
            courselabelbread.Text = CourseList.SelectedItem.Text;

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

            //NoTimetablePanel.Visible = false;

            //WeekPanel.Visible = true;

            Timetable.CssClass = "timetable";

            Timetable.Dispose();

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

            //Loop Through Time

            string[] CellTexts = new string[20];

            CellTexts[0] = "";
            CellTexts[1] = "9:15";
            CellTexts[2] = "9:45";
            CellTexts[3] = "10:15";
            CellTexts[4] = "10:45";
            CellTexts[5] = "11:15";
            CellTexts[6] = "11:45";
            CellTexts[7] = "12:15";
            CellTexts[8] = "12:45";
            CellTexts[9] = "13:15";
            CellTexts[10] = "13:45";
            CellTexts[11] = "14:15";
            CellTexts[12] = "14:45";
            CellTexts[13] = "15:15";
            CellTexts[14] = "15:45";
            CellTexts[15] = "16:15";
            CellTexts[16] = "16:45";
            CellTexts[17] = "17:15";
            CellTexts[18] = "17:45";
            CellTexts[19] = "18:15";

            MasterLastCol = 38;
            EndCol = 75;

            for (int i = 0; i <= 19; i++)
            {
                TableCell Cell = new TableCell();
                Cell.Text = CellTexts[i];

                if (i > 0)
                {
                    Cell.ColumnSpan = 2;
                }
                
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

                Boolean HasActs = false;

                DataRow[] TTActs = RQ.dataset.Tables[0].Select("Activity_Day = " + d);
               
                TableRow Day = new TableRow();
                Day.CssClass = "headerday firstrow";
                TableRow OriginalDay = Day;

                TableCell DayCell = new TableCell();
                DayCell.CssClass = "headerdays";

                if (d == 0)
                {
                    DayCell.Text = "<span>Mon</span>";
                    //Day.CssClass = Day.CssClass + " highlight";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 1)
                {
                    DayCell.Text = "<span>Tue</span>";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 2)
                {
                    DayCell.Text = "<span>Wed</span>";
                    //Day.CssClass = Day.CssClass + " highlight";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else if (d == 3)
                {
                    DayCell.Text = "<span>Thu</span>";
                    DayCell.RowSpan = 0;
                    DayCell.ColumnSpan = 0;
                }
                else
                {
                    DayCell.Text = "<span>Fri</span>";
                    //Day.CssClass = Day.CssClass + " highlight";
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
                    if (Act.StartTime < LastCol - 1)
                    {
                        //New Row

                        //Finish Off Row
                        if (LastCol < EndCol)
                        {
                            for (int c = LastCol; c <= EndCol; c++)
                            {
                                TableCell Cell = new TableCell();
                                Cell.Text = "";
                                Cell.CssClass = "blankcell";
                                Day.Cells.Add(Cell);
                            }

                        }

                        Timetable.Rows.Add(Day);

                        OriginalDay.Cells[0].RowSpan = OriginalDay.Cells[0].RowSpan + 1;

                        if (OriginalDay.Cells[0].RowSpan == 1) { OriginalDay.Cells[0].RowSpan = 2; }

                        //OriginalDay.CssClass += " NotLastRow";
                        
                        Day = new TableRow();

                        Day.CssClass = OriginalDay.CssClass;

                        Day.CssClass = Day.CssClass.Replace("firstrow", "");

                        //Day.CssClass += " notfirstrow";

                        //Day.BackColor = CellCol;

                        //LastCol = 37;
                        LastCol = MasterLastCol;
                    }

                    for (int c = LastCol; c <= Act.StartTime; c++)
                    {
                        TableCell Cell = new TableCell();
                        Cell.Text = "";
                        Cell.CssClass = "blankcell";
                        Day.Cells.Add(Cell);

                    }

                    TableCell ActCel = new TableCell();
                    ActCel.CssClass = "actcell";

                    String testWeek = Week.ToString();

                    if (sWeek != "00"){
                        testWeek = sWeek;
                    } 

                    String thisWeekClass = "";

                    if (Act.InWeek(testWeek))
                    {
                        thisWeekClass = " thisweek";
                        //ActCel.CssClass = ActCel.CssClass + " thisweek";
                    }

                    ActCel.ColumnSpan = Act.EndTime - Act.StartTime;

                    ActCel.CssClass = ActCel.CssClass + " spancols" + ActCel.ColumnSpan;
                 
                    ActCel.Text = "<table class='acttable'><tr class='toprow'><td colspan=3 class='actmod'>" + Act.ModuleNumber + "</td><td class='actlocation'>" + Act.Location + "</td></tr><tr><td colspan=4 class='acttitle spancols" + ActCel.ColumnSpan + "'>" + Act.Title + "</td></tr><tr class='bottomrow'><td class='actstart'>" + Act.StartTimeOut + "<span class='actstartend'> - " + Act.EndTimeOut + "</span></td><td colspan=2 class='actpattern " + thisWeekClass + "'><span class='actweekstext'>Weeks: </span>" + Act.Pattern + "</td><td class='actend'>" + Act.EndTimeOut + "</td></tr></table>";

                    //ActCel.Width = ActCel.ColumnSpan * 25;

                    Day.Cells.Add(ActCel);

                    RowCounter += 1;

                    LastCol = Act.EndTime + 1;

                    HasActs = true;
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
                        Cell.Text = "";
                        Cell.CssClass = "blankcell";
                        Day.Cells.Add(Cell);
                    }
                }

                if (!HasActs)
                {
                    Day.CssClass += " blankday";
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
            //loadweeks();

            generategrid(Convert.ToInt16(CourseList.SelectedValue), Convert.ToInt16(WeekList.SelectedValue));
        }


        protected void WeekButton_Click(object sender, EventArgs e)
        {
            generategrid(Convert.ToInt16(CourseList.SelectedValue),Convert.ToInt16(WeekList.SelectedValue));
        }



    }
}
