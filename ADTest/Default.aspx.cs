using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using System.Security;
using System.Web.Security;

namespace ADTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void logincmd_Click(object sender, EventArgs e)
        {
            ClassLdapAuth adAuth = new ClassLdapAuth(ldaptxt.Text);

            try
            {
                //Admin Page
                //if (true == adAuth.IsAuthenticated(ClassAppDetails.domain, UserName.Value, UserPass.Value) && adAuth.GetGroups().Contains(ClassAppDetails.admingroup))
                if (true == adAuth.IsAuthenticated(domaintxt.Text, loginusernametxt.Text, loginpasswordtxt.Text))
                {
                    // Retrieve the user's groups
                    string groups = "";
                    //= adAuth.GetGroups();
                    // Create the authetication ticket
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginusernametxt.Text, DateTime.Now, DateTime.Now.AddMinutes(60), false, groups);

                    // Now encrypt the ticket.
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    // Create a cookie and add the encrypted ticket to the cookie as data.
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    // Add the cookie to the outgoing cookies collection.
                    Response.Cookies.Add(authCookie);

                    // Redirect the user to the originally requested page
                    //FormsAuthentication.RedirectFromLoginPage(loginusernametxt.Text, false);

                    loginoutcomelbl.Text = "Logged In";
                }
                else
                {
                    loginoutcomelbl.Text = "Authentication failed, check username and password.";
                }
            }
            catch (Exception ex)
            {
               this.loginoutcomelbl.Text = "Error authenticating. " + ex.Message;
            }
        }

        protected void testcmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo User = new ClassUserInfo(detailsusernametxt.Text, ldaptxt.Text);

            this.detailsoutcome.Text = "Name: " + User.DisplayName + ", StudentID: " + User.StudentID + ", Email: " + User.Mail + ", Groups: " + User.Groups + ".";
        }
    }
}