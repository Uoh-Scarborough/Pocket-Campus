using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;

namespace Communications.Controls
{
    public partial class Control_List : System.Web.UI.UserControl
    {

        public enum ListType
        {
            Notices,Events,Menus,Tickers
        };

        public ListType type;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);
            DataSet DS = new DataSet();

            if (type == ListType.Notices)
            {
                //Load Notices
                DS = ClassNotice.loadDataset(UI);
            }
            else if (type == ListType.Events)
            {
                //Load Events
                DS = ClassEvents.loadDataset(UI);
            }
            else if (type == ListType.Menus)
            {
                //Load Menus
                DS = ClassMenu.loadDataset();
            }
            else if (type == ListType.Tickers)
            {
                //Load Menus
                DS = ClassTicker.loadDataset(UI);
            }

            GridView.DataSource = DS.Tables[0];
            GridView.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            int itemID = Convert.ToInt16(GridView.DataKeys[index].Value.ToString());

            if (e.CommandName == "EditItem")
            {
                switch (type)
                {
                    case ListType.Notices:
                        Response.Redirect("?cmd=Notice&id=" + itemID);
                        break;
                    case ListType.Events:
                        Response.Redirect("?cmd=Event&id=" + itemID);
                        break;
                    case ListType.Menus:
                        Response.Redirect("?cmd=Menu&id=" + itemID);
                        break;
                    case ListType.Tickers:
                        Response.Redirect("?cmd=Ticker&id=" + itemID);
                        break;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {

                switch (type)
                {
                    case ListType.Notices:
                        Response.Redirect("?cmd=Notice&act=Delete&id=" + itemID);
                        break;
                    case ListType.Events:
                        Response.Redirect("?cmd=Event&act=Delete&id=" + itemID);
                        break;
                    case ListType.Menus:
                        Response.Redirect("?cmd=Menu&act=Delete&id=" + itemID);
                        break;
                    case ListType.Tickers:
                        Response.Redirect("?cmd=Ticker&act=Delete&id=" + itemID);
                        break;
                }

            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Boolean valid = false;
            DateTime displayfrom; 

            if (e.Row.DataItem != null)
            {

                Boolean.TryParse(DataBinder.Eval(e.Row.DataItem, "CommsBase_Valid").ToString(), out valid);
                DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "CommsBase_DisplayFrom").ToString(), out displayfrom);

                if (valid)
                {
                    if (displayfrom <= DateTime.Now)
                    {
                        e.Row.CssClass = "basevalid";
                    }
                    else
                    {
                        e.Row.CssClass = "basevalidnotshowing";
                    }

                }
                else
                {
                    e.Row.CssClass = "baseinvalid";
                }

            }
        }


    }
}