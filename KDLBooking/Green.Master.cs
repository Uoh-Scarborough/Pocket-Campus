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
using PocketCampusClasses;

namespace KDLBooking
{
    public partial class Green : System.Web.UI.MasterPage
    {

        //public string sStyleSheet;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strClientIP;
            strClientIP = Request.ServerVariables["REMOTE_ADDR"];

            //Label1.Text = strClientIP;

            if (ClassAppDetails.kioskip.Contains(strClientIP))
            {
                //sStyleSheet = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                HtmlLink newStyleSheet = new HtmlLink();
                newStyleSheet.Href = "http://pocketcampusimages.scar.hull.ac.uk/BaseStyles/kioskstyle.css";
                newStyleSheet.Attributes.Add("type", "text/css");
                newStyleSheet.Attributes.Add("rel", "stylesheet");
                Page.Header.Controls.Add(newStyleSheet);
            }
        }
    }
}
