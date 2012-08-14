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

namespace Comms
{
    public partial class Default : System.Web.UI.Page
    {

        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

            if (CurrentUser.InGroup(ClassAppDetails.admingroup))
            {
                WelcomeScreenslbl.Text = "<li><a href=\"Welcomes.aspx\">Welcome Screens</a></li>";
                PageManagerlbl.Text = "<li><a href=\"PageManager.aspx\">Page Manager</a></li>";
            }
        }
    }
}
