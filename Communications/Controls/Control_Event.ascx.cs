using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using System.Web.Services;

namespace Communications.Controls
{
    public partial class Control_Event : System.Web.UI.UserControl
    {
        private int  EID = 0;
        private string cmd = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int.TryParse(Request["id"], out EID);

            if (Request["cmd"] == "Event")
            {

                String Action = Request["act"];

                if (Action == "Delete")
                {
                    ClassEvents Event = new ClassEvents(EID);

                    Titlelbl.Text = "Delete Event";

                    Deletelbl.Text = "Are you sure you want to delete the event titled " + Event.Title + "?";

                    Multiview.SetActiveView(DeleteView);

                }
                else
                {

                    Multiview.SetActiveView(AddEditView);

                    if (EID == 0)
                    {

                        Titlelbl.Text = "Add Event";
                        Control_Base.Mode = Communications.Control_Base.ModeType.Add;

                    }
                    else
                    {

                        if (!IsPostBack)
                        {

                            Titlelbl.Text = "Edit Event";
                            Control_Base.Mode = Communications.Control_Base.ModeType.Edit;

                            //Load up the form

                            ClassEvents Event = new ClassEvents(EID);

                            Control_Base.BaseID = Event.BaseID;
                            Control_Base.UpdateBase(Event.GetBase());
                            Control_Base.Location = Event.Location;
                            Control_Base.Time = Event.EventTime;

                        }

                    }
                }

            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            //Save the content

            ClassEvents Event = new ClassEvents();         

            if(EID == 0){
                
                Event.SetBase(Control_Base.UpdateClass(new ClassCommsBase()));

                Event.DisplayFrom = DateTime.Now;
                Event.Location = Control_Base.Location;
                Event.EventTime = Control_Base.Time;

                Event.Create();

                Savelbl.Text = "The Event has been created and submitted for approval.";

                Event.sendEmail(ClassCommsBase.CommsType.Event, ClassCommsBase.EmailType.Add, Event.ID, false, Event.PostedByEmail);
                Event.sendEmail(ClassCommsBase.CommsType.Event, ClassCommsBase.EmailType.Add, Event.ID, true, ClassAppDetails.adminemail);

            } else {

                Event = new ClassEvents(EID);

                ClassCommsBase Base = new ClassCommsBase();
                Base.LoadBaseFromID(Event.BaseID);

                Event.SetBase(Control_Base.UpdateClass(Base));

                Event.Save();
                Savelbl.Text = "The Event has been updated and saved succesfully.";
            }

            GenerateXML();

            
            Multiview.SetActiveView(SavedView);

        }


        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.CommandName == "Yes")
            {
                ClassEvents Event = new ClassEvents(EID);

                Event.Deleted = true;

                Event.Save();

                GenerateXML();
            }

            Response.Redirect("default.aspx");
        }

        protected void Okcmd_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }

        protected void Base_Validated(object sender, EventArgs e)
        {
            //Show Saved
            ClassEvents Event = new ClassEvents(EID);

            Event.sendEmail(ClassCommsBase.CommsType.Event, ClassCommsBase.EmailType.Valid, EID, false, Event.PostedByEmail);

            GenerateXML();

            Savelbl.Text = "The Event has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void Base_Invalidated(object sender, EventArgs e)
        {
            //Show Saved
            GenerateXML();

            Savelbl.Text = "The Event has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void GenerateXML()
        {
            PCWS.PocketCampusWebServicesCommunications WebServices = new PCWS.PocketCampusWebServicesCommunications();

            WebServices.Feeds();
        }

    }
}