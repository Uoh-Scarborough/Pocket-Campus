using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;
using System.Text.RegularExpressions;

namespace Communications
{
    public partial class Control_Base : System.Web.UI.UserControl
    {

        public enum ModeType
        {
            Add,
            Edit
        }

        public enum BaseType
        {
            Notice,
            Event,
            Menu,
            Ticker
        }

        private BaseType c_BaseType;
        private ModeType c_Mode;
        private int c_BaseID;

        public event EventHandler BaseValidated,BaseInvalidated;

        public ClassCommsBase Base;

        public ModeType Mode
        {
            get { return c_Mode; }
            set { c_Mode = value; }
        }

        public BaseType ModuleBaseType
        {
            get { return c_BaseType; }
            set { c_BaseType = value; }
        }

        public int BaseID
        {
            get { return c_BaseID; }
            set { c_BaseID = value; }
        }

        public string Location
        {
            get { return txtLocation.Text; }
            set { txtLocation.Text = value; }
        }

        public string Time
        {
            get { return txtTime.Text; }
            set { txtTime.Text = value; }
        }

        public int MenuType
        {
            get { return Convert.ToInt16(MenuTypeddl.SelectedValue); }
            set { MenuTypeddl.Items.FindByValue(value.ToString()).Selected = true;}
        }

        public int Recurrence
        {
            get { return Convert.ToInt16(Recurrenceddl.SelectedValue); }
            set { Recurrenceddl.Items.FindByValue(value.ToString()).Selected = true; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Mode == ModeType.Add)
            {
                Control_CategoryDropDown1.Load();
                pnl_PostedInfo.Visible = false;
            }
            else
            {
                pnl_PostedInfo.Visible = true;

            }

            if (ModuleBaseType == BaseType.Notice)
            {
                pnl_DisplayFrom.Visible = true;
                pnl_Time.Visible = false;
                pnl_Location.Visible = false;
            }
            else if (ModuleBaseType == BaseType.Event)
            {
                pnl_DisplayFrom.Visible = false;
                pnl_Time.Visible = true;
                pnl_Location.Visible = true;
                DisplayTolbl.Text = "Event Date:";
            }
            else if (ModuleBaseType == BaseType.Menu)
            {
                pnl_PostedInfo.Visible = false;
                pnl_DisplayFrom.Visible = false;
                pnl_Time.Visible = false;
                pnl_Location.Visible = false;
                pnl_General.Visible = false;
                pnl_MenuType.Visible = true;
                pnl_Title.Visible = false;
                DisplayTolbl.Text = "Menu Date:";
                Contentlbl.Text = "Menu:";
                pnl_Content.Visible = false;
                pnl_MenuContent.Visible = true;
            }
            else if (ModuleBaseType == BaseType.Ticker)
            {
                pnl_DisplayFrom.Visible = true;
                pnl_General.Visible = false;
                pnl_Title.Visible = true;
                pnl_Time.Visible = false;
                pnl_Location.Visible = false;
                pnl_Content.Visible = false;
            }

           
        }

        public ClassCommsBase UpdateClass(ClassCommsBase Base)
        {
            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            Base.BaseID = 0;
            
            if(hfBaseID.Value.ToString() != ""){

            Base.BaseID = Convert.ToInt16(hfBaseID.Value.ToString());

            }
            Base.Title = txt_Title.Text;
            if (ModuleBaseType == BaseType.Menu)
            {
                Base.Content = txt_Item1.Text + "</br><i>" + txt_Desc1.Text + "</i></br>" + txt_Item2.Text + "</br><i>" + txt_Decs2.Text + "</i></br>" + txt_Item3.Text + "</br><i>" + txt_Desc3.Text + "</i></br>" + txt_Item4.Text + "</br><i>" + txt_Desc4.Text + "</i></br>" + txt_Item5.Text + "</br><i>" + txt_Desc5.Text + "</i>";
            }
            else
            {
                Base.Content = txt_Content.Text;
            }
            Base.DisplayFrom = Cal_DisplayFrom.SelectedDate;
            Base.DisplayTo = Cal_DisplayTo.SelectedDate;
            Base.Urgent = chk_PriorityNotice.Checked;
            Base.Category = new ClassCategory(Control_CategoryDropDown1.CategoryID());

            if (Attachement.HasFile)
            {

                try
                {

                    string fileName = "/Attachements/" + Attachement.FileName.Insert(Attachement.FileName.LastIndexOf('.'),ClassUseful.CreateTimeStamp());
                    string directory = ClassAppDetails.uploaddir + fileName;

                    Attachement.SaveAs(directory);

                    Base.Attachment = fileName;

                }
                catch (Exception ex)
                {
                    //Do Nothing
                }

            } else if (hdAttachement.Value != "")
            {
                Base.Attachment = hdAttachement.Value;
            }

            Base.UseAttachement = chk_UserImage.Checked;

            if (Mode == ModeType.Edit)
            {

                //Base.Valid = chk_Valid.Checked;
                Base.InvalidReason = txt_InvalidReason.Text;
                Base.ValidatedBy = UI.DisplayName;
                Base.ValidatedDate = DateTime.Now.Date;

            }
            else
            {
                Base.PostedBy = UI.DisplayName;
                Base.PostedByEmail = UI.Mail;
                Base.PostedByID = UI.StudentID;
                Base.PostedDate = DateTime.Now.Date;
            }

            if (UI.InGroup(ClassAppDetails.admingroup))
            {
                //Auto Valid
                Base.Valid = true;
                Base.ValidatedBy = UI.DisplayName;
                Base.ValidatedDate = DateTime.Now.Date;
            }

            return Base;

        }

        public void UpdateBase(ClassCommsBase Base)
        {
                hfBaseID.Value = BaseID.ToString();
                Control_CategoryDropDown1.Load();

                txt_PostedBy.Text = Base.PostedBy;
                txt_PostedDate.Text = Base.PostedDate.ToShortDateString();
                txt_Validated.Text = Base.ValidatedBy;
                txt_ValidatedDate.Text = Base.ValidatedDate.ToShortDateString();
                txt_Title.Text = Base.Title;
                if (ModuleBaseType == BaseType.Menu)
                {
                    string[] Item = Regex.Split(Base.Content, "</br>");
                    
                    txt_Item1.Text = Item[0];
                    txt_Desc1.Text = Item[1].Substring(3,Item[1].Length - 7);
                    txt_Item2.Text = Item[2];
                    txt_Decs2.Text = Item[3].Substring(3, Item[3].Length - 7);
                    txt_Item3.Text = Item[4];
                    txt_Desc3.Text = Item[5].Substring(3, Item[5].Length - 7);
                    txt_Item4.Text = Item[6];
                    txt_Desc4.Text = Item[7].Substring(3, Item[7].Length - 7);
                    txt_Item5.Text = Item[8];
                    txt_Desc5.Text = Item[9].Substring(3, Item[9].Length - 7);
                }
                else
                {
                    txt_Content.Text = Base.Content;
                }
                Cal_DisplayFrom.SelectedDate = Base.DisplayFrom;
                Cal_DisplayTo.SelectedDate = Base.DisplayTo;
                chk_PriorityNotice.Checked = Base.Urgent;
                Control_CategoryDropDown1.SetCategoryID(Base.Category.ID);

                if (Base.Attachment != "")
                {
                    hdAttachement.Value = Base.Attachment;
                    Attachementlbl.Text = "<a href='Uploads/" + Base.Attachment + "'>Attachement</a>";
                }
                else
                {
                    Attachementlbl.Text = "";
                }

                //Base.Attachment = "";
                chk_UserImage.Checked = Base.UseAttachement;

                if (Mode == ModeType.Edit)
                {

                    if (Base.Valid)
                    {
                        Validcmd.CommandName = "Invalidate";
                        Validcmd.Text = "Invalidate";
                        pnl_Validated.Visible = true;
                        pnl_Reason.Visible = false;
                        Returncmd.Visible = false;
                    }
                    else
                    {
                        Validcmd.CommandName = "Validate";
                        Validcmd.Text = "Validate";
                        pnl_Validated.Visible = false;
                        pnl_Reason.Visible = true;
                        Returncmd.Visible = true;
                    }

                    //chk_Valid.Checked = Base.Valid;
                    txt_InvalidReason.Text = Base.InvalidReason;

                }

        }

        protected void Validcmd_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;

            ClassUserInfo UI = new ClassUserInfo(Context.User.Identity.Name);

            ClassCommsBase Base = new ClassCommsBase();
            BaseID = 0;

            if (hfBaseID.Value.ToString() != "")
            {

                BaseID = Convert.ToInt16(hfBaseID.Value);

            }

            Base.LoadBaseFromID(BaseID);

            


            if (btn.CommandName == "Validate")
            {

                
                Base.Valid = true;
                Base.ValidatedBy = UI.DisplayName;
                Base.ValidatedDate = DateTime.Now;

                Base.SaveBase();
                BaseValidated(this, EventArgs.Empty);
            }
            else
            {

                Base.Valid = false;
                Base.ValidatedBy = "";
                Base.ValidatedDate = DateTime.Now;

                Base.SaveBase();
                BaseInvalidated(this, EventArgs.Empty);

            }

            

            

        }

    }
}