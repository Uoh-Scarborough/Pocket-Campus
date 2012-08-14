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
using System.DirectoryServices;
using StandardClasses;

namespace Mobile
{
    public partial class Default : System.Web.UI.Page
    {
        public int RID, AID, VID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ssconnectionname);

            ClassAppDetails.sscurrentconnection = NC;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            //Check if Council Tax Letter Request Exists
            if (ClassRequest.requestExists(UI.StudentID, 1))
            {
                CouncilTaxcmd.Visible = false;
            }

        }

        protected void CouncilTaxcmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(1);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            CTEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 1;
        }

        protected void StatusLettercmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(2);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            SLEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 2;
        }

        protected void Transcriptcmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(3);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            SLEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 3;
        }

        protected void Logoutcmd_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

    }
}
