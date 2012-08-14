<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller image_filler;

void Page_Load(object o, EventArgs e)
{
  image_filler = new OboutInc.Editor.FieldsFiller(Page,"background-image",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<table border="0" cellspacing="0" cellpadding="0" style="margin: 2px; padding: 0px; width: 99%;">
<tr>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px; width: 100%;">
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("url","Image URL") %>:
</td>
<td>
<input id="txtFileName" type="text" size="40" />
</td>
<td align="left" style="white-space:nowrap;">
<div class='box' style='float:right; margin-right:7px;'>
<div title="<%= image_filler.Get("browse-title","Browse Images") %>" id='browsButton' style="font-size:7pt;padding-top:4px;width:48px;height:15px;width:expression('50px');height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= image_filler.Get("browse","Browse") %></div>
</div>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<fieldset style="padding: 0px;margin: 2px;">
<table border="0" cellspacing="2" cellpadding="0" style="padding: 0px; width: 90%; height:90%">
<tr>
<td valign="top" style="height: 85%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td>
<%= image_filler.Get("repeat","Repeat") %>:
</td>
<td>
<select size="1" id="selRepeat" style="margin:0px;">
<option value="REPEAT"> <%= image_filler.Get("repeat-all","REPEAT") %> </option>
<option value="REPEAT-X"> <%= image_filler.Get("repeat-x","REPEAT-X") %> </option>
<option value="REPEAT-Y"> <%= image_filler.Get("repeat-y","REPEAT-Y") %> </option>
<option value="NO-REPEAT"> <%= image_filler.Get("repeat-no","NO-REPEAT") %> </option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("attachment","Attachment") %>:
</td>
<td>
<select size="1" id="selAttachment" style="margin:0px;">
<option value="FIXED"> <%= image_filler.Get("attachment-fixed","FIXED") %> </option>
<option value="SCROLL"> <%= image_filler.Get("attachment-scroll","SCROLL") %> </option>
</select>
</td>
</tr>
</table>
</td>
<td valign="top" style="height: 85%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("position-hor","Position left") %>:
</td>
<td>
<select size="1" id="selPositionLeft" style="margin:0px;">
<option value="LEFT" selected="selected"> <%= image_filler.Get("position-hor-left","LEFT") %> </option>
<option value="CENTER"> <%= image_filler.Get("position-hor-center","CENTER") %> </option>
<option value="RIGHT"> <%= image_filler.Get("position-hor-right","RIGHT") %> </option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("position-vert","Position top") %>:
</td>
<td>
<select size="1" id="selPositionTop" style="margin:0px;">
<option value="TOP" selected="selected"> <%= image_filler.Get("position-vert-top","TOP") %> </option>
<option value="CENTER"> <%= image_filler.Get("position-vert-center","CENTER") %> </option>
<option value="BOTTOM"> <%= image_filler.Get("position-vert-bottom","BOTTOM") %> </option>
</select>
</td>
</tr>
</table>
</td>
</tr>
</table>
</fieldset>
</td>
</tr>
</table>

<div style='margin-top:2px;'>
<div class='box' style='float:right; margin-right:5px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= image_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= image_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>