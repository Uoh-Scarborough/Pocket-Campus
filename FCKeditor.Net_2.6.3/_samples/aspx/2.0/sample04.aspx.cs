using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _samples_aspx_sample04 : System.Web.UI.Page
{
	protected void Page_Load( object sender, EventArgs e )
	{
		// Set the base path. This is the URL path for the FCKeditor
		// installations. By default "/fckeditor/".
		FCKeditor1.BasePath = this.GetBasePath();

		LblPostedData.Text = "";
		PostedAlertBlock.Visible = false;
		PostedDataBlock.Visible = false;

		if ( Page.IsPostBack )
			return;

		// Set the startup editor value.
		FCKeditor1.Value = "<p>This is some <strong>sample text</strong>. You are using <a href=\"http://www.fckeditor.net/\">FCKeditor</a>.</p>";
	}

	/// <summary>
	/// Automatically calculates the editor base path based on the _samples
	/// directory. This is usefull only for these samples. A real application
	/// should use something like this instead:
	/// <code>
	/// FCKeditor1.BasePath = "/fckeditor/" ;	// "/fckeditor/" is the default value.
	/// </code>
	/// </summary>
	private string GetBasePath()
	{
		string path = Request.Url.AbsolutePath;
		int index = path.LastIndexOf( "_samples" );
		return path.Remove( index, path.Length - index );
	}

	protected void BtnSubmit_Click( object sender, EventArgs e )
	{
		// For sample purposes, print the editor value at the bottom of the
		// page. Note that we are encoding the value, so it will be printed as
		// is, intead of rendering it.
		LblPostedData.Text = HttpUtility.HtmlEncode( FCKeditor1.Value );

		// Make the posted data block visible.
		PostedDataBlock.Visible = true;
		PostedAlertBlock.Visible = true;
	}

	protected void cmbSkin_SelectedIndexChanged( object sender, EventArgs e )
	{
		// Relative to the "editor" folder in the FCKeditor installation.
		FCKeditor1.Config["SkinPath"] = this.GetBasePath() + "editor/skins/" + cmbSkin.SelectedValue + "/";
	}
}
