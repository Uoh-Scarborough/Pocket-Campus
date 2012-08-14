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

namespace Mobile
{
    public partial class Timetable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.ttcurrentconnection = NC;

            int YID = Convert.ToInt32(Request["yid"]);
            int CGID = Convert.ToInt32(Request["cgid"]);
            int CID = Convert.ToInt32(Request["cid"]);
            int SCID = Convert.ToInt32(Request["scid"]);
            int WID = Convert.ToInt32(Request["wid"]);

            if (CID != 0)
            {
                MultiView.ActiveViewIndex = 3;

                int NextWeek = ClassGeneral.getAcademicWeek() + 1;

                if (WID != 0)
                {
                    NextWeeklbl.Text = "<ul><li><a href=\"?cid=" + CID + "\">View this weeks timetable.</a></li><li><a href=\"?scid=" + CID + "\">Save as my Timetable</a></li></ul>";
                    TimetableLabel.Text = ClassActivities.ShowTimetables(CID, WID);
                }
                else
                {
                    NextWeeklbl.Text = "<ul><li><a href=\"?wid=" + NextWeek + "&amp;cid=" + CID + "\">View next weeks timetable.</a></li><li><a href=\"?scid=" + CID + "\">Save as my Timetable</a></li></ul>";
                    TimetableLabel.Text = ClassActivities.ShowTimetables(CID, ClassGeneral.getAcademicWeek());

                    
                }
            }
            else if (SCID != 0)
            {

                MultiView.ActiveViewIndex = 4;

                Response.Cookies["PocketCampusTimetable"]["Data"] = SCID.ToString();
                Response.Cookies["PocketCampusTimetable"]["Time"] = DateTime.Now.ToString("G");
                Response.Cookies["PocketCampusTimetable"].Expires = DateTime.Now.AddMonths(1);

            }
            else if (CGID != 0)
            {
                MultiView.ActiveViewIndex = 2;

                CourseLabel.Text = ClassCourse.GenerateCourseList(YID, CGID);
            }
            else if (YID != 0)
            {
                MultiView.ActiveViewIndex = 1;

                //Output Department List

                DepartmentLabel.Text = "<ul>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=4\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Creative Music Technology</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=2\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Digital Media</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=9\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Employability and Professional Skills</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=5\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>English</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=7\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Environmental and Marine Sciences</li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=8\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Free Electives</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=1\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Management Centre</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=6\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>School of Education</a></li>";
                DepartmentLabel.Text += "<li><a href=\"?yid=" + YID + "&cgid=3\"><img class=\"arrow\" src=\"../Images/ArrowButton.jpg\"/>Theatre and Performance</a></li>";
                DepartmentLabel.Text += "</ul>";
            }
            else
            {
                MultiView.ActiveViewIndex = 0;

                if (Request.Cookies["PocketCampusTimetable"] != null)
                {
                    MyTimetable.Text = "<ul><li><a href=\"?cid=" + Request.Cookies["PocketCampusTimetable"]["Data"] + "\">My Timetable</li></ul>";
                }
            }
        }
    }
}
