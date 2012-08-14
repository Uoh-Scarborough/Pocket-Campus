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
    public partial class PageManager : System.Web.UI.Page
    {

        public int AID, PID, VID;

        ClassConnection NC;
        ClassUserInfo CurrentUser;

        protected void Page_Load(object sender, EventArgs e)
        {

            NC = new ClassConnection(ClassAppDetails.configname, ClassAppDetails.noticesconnectionname);

            ClassAppDetails.commscurrentconnection = NC;

            AID = Convert.ToInt16(Request["aid"]);
            PID = Convert.ToInt16(Request["pid"]);

            if (!IsPostBack)
            {

                if (AID == 1)
                {

                    //Edit

                    LoadParentsList();
                    LoadNoticesEventsCategorys();

                    if (PID > 0)
                    {
                        ClassPages Page = new ClassPages(PID);

                        Titletxt.Text = Page.Title;
                        Tagtxt.Text = Page.Tag;
                        ContentEditor.Content = Page.StandardContent;
                        ContentEditorMobile.Content = Page.MobileContent;

                        Parentddl.SelectedValue = Page.Parent_ID.ToString();
                        NoticesCategoryddl.SelectedValue = Page.NoticesCateogryID.ToString();
                        EventsCategoryddl.SelectedValue = Page.EventsCateogryID.ToString();
                    }

                    Multiview.SetActiveView(ContentListView);

                }
                else if (AID == 2)
                {
                    //Delete

                    if (PID > 0)
                    {
                        ClassPages Page = new ClassPages(PID);

                        Page.Deleted = true;

                        Page.Save();

                        Multiview.SetActiveView(DeletedView);
                    }
                }
                else
                {

                    LoadTable();

                    Multiview.SetActiveView(PageListView);

                }
            }
            else
            {
                LoadTable();

                Multiview.SetActiveView(PageListView);
            }
        }

        public void LoadParentsList()
        {
            DataSet DS;

            DS = ClassPages.loadDataset();

            Parentddl.DataSource = DS;

            Parentddl.DataValueField = "Page_IDLNK";
            Parentddl.DataTextField = "Page_Title";

            Parentddl.DataBind();
        }

        public void LoadNoticesEventsCategorys()
        {
            DataSet DS;

            DS = ClassCategory.loadDataset();

            NoticesCategoryddl.DataSource = DS;
            EventsCategoryddl.DataSource = DS;

            NoticesCategoryddl.DataValueField = "Category_ID_LNK";
            NoticesCategoryddl.DataTextField = "Category_Title";

            EventsCategoryddl.DataValueField = "Category_ID_LNK";
            EventsCategoryddl.DataTextField = "Category_Title";

            NoticesCategoryddl.DataBind();
            EventsCategoryddl.DataBind();
        }

        public void LoadTable()
        {
            DataSet DS;

            DS = ClassPages.loadDataset();

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                TableRow TR = new TableRow();

                TableCell[] TC = new TableCell[5];
                TC[0] = new TableCell();
                TC[0].Text = DR[1].ToString();
                TC[1] = new TableCell();
                TC[1].Text = DR[2].ToString();
                TC[2] = new TableCell();
                DateTime DT1 = (DateTime)DR[3];
                TC[2].Text = DT1.ToShortDateString();
                TC[3] = new TableCell();
                TC[3].Text = "<a href='?aid=1&pid=" + DR[0].ToString() + "'>Edit</a>";
                TC[4] = new TableCell();
                TC[4].Text = "<a href='?aid=2&pid=" + DR[0].ToString() + "' onclick=\"return confirm('Are you sure you want to delete?');\" >Delete</a>";

                foreach (TableCell TabCel in TC)
                {
                    TR.Cells.Add(TabCel);
                }

                PagesTable.Rows.Add(TR);

            }
        }

        protected void Parentddl_DataBound(object sender, EventArgs e)
        {
            Parentddl.Items.Insert(0, new ListItem("Root","1"));
        }



        protected void SavePagecmd_Click(object sender, EventArgs e)
        {
            if (PID < 0)
            {
                ClassPages Page = new ClassPages();

                Page.Title = Titletxt.Text;

                Page.Tag = Tagtxt.Text;

                Page.Parent_ID = Convert.ToInt16(Parentddl.SelectedValue);

                Page.StandardContent = ContentEditor.Content;

                Page.MobileContent = ContentEditorMobile.Content;

                Page.NoticesCateogryID = Convert.ToInt16(NoticesCategoryddl.SelectedValue);

                Page.EventsCateogryID = Convert.ToInt16(EventsCategoryddl.SelectedValue);

                Page.Updated = DateTime.Now;

                Page.Create();
            }
            else
            {
                ClassPages Page = new ClassPages(PID);

                Page.Title = Titletxt.Text;

                Page.Tag = Tagtxt.Text;

                Page.Parent_ID = Convert.ToInt16(Parentddl.SelectedValue);

                Page.StandardContent = ContentEditor.Content;

                Page.MobileContent = ContentEditorMobile.Content;

                Page.NoticesCateogryID = Convert.ToInt16(NoticesCategoryddl.SelectedValue);

                Page.EventsCateogryID = Convert.ToInt16(EventsCategoryddl.SelectedValue);

                Page.Updated = DateTime.Now;

                Page.Save();
            }

            Multiview.SetActiveView(SavedView);
        }

        protected void NoticesCategoryddl_DataBound(object sender, EventArgs e)
        {
            NoticesCategoryddl.Items.Insert(0, new ListItem("None", "0"));
        }

        protected void EventsCategoryddl_DataBound(object sender, EventArgs e)
        {
            EventsCategoryddl.Items.Insert(0, new ListItem("None", "0"));
        }
    }
}