using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;

namespace Management
{
    public class ClassStats
    {

        private int c_ID;
        private ClassKiosks c_Kiosk;
        private DateTime c_DateTime;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public ClassKiosks Kiosk
        {
            get { return c_Kiosk; }
            set { c_Kiosk = value; }
        }

        public DateTime DateTime
        {
            get { return c_DateTime; }
            set { c_DateTime = value; }
        }

        
        public ClassStats()
        {
             //Initialise New Class
        }

        public ClassStats(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Stats WHERE Stat_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);
            RQ.RunQuery(Query);

            Kiosk = new ClassKiosks(Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString()));
            DateTime = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);
            bool Result;

            string Query = "INSERT INTO Stats (Stat_KioskIDLNK, Stat_DateTime) VALUES (" + Kiosk.ID + ",'" + DateTime.ToShortDateString() + " " + DateTime.ToShortTimeString() + "'); SELECT @@IDENTITY;";

            try
            {
                RQ.RunQuery(Query);
                c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public static void AddStat(string IP)
        {
            //Check if a Kiosk.

            if (IP == ClassAppDetails.kioskip1 || IP == ClassAppDetails.kioskip2 || IP == ClassAppDetails.kioskip3 || IP == ClassAppDetails.kioskip4)
            {
                

                ClassKiosks Kiosk = new ClassKiosks(IP);

                ClassStats Stat = new ClassStats();

                Stat.Kiosk = Kiosk;

                Stat.DateTime = DateTime.Now;

                //Stat.Referer = Referer;

                Stat.Create();

            }
        }

        public static int TodaysHits(int KioskID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);

            int Result;

            DateTime Today = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            DateTime EndToday = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,23,59,59);

            string Query = "SELECT Count(Stat_IDLNK) FROM Stats WHERE Stat_KioskIDLNK = " + KioskID + " AND Stat_DateTime >= '" + Today.ToShortDateString() + " " + Today.ToShortTimeString() + "' AND Stat_DateTime <= '" + EndToday.ToShortDateString() + " " + EndToday.ToShortTimeString() + "';";

            try
            {
                RQ.RunQuery(Query);
                Result = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            catch (Exception ex)
            {
                Result = 0;
            }
            return Result;
        }

        public static int WeeksHits(int KioskID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.managementcurrentconnection);

            int Result;

            DateTime StartDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            StartDay = StartDay.AddDays(-7);
            DateTime EndDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            EndDay.AddDays(-1);

            string Query = "SELECT Count(Stat_IDLNK) FROM Stats WHERE Stat_KioskIDLNK = " + KioskID + " AND Stat_DateTime >= '" + StartDay.ToShortDateString() + " " + StartDay.ToShortTimeString() + "' AND Stat_DateTime <= '" + EndDay.ToShortDateString() + " " + EndDay.ToShortTimeString() + "';";

            try
            {
                RQ.RunQuery(Query);
                Result = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0].ToString());
            }
            catch (Exception ex)
            {
                Result = 0;
            }
            return Result;
        }

    }
}
