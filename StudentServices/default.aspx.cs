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
using System.DirectoryServices;
using StandardClasses;
using PocketCampusClasses;

namespace StudentServices
{
    public partial class _Default : System.Web.UI.Page
    {
        public int RID, AID, VID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname,ClassAppDetails.studentservicesconnectionname);
            
            ClassAppDetails.studentservicesconnection = NC;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (UI.InGroup(ClassAppDetails.admingroup))
            {
                //Admin
                AID = Convert.ToInt32(Request["aid"]);
                RID = Convert.ToInt32(Request["rid"]);
                VID = Convert.ToInt32(Request["vid"]);

                if (!Page.IsPostBack)
                {

                    if (AID == 1)
                    {
                        //Edit
                        MultiView.ActiveViewIndex = 5;

                        ClassRequest Req = new ClassRequest(RID);

                        StudentName.Text = Req.Name;
                        StudentNumber.Text = Req.Number;
                        RequestType.Text = Req.Form.FormName;
                        RequestDate.Text = Req.RequestDate.ToShortDateString();
                        Completed.Checked = Req.Completed;
                        if (Req.Completed)
                        {
                            CompletedBy.Text = Req.CompletedBy;
                            CompletedDate.Text = Req.CompletedDate.ToShortDateString();
                        }
                    }
                    else
                    {
                        if (AID == 2)
                        {
                            //Delete
                            Response.Write(RID);

                            ClassRequest Req = new ClassRequest(RID);

                            Req.Deleted = true;

                            Req.Save();
                        }

                        MultiView.ActiveViewIndex = 4;


                        if (VID == 1)
                        {
                            //Completed Requests
                            ViewOptionslbl.Text = "<a href='default.aspx'>View New Requests</a>";
                            LoadTable(true);
                        }
                        else
                        {
                            //New Requests
                            ViewOptionslbl.Text = "<a href='default.aspx?vid=1'>View Completed Requests</a>";
                            LoadTable(false);
                        }

                    }
                }
                else
                {
                    //FormsAuthentication.SignOut();
                }
            }
            else ///if (UI.InGroup(ClassAppDetails.group))
            {
                //Student
                if (ClassRequest.requestExists(UI.StudentID, 1))
                {
                    CouncilTaxcmd.Visible = false;
                }
            }
            //else
            //{
                //Nothing todo

            //}
        }

        protected void CouncilTaxcmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(1);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            CTEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 1;
        }

        protected void StatusLettercmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(2);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            SLEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 2;
        }

        protected void Transcriptcmd_Click(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassRequestForms RF = new ClassRequestForms(3);

            ClassRequest Req = new ClassRequest(UI.DisplayName, UI.StudentID, UI.Mail, RF);

            SLEmaillbl.Text = UI.Mail;

            MultiView.ActiveViewIndex = 3;
        }

        protected void Logoutcmd_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        public void LoadTable(Boolean Completed)
        {
            DataSet DS = ClassRequest.loadDataset(Completed);

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
                DateTime Test = (DateTime)DR[4];
                TC[3].Text = Test.ToShortDateString();
                TC[3].CssClass = "date";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&rid=" + DR[0].ToString() + "'>Edit</a>";
                TC[5] = new TableCell();
                if (Completed)
                {
                    TC[5].Text = "<a href='?aid=2&rid=" + DR[0].ToString() + "&vid=1' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";
                }
                else
                {
                    TC[5].Text = "<a href='?aid=2&rid=" + DR[0].ToString() + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";
                }
                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                RequestTable.Rows.Add(TR);
            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {
            ClassRequest Rq = new ClassRequest(RID);

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (!Rq.Completed)
            {

                Rq.Completed = Completed.Checked;
                Rq.CompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                Rq.CompletedBy = UI.DisplayName + " (" + UI.Username + ")";

                if (Rq.Completed)
                {
                    Rq.CreateEmails();
                }

                Rq.Save();

                MultiView.ActiveViewIndex = 6;
            }
            else
            {
                Response.Redirect("default.aspx?vid=1");
            }


        }


    }
}
