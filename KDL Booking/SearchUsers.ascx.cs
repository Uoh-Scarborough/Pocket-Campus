using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using PocketCampusClasses;

namespace StudioBooking
{
    public partial class SearchUsers : System.Web.UI.UserControl
    {
        public event EventHandler ObjectChossen;
        //public String CommandArgument;
        public String UserID;
        public String ControlText;
        public Boolean Loaded;

        protected void Page_Load(object sender, EventArgs e)
        {

            //CommandArgument = Searchcmd.CommandArgument;

            if (IsPostBack)
            {
                Searchcmd_Click(sender, e);
            }

        }

        protected void PickButton_Click(object sender, EventArgs e)
        {
            Button Btn = new Button();

            if (sender.GetType() == Btn.GetType())
            {
                Btn = (Button)sender;

                UserID = Btn.ID.Substring(4);

                if (ObjectChossen != null)
                {
                    ObjectChossen(this, e);
                }
            }
            
        }

        protected void Searchcmd_Click(object sender, EventArgs e)
        {
            String Filter = "";

            
            if (StudentIDtxt.Text != "")
            {
                //Search Student ID
                Filter = String.Format("(&(description=*{0}*)(objectCategory=person)((objectClass=user)))", StudentIDtxt.Text.Trim());
                Search(Filter);
            }
            
            if (Surnametxt.Text != "")
            {
                //Search Surname
                Filter = String.Format("(&(displayName=*{0}*)(objectCategory=person)((objectClass=user)))", Surnametxt.Text.Trim());
                Search(Filter);
                
            }

            //Searchcmd.CommandArgument = CommandArgument;
           
        }

        private void Search(string Filter)
        {
            if (!Loaded)
            {

                DirectorySearcher search = new DirectorySearcher(ClassAppDetails.ldapserver);

                //search.Filter = "((displayName=*" + Surname + "*)(objectCategory=person))";
                search.Filter = Filter;
                search.PropertiesToLoad.Add("samaccountname");
                search.PropertiesToLoad.Add("description");
                search.PropertiesToLoad.Add("displayName");
                search.PropertiesToLoad.Add("mail");
                SearchResultCollection results = search.FindAll();

                foreach (SearchResult result in results)
                {
                    TableRow TR = new TableRow();

                    TableCell[] TC = new TableCell[4];
                    TC[0] = new TableCell();
                    TC[0].Text = (String)result.Properties["displayName"][0];
                    TC[1] = new TableCell();
                    try
                    {
                        TC[1].Text = (String)result.Properties["description"][0];
                    }
                    catch
                    {

                    }

                    try
                    {
                        TC[2] = new TableCell();
                        TC[2].Text = (String)result.Properties["mail"][0];
                    }
                    catch
                    {

                    }

                    //Create Pick Button
                    Button PickBtn = new Button();
                    PickBtn.Text = ControlText;
                    PickBtn.ID = "Pick" + (String)result.Properties["samaccountname"][0];
                    PickBtn.Click += new EventHandler(PickButton_Click);
                    PickBtn.BorderStyle = BorderStyle.None;
                    PickBtn.BackColor = System.Drawing.Color.White;

                    TC[3] = new TableCell();
                    TC[3].Controls.Add(PickBtn);

                    foreach (TableCell TabCel in TC)
                    {
                        TR.Cells.Add(TabCel);
                    }

                    UserTable.Rows.Add(TR);

                    Loaded = true;
                }

            }
            
        }

    }
}