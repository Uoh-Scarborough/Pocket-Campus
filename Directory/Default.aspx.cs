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
    public partial class Default : System.Web.UI.Page
    {

        ClassConnection NC;

        int PID;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.directoryconnectionname);

            ClassAppDetails.directorycurrentconnection = NC;

            PID = Convert.ToInt32(Request["pid"]);

            if (!IsPostBack)
            {
                loadDepartments();

                if (PID != 0)
                {
                    MultiView.ActiveViewIndex = 2;

                    //Load Information

                    ClassDirectory Person = new ClassDirectory(PID);

                    Namelbl.Text = Person.Name;
                    Emaillbl.Text = Person.Email;
                    try
                    {
                        Departmentlbl.Text = Person.Department.Name;
                    }
                    catch
                    {
                        Departmentlbl.Text = "";
                    }
                    Phonelbl.Text = "+44 (0)1723 35 " + Person.Telephone;
                    OfficeHourslbl.Text = Person.OfficeHours;
                    Officelbl.Text = Person.Room;
                }
                else
                {
                    MultiView.ActiveViewIndex = 3;
                }
            }
        }

        public void loadDepartments()
        {
            Departmentscmb.Items.Clear();

            ListItem[] Departments = ClassDepartment.loadList();

            foreach (ListItem Department in Departments)
            {
                Departmentscmb.Items.Add(Department);
            }
        }

        protected void SearchStaffcmd_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;

            StaffListlbl.Text = "Search Results";

            DataSet DS;

            StaffListInstructionslbl.Text = "The list below shows the people in the system.";

            DS = ClassDirectory.loadDataset(SearchNametxt.Text.Trim());

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[4];
                TC[0] = new TableCell();
                TC[0].Text = "<a href=\"?pid=" + DR[0] + "\">" + DR[1].ToString() + "</a>";
                TC[1] = new TableCell();
                TC[1].Text = DR[2].ToString();
                TC[2] = new TableCell();
                TC[2].Text = DR[3].ToString();
                TC[3] = new TableCell();
                TC[3].Text = DR[4].ToString();
                
                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                StaffListtbl.Rows.Add(TR);
            }
        }

        protected void ViewDepartmentcmd_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;

            int DID = Convert.ToInt16(Departmentscmb.SelectedValue);

            ClassDepartment Dept = new ClassDepartment(DID);

            DeptDepartmentlbl.Text = Dept.Name;
            DeptEmaillbl.Text = "<a href=\"" + Dept.Email + "\">" + Dept.Email + "</a>";
            DeptPhonelbl.Text = Dept.Phone;
            DeptFaxlbl.Text = Dept.Phone;
            DeptOfficelbl.Text = Dept.Office;
            DeptOfficeHourslbl.Text = Dept.Opening;

            DataSet DS;

            StaffListInstructionslbl.Text = "The list below shows the people who are in " + Dept.Name + ".";

            DS = ClassDirectory.loadDataset(Dept.ID);

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[4];
                TC[0] = new TableCell();
                TC[0].Text = "<a href=\"?pid=" + DR[0] + "\">" + DR[1].ToString() + "</a>";
                TC[1] = new TableCell();
                TC[1].Text = DR[2].ToString();
                TC[2] = new TableCell();
                TC[2].Text = DR[3].ToString();
                TC[3] = new TableCell();
                TC[3].Text = DR[4].ToString();

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                DeptStaffListtbl.Rows.Add(TR);
            }
        }

      

        }

}
