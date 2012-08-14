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
    public partial class DirectoryManager : System.Web.UI.Page
    {

        public int AID, PID;

        ClassConnection NC;

        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.directoryconnectionname);

            ClassAppDetails.directorycurrentconnection = NC;

            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

            if (!CurrentUser.InGroup("DirectoryManagement"))
            {
                //Send to Editor
                Response.Redirect("Editor.aspx");
            }
            else
            {

                AID = Convert.ToInt32(Request["aid"]);
                PID = Convert.ToInt32(Request["pid"]);

                if (!Page.IsPostBack)
                {
                    if (AID == 1)
                    {

                        if (PID == -1)
                        {
                            //Add
                            //AddEditInstructionlbl.Text = "Complete the form below and click the Save button to add a new department to the system.</p>";
                            //MultiView.SetActiveView(AddEditView);
                        }
                        else
                        {
                            //Edit
                            AddEditInstructionlbl.Text = "Make any changes to the department below and click the Save button.";

                            ClassDirectory Person = new ClassDirectory(PID);

                            LoadDepartmentDropDown();

                            Nametxt.Text = Person.Name;
                            Emailtxt.Text = Person.Email;
                            Phonetxt.Text = Person.Telephone;
                            OfficeHourstxt.Text = Person.OfficeHours;
                            Officetxt.Text = Person.Room;
                            //Departmentcmb.SelectedValue = Person.Department;
                            Departmentcmb.SelectedValue = Person.Department.ID.ToString();

                            MultiView.SetActiveView(AddEditView);
                        }
                    }
                    else
                    {
                        if (AID == 2)
                        {

                            //Delete

                            ClassDirectory Person = new ClassDirectory(PID);

                            Person.Deleted = true;

                            Person.Save();

                        }
                        else if (AID == 3)
                        {
                            //Generate List

                            UpdateDatabaseList();
                        }

                        MultiView.SetActiveView(ListView);
                        LoadTable();


                    }
                }

            }
        }

        public void LoadDepartmentDropDown()
        {
            //Load Department Drop Down

            ListItem[] Departments = ClassDepartment.loadList();

            //Loop Through Departments

            foreach (ListItem Dept in Departments)
            {
                Departmentcmb.Items.Add(Dept);
            }
        }

        public void UpdateDatabaseList()
        {
            //Load Users

            ArrayList Users = ClassUserInfo.GetUsers();

            foreach (ClassUserInfo User in Users)
            {
                if (!ClassDirectory.Exists(User.Username))
                {
                    ClassDirectory NewUser = new ClassDirectory();

                    NewUser.Username = User.Username;
                    NewUser.Name = User.DisplayName;

                    string[] NameSplit = User.DisplayName.Split(' ');

                    NewUser.Surname = NameSplit[NameSplit.Length - 1];
                    NewUser.Email = User.Mail;

                    NewUser.Create();
                }
            }
        }

        public void LoadTable()
        {
            DataSet DS;

            DepartmentInstructionslbl.Text = "The list below shows the people in the system.";

            DS = ClassDirectory.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = DR[1].ToString();
                TC[1] = new TableCell();
                TC[1].Text = DR[2].ToString();
                TC[2] = new TableCell();
                TC[2].Text = DR[3].ToString();
                TC[3] = new TableCell();
                TC[3].Text = DR[4].ToString();
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&pid=" + DR[0].ToString() + "'>Edit</a>";
                TC[5] = new TableCell();
                TC[5].Text = "<a href='?aid=2&pid=" + DR[0].ToString() + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                DirectoryTable.Rows.Add(TR);
            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {
            if (PID == -1)
            {
                //Add New
                ClassDirectory Person = new ClassDirectory(0);

                ClassDepartment Dept = new ClassDepartment(Convert.ToInt16(Departmentcmb.SelectedValue.ToString()));

                Person.Name = Nametxt.Text;
                Person.Email = Emailtxt.Text;
                Person.Department = Dept;
                Person.Telephone = Phonetxt.Text;
                Person.Room = Officetxt.Text;
                Person.OfficeHours = OfficeHourstxt.Text;

                Person.Save();

            }
            else
            {
                //Save Old

                ClassDirectory Person = new ClassDirectory(PID);

                ClassDepartment Dept = new ClassDepartment(Convert.ToInt16(Departmentcmb.SelectedValue.ToString()));

                Person.Department = Dept;
                Person.Telephone = Phonetxt.Text;
                Person.Room = Officetxt.Text;
                Person.OfficeHours = OfficeHourstxt.Text;

                Person.Save();
            }

            Response.Redirect("DirectoryManager.aspx");
        }
    }
}
