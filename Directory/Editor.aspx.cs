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

namespace Directory
{
    public partial class Editor : System.Web.UI.Page
    {

        ClassConnection NC;
        ClassUserInfo CurrentUser;

            
        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.directoryconnectionname);

            ClassAppDetails.directorycurrentconnection = NC;

            Departmentcmb.Items.AddRange(ClassDepartment.loadList());

            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

           Multiview.SetActiveView(EditView);

                if (CurrentUser.InGroup("Staff"))
                {
                    //OK

                    if (ClassDirectory.Exists(CurrentUser.Username))
                    {
                        ClassDirectory Person = new ClassDirectory(CurrentUser.Username);

                        Nametxt.Text = Person.Name;
                        Emailtxt.Text = Person.Email;
                        Telephonetxt.Text = Person.Telephone;
                        Roomtxt.Text = Person.Room;
                        OfficeHourstxt.Text = Person.OfficeHours;

                    }
                    else
                    {

                        Nametxt.Text = CurrentUser.DisplayName;
                        Emailtxt.Text = CurrentUser.Mail;

                    }
                }
                else
                {
                    //Log Out
                    Response.Redirect("http://directory.scar.hull.ac.uk");

                }
            
           
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {
            //Save

            if (ClassDirectory.Exists(CurrentUser.Username))
            {
                //Save
                ClassDirectory Person = new ClassDirectory(CurrentUser.Username);

                Person.Username = CurrentUser.Username;
                Person.Name = Nametxt.Text;
                Person.Email = Emailtxt.Text;
                Person.Telephone = Telephonetxt.Text;
                Person.Room = Roomtxt.Text;
                Person.OfficeHours = OfficeHourstxt.Text;
                Person.Department = new ClassDepartment(Convert.ToInt16(Departmentcmb.SelectedValue));

                Person.Save();

            }
            else
            {
                //Save New

                ClassDirectory Person = new ClassDirectory();

                Person.Username = CurrentUser.Username;
                Person.Name = Nametxt.Text;
                Person.Email = Emailtxt.Text;
                Person.Telephone = Telephonetxt.Text;
                Person.Room = Roomtxt.Text;
                Person.OfficeHours = OfficeHourstxt.Text;
                Person.Department = new ClassDepartment(Convert.ToInt16(Departmentcmb.SelectedValue));

                Person.Create();
            }

            Multiview.SetActiveView(SaveView);
        }
    }
}
