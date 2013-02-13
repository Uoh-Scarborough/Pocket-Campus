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
    public partial class Control_Ticker : System.Web.UI.UserControl
    {
        private int  TID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int.TryParse(Request["id"], out TID);

            if (Request["cmd"] == "Ticker")
            {

                String Action = Request["act"];

                if (Action == "Delete")
                {
                    ClassTicker Ticker = new ClassTicker(TID);

                    Titlelbl.Text = "Delete Ticker";

                    Deletelbl.Text = "Are you sure you want to delete the ticker titled " + Ticker.Title + "?";

                    Multiview.SetActiveView(DeleteView);

                }
                else
                {

                    Multiview.SetActiveView(AddEditView);

                    if (TID == 0)
                    {

                        Titlelbl.Text = "Add Ticker";
                        Control_Base.Mode = Communications.Control_Base.ModeType.Add;

                    }
                    else
                    {

                        if (!IsPostBack)
                        {

                            Titlelbl.Text = "Edit Ticker";
                            Control_Base.Mode = Communications.Control_Base.ModeType.Edit;

                            //Load up the form

                            ClassTicker Ticker = new ClassTicker(TID);

                            Control_Base.BaseID = Ticker.BaseID;
                            Control_Base.UpdateBase(Ticker.GetBase());

                        }

                    }
                }
            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            //Save the content

            ClassTicker Ticker = new ClassTicker();

           

            if(TID == 0){

                Ticker.SetBase(Control_Base.UpdateClass(new ClassCommsBase()));

                Ticker.Category = new ClassCategory(18);

                Ticker.Create();

                Savelbl.Text = "The Ticker has been created and submitted for approval.";

                Ticker.sendEmail(ClassCommsBase.CommsType.Ticker, ClassCommsBase.EmailType.Add, Ticker.ID, false, Ticker.PostedByEmail);
                Ticker.sendEmail(ClassCommsBase.CommsType.Ticker, ClassCommsBase.EmailType.Add, Ticker.ID, true, ClassAppDetails.adminemail);

            } else {

                Ticker = new ClassTicker(TID);

                ClassCommsBase Base = new ClassCommsBase();
                Base.LoadBaseFromID(Ticker.BaseID);

                Ticker.SetBase(Control_Base.UpdateClass(Base));

                Ticker.Category = new ClassCategory(18);

                Ticker.Save();
                Savelbl.Text = "The Ticker has been updated and saved succesfully.";
            }

            GenerateXML();

            
            Multiview.SetActiveView(SavedView);

        }


        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.CommandName == "Yes")
            {
                ClassTicker Ticker = new ClassTicker(TID);

                Ticker.Deleted = true;

                Ticker.Save();

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
            ClassTicker Ticker = new ClassTicker(TID);

            Ticker.sendEmail(ClassCommsBase.CommsType.Ticker, ClassCommsBase.EmailType.Valid, TID, false, Ticker.PostedByEmail);

            GenerateXML();

            Savelbl.Text = "The Ticker has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void Base_Invalidated(object sender, EventArgs e)
        {
            //Show Saved
            GenerateXML();

            Savelbl.Text = "The Ticker has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void GenerateXML()
        {
            PCWS.PocketCampusWebServicesCommunications WebServices = new PCWS.PocketCampusWebServicesCommunications();

            WebServices.Feeds();
        }

    }
}