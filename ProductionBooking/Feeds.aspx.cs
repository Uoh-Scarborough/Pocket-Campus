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
using System.Xml;
using System.Text;
using StandardClasses;
using PocketCampusClasses;

namespace ProductionBooking
{
    public partial class Feeds : System.Web.UI.Page
    {

        public void LoadFeed()
        {

            ClassConnection TTConn, PBConn;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;

            using (XmlWriter writer = XmlWriter.Create(Response.OutputStream, settings))
            {
                int AID = Convert.ToInt32(Request["aid"]);
                string RID = Request["rid"];

                TTConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);
                PBConn = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);

                //AID 1 = Now
                //AID 2 = Next 5
                //AID 3 = Signs

                //Get Day
                int Day = StandardClasses.ClassGeneral.getWeekDay();

                //Get Week
                int Week = StandardClasses.ClassGeneral.getAcademicWeek();

                string sWeek = Week.ToString();

                if (Week <= 9)
                {
                    sWeek = "0" + Week.ToString();
                }

                //Get Now As ID
                int Now = StandardClasses.ClassGeneral.getTimeCode();

                if (AID == 1)
                {

                    try
                    {

                        writer.WriteStartDocument();
                        writer.WriteStartElement("now");
                        writer.WriteStartElement("nowon");

                        ClassReadQuery RQ0 = new ClassReadQuery(TTConn);

                        RQ0.RunQuery("SELECT Activity_Title, Activity_StartTime, Activity_EndTime FROM Activities WHERE (Activity_Day = " + Day + ") AND (Activity_Location LIKE '%" + RID + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') AND (Activity_StartTime <= " + Now + ") AND (Activity_EndTime >= " + Now + ") AND Activity_Deleted = 0;");

                        ClassReadQuery RQ1 = new ClassReadQuery(PBConn);

                        RQ1.RunQuery("SELECT Booking_Title, Booking_StartTime, Booking_EndTime FROM Bookings WHERE (Booking_Day = " + Day + ") AND (Booking_Location LIKE '%" + RID + "%') AND (Booking_Week = " + Week + ") AND (Booking_StartTime <= " + Now + ") AND (Booking_EndTime >= " + Now + ") AND Booking_Deleted = 0;");

                        if (RQ0.dataset.Tables[0].Rows.Count > 0)
                        {
                            //Activity

                            double StartInt = Convert.ToInt16(RQ0.dataset.Tables[0].Rows[0].ItemArray[1]);
                            double EndInt = Convert.ToInt16(RQ0.dataset.Tables[0].Rows[0].ItemArray[2]);
                            double Duration = EndInt - StartInt;
                            double CurrentPos = EndInt - ClassGeneral.getTimeCode();

                            double PerInt = ((Duration - CurrentPos) / Duration) * 100 * 3;

                            writer.WriteElementString("room", RID);
                            writer.WriteElementString("activity", RQ0.dataset.Tables[0].Rows[0].ItemArray[0].ToString());
                            writer.WriteElementString("starttime", StandardClasses.ClassGeneral.getTime(Convert.ToInt32(StartInt)));
                            writer.WriteElementString("endtime", StandardClasses.ClassGeneral.getTime(Convert.ToInt32(EndInt)));
                            writer.WriteElementString("percentthrough", PerInt.ToString());

                        }
                        else if (RQ1.dataset.Tables[0].Rows.Count > 0)
                        {
                            //Booking

                            int StartInt = Convert.ToInt16(RQ1.dataset.Tables[0].Rows[0].ItemArray[1]);
                            int EndInt = Convert.ToInt16(RQ1.dataset.Tables[0].Rows[0].ItemArray[2]);
                            //double PerInt = ((EndInt - StartInt) / EndInt) * 100;
                            double PerInt =  ((300 / (EndInt - StartInt)) * ((ClassGeneral.getTimeCode() - StartInt)));
                     

                            writer.WriteElementString("room", RID);
                            writer.WriteElementString("activity", RQ1.dataset.Tables[0].Rows[0].ItemArray[0].ToString());
                            writer.WriteElementString("starttime", StandardClasses.ClassGeneral.getTime(StartInt));
                            writer.WriteElementString("endtime", StandardClasses.ClassGeneral.getTime(EndInt));
                            writer.WriteElementString("percentthrough", PerInt.ToString());

                        }
                        else
                        {
                            //Space is Free

                            ClassReadQuery RQ2 = new ClassReadQuery(TTConn);

                            RQ2.RunQuery("SELECT Top 1 Activity_StartTime FROM Activities WHERE (Activity_Day = " + Day + ") AND (Activity_Location LIKE '%" + RID + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') AND (Activity_StartTime > " + Now + ") AND Activity_Deleted = 0 ORDER BY Activity_StartTime");

                            ClassReadQuery RQ3 = new ClassReadQuery(PBConn);

                            RQ3.RunQuery("SELECT Top 1 Booking_StartTime FROM Bookings WHERE (Booking_Day = " + Day + ") AND (Booking_Location LIKE '%" + RID + "%') AND (Booking_Week = " + Week + ") AND (Booking_StartTime >= " + Now + ") AND Booking_Deleted = 0 ORDER BY Booking_StartTime;");

                            string TimeString = "23:00";

                            if (RQ2.dataset.Tables[0].Rows.Count > 0 && RQ3.dataset.Tables[0].Rows.Count > 0)
                            {
                                //Compare Times
                                int TT = Convert.ToInt16(RQ2.dataset.Tables[0].Rows[0].ItemArray[0]);
                                int BT = Convert.ToInt16(RQ3.dataset.Tables[0].Rows[0].ItemArray[0]);
                                if (TT < BT)
                                {
                                    TimeString = StandardClasses.ClassGeneral.getTime(TT);
                                }
                                else
                                {
                                    TimeString = StandardClasses.ClassGeneral.getTime(BT);
                                }
                            }
                            else if (RQ2.dataset.Tables[0].Rows.Count > 0)
                            {
                                //Take RQ2 Time
                                TimeString = StandardClasses.ClassGeneral.getTime(Convert.ToInt16(RQ2.dataset.Tables[0].Rows[0].ItemArray[0]));
                            }
                            else if (RQ3.dataset.Tables[0].Rows.Count > 0)
                            {
                                //Take RQ3 Time
                                TimeString = StandardClasses.ClassGeneral.getTime(Convert.ToInt16(RQ3.dataset.Tables[0].Rows[0].ItemArray[0]));
                            }



                            writer.WriteElementString("room", RID);
                            writer.WriteElementString("activity", "Room Free");
                            writer.WriteElementString("starttime", StandardClasses.ClassGeneral.getTime(StandardClasses.ClassGeneral.getTimeCode()));
                            writer.WriteElementString("endtime", TimeString);
                            writer.WriteElementString("percentthrough", "0");

                        }

                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndDocument();

                    }
                    catch (Exception ex)
                    {
                        // Do Nothing
                    }

                    TTConn.connection.Close();
                    PBConn.connection.Close();

                }
                else if (AID == 2)
                {
                    try
                    {

                        writer.WriteStartDocument();
                        writer.WriteStartElement("next");
                        

                        ClassReadQuery RQ0 = new ClassReadQuery(TTConn);

                        //RQ0.RunQuery("SELECT DISTINCT(Activity_Module) As Module, Activity_Title As Activity, Activity_StartTime As StartTime, Activity_EndTime As EndTime FROM Activities WHERE (Activity_Day = 1) AND (Activity_Location LIKE '%PS2%') AND (Activity_Weeks LIKE '%31%') AND (Activity_StartTime >= 45) AND Activity_Deleted = 0;");

                        RQ0.RunQuery("SELECT DISTINCT(Activity_Module) As Module, Activity_Title As Activity, Activity_StartTime As StartTime, Activity_EndTime As EndTime FROM Activities WHERE (Activity_Day = " + Day + ") AND (Activity_Location LIKE '%" + RID + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') AND (Activity_StartTime >= " + Now + ") AND Activity_Deleted = 0;");

                        ClassReadQuery RQ1 = new ClassReadQuery(PBConn);

                        RQ1.RunQuery("SELECT Booking_Title As Activity, Booking_StartTime As StartTime, Booking_EndTime As EndTime FROM Bookings WHERE (Booking_Day = " + Day + ") AND (Booking_Location LIKE '%" + RID + "%') AND (Booking_Week = " + Week + ") AND (Booking_StartTime >= " + Now + ") AND Booking_Deleted = 0;");

                        DataSet Ds = RQ0.dataset;

                        string Test = Ds.ToString();

                        Ds.Merge(RQ1.dataset);

                        string Test2 = Ds.ToString();

                        DataRow [] DR = Ds.Tables[0].Select(null, "StartTime");

                        //Ds.Tables[0].Select("StartTime");

                        int max = Ds.Tables[0].Rows.Count;

                        if (max > 4) {
                            max = 4;
                        }

                        string NextDetails = "";

                        for (int i = 0; i <= max-1; i++)
                        {
                            //NextDetails += Ds.Tables[0].Rows[i].ItemArray[1].ToString().Trim() + " - (" + StandardClasses.ClassGeneral.getTime(Convert.ToInt16(Ds.Tables[0].Rows[i].ItemArray[2])) + " - " + StandardClasses.ClassGeneral.getTime(Convert.ToInt16(Ds.Tables[0].Rows[i].ItemArray[3])) + ")\r\n";
                            NextDetails += DR[i].ItemArray[1].ToString().Trim() + " - (" + StandardClasses.ClassGeneral.getTime(Convert.ToInt16(DR[i].ItemArray[2])) + " - " + StandardClasses.ClassGeneral.getTime(Convert.ToInt16(DR[i].ItemArray[3])) + ")\r\n";
                        }

                        writer.WriteStartElement("nexton");

                        writer.WriteElementString("activity", NextDetails);

                        writer.WriteEndElement();


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (AID == 3)
                {
                    //Signs


                    ClassReadQuery RQ = new ClassReadQuery(PBConn);

                    RQ.RunQuery("SELECT TOP 1 Signs_SignType_ID_LNK, SignType_FileType FROM Signs_View WHERE Signs_Room = '" + RID + "' AND Signs_DisplayFrom <= '" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "' AND Signs_DisplayTo >= '" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "';");

                    writer.WriteStartDocument();
                    writer.WriteStartElement("signs");

                    if (RQ.numberofresults > 0)
                    {
                        

                        writer.WriteStartElement("sign");
                        //writer.WriteElementString("url", "http://172.16.253.128/productionbooking/Signs/" + RQ.dataset.Tables[0].Rows[0].ItemArray[0] + RQ.dataset.Tables[0].Rows[0].ItemArray[1]);
                        writer.WriteElementString("url", "http://productionbooking.scar.hull.ac.uk/Signs/" + RQ.dataset.Tables[0].Rows[0].ItemArray[0] + RQ.dataset.Tables[0].Rows[0].ItemArray[1]);
                        writer.WriteEndElement();
                    } 
                    
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadFeed();        
        }
        
    }
}