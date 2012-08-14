using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;

namespace Communications.Controls
{
    public partial class Control_CategoryDropDown : System.Web.UI.UserControl
    {

        public Int32 CategoryID()
        {
            return Convert.ToInt32(dd_Category.SelectedValue);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dd_Category.DataSource = PocketCampusClasses.ClassCategory.loadDataset().Tables[0];

            dd_Category.DataTextField = "Category_Title";

            dd_Category.DataValueField = "Category_ID_LNK";

            dd_Category.DataBind();
        }
    }
}