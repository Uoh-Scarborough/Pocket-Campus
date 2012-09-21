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
using System.Runtime.InteropServices;
using StandardClasses;
using PocketCampusClasses;

namespace StudioBooking
{
    public partial class login : System.Web.UI.Page
    {

        ClassConnection PBNC;

        protected void Login_Click(object sender, EventArgs e)
        {
            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);

            ClassAppDetails.bookingcurrentconnection = PBNC;

           
            
            // Path to you LDAP directory server.
            // Contact your network administrator to obtain a valid path.
            ClassLdapAuth adAuth = new ClassLdapAuth(ClassAppDetails.ldapserver);

            try
            {
                //Admin Page
                //if (true == adAuth.IsAuthenticated(ClassAppDetails.domain, UserName.Value, UserPass.Value) && adAuth.GetGroups().Contains(ClassAppDetails.admingroup))
                if (true == adAuth.IsAuthenticated(ClassAppDetails.domain, UserName.Value, UserPass.Value))
                {

                    //Check Studio Booking Groups
                    if (ClassGroupMembers.IsActiveUser(UserName.Value))
                    {


                        // Retrieve the user's groups
                        string groups = adAuth.GetGroups();
                        // Create the authetication ticket
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, UserName.Value, DateTime.Now, DateTime.Now.AddMinutes(60), false, groups);

                        // Now encrypt the ticket.
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                        // Create a cookie and add the encrypted ticket to the cookie as data.
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        // Add the cookie to the outgoing cookies collection.
                        Response.Cookies.Add(authCookie);

                        // Redirect the user to the originally requested page
                        FormsAuthentication.RedirectFromLoginPage(UserName.Value, false);

                    }
                    else
                    {

                        lblResults.Text = "Only SANM Students have automatic booking rights for the Performance Studios. Non SANM Staff and Students who would like to request a booking, please email <a href='mailto:production-scar@hull.ac.uk'>production-scar@hull.ac.uk</a>.";

                    }
                }
                else
                {
                    lblResults.Text = "Authentication failed, check username and password.";
                }
            }
            catch (Exception ex)
            {
                lblResults.Text = "Error authenticating. " + ex.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUserAgent = Request.UserAgent.ToString().ToLower();
            if (strUserAgent != null)
            {
                if (Request.Browser.IsMobileDevice == true || strUserAgent.Contains("iphone") ||
                    strUserAgent.Contains("blackberry") || strUserAgent.Contains("mobile") ||
                    strUserAgent.Contains("windows ce") || strUserAgent.Contains("opera mini") ||
                    strUserAgent.Contains("palm"))
                {
                    Response.Redirect("http://m.studiobooking.scar.hull.ac.uk");
                }
            }
           
            if(!IsPostBack){
               FormsAuthentication.SignOut();
            }
        }
    }
}
