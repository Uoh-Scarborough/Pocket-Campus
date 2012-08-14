
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
    public class ClassWelcome : ClassBase
    {
        private string c_Title, c_Message;
        private DateTime c_ShowFrom, c_ShowTo;
        
        private ArrayList c_Screens = new ArrayList();

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public string Message
        {
            get { return c_Message.Trim(); }
            set { c_Message = value.Trim(); }
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

        public ArrayList Screens
        {
            get { return c_Screens; }
            set { c_Screens = value; }
        }

        public string ScreensList
        {
            get
            {
                string returnStr = "";
                foreach (ClassScreens Screen in c_Screens)
                {
                    returnStr += "/" + Screen.ID + "/ ";
                }

                return returnStr;
            }
        }

        public ClassWelcome()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassWelcome(int ID)
        {
            //Initialise New Class
            
            string Query = "SELECT * From Welcomes WHERE Welcome_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);

            //Load Screens
            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ1.RunQuery("SELECT * FROM WelcomeScreens WHERE WelcomeScreens_WelcomeIDLNK = " + c_ID + " AND WelcomeScreens_Deleted = 0;");

            foreach (DataRow DR in RQ1.dataset.Tables[0].Rows)
            {
                Screens.Add(new ClassScreens(Convert.ToInt16(DR["WelcomeScreens_ScreenIDLNK"])));
            }
        }

        public ClassWelcome(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Welcome_ID_LNK"]);
            Title = DR["Welcome_Title"].ToString();
            Message = DR["Welcome_Message"].ToString();
            ShowFrom = Convert.ToDateTime(DR["Welcome_ShowFrom"]);
            ShowTo = Convert.ToDateTime(DR["Welcome_ShowTo"]);
            Deleted = Convert.ToBoolean(DR["Welcome_Deleted"]);
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;
            string Query = "INSERT INTO Welcomes (Welcome_Title, Welcome_Message, Welcome_ShowFrom, Welcome_ShowTo, Welcome_Deleted) VALUES ('" + Title + "','" + Message + "','" + ShowFrom.ToShortDateString() + "','" + ShowTo.ToShortDateString() + "', 0) SELECT @@IDENTITY;";

            try
            {
                RQ.RunQuery(Query);
                c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);

                foreach (ClassScreens Screen in c_Screens)
                {
                    ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);

                    WQ.RunQuery("INSERT INTO WelcomeScreens (WelcomeScreens_WelcomeIDLNK, WelcomeScreens_ScreenIDLNK, WelcomeScreens_Deleted) VALUES (" + c_ID + ", " + Screen.ID + ",0);");
                }

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
            string Query = "UPDATE Welcomes SET Welcome_Title = '" + Title + "', Welcome_Message = '" + Message + "', Welcome_ShowFrom = '" + ShowFrom.ToShortDateString() + "', Welcome_ShowTo = '" + ShowTo.ToShortDateString() + "', Welcome_Deleted = " + Deleted.GetHashCode() + " WHERE Welcome_ID_LNK = " + ID + ";";
            try
            {
                WQ.RunQuery(Query);

                ClassWriteQuery WQ1 = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);

                WQ1.RunQuery("UPDATE WelcomeScreens SET WelcomeScreens_Deleted = 1 WHERE WelcomeScreens_WelcomeIDLNK = " + c_ID + ";");

                foreach (ClassScreens Screen in c_Screens)
                {
                    ClassWriteQuery WQ2 = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);

                    WQ.RunQuery("INSERT INTO WelcomeScreens (WelcomeScreens_WelcomeIDLNK, WelcomeScreens_ScreenIDLNK, WelcomeScreens_Deleted) VALUES (" + c_ID + ", " + Screen.ID + ",0);");
                }

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
            Query = "SELECT * FROM Welcomes WHERE Welcome_Deleted = 0 AND Welcome_ShowTo >= '" + DateTime.Now.ToShortDateString() + "' ORDER BY Welcome_Title";
             
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        

       
    }
}
