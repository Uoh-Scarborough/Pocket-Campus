using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ProductionBooking;
using StandardClasses;

namespace ProductionBookingMobile
{
    public partial class Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ProductionBooking.ClassAppDetails.ttcurrentconnection = TTNC;

            ClassConnection PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.pbconnectionname);

            ProductionBooking.ClassAppDetails.pbcurrentconnection = PBNC;

            string RID = "";
            int WID = Convert.ToInt32(Request["wid"]);
            int DID = Convert.ToInt32(Request["did"]);
            Boolean VID = Convert.ToBoolean(Request["view"]);

            try
            {
                RID = Request["rid"].ToString();

            }
            catch
            {
                //Do Nothing
            }

            if (RID != "" && WID > 0 && DID >= 0)
            {
                //Load TT

                LoadTimeTable(RID, WID, DID);

                MultiView.SetActiveView(RoomView);
            }
            else if (RID != "" && WID == 0 && DID == 0)
            {
                //Load Dates

                LoadDates(RID);

                MultiView.SetActiveView(DatesList);

            }
            else if (VID)
            {
                MultiView.SetActiveView(RoomList);
            }
            else
            {
                MultiView.SetActiveView(DefaultView);
            }

        }

        private void LoadDates(string RID)
        {
            int AcWeek = ClassGeneral.getAcademicWeek();

            int AcDay = ClassGeneral.getWeekDay();

            String outstr = "<ul>";

            outstr += "<li>Studio: " + RID + "</li>";

            outstr += "</ul><ul>";

            for (int i = 0; i <= 13; i++)
            {
                outstr += "<li><a href=\"?rid=" + RID + "&amp;wid=" + AcWeek + "&amp;did=" + AcDay + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/>" + ClassGeneral.getAcademicDate(AcWeek, AcDay) + "</a></li>";

                if (AcDay >= 6)
                {
                    AcWeek++;
                    AcDay = 0;
                }
                else
                {
                    AcDay++;
                }
            }

            outstr += "</ul>";

            DatesLabel.Text = outstr;
        }

        private void LoadTimeTable(string RID, int Week, int Day)
        {
            ReturntoDateslbl.Text = "<a href='?rid=" + RID +"'>Return to Dates List</a>";

            RoomTable.Dispose();

            RoomTable.BorderStyle = BorderStyle.Solid;
            RoomTable.BorderWidth = 1;

            RoomTable.Rows.Clear();

            RoomTable.CellPadding = 0;
            RoomTable.CellSpacing = 0;

            Roomlbl.Text = RID;

            ArrayList ActivityList = ProductionBooking.ClassBookings.GenerateDaySet(Day, Week, RID);

            string[] CellsTexts = new string[18];

            CellsTexts[0] = "06:15";
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

            int j = 0;
            int drawnext = -1;
            int activitycounter = 0;

            //63

            for (int i = 0; i <= 71; i++)
            {

                TableRow Row = new TableRow();
                Row.BorderStyle = BorderStyle.Dashed;
                Row.BorderWidth = 1;

                if (i % 4 == 0)
                {
                    //Row.Style = "border-top:solid 1px";

                    Row.Style.Add("border-top", "solid 1px");

                }

                if (j == 0)
                {
                    //Add Time to Row


                    TableCell Cell = new TableCell();
                    Cell.Font.Size = 8;
                    Cell.Text = CellsTexts[i / 4];
                    Cell.VerticalAlign = VerticalAlign.Top;
                    Cell.RowSpan = 4;
                    Cell.Width = 30;
                    Cell.Height = 80;

                    //Cell.BorderStyle = BorderStyle.Solid;

                    Row.Cells.Add(Cell);


                }

                j++;

                if (j > 3)
                {
                    j = 0;
                }

                TableCell Cell3 = new TableCell();
                Cell3.BorderStyle = BorderStyle.None;
                Cell3.BorderWidth = 0;
                Cell3.Style.Add("border-top", "dashed 1px");
                Cell3.Width = 20;
                Cell3.Text = "&nbsp;";

                Row.Cells.Add(Cell3);

                if (drawnext == i - 1)
                {

                    TableCell Cell2 = new TableCell();
                    Cell2.Width = 250;

                    if (ActivityList.Count > 0 && activitycounter <= ActivityList.Count - 1)

                        if (ActivityList[activitycounter].GetType().ToString() == "StudioBooking.ClassBookings")
                        {
                            //Booking
                            ClassBookings Booking = (ClassBookings)ActivityList[activitycounter];

                            //33 - 6 = 27

                            if ((Booking.StartTime - 25) == i)
                            {
                                //Draw This
                                Cell2.RowSpan = Booking.EndTime - Booking.StartTime;
                                int Height = (Cell2.RowSpan * 20) - 18;
                                Cell2.Text = "<table width='220px' style='border-collapse:collapse'><tr><td style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandTop.png) no-repeat;height:9px; cell-padding:0px; cell-spacing:0px;'><td></tr><tr><td height='" + Height + "px' style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandMiddle.png) repeat-y; cellpadding:0px; cellspacing:0px'>" + Booking.Name + " (" + Booking.StartTimeOut + "-" + Booking.EndTimeOut + ")</td></tr><tr><td style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandBottom.png) no-repeat; height:9px'></td></tr></table>";

                                Cell2.BorderStyle = BorderStyle.None;
                                //Cell2.Style.Add("z-index", 1);
                                //Cell2.BorderStyle = BorderStyle.Solid;
                                //Cell2.BorderWidth = 1;
                                drawnext += (Booking.EndTime - Booking.StartTime);
                                activitycounter++;
                            }
                            else
                            {
                                //Draw Blank
                                Cell2.Text = "<a href='Booking.aspx?RID=" + RID + "&amp;WID=" + Week + "&amp;DID=" + Day + "&amp;STID=" + (i + 26) + "'>&nbsp;</a>";
                                drawnext++;
                            }

                        }
                        else
                        {
                            //Activity
                            ClassActivities Activity = (ClassActivities)ActivityList[activitycounter];

                            if ((Activity.StartTime - 25) == i)
                            {
                                //Draw This
                                Cell2.RowSpan = Activity.EndTime - Activity.StartTime;
                                int Height = (Cell2.RowSpan * 20) - 18;
                                Cell2.Text = "<table width='220px' style='border-collapse:collapse'><tr><td style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandTop.png) no-repeat;height:9px; cell-padding:0px; cell-spacing:0px;'><td></tr><tr><td height='" + Height + "px' style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandMiddle.png) repeat-y; cellpadding:0px; cellspacing:0px'>" + Activity.Title + " (" + Activity.StartTimeOut + "-" + Activity.EndTimeOut + ")</td></tr><tr><td style='background:url(http://pocketcampusimages.scar.hull.ac.uk/CalendarBandBottom.png) no-repeat; height:9px'></td></tr></table>";

                                Cell2.BorderStyle = BorderStyle.None;
                                //Cell2.Style.Add("z-index", 1);
                                //Cell2.BorderStyle = BorderStyle.Solid;
                                //Cell2.BorderWidth = 1;
                                drawnext += (Activity.EndTime - Activity.StartTime);
                                activitycounter++;
                            }
                            else
                            {
                                //Draw Blank
                                Cell2.Text = "<a href='Booking.aspx?RID=" + RID + "&amp;WID=" + Week + "&amp;DID=" + Day + "&amp;TID=" + (i + 26) + "'>&nbsp;</a>";
                                drawnext++;
                            }
                        }
                    else
                    {

                        //Draw Blank

                        Cell2.Text = "<a href='Booking.aspx?RID=" + RID + "&amp;WID=" + Week + "&amp;DID=" + Day + "&amp;TID=" + (i + 26) + "'>&nbsp;</a>";
                        drawnext++;
                    }

                    Row.Cells.Add(Cell2);

                }



                Row.Cells.Add(Cell3);

                RoomTable.Rows.Add(Row);
                //drawnext++;
            }


        }
    }
}
