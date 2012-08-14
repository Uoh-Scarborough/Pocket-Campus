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
using PocketCampusClasses;

namespace PocketCampus
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassAppDetails.commscurrentconnection = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            if (Request.Browser["IsMobileDevice"] == "true")
            {
                Response.Redirect("http://m.pocketcampus.scar.hull.ac.uk");
            }

            loadMaps();

            loadNoticesandEvents();
        }



        public void loadNoticesandEvents()
        {
            Noticeslbl.Text = ClassNotice.loadNoticesList();

            Eventslbl.Text = ClassEvent.loadEventsList();

        }

        public void loadMaps()
        {
            String IPAddr = Request.ServerVariables["REMOTE_ADDR"];

            if (IPAddr == ClassAppDetails.kioskip1)
            {
                Mapslbl.Text = "<a href=\"http://maps.scar.hull.ac.uk?kioskid=worsley\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/MapsButton.png\" alt=\"Campus Maps\" /></a>";
            }
            else if (IPAddr == ClassAppDetails.kioskip2)
            {
                Mapslbl.Text = "<a href=\"http://maps.scar.hull.ac.uk?kioskid=quad3\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/MapsButton.png\" alt=\"Campus Maps\" /></a>";
            }
            else if (IPAddr == ClassAppDetails.kioskip3)
            {
                Mapslbl.Text = "<a href=\"http://maps.scar.hull.ac.uk?kioskid=library\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/MapsButton.png\" alt=\"Campus Maps\" /></a>";
            }
            else if (IPAddr == ClassAppDetails.kioskip4)
            {
                Mapslbl.Text = "<a href=\"http://maps.scar.hull.ac.uk\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/MapsButton.png\" alt=\"Campus Maps\" /></a>";
            }
            else
            {
                Mapslbl.Text = "<a href=\"http://maps.scar.hull.ac.uk\"><img src=\"http://pocketcampusimages.scar.hull.ac.uk/MapsButton.png\" alt=\"Campus Maps\" /></a>";
            }


        }
    }
}
