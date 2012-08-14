using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using PocketCampusClasses;

namespace KDLBooking
{
    public partial class Restrict : System.Web.UI.Page
    {

        ClassConnection SBNC, TTNC;

        protected void Page_Load(object sender, EventArgs e)
        {

            SBNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);
            TTNC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.ttconnectionname);

            ClassAppDetails.bookingcurrentconnection = SBNC;
            ClassAppDetails.ttcurrentconnection = TTNC;

            //if (!IsPostBack)
            //{

                //Load Restricted Users

                ClassReadQuery RQ = new ClassReadQuery(SBNC);
                RQ.RunQuery("SELECT * FROM Restricts WHERE Restrict_Deleted = 0");

                DataSet DS = RQ.dataset;

                RestrictedUsersTable.Rows.Clear();

                TableHeaderRow HR = new TableHeaderRow();
                HR.BackColor = System.Drawing.Color.FromArgb(212, 186, 107);
                HR.ForeColor = System.Drawing.Color.White;
                HR.Font.Bold = true;

                TableHeaderCell NameCel = new TableHeaderCell();
                NameCel.Text = "Name";
                NameCel.Width = 300;
                NameCel.Font.Bold = true;

                TableHeaderCell RestrictedRoomCel = new TableHeaderCell();
                RestrictedRoomCel.Text = "Restricted Rooms";
                RestrictedRoomCel.Width = 300;
                RestrictedRoomCel.Font.Bold = true;

                TableHeaderCell ControlsCel = new TableHeaderCell();
                ControlsCel.Text = "Controls";
                ControlsCel.Font.Bold = true;
                ControlsCel.Width = 100;
                ControlsCel.ColumnSpan = 2;

                HR.Cells.Add(NameCel);
                HR.Cells.Add(RestrictedRoomCel);
                HR.Cells.Add(ControlsCel);

                RestrictedUsersTable.Rows.Add(HR);

                foreach (DataRow DR in DS.Tables[0].Rows)
                {
                    TableRow TR = new TableRow();

                    TableCell[] TC = new TableCell[4];

                    try
                    {
                        ClassUserInfo UI = new ClassUserInfo(DR[1].ToString().Trim());

                        TC[0] = new TableCell();
                        TC[0].Text = UI.DisplayName + " (" + DR[1].ToString().Trim() + ")";
                    }
                    catch(Exception ex)
                    {
                        TC[0] = new TableCell();
                        TC[0].Text = DR[1].ToString();
                    }
                    TC[1] = new TableCell();
                    TC[1].Text = ClassRestrict.GenerateRestrictedRooms(DR);

                    //Create Edit Button
                    Button EditBtn = new Button();
                    EditBtn.Text = "Edit";
                    EditBtn.ID = "Edit" + DR[1].ToString();
                    EditBtn.Click += new EventHandler(EditButton_Click);
                    EditBtn.BorderStyle = BorderStyle.None;
                    EditBtn.BackColor = System.Drawing.Color.White;
                    EditBtn.CssClass = "button";

                    //Create Print Button
                    Button DeleteBtn = new Button();
                    DeleteBtn.Text = "Delete";
                    DeleteBtn.ID = "Delete" + DR[1].ToString();
                    DeleteBtn.Click += new EventHandler(DeleteButton_Click);
                    DeleteBtn.BorderStyle = BorderStyle.None;
                    DeleteBtn.BackColor = System.Drawing.Color.White;
                    DeleteBtn.CssClass = "button";

                    TC[2] = new TableCell();
                    TC[2].Controls.Add(EditBtn);
                    TC[3] = new TableCell();
                    TC[3].Controls.Add(DeleteBtn);
                    
                    foreach (TableCell TabCel in TC)
                    {
                        TR.Cells.Add(TabCel);
                    }

                    RestrictedUsersTable.Rows.Add(TR);
                }


                MultiView.SetActiveView(ListView);
            //}
        }

        protected void Restrictcmd_Click(object sender, EventArgs e)
        {
            //Find User
            DisplayUser(UserIDtxt.Text);
           
        }

        public void DisplayUser(String UserID)
        {
            try
            {
                ClassUserInfo UI = new ClassUserInfo(UserID);

                UserIDHidden.Value = UserID;
                Nametxt.Text = UI.DisplayName;
                Emailtxt.Text = UI.Mail;

                int RestrictID = ClassRestrict.Exists(UserID);

                if (RestrictID > 0)
                {
                    //Found

                    ClassRestrict RestrictedUser = new ClassRestrict(RestrictID);

                    RecordingStudio1chk.Checked = RestrictedUser.RecordingStudio1;
                    RecordingStudio2chk.Checked = RestrictedUser.RecordingStudio2;
                    MusicRoomchk.Checked = RestrictedUser.TheMusicRoom;
                    RehearsalStudio1chk.Checked = RestrictedUser.RehearsalStudio1;
                    RehearsalStudio2chk.Checked = RestrictedUser.RehearsalStudio2;
                    ResearchStudiochk.Checked = RestrictedUser.ResearchStudio;
                    OverdubStudiochk.Checked = RestrictedUser.OverdubStudio;
                    SeminarWorkstation1chk.Checked = RestrictedUser.SeminarWorkstation1;
                    SeminarWorkstation2chk.Checked = RestrictedUser.SeminarWorkstation2;
                    SeminarWorkstation3chk.Checked = RestrictedUser.SeminarWorkstation3;
                    MixingStudio1chk.Checked = RestrictedUser.MixingStudio1;
                    MixingStudio2chk.Checked = RestrictedUser.MixingStudio2;
                    MixingStudio3chk.Checked = RestrictedUser.MixingStudio3;
                    ElectronicaStudiochk.Checked = RestrictedUser.ElectronicaStudio;
                    SeminarRoom2chk.Checked = RestrictedUser.SeminarRoom2;

                }

                MultiView.SetActiveView(UserView);
            }
            catch
            {
                MultiView.SetActiveView(UserNotFoundView);
            }
        }

        protected void Deletecmd_Click(object sender, EventArgs e)
        {
            String RID = RIDHidden.Value;

            ClassRestrict Restrict = new ClassRestrict(Convert.ToInt16(RID));

            Restrict.Deleted = true;

            Restrict.Save();

            MultiView.SetActiveView(Deleted);

        }

        protected void Savecmd_Click(object sender, EventArgs e)
        {
            int RestrictID = ClassRestrict.Exists(UserIDHidden.Value.ToString());

            if (RestrictID > 0)
            {
                //Update

                ClassRestrict RestrictedUser = new ClassRestrict(RestrictID);

                RestrictedUser.RecordingStudio1 = RecordingStudio1chk.Checked;
                RestrictedUser.RecordingStudio2 = RecordingStudio2chk.Checked;
                RestrictedUser.TheMusicRoom = MusicRoomchk.Checked;
                RestrictedUser.RehearsalStudio1 = RehearsalStudio1chk.Checked;
                RestrictedUser.RehearsalStudio2 = RehearsalStudio2chk.Checked;
                RestrictedUser.ResearchStudio = ResearchStudiochk.Checked;
                RestrictedUser.OverdubStudio = OverdubStudiochk.Checked;
                RestrictedUser.SeminarWorkstation1 = SeminarWorkstation1chk.Checked;
                RestrictedUser.SeminarWorkstation2 = SeminarWorkstation2chk.Checked;
                RestrictedUser.SeminarWorkstation3 = SeminarWorkstation3chk.Checked;
                RestrictedUser.MixingStudio1 = MixingStudio1chk.Checked;
                RestrictedUser.MixingStudio2 = MixingStudio2chk.Checked;
                RestrictedUser.MixingStudio3 = MixingStudio3chk.Checked;
                RestrictedUser.ElectronicaStudio = ElectronicaStudiochk.Checked;
                RestrictedUser.SeminarRoom2 = SeminarRoom2chk.Checked;

                if (!RecordingStudio1chk.Checked && !RecordingStudio2chk.Checked && !MusicRoomchk.Checked && !RecordingStudio1chk.Checked && !RehearsalStudio1chk.Checked && !RehearsalStudio2chk.Checked && !ResearchStudiochk.Checked && !OverdubStudiochk.Checked && !SeminarWorkstation1chk.Checked && !SeminarWorkstation2chk.Checked && !SeminarWorkstation3chk.Checked && !MixingStudio1chk.Checked && !MixingStudio2chk.Checked && !ElectronicaStudiochk.Checked && !MixingStudio3chk.Checked)
                {
                    RestrictedUser.Deleted = true;
                } 

                RestrictedUser.Save();

            }
            else
            {
                //Save New

                ClassRestrict RestrictedUser = new ClassRestrict();

                RestrictedUser.UserID = UserIDHidden.Value.ToString();

                RestrictedUser.RecordingStudio1 = RecordingStudio1chk.Checked;
                RestrictedUser.RecordingStudio2 = RecordingStudio2chk.Checked;
                RestrictedUser.TheMusicRoom = MusicRoomchk.Checked;
                RestrictedUser.RehearsalStudio1 = RehearsalStudio1chk.Checked;
                RestrictedUser.RehearsalStudio2 = RehearsalStudio2chk.Checked;
                RestrictedUser.ResearchStudio = ResearchStudiochk.Checked;
                RestrictedUser.OverdubStudio = OverdubStudiochk.Checked;
                RestrictedUser.SeminarWorkstation1 = SeminarWorkstation1chk.Checked;
                RestrictedUser.SeminarWorkstation2 = SeminarWorkstation2chk.Checked;
                RestrictedUser.SeminarWorkstation3 = SeminarWorkstation3chk.Checked;
                RestrictedUser.MixingStudio1 = MixingStudio1chk.Checked;
                RestrictedUser.MixingStudio2 = MixingStudio2chk.Checked;
                RestrictedUser.MixingStudio3 = MixingStudio3chk.Checked;
                RestrictedUser.ElectronicaStudio = ElectronicaStudiochk.Checked;
                RestrictedUser.SeminarRoom2 = SeminarRoom2chk.Checked;

                RestrictedUser.Create();

            }

            MultiView.SetActiveView(Completed);
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button Btn = new Button();

            if (sender.GetType() == Btn.GetType())
            {
                Btn = (Button)sender;

                DisplayUser(Btn.ID.Substring(4).Trim());
            }
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button Btn = new Button();

            if (sender.GetType() == Btn.GetType())
            {
                Btn = (Button)sender;

                int RestrictID = ClassRestrict.Exists(Btn.ID.Substring(6).Trim());

                RIDHidden.Value = RestrictID.ToString();

                ClassReadQuery RQ = new ClassReadQuery(SBNC);
                RQ.RunQuery("SELECT * FROM Restricts WHERE Restrict_IDLNK = '" + RestrictID + "' AND Restrict_Deleted = 0");

                DeleteUserlbl.Text = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();
                DeleteRestrictionslbl.Text = ClassRestrict.GenerateRestrictedRooms(RQ.dataset.Tables[0].Rows[0]);

                MultiView.SetActiveView(DeleteView);
            }
        }

    }
}