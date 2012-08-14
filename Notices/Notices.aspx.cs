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
using PocketCampusClasses;

namespace Comms
{
    public partial class Notices : System.Web.UI.Page
    {
        public int AID, NID, VID;

        ClassConnection NC;
        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

            AID = Convert.ToInt32(Request["aid"]);
            NID = Convert.ToInt32(Request["nid"]);
            VID = Convert.ToInt32(Request["vid"]);

            Categorylst.Items.AddRange(ClassCategory.loadList());

            if (!Page.IsPostBack)
            {
                if (AID == 1)
                {

                    if (NID == -1)
                    {
                        //Add
                        AddEditlbl.Text = "Add Notice";
                        AddEditInstructionlbl.Text = "Complete the form below and click the Save Notice button to add a new notice to the system. <b>N.B.</b> Before the notice goes live it will be validated.</p>";
                        Categorylst.Items.AddRange(ClassCategory.loadList());

                        MultiView.SetActiveView(AddEditView);
                    }
                    else
                    {
                        //Edit

                        //Get Notice
                        AddEditlbl.Text = "Edit Notice";
                        AddEditInstructionlbl.Text = "Make any changes to the notice below and click the Save Notice button. <b>N.B.</b> Editing the notice will instantly invalidate it, and it will need re-validating.";

                        ClassNotice NewNotice = new ClassNotice(NID);

                        if (CurrentUser.InGroup(ClassAppDetails.admingroup))
                        {
                            AdminCategorylst.Items.AddRange(ClassCategory.loadList());
                             
                            AdminPostedBytxt.Text = NewNotice.PostedBy + " (" + NewNotice.PostedByID + ")";
                            AdminPostedDatetxt.Text = NewNotice.PostedDate.ToShortDateString();
                            AdminTitletxt.Text = NewNotice.Title;
                            AdminNoticetxt.Text = NewNotice.Notice;
                            AdminDisplayFromCal.SelectedDate = NewNotice.DisplayFrom;
                            AdminDisplayToCal.SelectedDate = NewNotice.DisplayTo;
                            AdminUrgentchk.Checked = NewNotice.Urgent;
                            if (NewNotice.Attachment != "")
                            {
                                AdminAttachmentURLlbl.Text = "<a href='" + NewNotice.Attachment + "'>View Attachment</a>";
                            }
                            AdminValidchk.Checked = NewNotice.Valid;
                            AdminInvalidReasontxt.Text = NewNotice.InvalidReason;
                            AdminCategorylst.SelectedValue = NewNotice.Category.ID.ToString();

                            MultiView.SetActiveView(AdminView);
                        }
                        else
                        {
                            Categorylst.Items.AddRange(ClassCategory.loadList());

                            Titletxt.Text = NewNotice.Title;
                            Noticetxt.Text = NewNotice.Notice;
                            DisplayFromCal.SelectedDate = NewNotice.DisplayFrom;
                            DisplayToCal.SelectedDate = NewNotice.DisplayTo;
                            Urgentchk.Checked = NewNotice.Urgent;
                            Categorylst.SelectedValue = NewNotice.Category.ID.ToString();

                            MultiView.SetActiveView(AddEditView);
                        }
                    }
                }
                else
                {
                    if (AID == 2)
                    {
                    
                        //Delete

                        ClassNotice Notice = new ClassNotice(NID);

                        Notice.Deleted = true;

                        Notice.Save();
                        
                    }

                    MultiView.SetActiveView(ListView);
                    LoadTable();
                }
            }
        }

        public void LoadTable()
        {
            DataSet DS;

            if (CurrentUser.InGroup(ClassAppDetails.admingroup))
            {
                
                if (VID == 1)
                {
                    Noticelbl.Text = "Validated Notices";
                    NoticesInstructionslbl.Text = "The list below shows the notices that are validated on the system.</p><p><a href=\"?vid=0\">View Unvalidated Notices</a>";
                    DS = ClassNotice.loadDataset(CurrentUser, 1);
                }
                else
                {
                    Noticelbl.Text = "Notices to Validate";
                    NoticesInstructionslbl.Text = "The list below shows the notices that currently require validation in the system.</p><p><a href=\"?vid=1\">View Validated Notices</a>";
                    DS = ClassNotice.loadDataset(CurrentUser, 0);
                }
                
            }
            else
            {
                Noticelbl.Text = "Your Notices";
                NoticesInstructionslbl.Text = "The list below shows the notices that you currently have in the system. From this screen you can add a notice, or edit one you currently have.";
                DS = ClassNotice.loadDataset(CurrentUser,2);
            }

            

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                ClassNotice Notice = new ClassNotice(DR);

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = Notice.Title;
                TC[1] = new TableCell();
                TC[1].Text = Notice.DisplayFrom.ToShortDateString();
                TC[2] = new TableCell();
                TC[2].Text = Notice.DisplayTo.ToShortDateString();
                TC[3] = new TableCell();
                TC[3].Text = Notice.Valid.ToString();
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&nid=" + Notice.ID + "'>Edit</a>";
                TC[5] = new TableCell();
                TC[5].Text = "<a href='?aid=2&nid=" + Notice.ID + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";
               
                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                NoticesTable.Rows.Add(TR);
            }
        }

        protected void SaveEventcmd_Click(object sender, EventArgs e)
        {
            //Create New Notice

            if (NID == -1)
            {
                //New Notice
                ClassNotice Notice = new ClassNotice();

                Notice.Title = Titletxt.Text;
                Notice.Notice = Noticetxt.Text;
                Notice.DisplayFrom = DisplayFromCal.SelectedDate;
                Notice.DisplayTo = DisplayToCal.SelectedDate;
                Notice.Urgent = Urgentchk.Checked;
                Notice.Category = new ClassCategory(Convert.ToInt16(Categorylst.SelectedValue.ToString()));
                Notice.Valid = false;

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Notice.PostedBy = UI.DisplayName;
                Notice.PostedByID = UI.StudentID;
                Notice.PostedByEmail = UI.Mail;
                Notice.PostedDate = DateTime.Now;

                Notice.Create();

                if (Attachment.HasFile)
                {
                    try
                    {
                        Attachment.SaveAs(ClassAppDetails.uploaddir + "Notices/" + Notice.ID + Attachment.FileName);
                        Notice.Attachment = "Attachments/Notices/" +  Notice.ID + Attachment.FileName;
                    }
                    catch (Exception ex)
                    {
                        //Errorlbl.Text = "Error: " + ex.Message.ToString() + ".";
                    }
                }

                Notice.Save();

                Notice.sendAdminEmail();

            }
            else
            {
                //Edit Notice
                ClassNotice Notice = new ClassNotice(NID);

                Notice.Title = Titletxt.Text;
                Notice.Notice = Noticetxt.Text;
                Notice.DisplayFrom = DisplayFromCal.SelectedDate;
                Notice.DisplayTo = DisplayToCal.SelectedDate;
                Notice.Urgent = Urgentchk.Checked;
                Notice.Category = new ClassCategory(Convert.ToInt16(Categorylst.SelectedValue.ToString()));
                Notice.Valid = false;

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Notice.PostedBy = UI.DisplayName;
                Notice.PostedByID = UI.StudentID;
                Notice.PostedByEmail = UI.Mail;
                Notice.PostedDate = DateTime.Now;

                if (Attachment.HasFile)
                {
                    try
                    {
                        Attachment.SaveAs(ClassAppDetails.uploaddir + "Notices/" + Notice.ID + Attachment.FileName);
                        Notice.Attachment = "Attachments/Notices" + Notice.ID + Attachment.FileName;
                    }
                    catch (Exception ex)
                    {
                        //Errorlbl.Text = "Error: " + ex.Message.ToString() + ".";
                    }
                }

                Notice.Save();
            }

            Response.Redirect("notices.aspx");
        }

        protected void AdminSavecmd_Click(object sender, EventArgs e)
        {
            ClassNotice CN = new ClassNotice(NID);

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            CN.Title = AdminTitletxt.Text;
            CN.Notice = AdminNoticetxt.Text;
            CN.DisplayFrom = AdminDisplayFromCal.SelectedDate;
            CN.DisplayTo = AdminDisplayToCal.SelectedDate;
            CN.Urgent = AdminUrgentchk.Checked;
            CN.Category = new ClassCategory(Convert.ToInt16(AdminCategorylst.SelectedValue.ToString()));

            if (AdminValidchk.Checked == true)
            {
                CN.Valid = true;
            }
            else
            {
                CN.Valid = false;
            }

            CN.ValidatedBy = UI.DisplayName + " (" + UI.StudentID + ")";
            CN.ValidatedDate = DateTime.Now;
            CN.InvalidReason = AdminInvalidReasontxt.Text;

            CN.Save();

            CN.sendEmail();

            Response.Redirect("notices.aspx");
        }

       
     
    }
}
