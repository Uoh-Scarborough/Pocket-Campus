using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;

namespace ProductionBooking
{
    public partial class Constraints : System.Web.UI.Page
    {
        ClassConnection PBNC;



        protected void Page_Load(object sender, EventArgs e)
        {

            PBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            
            ClassAppDetails.bookingcurrentconnection = PBNC;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            if (!ClassGroupMembers.IsAdmin(UI.Username))
            {
                Response.Redirect("default.aspx");
            }
            else
            {

                LoadList();

                if (!IsPostBack)
                {
                    MultiView.SetActiveView(ConstaintsList);


                }
            }
           
        }

        protected void LoadList()
        {
            ConstraintsGrid.DataSource = ClassConstraint.loadDataset();
            ConstraintsGrid.DataKeyNames = new string[] { "Constraint_ID_LNK" };
            ConstraintsGrid.DataBind();
        }

        protected void AddConstraintlbtn_Click(object sender, EventArgs e)
        {

            LoadStartEndTimeList();

            AddConstraintbtn.CommandArgument = "Add";

            MultiView.SetActiveView(AddEdit);
        }

        private void LoadStartEndTimeList()
        {
            StartBookingDDL.Items.Clear();
            EndBookingDDL.Items.Clear();

            for (int i = 32; i <= 93; i++)
            {
                int nTID = i;
                ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                StartBookingDDL.Items.Add(LI);
                ListItem LI1 = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                EndBookingDDL.Items.Add(LI1);
            }
        }

        protected void AddConstraintbtn_Click(object sender, EventArgs e)
        {
            //Check for Duplicate Constraint Name

            if(AddConstraintbtn.CommandArgument == "Add")
            {

                if (ClassConstraint.Exists(ConstraintTitletxt.Text.Trim()))
                {
                    Errorlbl.Text = "A constraint with this name already exists, please enter a different name.";
                    Errorlbl.Visible = true;
                }
                else
                {
                    //Start Add Processs.

                    ClassConstraint Constraint = new ClassConstraint();
                    
                    Constraint = UpdateConstraintClass(Constraint);

                    Constraint.Create();

                    DialogTitle.Text = "Constraint Added";
                    DialogText.Text = "The new constraint has been succesfully added to the system.";

                    MultiView.SetActiveView(Dialog);
                }
            } else {

                //Start Edit Process.

                ClassConstraint Constraint = new ClassConstraint(Convert.ToInt32(ConstraintIDHid.Value.ToString()));

                Constraint = UpdateConstraintClass(Constraint);

                Constraint.Save();

                DialogTitle.Text = "Constraint Saved";
                DialogText.Text = "The constraint has been succesfully saved.";

                MultiView.SetActiveView(Dialog);

            }
           
        }

        private ClassConstraint UpdateConstraintClass(ClassConstraint Constraint)
        {

            Constraint.Title = ConstraintTitletxt.Text;
            Constraint.StartDate = StartDateCal.SelectedDate;
            Constraint.EndDate = EndDateCal.SelectedDate;
            Constraint.Type = Convert.ToInt16(ConstraintTypelst.SelectedValue);
            try
            {
                Constraint.Value = Convert.ToInt16(Valuetxt.Text);
            }
            catch
            {
                Constraint.Value = 0;
            }
                
            Constraint.BookableStart = Convert.ToInt16(StartBookingDDL.SelectedValue);
            Constraint.BookableEnd = Convert.ToInt16(EndBookingDDL.SelectedValue);

            //Add Rooms

            Constraint.Rooms = new System.Collections.ArrayList();

            foreach (ListItem LI in RoomList.Items)
            {
                if (LI.Selected)
                {
                    Constraint.Rooms.Add(LI.Value);
                }
            }

            return Constraint;

        }

        protected void ConstraintTypelst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConstraintTypelst.SelectedValue == "0")
            {
                StartEndPanel.Visible = true;
                ValuePanel.Visible = false;
            }
            else
            {
                StartEndPanel.Visible = false;
                ValuePanel.Visible = true;
            }

            if (ConstraintTypelst.SelectedValue == "3")
            {
                Restrictionlbl.Text = "(Weeks)";
            }
            else
            {
                Restrictionlbl.Text = "(Minutes)";
            }

        }

        protected void ConstraintsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ViewConstraint" || e.CommandName == "DeleteConstraint")
            {

                Int16 rIndex = Convert.ToInt16(e.CommandArgument.ToString());

                Int16 CID = Convert.ToInt16(ConstraintsGrid.DataKeys[rIndex].Value.ToString());

                if (e.CommandName == "ViewConstraint")
                {
                    //Show

                    LoadStartEndTimeList();
                    MultiView.SetActiveView(AddEdit);

                    ClassConstraint Constraint = new ClassConstraint(CID);

                    ConstraintTitletxt.Text = Constraint.Title;
                    StartDateCal.SelectedValue = Constraint.StartDate;
                    EndDateCal.SelectedValue = Constraint.EndDate;
                    ConstraintTypelst.SelectedIndex = Constraint.Type;
                    ConstraintTypelst_SelectedIndexChanged(sender, e);
                    Valuetxt.Text = Constraint.Value.ToString();
                    StartBookingDDL.ClearSelection();
                    StartBookingDDL.Items.FindByValue(Constraint.BookableStart.ToString()).Selected = true;
                    EndBookingDDL.ClearSelection();
                    EndBookingDDL.Items.FindByValue(Constraint.BookableEnd.ToString()).Selected = true;

                    foreach (ListItem LI in RoomList.Items)
                    {
                        if (Constraint.Rooms.Contains(LI.Text))
                        {
                            LI.Selected = true;
                        }
                    }

                    ConstraintIDHid.Value = CID.ToString();

                    AddConstraintbtn.CommandArgument = "Edit";
                    AddConstraintbtn.Text = "Edit Constraint";
                }
                else
                {
                    //Delete

                    ClassConstraint Constraint = new ClassConstraint(CID);

                    Constraint.Deleted = true;

                    Constraint.Save();

                    MultiView.SetActiveView(ConstaintsList);

                    LoadList();

                }

            }

        }

        protected void Homebtn_Click(object sender, EventArgs e)
        {
            MultiView.SetActiveView(ConstaintsList);
        }

        protected void ConstraintsGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataView DV = new DataView(ClassConstraint.loadDataset().Tables[0]);

            DV.Sort = e.SortExpression;

            ConstraintsGrid.DataSource = DV;
           
            ConstraintsGrid.DataKeyNames = new string[] { "Constraint_ID_LNK" };
            ConstraintsGrid.DataBind();
        }

  



    }
}