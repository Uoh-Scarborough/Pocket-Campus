using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StandardClasses;
using System.Data;
using PocketCampusClasses;

namespace Comms
{
    public partial class Videos : System.Web.UI.Page
    {
        public int AID, VID;

        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            AID = Convert.ToInt32(Request["aid"]);
            VID = Convert.ToInt32(Request["vid"]);

            if (!IsPostBack)
            {
                if (AID == 1)
                {
                    MultiView.ActiveViewIndex = 1;

                    if (VID == -1)
                    {
                        //Add
                        HiddenEditField.Value = "0";
                    }
                    else
                    {
                        //Edit
                        HiddenEditField.Value = VID.ToString();

                        ClassVideos Video = new ClassVideos(VID);

                        URLtxt.Text = Video.VideoURL;
                        ShowFromCal.SelectedDate = Video.ShowFrom;
                        ShowToCal.SelectedDate = Video.ShowTo;
                    }
                }
                else
                {
                    if (AID == 2)
                    {

                        //Delete

                        ClassVideos Video = new ClassVideos(VID);

                        Video.Deleted = true;

                        Video.Save();

                    }

                    MultiView.ActiveViewIndex = 0;
                    LoadTable();
                }
            }
            else
            {
                LoadTable();
            }

        }

        public void LoadTable()
        {
            DataSet DS;

            DS = ClassVideos.loadDataset();
            
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                ClassVideos Video = new ClassVideos(DR);

                TableCell[] TC = new TableCell[5];
                TC[0] = new TableCell();
                TC[0].Text = Video.VideoURL;
                TC[1] = new TableCell();
                TC[1].Text = Video.ShowFrom.ToShortDateString();
                TC[2] = new TableCell();
                TC[2].Text = Video.ShowTo.ToShortDateString();
                TC[3] = new TableCell();
                TC[3].Text = "<a href='?aid=1&wid=" + Video.ID + "'>Edit</a>";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=2&wid=" + Video.ID + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                VideoTable.Rows.Add(TR);
            }
        }

        protected void SaveVideocmd_Click(object sender, EventArgs e)
        {
            if (HiddenEditField.Value != "0")
            {
                //Save Changes
                ClassVideos Video = new ClassVideos(Convert.ToInt16(HiddenEditField.Value));

                Video.VideoURL = URLtxt.Text;
                Video.ShowFrom = ShowFromCal.SelectedDate;
                Video.ShowTo = ShowToCal.SelectedDate;

                Video.Save();

                CompletedHeaderlbl.Text = "Video Saved";
                Completedlbl.Text = "The Video you have edited has been succesfully saved to the system, and will go live at the appropriate time.";

                MultiView.ActiveViewIndex = 2;
            }
            else
            {
                //Create Video
                ClassVideos Video = new ClassVideos();

                Video.VideoURL = URLtxt.Text;
                Video.ShowFrom = ShowFromCal.SelectedDate;
                Video.ShowTo = ShowToCal.SelectedDate;
                
                Video.Create();

                CompletedHeaderlbl.Text = "Video Added";
                Completedlbl.Text = "The Video you have added has been succesfully added to the system, and will go live at the appropriate time.";

                MultiView.ActiveViewIndex = 2;
            }
        }

    }
 
}