
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
    public class ClassScreens : ClassBase
    {
        private string c_Name;
        
        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public ClassScreens()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassScreens(int ID)
        {
            //Initialise New Class
            
            string Query = "SELECT * From Screens WHERE Screen_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassScreens(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Screen_ID_LNK"].ToString());
            Name = DR["Screen_Names"].ToString();
            Deleted = Convert.ToBoolean(DR["Screen_Deleted"].ToString());
        }

        public static ListItem[] loadList()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            RQ.RunQuery("SELECT * FROM Screens WHERE Screen_Deleted = 0 ORDER BY Screen_Names");

            ListItem[] List = new ListItem[RQ.numberofresults];

            int Counter = 0;

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ClassScreens Screen = new ClassScreens(DR);

                ListItem LI = new ListItem(Screen.Name, Screen.ID.ToString());

                List[Counter] = LI;

                Counter += 1;
            }

            return List;
        }

        

       
    }
}
