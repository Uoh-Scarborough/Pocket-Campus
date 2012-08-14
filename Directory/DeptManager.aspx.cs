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
    public partial class DeptManager : System.Web.UI.Page
    {

        public int AID, DID;

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
                DID = Convert.ToInt32(Request["did"]);

                if (!Page.IsPostBack)
                {
                    if (AID == 1)
                    {

                        if (DID == -1)
                        {
                            //Add
                            AddEditlbl.Text = "Add Department";
                            AddEditInstructionlbl.Text = "Complete the form below and click the Save button to add a new department to the system.</p>";
                            MultiView.SetActiveView(AddEditView);
                        }
                        else
                        {
                            //Edit

                            //Get Notice
                            AddEditlbl.Text = "Edit Department";
                            AddEditInstructionlbl.Text = "Make any changes to the department below and click the Save button.";

                            ClassDepartment Department = new ClassDepartment(DID);

                            Departmenttxt.Text = Department.Name;
                            Emailtxt.Text = Department.Email;
                            Phonetxt.Text = Department.Phone;
                            Faxtxt.Text = Department.Fax;
                            Officetxt.Text = Department.Office;
                            Opeingtxt.Text = Department.Opening;

                            MultiView.SetActiveView(AddEditView);
                        }
                    }
                    else
                    {
                        if (AID == 2)
                        {

                            //Delete

                            ClassDepartment Department = new ClassDepartment(DID);

                            Department.Deleted = true;

                            Department.Save();

                        }

                        MultiView.SetActiveView(ListView);
                        LoadTable();
                    }
                }
            }
        }

        public void LoadTable()
        {
            DataSet DS;

            Departmentlbl.Text = "Departments";
            DepartmentInstructionslbl.Text = "The list below shows the departments in the system.</p><p><a href=\"?did=-1\">Add a new department</a>";

            DS = ClassDepartment.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = DR[1].ToString();
                TC[1] = new TableCell();
                TC[1].Text = DR[3].ToString();
                TC[2] = new TableCell();
                TC[2].Text = DR[4].ToString();
                TC[3] = new TableCell();
                TC[3].Text = DR[5].ToString();
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&did=" + DR[0].ToString() + "'>Edit</a>";
                TC[5] = new TableCell();
                TC[5].Text = "<a href='?aid=2&did=" + DR[0].ToString() + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                DepartmentTable.Rows.Add(TR);
            }
        }


        protected void Savecmd_Click(object sender, EventArgs e)
        {
            //Save Department
            if (DID == -1)
            {
                //New Event
                ClassDepartment Department = new ClassDepartment();

                Department.Name = Departmenttxt.Text;
                Department.Email = Emailtxt.Text;
                Department.Phone = Phonetxt.Text;
                Department.Fax = Faxtxt.Text;
                Department.Office = Officetxt.Text;
                Department.Opening = Opeingtxt.Text;

                Department.Create();

            }
            else
            {

                ClassDepartment Department = new ClassDepartment(DID);

                Department.Name = Departmenttxt.Text;
                Department.Email = Emailtxt.Text;
                Department.Phone = Phonetxt.Text;
                Department.Fax = Faxtxt.Text;
                Department.Office = Officetxt.Text;
                Department.Opening = Opeingtxt.Text;

                Department.Save();

            }

            Response.Redirect("DeptManager.aspx");
        }
    }
}
