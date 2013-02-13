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
    public partial class Control_Notice : System.Web.UI.UserControl
    {
        private int  NID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int.TryParse(Request["id"], out NID);

            if (Request["cmd"] == "Notice")
            {

                String Action = Request["act"];

                if (Action == "Delete")
                {
                    ClassNotice Notice = new ClassNotice(NID);

                    Titlelbl.Text = "Delete Notice";

                    Deletelbl.Text = "Are you sure you want to delete the notice titled " + Notice.Title + "?";

                    Multiview.SetActiveView(DeleteView);

                }
                else
                {

                    Multiview.SetActiveView(AddEditView);

                    if (NID == 0)
                    {

                        Titlelbl.Text = "Add Notice";
                        Control_Base.Mode = Communications.Control_Base.ModeType.Add;

                    }
                    else
                    {

                        if (!IsPostBack)
                        {

                            Titlelbl.Text = "Edit Notice";
                            Control_Base.Mode = Communications.Control_Base.ModeType.Edit;

                            //Load up the form

                            ClassNotice Notice = new ClassNotice(NID);

                            Control_Base.BaseID = Notice.BaseID;
                            Control_Base.UpdateBase(Notice.GetBase());

                        }

                    }
                }
            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            //Save the content

            ClassNotice Notice = new ClassNotice();

           

            if(NID == 0){

                Notice.SetBase(Control_Base.UpdateClass(new ClassCommsBase()));

                Notice.Create();

                Savelbl.Text = "The Notice has been created and submitted for approval.";

                Notice.sendEmail(ClassCommsBase.CommsType.Notice, ClassCommsBase.EmailType.Add, Notice.ID, false, Notice.PostedByEmail);
                Notice.sendEmail(ClassCommsBase.CommsType.Notice, ClassCommsBase.EmailType.Add, Notice.ID, true, ClassAppDetails.adminemail);

            } else {

                Notice = new ClassNotice(NID);

                ClassCommsBase Base = new ClassCommsBase();
                Base.LoadBaseFromID(Notice.BaseID);

                Notice.SetBase(Control_Base.UpdateClass(Base));

                Notice.Save();
                Savelbl.Text = "The Notice has been updated and saved succesfully.";
            }

            GenerateXML();

            
            Multiview.SetActiveView(SavedView);

        }


        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.CommandName == "Yes")
            {
                ClassNotice Notice = new ClassNotice(NID);

                Notice.Deleted = true;

                Notice.Save();

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
            ClassNotice Notice = new ClassNotice(NID);

            Notice.sendEmail(ClassCommsBase.CommsType.Notice, ClassCommsBase.EmailType.Valid, NID, false, Notice.PostedByEmail);

            GenerateXML();

            Savelbl.Text = "The Notice has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void Base_Invalidated(object sender, EventArgs e)
        {
            //Show Saved
            GenerateXML();

            Savelbl.Text = "The Notice has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void GenerateXML()
        {
            PCWS.PocketCampusWebServicesCommunications WebServices = new PCWS.PocketCampusWebServicesCommunications();

            WebServices.Feeds();
        }

    }
}