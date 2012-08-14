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
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            int EID = Convert.ToInt32(Request["eid"]);

            if (EID != 0)
            {
                LoadEvent(EID);
            }
            else
            {
                LoadEvents();
            }
        }

        public void LoadEvents()
        {
            MultiView.ActiveViewIndex = 0;

            ArrayList Events = ClassEvent.loadDataset();

            String returnStr = "<ul>";

            foreach (ClassEvent Event in Events)
            {
                returnStr += "<li><a href=\"?eid=" + Event.ID + "\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>" + Event.Title + "</a></li>";
            }

            returnStr += "</ul>";

            Eventslbl.Text = returnStr;
        }

        public void LoadEvent(int EID)
        {
            MultiView.ActiveViewIndex = 0;

            ClassEvent Event = new ClassEvent(EID);

            String returnStr = "<ul>";

            returnStr += "<li><h1>" + Event.Title + "</h1><p>" + Event.Event + "</p><ul class=\"inner\"><li><span class=\"contact\">Location:</span>" + Event.Location + "</li><li><span class=\"contact\">Date / Time:</span>" + Event.EventDateTime.ToShortDateString() + " " + Event.EventDateTime.ToShortTimeString() + "</li><li><span class=\"contact\">Duration:</span>" + Event.EventDuration + "</li></ul></li>";
            
            returnStr += "</ul>";

            Eventslbl.Text = returnStr;
        }
    }
}
