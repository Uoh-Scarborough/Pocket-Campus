
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
    public class ClassCategory : ClassBase
    {
      
        private string c_Title;
      
        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public ClassCategory()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassCategory(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Categories WHERE Category_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassCategory(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Category_ID_LNK"].ToString());
            Title = DR["Category_Title"].ToString();
            Deleted = Convert.ToBoolean(DR["Category_Deleted"].ToString());
        }



        public static ListItem[] loadList()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            RQ.RunQuery("SELECT * FROM Categories WHERE Category_Deleted = 0 ORDER BY Category_Title");

            ListItem[] List = new ListItem[RQ.numberofresults];

            int Counter = 0;

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {

                ClassCategory Cat = new ClassCategory(DR);

                ListItem LI = new ListItem(Cat.Title, Cat.ID.ToString());

                List[Counter] = LI;

                Counter += 1;
            }

            return List;
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            Query = "SELECT * FROM Categories WHERE Category_Deleted = 0 ORDER BY Category_Title";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

    }
}
