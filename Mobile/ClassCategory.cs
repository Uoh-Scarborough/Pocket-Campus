
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

namespace Mobile
{
    public class ClassCategory
    {
        private int c_ID;
        private string c_Title;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassCategory()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassCategory(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Categories WHERE Category_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[2]);
        }

        public static ListItem[] loadList()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            RQ.RunQuery("SELECT * FROM Categories WHERE Category_Deleted = 0 ORDER BY Category_Title");

            ListItem[] List = new ListItem[RQ.numberofresults];

            int Counter = 0;

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                ListItem LI = new ListItem(DR[1].ToString(), DR[0].ToString());

                List[Counter] = LI;

                Counter += 1;
            }

            return List;
        }

        

       
    }
}
