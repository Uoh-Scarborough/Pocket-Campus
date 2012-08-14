using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using PocketCampusClasses;

namespace Comms
{
    public partial class Slideshow : System.Web.UI.Page
    {

        public int AID, SID, SSID;

        ClassConnection NC;
        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.commsconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            CurrentUser = new ClassUserInfo(Context.User.Identity.Name);

            AID = Convert.ToInt32(Request["aid"]);
            SID = Convert.ToInt32(Request["sid"]);
            SSID = Convert.ToInt32(Request["ssid"]);

            if (!Page.IsPostBack)
            {
                if (AID == 1)
                {

                    if (SID == -1)
                    {
                        //Add
                        AddEditlbl.Text = "Add SlideShow";
                        AddEditInstructionlbl.Text = "Complete the form below and click the Save Slideshow button to add a new slideshow to the system. N.B. You cannot add slides until you have saved the slideshow.</p>";
                        MultiView.SetActiveView(AddEditView);
                    }
                    else
                    {
                        //Edit
                        AddEditlbl.Text = "Edit Slideshow";
                        AddEditInstructionlbl.Text = "Make any changes to the slides below and click the Save Slideshow button.";

                        ClassSlideshow NewSlideShow = new ClassSlideshow(SSID);

                        Titletxt.Text = NewSlideShow.Title;
                        DisplayFromCal.SelectedDate = NewSlideShow.DisplayFrom;
                        DisplayToCal.SelectedDate = NewSlideShow.DisplayTo;

                        MultiView.SetActiveView(AddEditView);
                    }
                }
                else
                {
                    if (AID == 2)
                    {

                        //Delete

                        ClassSlideshow SlideShow = new ClassSlideshow(SSID);

                        SlideShow.Deleted = true;

                        SlideShow.Save();

                    }

                    MultiView.SetActiveView(ListView);
                    //LoadTable();
                }
            }
            else
            {
                MultiView.SetActiveView(ListView);
                LoadTable();
            }

            //MultiView.SetActiveView(AddEditView);
        }

        public void LoadTable()
        {
            DataSet DS;

            Slideshowlbl.Text = "Slideshows";
            SlideshowInstructionslbl.Text = "The list below shows the slideshows on the system.";
            DS = ClassSlideshow.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                ClassSlideshow Slideshow = new ClassSlideshow(DR);

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = Slideshow.Title;
                TC[1] = new TableCell();
                TC[1].Text = Slideshow.DisplayFrom.ToShortDateString();
                TC[2] = new TableCell();
                TC[2].Text = Slideshow.DisplayTo.ToShortDateString();
                TC[3] = new TableCell();
                TC[3].Text = "<a href='?aid=1&eid=" + Slideshow.ID + "'>Edit</a>";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=2&eid=" + Slideshow.ID + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                SlideshowTable.Rows.Add(TR);
            }
        }

        protected void SaveSlideshowcmd_Click(object sender, EventArgs e)
        {

        }
    }
}