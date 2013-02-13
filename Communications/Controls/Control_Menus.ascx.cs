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
    public partial class Control_Menus : System.Web.UI.UserControl
    {
        private int  MID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int.TryParse(Request["id"], out MID);

            if (Request["cmd"] == "Menu")
            {

                String Action = Request["act"];

                if (Action == "Delete")
                {
                    ClassMenu Menu = new ClassMenu(MID);

                    Titlelbl.Text = "Delete Menu";

                    Deletelbl.Text = "Are you sure you want to delete the notice titled " + Menu.Title + "?";

                    Multiview.SetActiveView(DeleteView);

                }
                else
                {

                    Multiview.SetActiveView(AddEditView);

                    if (MID == 0)
                    {

                        Titlelbl.Text = "Add Menu";
                        Control_Base.Mode = Communications.Control_Base.ModeType.Add;

                    }
                    else
                    {

                        Titlelbl.Text = "Edit Menu";
                        Control_Base.Mode = Communications.Control_Base.ModeType.Edit;

                        //Load up the form

                        if (!IsPostBack)
                        {

                            ClassMenu Menu = new ClassMenu(MID);

                            Control_Base.BaseID = Menu.BaseID;
                            Control_Base.UpdateBase(Menu.GetBase());

                        }

                    }
                }
            }
        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {

            //Save the content

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassMenu Menu = new ClassMenu();

            if(MID == 0){

                Menu.SetBase(Control_Base.UpdateClass(new ClassCommsBase()));

                Menu.Valid = true;
                Menu.ValidatedBy = UI.DisplayName;
                Menu.ValidatedDate = DateTime.Now;

                switch (Menu.Type)
	            {
		            case ClassMenu.menuTypes.Breakfast:
                        Menu.Title = Menu.DisplayTo.ToShortDateString() + " (Breakfast)";
                        break;
                    case ClassMenu.menuTypes.Lunch:
                        Menu.Title = Menu.DisplayTo.ToShortDateString() + " (Lunch)";
                        break;
                    case ClassMenu.menuTypes.Dinner:
                        Menu.Title = Menu.DisplayTo.ToShortDateString() + " (Dinner)";
                        break;
                    default:
                        break;
	           }

                //Menu.Title = Menu.DisplayTo.ToShortDateString() + " " + Menu.Type;
                Menu.DisplayFrom = Menu.DisplayTo;
                Menu.Type = (ClassMenu.menuTypes)Control_Base.MenuType;
                Menu.Recurrence = Control_Base.Recurrence;
                Menu.Category = new ClassCategory(14);

                Menu.Create();

                Savelbl.Text = "The Menu has been created.";

                Menu.sendEmail(ClassCommsBase.CommsType.Menu, ClassCommsBase.EmailType.Add, Menu.ID, false, Menu.PostedByEmail);
                Menu.sendEmail(ClassCommsBase.CommsType.Menu, ClassCommsBase.EmailType.Add, Menu.ID, true, ClassAppDetails.adminemail);

            } else {

                Menu = new ClassMenu(MID);

                ClassCommsBase Base = new ClassCommsBase();
                Base.LoadBaseFromID(Menu.BaseID);

                Menu.SetBase(Control_Base.UpdateClass(Base));

                Menu.Valid = true;
                Menu.ValidatedBy = UI.DisplayName;
                Menu.ValidatedDate = DateTime.Now;

                Menu.Title = Menu.DisplayTo.ToShortDateString() + " " + Menu.Type;
                Menu.DisplayFrom = Menu.DisplayTo;
                Menu.Type = (ClassMenu.menuTypes)Control_Base.MenuType;
                Menu.Recurrence = Control_Base.Recurrence;
                Menu.Category = new ClassCategory(14);

                Menu.Save();
                Savelbl.Text = "The Menu has been updated.";
            }

            GenerateXML();

            
            Multiview.SetActiveView(SavedView);

        }


        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.CommandName == "Yes")
            {
                ClassMenu Menu = new ClassMenu(MID);

                Menu.Deleted = true;

                Menu.Save();

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
            ClassMenu Menu = new ClassMenu(MID);

            Menu.sendEmail(ClassCommsBase.CommsType.Menu, ClassCommsBase.EmailType.Valid, MID, false, Menu.PostedByEmail);

            GenerateXML();

            Savelbl.Text = "The Menu has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void Base_Invalidated(object sender, EventArgs e)
        {
            //Show Saved
            GenerateXML();

            Savelbl.Text = "The Menu has been updated and saved succesfully.";
            Multiview.SetActiveView(SavedView);
        }

        protected void GenerateXML()
        {
            PCWS.PocketCampusWebServicesCommunications WebServices = new PCWS.PocketCampusWebServicesCommunications();

            WebServices.Feeds();
        }

    }
}