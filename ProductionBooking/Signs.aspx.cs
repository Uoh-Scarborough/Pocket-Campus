using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using StandardClasses;
using PocketCampusClasses;

namespace ProductionBooking
{
    public partial class Signs : System.Web.UI.Page
    {

        public int AID, SID, STID;

        ClassConnection NC;


        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.bookingconnectionname);

            ClassAppDetails.bookingcurrentconnection = NC;

            AID = Convert.ToInt32(Request["aid"]);
            SID = Convert.ToInt32(Request["sid"]);
            STID = Convert.ToInt32(Request["stid"]);

            if (!Page.IsPostBack)
            {
                if (AID == 1)
                {
                    SetupSignTypes();
                    SetupTimes();
                    if (SID == -1)
                    {
                        //Add New

                        AddEditlbl.Text = "Add Sign";
                        AddEditInstructionlbl.Text = "Complete the form below and click the Save Sign button to add a new event to the system.";
                        MultiView.SetActiveView(AddEditView);
                    }
                    else
                    {
                        //Edit

                        AddEditlbl.Text = "Edit Sign";
                        AddEditInstructionlbl.Text = "Complete the form below and click the Save Sign button.";
                        MultiView.SetActiveView(AddEditView);

                        ClassSign Sign = new ClassSign(SID);

                        Roomcmb.Text = Sign.Room;
                        DisplayFromDate.SelectedDate = Sign.DisplayFrom;
                        DisplayFromTimecmb.SelectedValue = ClassGeneral.getTimeCode(Sign.DisplayFrom).ToString();                        
                        //DisplayFromTimecmb.SelectedValue = "10";
                        DisplayToDate.SelectedDate = Sign.DisplayTo;
                        DisplayToTimecmb.SelectedValue = ClassGeneral.getTimeCode(Sign.DisplayTo).ToString();
                        //DisplayToTimecmb.SelectedValue = "20";
                        Signcmb.SelectedValue = Sign.Sign.ID.ToString();
                    }
                }
                else
                {
                    if (AID == 2)
                    {
                        //Delete
                        ClassSign Sign = new ClassSign(SID);

                        Sign.Deleted = true;

                        Sign.Save();
                    }

                    //List

                    MultiView.SetActiveView(ListView);

                    LoadTable();

                }

            }
        }

        private void SetupSignTypes()
        {
            //Setup Course Venue
            Signcmb.Items.Clear();

            Signcmb.DataSource = ClassSignType.loadDataset();

            Signcmb.DataTextField = "SignType_Name";
            Signcmb.DataValueField = "SignType_ID_LNK";

            Signcmb.DataBind();
        }

        private void SetupTimes()
        {
            DisplayFromTimecmb.Items.Clear();
            DisplayToTimecmb.Items.Clear();

            for (int i = 0; i <= 95; i++)
            {
                int nTID = i;
                ListItem LI = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                ListItem LI1 = new ListItem(ClassGeneral.getTime(nTID), nTID.ToString());
                DisplayFromTimecmb.Items.Add(LI);
                DisplayToTimecmb.Items.Add(LI1);
            }

            
        }

        public void LoadTable()
        {
            DataSet DS;

            DS = ClassSign.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[5];
                TC[0] = new TableCell();
                TC[0].Text = DR[4].ToString();
                TC[1] = new TableCell();
                DateTime DT1 = (DateTime)DR[2];
                DateTime DT2 = (DateTime)DR[3];
                TC[1].Text = DT1.ToShortDateString() + " " + DT1.ToShortTimeString() + " - " + DT2.ToShortDateString() + " " + DT2.ToShortTimeString();
                TC[2] = new TableCell();
                TC[2].Text = DR[6].ToString();
                TC[3] = new TableCell();
                TC[3].Text = "<a href='?aid=1&sid=" + DR[0].ToString() + "'>Edit</a>";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=2&sid=" + DR[0].ToString() + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                SignsTable.Rows.Add(TR);
            }

        }

        protected void SaveSignTypecmd_Click(object sender, EventArgs e)
        {
            //Save Sign Type
            if (TempID.Value == "-1")
            {
                //New Sign
                ClassSignType SignType = new ClassSignType();

                SignType.Name = SignTitletxt.Text;
                SignType.FileType = "";

                SignType.Create();

                if (SignUpload.HasFile)
                {
                    try
                    {

                        //Get Extension

                        String ext = SignUpload.FileName.Substring(SignUpload.FileName.LastIndexOf('.'));

                        SignUpload.SaveAs(ClassAppDetails.uploaddir + "Signs/" + SignType.ID + ext);
                        SignType.FileType = ext;
                    }
                    catch (Exception ex)
                    {
                        //Do Nothing
                    }
                }

                SignType.Save();
            }
            else
            {
                //Replace Sign

                ClassSignType SignType = new ClassSignType(Convert.ToInt32(TempID.Value));

                SignType.Name = SignTitletxt.Text;

                if (SignUpload.HasFile)
                {
                    try
                    {

                        //Get Extension

                        String ext = SignUpload.FileName.Substring(SignUpload.FileName.LastIndexOf('.'));

                        SignUpload.SaveAs(ClassAppDetails.uploaddir + "Signs/" + SignType.ID + ext);
                        SignType.FileType = ext;
                    }
                    catch (Exception ex)
                    {
                        //Do Nothing
                    }
                }

                SignType.Save();

            }

            MultiView.SetActiveView(MessageView);
            MessageTitlelbl.Text = "Sign Type Added";
            Messagelbl.Text = "The Sign Type has been succesfully added, <a href=\"?aid=1&sid=" + SID + "\">Return to the Sign Management Screen</a>";

        }

        protected void DeleteSignTypecmd_Click(object sender, EventArgs e)
        {
            ClassSignType SignType = new ClassSignType(Convert.ToInt32(TempID.Value));

            SignType.Deleted = true;

            SignType.Save();

            MultiView.SetActiveView(MessageView);
            MessageTitlelbl.Text = "Sign Type Deleted";
            Messagelbl.Text = "The Sign Type has been succesfully deleted, <a href=\"?aid=1&sid=" + SID + "\">Return to the Sign Management Screen</a>";
        }

        protected void SaveSigncmd_Click(object sender, EventArgs e)
        {
            if (SID == -1)
            {
                //Add New

                ClassSign NewSign = new ClassSign();

                NewSign.Room = Roomcmb.SelectedItem.Text;
                NewSign.DisplayFrom = Convert.ToDateTime(DisplayFromDate.SelectedDate.ToShortDateString() + " " + DisplayFromTimecmb.SelectedItem.Text);
                NewSign.DisplayTo = Convert.ToDateTime(DisplayToDate.SelectedDate.ToShortDateString() + " " + DisplayToTimecmb.SelectedItem.Text);
                NewSign.Sign = new ClassSignType(Convert.ToInt32(Signcmb.SelectedValue));

                NewSign.Create();

                MultiView.SetActiveView(MessageView);
                MessageTitlelbl.Text = "Sign Saved";
                Messagelbl.Text = "The Sign has been succesfully saved, <a href=\"Signs.aspx\">Return to the Sign Management Home</a>";

            }
            else
            {
                //Edit

                ClassSign NewSign = new ClassSign(SID);

                NewSign.Room = Roomcmb.SelectedItem.Text;
                NewSign.DisplayFrom = Convert.ToDateTime(DisplayFromDate.SelectedDate.ToShortDateString() + " " + DisplayFromTimecmb.SelectedItem.Text);
                NewSign.DisplayTo = Convert.ToDateTime(DisplayToDate.SelectedDate.ToShortDateString() + " " + DisplayToTimecmb.SelectedItem.Text);
                NewSign.Sign = new ClassSignType(Convert.ToInt32(Signcmb.SelectedValue));

                NewSign.Save();

                MultiView.SetActiveView(MessageView);
                MessageTitlelbl.Text = "Sign Saved";
                Messagelbl.Text = "The Sign has been succesfully saved, <a href=\"Signs.aspx\">Return to the Sign Management Home</a>";
            }
        }

        protected void AddSignTypecmd_Click(object sender, ImageClickEventArgs e)
        {
            MultiView.SetActiveView(AddEditSignView);
            AddEditSignlbl.Text = "Add";
            AddEditSignInfolbl.Text = "To Add a new Sign Type to the system, please complete the form below and click the Save Sign Button.";
            DeleteSignTypecmd.Visible = false;
            TempID.Value = "-1";
        }

        protected void SignTypeEditcmd_Click(object sender, ImageClickEventArgs e)
        {
            MultiView.SetActiveView(AddEditSignView);
            
            AddEditSignlbl.Text = "Edit";
            AddEditSignInfolbl.Text = "To Edit this Sign Type, please update the form below and click the Save Sign Button.";
            DeleteSignTypecmd.Visible = true;

            ClassSignType SignType = new ClassSignType(Convert.ToInt32(Signcmb.SelectedValue));

            SignTitletxt.Text = SignType.Name;

            TempID.Value = Signcmb.SelectedValue ;

        }

        


    }
}