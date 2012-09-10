using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;


namespace KDLBookingMobile
{
    public partial class Login : System.Web.UI.Page
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

                        lblResults.Text = "Your username is not registered in any active groups, and therefore you have no access to the system.";

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
            FormsAuthentication.SignOut();
        }
    }
}