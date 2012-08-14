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
    public partial class CampusInfo : System.Web.UI.Page
    {

        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        { 

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            string Tag = "";

            Tag = Request["page"];


            if (Tag != null)
            {

                if (ClassPages.PageExists(Tag))
                {

                    //Page to Load

                    ClassPages LivePage = new ClassPages(Tag);

                    headerlbl.Text = LivePage.Title;

                    contentlbl.Text = LivePage.MobileContent;

                    contentlbl.Text += "<ul><li>&copy Scarborough Campus 2010</li></ul>";

                    Multiview.SetActiveView(ContentView);

                }
                else
                {

                    Multiview.SetActiveView(DefaultView);

                }

            }
            else
            {

                Multiview.SetActiveView(DefaultView);

            }

        }
    }
}
