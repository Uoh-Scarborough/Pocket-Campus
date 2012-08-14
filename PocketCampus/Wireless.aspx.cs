using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PocketCampus
{
    public partial class Wireless : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Get UserAgent Details

            string UserAgent = Request.UserAgent.ToString();

            if (UserAgent.Contains("Windows"))
            {
                Response.Redirect("http://pocketcampus.scar.hull.ac.uk");
            }
            else
            {
                Response.Redirect("http://info.scar.hull.ac.uk/Download/MacWireless.pdf");
            }

            //Response.Write(Request.UserAgent.ToString());

        }
    }
}