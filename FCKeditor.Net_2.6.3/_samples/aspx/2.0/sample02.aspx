<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sample02.aspx.cs" Inherits="_samples_aspx_sample02" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>FCKeditor - Sample</title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="robots" content="noindex, nofollow" />
	<link href="../sample.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		pre { background-color: #f5f5f5; padding: 5px; border: #d3d3d3 1px solid; }
	</style>
	<script type="text/javascript">

//
// Here we are using JavaScript to retrieve the available languages from
// FCKeditor. This is completely optional and is needed onl for sample purposes.
//
// It shows also how to interact with the editor instance through JavaScript.
//

/**
 * FCKeditor_OnComplete is a special function which is called when an FCKeditor
 * instance is loaded on the page and available for code interaction.
 */
function FCKeditor_OnComplete( editorInstance )
{
	// Get the language combo.
	var combo = document.getElementById( 'xLanguages' ) ;

	// Remove all options. (#1399)
	combo.innerHTML = '' ;
	
	// Retrieve the available languages object.
	var availableLanguages = editorInstance.Language.AvailableLanguages ;
	
	// Fill an array with all available languages.
	var languages = new Array() ;
	for ( code in availableLanguages )
		languages.push( { Code : code, Name : availableLanguages[code] } ) ;

	// Sort the array using a custom comparison function.
	languages.sort( SortLanguage ) ;

	// Add all languages to the combo.
	for ( var i = 0 ; i < languages.length ; i++ )
		AddComboOption( combo, languages[i].Name + ' (' + languages[i].Code + ')', languages[i].Code ) ;
	
	// Set the combo value to the current language.
	combo.value = editorInstance.Language.ActiveLanguage.Code ;
	
	combo.style.visibility = '' ;
}

/**
 * Custom function to sort the language entries.
 */
function SortLanguage( langA, langB )
{
	return ( langA.Name < langB.Name ? -1 : langA.Name > langB.Name ? 1 : 0 ) ;
}

/**
 * Utility function to append options in a <select> element.
 */
function AddComboOption( combo, optionText, optionValue )
{
	var option = document.createElement( 'option' ) ;

	combo.options.add( option ) ;

	option.innerHTML = optionText ;
	option.value     = optionValue ;

	return option ;
}

/**
 * Reloads the page, passing the specified language code in the querystring.
 */
function ChangeLanguage( languageCode )
{
	window.location.href = window.location.pathname + "?lang=" + languageCode ;
}

	</script>
</head>
<body>
	<form runat="server">
		<h1>
			FCKeditor - ASP.NET - Sample 2</h1>
		<p>
			This sample shows the editor in all its available languages.
		</p>
		<hr />
		<table cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td>
					Select a language:&nbsp;
				</td>
				<td>
					<select id="xLanguages" onchange="ChangeLanguage(this.value);" style="visibility: hidden">
						<option>&nbsp;</option>
					</select>
				</td>
			</tr>
		</table>
		<p id="PostedAlertBlock" style="color: Red" runat="server" visible="false">
			The posted data has been printed at the bottom of the page.
		</p>
		<div style="margin-top: 10px;">
			<!---
				Here we have the FCKeditor component tag. It has been created
				by dragging the FCKeditor icon from the toolbar to the page, in design mode.
			--->
			<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server">
			</FCKeditorV2:FCKeditor>
		</div>
		<p>
			<asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
		</p>
		<div id="PostedDataBlock" runat="server" visible="false">
			<p>
				Posted data:
			</p>
			<pre><asp:Label ID="LblPostedData" runat="server"></asp:Label></pre>
		</div>
	</form>
</body>
</html>
