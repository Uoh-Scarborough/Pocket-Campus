using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using StudioBooking;

namespace StudioBookingMobile
{
    public partial class ExistingBookings : System.Web.UI.Page
    {

        ClassConnection PBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {
            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.sbconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.sbcurrentconnection = PBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            int BID = Convert.ToInt16(Request["bid"]);

            if (BID>0)
            {
                ClassBookings Booking = new ClassBookings(BID);



                if (Booking.Username == UI.Username)
                {
                    //Go Ahead
                    Locationlbl.Text = Booking.Location;
                    Datelbl.Text = ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day);
                    StartTimelbl.Text = Booking.StartTimeOut;
                    EndTimelbl.Text = Booking.EndTimeOut;
                    EditBookingbtn.CommandArgument = Booking.ID.ToString();
                    DeleteButtonbtn.CommandArgument = Booking.ID.ToString();

                    MultiView.SetActiveView(OptionView);


                }
                else
                {
                    //Wrong Users
                }
            }
            else
            {
                //Load Bookings in a List

                MultiView.SetActiveView(ListView);

                Bookingslbl.Text = ClassBookings.GenerateBookingsList(UI.Username);

                
            }
        }

        protected void EditBookingbtn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton IB = (ImageButton)sender;
            Response.Redirect("Booking.aspx?BID=" + IB.CommandArgument);
        }

        protected void DeleteButtonbtn_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton IB = (ImageButton)sender;
            Response.Redirect("Booking.aspx?BID=" + IB.CommandArgument + "&DELID=1");
        }
    }
}