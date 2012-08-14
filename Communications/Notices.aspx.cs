using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;

namespace Communications
{
    public partial class Notices : System.Web.UI.Page
    {

        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = NC;
        }
    }
}