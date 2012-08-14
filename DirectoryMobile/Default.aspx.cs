using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using Directory;

namespace DirectoryMobile
{
    public partial class _Default : System.Web.UI.Page
    {
        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.dirconnectionname);

            Directory.ClassAppDetails.directorycurrentconnection = NC;

            //ClassAppDetails.dircurrentconnection = NC;

            int AID = 0;
            int DID = 0;
            int PID = 0;

            AID = Convert.ToInt16(Request["aid"]);
            DID = Convert.ToInt16(Request["did"]);
            PID = Convert.ToInt16(Request["pid"]);

            if (DID != 0)
            {
                //Show Dept Details
                ClassDepartment Dept = new ClassDepartment(DID);

                Departmentlbl.Text = Dept.Name;
                DeptEmailbl.Text = "<a class=\"normal\" href=\"mailto:" + Dept.Email + "\">" + Dept.Email + "</a>";
                DeptTelephonelbl.Text = "<a class=\"normal\" href=\"tel:" + Dept.Phone + "\">" + Dept.Phone + "</a>";
                DeptFaxlbl.Text = "<a  class=\"normal\" href=\"tel:" + Dept.Fax + "\">" + Dept.Fax + "</a>";
                DeptOfficelbl.Text = Dept.Office;
                DeptOpeninglbl.Text = Dept.Opening;

                DepartmentDirectoryList.Text = ClassDirectory.MobileList(DID);

                MultiView.SetActiveView(DepartmentDirectoryView);


            }
            else if (PID != 0)
            {
                //Show Person

                ClassDirectory Dir = new ClassDirectory(PID);

                IndNamelbl.Text = Dir.Name;
                IndEmaillbl.Text = "<a class=\"normal\" href=\"mailto:" + Dir.Email + "\">" + Dir.Email + "</a>";
                IndTellbl.Text = "<a class=\"normal\" href=\"tel:" + Dir.Telephone + "\">" + Dir.Telephone +"</a>";
                IndOfficelbl.Text = Dir.Room;
                IndOfficeOpeninglbl.Text = Dir.OfficeHours;

                DeptIDlbl.Text = "<a href=\"default.aspx?did=" + Dir.Department.ID.ToString() + "\">Return to Department</a>";

                MultiView.SetActiveView(IndividualView);
            }
            else if (AID == 1)
            {
                //Show Dept List
                DepartmentList.Text = ClassDepartment.MobileList();

                MultiView.SetActiveView(DepartmentListView);
            }
            else if (AID == 2)
            {
                //Show Search

                MultiView.SetActiveView(SearchView);
            }
            else
            {
                //Show Opening
                MultiView.SetActiveView(DefaultView);
            }

        }

        protected void Searchbtn_Click(object sender, ImageClickEventArgs e)
        {
            MultiView.SetActiveView(DirectoryView);

            DirectoryListViewLabel.Text = ClassDirectory.MobileList(Searchtxt.Text);
        }
    }
}