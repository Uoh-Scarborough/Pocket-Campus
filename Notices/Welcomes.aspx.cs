using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using StandardClasses;
using PocketCampusClasses;

namespace Comms
{
    public partial class Welcomes : System.Web.UI.Page
    {
        public int AID, WID;

        ClassConnection NC;

        protected void Page_Load(object sender, EventArgs e)
        {
            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            AID = Convert.ToInt32(Request["aid"]);
            WID = Convert.ToInt32(Request["wid"]);

            if (!IsPostBack)
            {
                if (AID == 1)
                {
                    MultiView.ActiveViewIndex = 1;

                    if (WID == -1)
                    {
                        //Add
                        HiddenEditField.Value = "0";
                        loadScreens();
                    }
                    else
                    {
                        //Edit
                        HiddenEditField.Value = WID.ToString();
                        loadScreens();

                        ClassWelcome Welcome = new ClassWelcome(WID);

                        Titletxt.Text = Welcome.Title;
                        Messagetxt.Text = Welcome.Message;
                        ShowFromCal.SelectedDate = Welcome.ShowFrom;
                        ShowToCal.SelectedDate = Welcome.ShowTo;

                        for (int i = 0; i <= ScreensLst.Items.Count - 1; i++)
     
                        {
                            if (Welcome.ScreensList.Contains("/" + ScreensLst.Items[i].Value + "/"))
                            {
                                ScreensLst.Items[i].Selected = true;
                            }
                        }

                    }
                }
                else
                {
                    if (AID == 2)
                    {

                        //Delete

                        ClassWelcome Welcome = new ClassWelcome(WID);

                        Welcome.Deleted = true;

                        Welcome.Save();

                    }

                    MultiView.ActiveViewIndex = 0;
                    LoadTable();
                }
            }

        }

        public void LoadTable()
        {
            DataSet DS;

            DS = ClassWelcome.loadDataset();
            
            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                ClassWelcome Welcome = new ClassWelcome(DR);

                TableCell[] TC = new TableCell[6];
                TC[0] = new TableCell();
                TC[0].Text = Welcome.Title;
                TC[1] = new TableCell();
                TC[1].Text = Welcome.ShowFrom.ToShortDateString();
                TC[2] = new TableCell();
                TC[2].Text = Welcome.ShowTo.ToShortDateString();
                TC[3] = new TableCell();
                TC[3].Text = "Screens";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=1&wid=" + Welcome.ID + "'>Edit</a>";
                TC[5] = new TableCell();
                TC[5].Text = "<a href='?aid=2&wid=" + Welcome.ID + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                WelcomeTable.Rows.Add(TR);
            }
        }

        private void loadScreens()
        {
            ScreensLst.Items.Clear();

            ScreensLst.Items.AddRange(ClassScreens.loadList());
        }

        protected void SaveWelcomecmd_Click(object sender, EventArgs e)
        {
            if (HiddenEditField.Value != "0")
            {
                //Save Changes
                ClassWelcome Welcome = new ClassWelcome(Convert.ToInt16(HiddenEditField.Value));

                Welcome.Title = Titletxt.Text;
                Welcome.Message = Messagetxt.Text;
                Welcome.ShowFrom = ShowFromCal.SelectedDate;
                Welcome.ShowTo = ShowToCal.SelectedDate;

                Welcome.Screens.Clear();

                for (int i = 0; i <= ScreensLst.Items.Count - 1; i++)
                {
                    if (ScreensLst.Items[i].Selected == true)
                    {
                        Welcome.Screens.Add(new ClassScreens(Convert.ToInt16(ScreensLst.Items[i].Value)));
                    }
                }

                Welcome.Save();

                CompletedHeaderlbl.Text = "Welcome Screen Saved";
                Completedlbl.Text = "The Welcome Screen you have edited has been succesfully saved to the system, and will go live at the appropriate time.";

                MultiView.ActiveViewIndex = 2;
            }
            else
            {
                //Create Welcome
                ClassWelcome Welcome = new ClassWelcome();

                Welcome.Title = Titletxt.Text;
                Welcome.Message = Messagetxt.Text;
                Welcome.ShowFrom = ShowFromCal.SelectedDate;
                Welcome.ShowTo = ShowToCal.SelectedDate;

                for (int i = 0; i <= ScreensLst.Items.Count - 1; i++)
                {
                    if (ScreensLst.Items[i].Selected == true)
                    {
                        Welcome.Screens.Add(new ClassScreens(Convert.ToInt16(ScreensLst.Items[i].Value)));
                    }
                }

                Welcome.Create();

                CompletedHeaderlbl.Text = "Welcome Screen Created";
                Completedlbl.Text = "The Welcome Screen you have added has been succesfully added to the system, and will go live at the appropriate time.";

                MultiView.ActiveViewIndex = 2;
            }
        }

    }
}
