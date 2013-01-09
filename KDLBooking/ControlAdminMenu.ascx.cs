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

namespace KDLBooking
{
    public partial class ControlAdminMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (ClassGroupMembers.IsAdmin(UI.Username))
            {

                AdminMenulbl.Visible = true;
                AdminMenuOptionslbl.Visible = true;

            }
            else
            {

                AdminMenulbl.Visible = false;
                AdminMenuOptionslbl.Visible = false;

            }
        }
    }
}