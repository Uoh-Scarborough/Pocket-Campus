<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"spell-checker",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<table border="0" cellspacing="0" cellpadding="0" style="margin: 2px;">
<tr>
<td valign="top">
<table border="0" cellspacing="0" cellpadding="0" style="padding-left: 2px;">
<tr>
<td align="left">
<table border="0" cellspacing="0" cellpadding="3" style="width:100%;" >
<tr>
<td  style="white-space: nowrap;" align="left">
<span style="font-family:Arial;font-size: 12px;font-weight:bold;" ><%= filler.Get("not-in-dictionary","Not in Dictionary") %>:</span>
</td>
<td style="width: 100%;">
<input id="not_found" type="text" readonly="readonly" style="width: 100%;" />
</td>
</tr>
<tr>
<td align="left" style="padding: 2px; white-space:nowrap;">
<span style="font-family:Arial;font-size: 12px;font-weight:bold;" ><%= filler.Get("replace-with","Replace with") %>:</span>
</td>
<td style="width: 100%;">
<input id="replace" type="text" style="width: 100%;" />
</td>
</tr>
<tr>
<td align="left" style="padding: 2px;">
<span style="font-family:Arial;font-size: 12px;font-weight:bold;" ><%= filler.Get("suggestions","Suggestions") %>:</span>
</td>
<td align="right">
<span id="itemfrom"></span>&nbsp;<span id="language" style="color: #0000cd;"></span>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<div id="select_place" style="width:305px;margin:0px;padding:0px;">
<div id="working"></div>
<select id="select" style="width:100%;margin:0px;padding:0px;overflow: scroll; display: none;" size="10"></select>
</div>
</td>
</tr>
</table>
</td>
<td valign="top">
<table border="0" style="margin-bottom:2px; margin-top:0px;">
<tr>
<td align="center">
<div class='box'>
<div id='ignore' style="padding-top:5px;width:138px;height:18px;width:expression('140px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_sp_bg.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("ignore","Ignore") %></div>
</div>
</td>
</tr>
<tr>
<td align="center">
<div class='box'>
<div id='ignore_all' style="padding-top:5px;width:138px;height:18px;width:expression('140px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_sp_bg.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("ignore-all","Ignore All") %></div>
</div>
</td>
</tr>
<tr>
<td align="center">
<div class='box'>
<div id='change' style="padding-top:5px;width:138px;height:18px;width:expression('140px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_sp_bg.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("change","Change") %></div>
</div>
</td>
</tr>
<tr>
<td align="center">
<div class='box'>
<div id='change_all' style="padding-top:5px;width:138px;height:18px;width:expression('140px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_sp_bg.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("change-all","Change All") %></div>
</div>
</td>
</tr>
<tr>
<td align="center">
<div class='box'>
<div id='add_to_dictionary' style="padding-top:5px;width:138px;height:18px;width:expression('140px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_sp_bg.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("add-custom","Add Custom") %></div>
</div>
</td>
</tr>
<tr>
<td align="center">
<span class='box'>
<input id='undo' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_undo_n.gif") %>' class='button' style='margin-left:4px;'/>
</span>
</td>
</tr>
<tr>
<td align="center">
<table border="0" cellspacing="5" cellpadding="0"  style='margin-top:12px;'>
<tr>
<td>
<div class='box'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("ok","OK") %></div>
</div>
</td>
<td>
<div class='box'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("cancel","Cancel") %></div>
</div>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</table>

</body>
</html>
