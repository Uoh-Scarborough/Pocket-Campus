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
    public partial class Events : System.Web.UI.Page
    {
        public int AID, EID, VID;

        ClassConnection NC;
        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

            AID = Convert.ToInt32(Request["aid"]);
            EID = Convert.ToInt32(Request["eid"]);
            VID = Convert.ToInt32(Request["vid"]);

            Categorylst.Items.AddRange(ClassCategory.loadList());

            if (!Page.IsPostBack)
            {
                if (AID == 1)
                {

                    if (EID == -1)
                    {
                        //Add
                        AddEditlbl.Text = "Add Event";
                        AddEditInstructionlbl.Text = "Complete the form below and click the Save Event button to add a new event to the system. <b>N.B.</b> Before the event goes live it will be validated.</p>";
                        Categorylst.Items.AddRange(ClassCategory.loadList());
                        MultiView.SetActiveView(AddEditView);
                    }
                    else
                    {
                        //Edit

                        //Get Notice
                        AddEditlbl.Text = "Edit Event";
                        AddEditInstructionlbl.Text = "Make any changes to the notice below and click the Save Event button. <b>N.B.</b> Editing the event will instantly invalidate it, and it will need re-validating.";

                        ClassEvent NewEvent = new ClassEvent(EID);

                        if (CurrentUser.InGroup(ClassAppDetails.admingroup))
                        {
             
                            AdminCategorylst.Items.AddRange(ClassCategory.loadList());
                             
                            AdminPostedBytxt.Text = NewEvent.PostedBy + " (" + NewEvent.PostedByID + ")";
                            AdminPostedDatetxt.Text = NewEvent.PostedDate.ToShortDateString();
                            AdminTitletxt.Text = NewEvent.Title;
                            AdminEventtxt.Text = NewEvent.Event;
                            AdminLocationstxt.Text = NewEvent.Location;
                            AdminEventDateCal.SelectedDate = NewEvent.EventDateTime;
                            AdminEventTimetxt.Text = NewEvent.EventDateTime.ToShortTimeString();
                            AdminDurationtxt.Text = NewEvent.EventDuration;
                            AdminCategorylst.SelectedValue = NewEvent.Category.ID.ToString();
                            if (NewEvent.Attachment != "")
                            {
                                AdminAttachmentURLlbl.Text = "<a href='" + NewEvent.Attachment + "'>View Attachment</a>";
                            }
                            AdminValidchk.Checked = NewEvent.Valid;
                            AdminInvalidReasontxt.Text = NewEvent.InvalidReason;

                            MultiView.SetActiveView(AdminView);
                        }
                        else
                        {
                           
                            Categorylst.Items.AddRange(ClassCategory.loadList());

                            Titletxt.Text = NewEvent.Title;
                            Eventtxt.Text = NewEvent.Event;
                            Locationtxt.Text = NewEvent.Location;
                            EventDateCal.SelectedDate = NewEvent.EventDateTime;
                            EventTimetxt.Text = NewEvent.EventDateTime.ToShortTimeString();
                            Durationtxt.Text = NewEvent.EventDuration;
                            Categorylst.SelectedValue = NewEvent.Category.ID.ToString();

                            MultiView.SetActiveView(AddEditView);
                        }
                    }
                }
                else
                {
                    if (AID == 2)
                    {
                    
                        //Delete

                        ClassEvent Event = new ClassEvent(EID);

                        Event.Deleted = true;

                        Event.Save();
                        
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
                    Eventlbl.Text = "Validated Events";
                    EventsInstructionslbl.Text = "The list below shows the events that are validated on the system.</p><p><a href=\"?vid=0\">View Unvalidated Events</a>";
                    DS = ClassEvent.loadDataset(CurrentUser, 1);
                }
                else
                {
                    Eventlbl.Text = "Event to Validate";
                    EventsInstructionslbl.Text = "The list below shows the events that currently require validation in the system.</p><p><a href=\"?vid=1\">View Validated Events</a>";
                    DS = ClassEvent.loadDataset(CurrentUser, 0);
                }
                
            }
            else
            {
                Eventlbl.Text = "Your Events";
                EventsInstructionslbl.Text = "The list below shows the events that you currently have in the system. From this screen you can add an event, or edit one you currently have.";
                DS = ClassEvent.loadDataset(CurrentUser,2);
            }

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                ClassEvent Event = new ClassEvent(DR);

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = Event.Title;
                TC[1] = new TableCell();
                TC[1].Text = Event.EventDateTime.ToShortDateString() + " " + Event.EventDateTime.ToShortTimeString();
                TC[2] = new TableCell();
                TC[2].Text = Event.Location;
                TC[3] = new TableCell();
                TC[3].Text = Event.Valid.ToString();
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&eid=" + Event.ID + "'>Edit</a>";
                TC[5] = new TableCell();
                TC[5].Text = "<a href='?aid=2&eid=" + Event.ID + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";
               
                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                EventsTable.Rows.Add(TR);
            }
        }

        protected void SaveNoticecmd_Click(object sender, EventArgs e)
        {
            //Create New Event

            if (EID == -1)
            {
                //New Event
                ClassEvent Event = new ClassEvent();

                Event.Title = Titletxt.Text;
                Event.Event = Eventtxt.Text;
                Event.Location = Locationtxt.Text;
                Event.EventDateTime = EventDateCal.SelectedDate;

                //Sort Out the Time
                String[] Times = EventTimetxt.Text.Split(':');

                Event.EventDateTime = Event.EventDateTime.AddHours(Convert.ToDouble(Times[0]));
                Event.EventDateTime = Event.EventDateTime.AddMinutes(Convert.ToDouble(Times[1]));

                Event.EventDuration = Durationtxt.Text;

                Event.Category = new ClassCategory(Convert.ToInt16(Categorylst.SelectedValue.ToString()));

                Event.Valid = false;

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Event.PostedBy = UI.DisplayName;
                Event.PostedByID = UI.StudentID;
                Event.PostedByEmail = UI.Mail;
                Event.PostedDate = DateTime.Now;

                Event.Create();

                if (Attachment.HasFile)
                {
                    try
                    {
                        Attachment.SaveAs(ClassAppDetails.uploaddir + "Events/" + Event.ID + Attachment.FileName);
                        Event.Attachment = "Attachments/Events/" + Event.ID + Attachment.FileName;
                    }
                    catch (Exception ex)
                    {
                        //Errorlbl.Text = "Error: " + ex.Message.ToString() + ".";
                    }
                }

                Event.Save();

                Event.sendAdminEmail();

            }
            else
            {
                //Edit Notice
                ClassEvent Event = new ClassEvent(EID);

                Event.Title = Titletxt.Text;
                Event.Event = Eventtxt.Text;
                Event.Location = Locationtxt.Text;                
                
                Event.EventDateTime = EventDateCal.SelectedDate;

                //Sort Out the Time
                String[] Times = EventTimetxt.Text.Split(':');

                Event.EventDateTime = Event.EventDateTime.AddHours(Convert.ToDouble(Times[0]));
                Event.EventDateTime = Event.EventDateTime.AddMinutes(Convert.ToDouble(Times[1]));

                Event.EventDuration = Durationtxt.Text;

                Event.Category = new ClassCategory(Convert.ToInt16(Categorylst.SelectedValue.ToString()));

                Event.Valid = false;

                ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

                Event.PostedBy = UI.DisplayName;
                Event.PostedByID = UI.StudentID;
                Event.PostedByEmail = UI.Mail;
                Event.PostedDate = DateTime.Now;

                if (Attachment.HasFile)
                {
                    try
                    {
                        Attachment.SaveAs(ClassAppDetails.uploaddir + "Events/" + Event.ID + Attachment.FileName);
                        Event.Attachment = "Attachments/Events/" + Event.ID + Attachment.FileName;
                    }
                    catch (Exception ex)
                    {
                        //Errorlbl.Text = "Error: " + ex.Message.ToString() + ".";
                    }
                }

                Event.Save();
            }

            Response.Redirect("events.aspx");
        }

        protected void Invalidatecmd_Click(object sender, EventArgs e)
        {
            ClassEvent Event = new ClassEvent(EID);

            Event.Title = AdminTitletxt.Text;
            Event.Event = AdminEventtxt.Text;

            Event.EventDateTime = EventDateCal.SelectedDate;

            //Sort Out the Time
            String[] Times = AdminEventTimetxt.Text.Split(':');

            Event.EventDateTime = Event.EventDateTime.AddHours(Convert.ToDouble(Times[0]));
            Event.EventDateTime = Event.EventDateTime.AddMinutes(Convert.ToDouble(Times[1]));

            Event.EventDuration = AdminDurationtxt.Text;

            Event.Valid = false;

            Event.Save();

            Response.Redirect("events.aspx");
        }

        protected void AdminSavecmd_Click(object sender, EventArgs e)
        {

            ClassEvent Event = new ClassEvent(EID);

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            Event.Title = AdminTitletxt.Text;
            Event.Event = AdminEventtxt.Text;

            Event.EventDateTime = AdminEventDateCal.SelectedDate;

            //Sort Out the Time
            String[] Times = AdminEventTimetxt.Text.Split(':');

            Event.EventDateTime = Event.EventDateTime.AddHours(Convert.ToDouble(Times[0]));
            Event.EventDateTime = Event.EventDateTime.AddMinutes(Convert.ToDouble(Times[1]));

            Event.EventDuration = AdminDurationtxt.Text;

            if (AdminValidchk.Checked == true)
            {
                Event.Valid = true;
            }
            else
            {
                Event.Valid = false;
            }

            Event.ValidatedBy = UI.DisplayName + " (" + UI.StudentID + ")";
            Event.ValidatedDate = DateTime.Now;
            Event.InvalidReason = AdminInvalidReasontxt.Text;

            Event.Save();

            Event.sendEmail();

            Response.Redirect("events.aspx");
        }

        protected void LogOutcmd_Click(object sender, EventArgs e)
        {

        }
     
    }
}
