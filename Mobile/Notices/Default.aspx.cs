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

namespace Mobile
{
    public partial class Notices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            int NID = Convert.ToInt32(Request["nid"]);

            if (NID != 0)
            {
                LoadNotice(NID);
            }
            else
            {
                LoadNotices();
            }
        }

        public void LoadNotices()
        {
            MultiView.ActiveViewIndex = 0;

            ArrayList Notices = ClassNotice.loadDataset();

            String returnStr = "<ul>";

            foreach (ClassNotice Notice in Notices)
            {
                returnStr += "<li><a href=\"?nid=" + Notice.ID + "\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>" + Notice.Title + "</a></li>";
            }

            returnStr += "</ul>";

            Noticeslbl.Text = returnStr;
        }

        public void LoadNotice(int NID)
        {
            MultiView.ActiveViewIndex = 0;

            ClassNotice Notice = new ClassNotice(NID);

            String returnStr = "<ul>";

            returnStr += "<li><h1>" + Notice.Title + "</h1><p>" + Notice.Notice + "</p></li>";
            
            returnStr += "</ul>";

            Noticeslbl.Text = returnStr;
        }
    }
}
