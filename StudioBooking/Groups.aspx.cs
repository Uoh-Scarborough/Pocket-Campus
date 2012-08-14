using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using StandardClasses;

namespace StudioBooking
{
    public partial class Groups : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    MultiView.SetActiveView(ViewGroupList);
                }
                else
                {
                    if (Convert.ToBoolean(ViewState["MembersTableLoaded"]) && (!Convert.ToBoolean(ViewState["ShowSearch"])))
                    {
                        MultiView.SetActiveView(ViewGroupMembers);
                        LoadGroupMembers();
                    }
                }
            }
        }

        protected void SearchCtl_ObjectChossen(object sender, EventArgs e) 
        {
            ClassGroupMembers Members = new ClassGroupMembers();

            Members.UserID = SearchCtl.UserID;
            Members.GroupID = Convert.ToInt16(ViewState["GroupID"]);
            Members.Create();

            MultiView.SetActiveView(ViewGroupMembers);
            LoadGroupMembers();
        
        }

        protected void AddGrouplbtn_Click(object sender, EventArgs e)
        {
            MultiView.SetActiveView(ViewAddGroup);
        }

        protected void AddGroupbtn_Click(object sender, EventArgs e)
        {
            //Add Group to DB.

            if (!ClassGroup.Exists(GroupNametxt.Text.Trim()))
            {
                ClassGroup NewGroup = new ClassGroup();
                NewGroup.Name = GroupNametxt.Text;
                NewGroup.Create();

                SuccessHeaderlbl.Text = "Group Added";
                SuccessMainlbl.Text = "The Group has been successfully added.";

                MultiView.SetActiveView(ViewSuccess);
            }
            else
            {
                Errorlbl.Text = "A Group with this name already exists. Please choose enter another name.";
                Errorlbl.Visible = true;
            }
        }

        protected void ViewGroupList_Load(object sender, EventArgs e)
        {
            //Load Groups to Table

            DataSet Groups = ClassGroup.loadDataset();

            GroupsGrid.DataSource = Groups;
            GroupsGrid.DataKeyNames = new string[] { "Group_ID_LNK" };
            GroupsGrid.DataBind();

            

        }

        protected void Homebt_OnClick(object sender, EventArgs e)
        {
            MultiView.SetActiveView(ViewGroupList);
        }

        protected void LoadGroupMembers()
        {
            //Clear

            int GroupID = Convert.ToInt16(ViewState["GroupID"]);

            MembersGrid.DataSource = ClassGroupMembers.LoadDataset(GroupID);
            MembersGrid.DataKeyNames = new string[] { "ID" };
            MembersGrid.DataBind();

            ViewState["MembersTableLoaded"] = true;
            ViewState["ShowSearch"] = false;

            ClassGroup Group = new ClassGroup(GroupID);

            GroupNamelbl.Text = Group.Name;
        }

        protected void AddMemberlbtn_OnClick(object sender, EventArgs e)
        {
            ViewState["ShowSearch"] = true;
            MultiView.SetActiveView(ViewAddGroupMember);
        }

        protected void GroupsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            Int16 rIndex = Convert.ToInt16(e.CommandArgument.ToString());

            Int16 GID = Convert.ToInt16(GroupsGrid.DataKeys[rIndex].Value.ToString());

            if (e.CommandName == "ViewGroup")
            {

                ViewState["GroupID"] = GID;

                MultiView.SetActiveView(ViewGroupMembers);

                LoadGroupMembers();

            }
            else if (e.CommandName == "DeleteGroup")
            {
                ViewState["GroupID"] = GID;

                ClassGroup Group = new ClassGroup(GID);

                Group.Deleted = true;

                Group.Save();

                MultiView.SetActiveView(ViewGroupList);

                ViewGroupList_Load(sender,e);
                
            }
            else if (e.CommandName == "GroupConstraints")
            {
                //Show Group Constraints

                ViewState["GroupID"] = GID;

                MultiView.SetActiveView(ViewGroupConstraints);

                LoadConstraints(GID);

            }

        }

        private void LoadConstraints(int GID)
        {
      
            ClassGroup Group = new ClassGroup(GID);

            GroupNamelbl1.Text = Group.Name;

            LoadList(Group, StudioClosureList, ClassConstraint.loadDataset(0));
            LoadList(Group, WeeklyAllowanceList, ClassConstraint.loadDataset(1));
            LoadList(Group, DurationList, ClassConstraint.loadDataset(2));
            LoadList(Group, BookingRangeList, ClassConstraint.loadDataset(3));

        }

        private void LoadList(ClassGroup Group,CheckBoxList CBL, DataSet DS)
        {
            CBL.Items.Clear();

            foreach (DataRow Constraint in DS.Tables[0].Rows)
            {
                ListItem LI = new ListItem();

                LI.Text = Constraint["Constraint_Title"].ToString();
                LI.Value = Constraint["Constraint_ID_LNK"].ToString();

                if (Group.Constraints.Contains(Constraint["Constraint_ID_LNK"]))
                {
                    LI.Selected = true;
                }

                CBL.Items.Add(LI);
            }
        }

        protected void MembersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Int16 rIndex = Convert.ToInt16(e.CommandArgument.ToString());

            Int16 UID = Convert.ToInt16(MembersGrid.DataKeys[rIndex].Value.ToString());

            if (e.CommandName == "DeleteUser")
            {
                ClassGroupMembers.Delete(UID);

                MultiView.SetActiveView(ViewGroupMembers);

                LoadGroupMembers();
            }
        }

        protected void SaveConstraintsbtn_Click(object sender, EventArgs e)
        {
            //Save Constraints

            int GID = Convert.ToInt16(ViewState["GroupID"]);

            ClassGroup Group = new ClassGroup(GID);

            Group.Constraints = new ArrayList();

            CombineItems(StudioClosureList, Group);
            CombineItems(WeeklyAllowanceList, Group);
            CombineItems(DurationList, Group);
            CombineItems(BookingRangeList, Group);

            Group.Save();

            MultiView.SetActiveView(ViewSuccess);

            SuccessHeaderlbl.Text = "Constraints Saved";

            SuccessMainlbl.Text = "The selected constraints have been added to the group, and will be instantly active.";
        }


        private void CombineItems(CheckBoxList CBL, ClassGroup Group)
        {
            foreach (ListItem LI in CBL.Items)
            {
                if (LI.Selected)
                {
                    Group.Constraints.Add(LI.Value);
                }
            }
        }





    }
}