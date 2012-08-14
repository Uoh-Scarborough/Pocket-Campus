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

namespace Timetables.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClassConnection NC = new ClassConnection(ClassAppDetails.configname,ClassAppDetails.ttconnectionname);

            ClassAppDetails.ttcurrentconnection = NC;

            Errorlbl.Text = "";
        }

        protected void Uploadcmd_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
                try
                {
                    FileUpload1.SaveAs(ClassAppDetails.uploaddir + FileUpload1.FileName);

                    ClassTimetableImport.Generate(ClassAppDetails.uploaddir + FileUpload1.PostedFile.FileName);

                    Errorlbl.Text = "Upload Complete.";
                }
                catch (Exception ex)
                {
                    Errorlbl.Text = "Error: " + ex.Message.ToString() + ".";
                }
            }
            else
            {
                Errorlbl.Text = "You have not specified a file.";
            }

        }

    }
}
