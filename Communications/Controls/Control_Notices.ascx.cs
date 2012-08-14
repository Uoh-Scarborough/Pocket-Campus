using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PocketCampusClasses;

namespace Communications
{
    public partial class Control_Notices : System.Web.UI.UserControl
    {

        public enum ModeType
        {
            Add,
            Edit
        }

        private ModeType c_Mode;

        public ModeType Mode
        {
            get { return c_Mode; }
            set { c_Mode = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Mode == ModeType.Add)
            {
                pnl_PostedInfo.Visible = false;
                pnl_ValidInfo.Visible = false;
                lbl_Header.Text = "Add Notice";
                btn_Action.Text = "Add Notice";
            }
            else
            {
                pnl_PostedInfo.Visible = true;
                pnl_ValidInfo.Visible = true;
                lbl_Header.Text = "Edit Notice";
                btn_Action.Text = "Save Notice";
            }
        }

        protected void btn_Action_Click(object sender, EventArgs e)
        {
            if (Mode == ModeType.Add)
            {
                AddNotice();
            }
            else
            {
                EditNotice();
            }
        }

        private void AddNotice()
        {
            ClassNotice Notice = new ClassNotice();

            

        }

        private void EditNotice()
        {

        }

        private void UpdateClass(ClassNotice Notice)
        {
            Notice.Title = txt_Title.Text;
            Notice.Notice = txt_Notice.Text;
            Notice.DisplayFrom = Cal_DisplayFrom.SelectedDate;
            Notice.DisplayTo = Cal_DisplayTo.SelectedDate;
            Notice.Urgent = chk_PriorityNotice.Checked;
            //Notice.Category = 
            //Notice.Attachment = 
            
        }

    }
}