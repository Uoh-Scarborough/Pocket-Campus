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

namespace StudentServices
{
    public partial class login : System.Web.UI.Page
    {

        protected void Login_Click(object sender, EventArgs e)
        {
            // Path to you LDAP directory server.
            // Contact your network administrator to obtain a valid path.
            ClassLdapAuth adAuth = new ClassLdapAuth(ClassAppDetails.ldapserver);

            try
            {
  
                if (true == adAuth.IsAuthenticated(ClassAppDetails.domain, UserName.Value, UserPass.Value))
                {
                    // Retrieve the user's groups
                    string groups = adAuth.GetGroups();
                    // Create the authetication ticket
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, UserName.Value, DateTime.Now, DateTime.Now.AddMinutes(60), false, groups);

                    // Now encrypt the ticket.
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    // Create a cookie and add the encrypted ticket to the cookie as data.
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    authCookie.Domain = "pocketcampus.scar.hull.ac.uk";
                    // Add the cookie to the outgoing cookies collection.
                    Response.Cookies.Add(authCookie);

                    // Redirect the user to the originally requested page
                    //Response.Redirect(FormsAuthentication.GetRedirectUrl("User", false));

                    ClassUserInfo UI = new ClassUserInfo(UserName.Value);

                    if ((UI.Mail == "") || (UI.Mail == null))
                    {
                        lblResults.Text = "Your email address is not yet registered on the Active Directory System. Please try again again in 24 hours. If after a couple of attempts this is not working, please contact Campus Connect.";
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(UserName.Value, false);
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

            Infolbl.Text = "Welcome to the Scarborough Student Services homepage. To request a status letter and council tax letter you must first login using your campus username and password.";
            Adminlbl.Visible = false;
          
        }
    }
}
