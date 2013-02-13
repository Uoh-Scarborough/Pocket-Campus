using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;

namespace Communications.Controls
{
    public partial class Control_Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (UI.InGroup(ClassAppDetails.admingroup))
            {

                AdminMenulbl.Visible = true;
                AdminMenuOptionslbl.Visible = true;

            }
            else
            {

                AdminMenulbl.Visible = false;
                AdminMenuOptionslbl.Visible = false;

            }

            if(ClassGroupMembers.IsMember(UI.Username,"Menu Management"))
            {
                MenuOptionslbl.Visible = true;
            } else {
                MenuOptionslbl.Visible = false;
            }


        }
    }
}