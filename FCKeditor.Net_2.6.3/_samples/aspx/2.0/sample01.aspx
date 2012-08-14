<%@ Page Language="C#" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--
 * FCKeditor - The text editor for Internet - http://www.fckeditor.net
 * Copyright (C) 2003-2008 Frederico Caldeira Knabben
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 * Sample page.
--%>
<script runat="server">

	// In this page, we are placing all server side code inline to the page, to
	// avoid having to compile the page in your web site to run it.
	// Of course it would work in the same way with Code Behind.

	protected void Page_Load( object sender, EventArgs e )
	{
		if ( Page.IsPostBack )
			return;

		// Set the startup editor value.
		FCKeditor1.Value = "<p>This is some <strong>sample text</strong>. You are using <a href=\"http://www.fckeditor.net/\">FCKeditor</a>.</p>";
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

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>FCKeditor - Sample</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="robots" content="noindex, nofollow" />
	<link href="../sample.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		pre { background-color: #f5f5f5; padding: 5px; border: #d3d3d3 1px solid; }
	</style>
</head>
<body>
	<form runat="server">
		<h1>
			FCKeditor - ASP.NET - Sample 1
		</h1>
		<p>
			This sample displays a normal HTML form with an FCKeditor with full features enabled.
		</p>
		<p>
			No code behind is used so you don't need to compile the ASPX pages to make it work.
			All other samples use code behind.
		</p>
		<p id="PostedAlertBlock" style="color: Red" runat="server" visible="false">
			The posted data has been printed at the bottom of the page.
		</p>
		<p>
			<!---
				Here we have the FCKeditor component tag. It has been created
				by dragging the FCKeditor icon from the toolbar to the page, in design mode.
			--->
			<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server">
			</FCKeditorV2:FCKeditor>
		</p>
		<p>
			<asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Submit" />
		</p>
	</form>
	<div id="PostedDataBlock" runat="server" visible="false">
		<p>
			Posted data:
		</p>
		<pre><asp:Label ID="LblPostedData" runat="server"></asp:Label></pre>
	</div>
</body>
</html>
