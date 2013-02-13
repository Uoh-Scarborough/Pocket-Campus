using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;

namespace Communications
{
    public partial class _Default : System.Web.UI.Page
    {

        ClassConnection CNC;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            CNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = CNC;
            //Becuase we use Groups we need to initialise the Booking Connection too.
            ClassAppDetails.bookingcurrentconnection = CNC;

            string CMD = Request["cmd"];

            if (CMD == "Notice")
            {
                Multiview.SetActiveView(AddEditNotice);
            }
            else if (CMD == "Event")
            {
                Multiview.SetActiveView(AddEditEvent);
            }
            else if (CMD == "Menu")
            {
                Multiview.SetActiveView(AddEditMenu);
            }
            else if (CMD == "Ticker")
            {
                Multiview.SetActiveView(AddEditTicker);
            }
            else
            {

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                if (ClassGroupMembers.IsMember(UI.Username, "Menu Management"))
                {
                    pnl_Menu.Visible = true;
                }
                else
                {
                    pnl_Menu.Visible = false;
                }

                Multiview.SetActiveView(Lists);
            }
        }

    }
}