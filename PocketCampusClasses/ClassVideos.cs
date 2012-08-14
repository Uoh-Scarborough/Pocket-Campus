
using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassVideos : ClassBase
    {
        private string c_VideoURL;
        private DateTime c_ShowFrom, c_ShowTo;
        
        public string VideoURL
        {
            get { return c_VideoURL.Trim(); }
            set { c_VideoURL = value.Trim(); }
        }

        public DateTime ShowFrom
        {
            get { return c_ShowFrom; }
            set { c_ShowFrom = value; }
        }

        public DateTime ShowTo
        {
            get { return c_ShowTo; }
            set { c_ShowTo = value; }
        }

        public ClassVideos()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassVideos(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Videos WHERE Video_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);


        }

        public ClassVideos(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Video_ID_LNK"].ToString());
            VideoURL = DR["Video_URL"].ToString();
            ShowFrom = Convert.ToDateTime(DR["Video_ShowFrom"].ToString());
            ShowTo = Convert.ToDateTime(DR["Video_ShowTo"].ToString());
            Deleted = Convert.ToBoolean(DR["Video_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;
            string Query = "INSERT INTO Videos (Video_URL, Video_ShowFrom, Video_ShowTo, Video_Deleted) VALUES ('" + VideoURL + "', '" + ShowFrom.ToShortDateString() + "','" + ShowTo.ToShortDateString() + "', 0) SELECT @@IDENTITY;";

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

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);
            bool Result;
            string Query = "UPDATE Videos SET Video_URL = '" + VideoURL + "', Video_ShowFrom = '" + ShowFrom.ToShortDateString() + "', Video_ShowTo = '" + ShowTo.ToShortDateString() + "', Video_Deleted = " + Deleted.GetHashCode() + " WHERE Video_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query);

                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";
            
            //Show Valid
            Query = "SELECT * FROM Videos WHERE Video_Deleted = 0 AND Video_ShowTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Video_ID_LNK";
             
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        

       
    }
}
