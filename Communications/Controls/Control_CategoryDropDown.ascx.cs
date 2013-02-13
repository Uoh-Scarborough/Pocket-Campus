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

        public int CategoryID()
        {
            return Convert.ToInt16(dd_Category.SelectedValue);
        }

        public void SetCategoryID(int ID)
        {
            try
            {
                dd_Category.Items.FindByValue(ID.ToString()).Selected = true;
            }
            catch
            {

            }
             

        }


        public void Load()
        {
            if (dd_Category.Items.Count == 0)
            {

                dd_Category.DataSource = PocketCampusClasses.ClassCategory.loadDataset().Tables[0];

                dd_Category.DataTextField = "Category_Title";

                dd_Category.DataValueField = "Category_ID_LNK";

                dd_Category.DataBind();
            }
        }

        

    }
}