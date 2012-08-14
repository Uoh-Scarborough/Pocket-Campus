<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller media_filler;

void Page_Load(object o, EventArgs e)
{
  media_filler = new OboutInc.Editor.FieldsFiller(Page,"media",Page.Request["localization_path"],Page.Request["language"]);
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
<td align="left" style="white-space:nowrap">
<%= media_filler.Get("url","Media URL") %>:
</td>
<td>
<input id="txtFileName" type="text" size="40" />
</td>
<td align="left" style="white-space:nowrap">
<div class='box' style='float:right; margin-right:7px;'>
<div title="<%= media_filler.Get("browse-title","Browse Media") %>" id='browsButton' style="font-size:7pt;padding-top:4px;width:48px;height:15px;width:expression('50px');height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= media_filler.Get("browse","Browse") %></div>
</div>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<fieldset style="margin: 2px; padding: 0px;">
<table border="0" cellspacing="2" cellpadding="0" style="width: 100%">
<tr>
<td valign="top" style="">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td style="white-space:nowrap" align="left">
<%= media_filler.Get("autostart","Autostart") %>:
</td>
<td style="white-space:nowrap" align="left">
<input id='autostart' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
</td>
</tr>
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("showcontrols","Show controls") %>:
</td>
<td>
<input id='showcontrols' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
</td>
</tr>
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("showtracker","Show tracker") %>:
</td>
<td>
<input id='showtracker' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
</td>
</tr>
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("showstatusbar","Show status bar") %>:
</td>
<td>
<input id='showstatusbar' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
</td>
</tr>
</table>
</td>
<td valign="top" style="">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("width","Width") %>:
</td>
<td>
<input type="text" id="mediaWidth"  size="3" style='vertical-align:middle;' />
</td>
</tr>
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("height","Height") %>:
</td>
<td>
<input type="text" id="mediaHeight" size="3" style='vertical-align:middle;' />
</td>
</tr>
<tr>
<td style="white-space:nowrap">
<%= media_filler.Get("alignment","Alignment") %>:
</td>
<td>
<select size="1" id="selAlignment" style="margin:0px;">
<option value=""> <%= media_filler.Get("align-not-set","Not set") %> </option>
<option value="left"> <%= media_filler.Get("align-left","Left") %> </option>
<option value="right"> <%= media_filler.Get("align-right","Right") %> </option>
<option value="texttop"> <%= media_filler.Get("align-texttop","Texttop") %> </option>
<option value="absmiddle"> <%= media_filler.Get("align-absmiddle","Absmiddle") %> </option>
<option value="baseline" selected="selected"> <%= media_filler.Get("align-baseline","Baseline") %> </option>
<option value="absbottom"> <%= media_filler.Get("align-absbottom","Absbottom") %> </option>
<option value="bottom"> <%= media_filler.Get("align-bottom","Bottom") %> </option>
<option value="middle"> <%= media_filler.Get("align-middle","Middle") %> </option>
<option value="top"> <%= media_filler.Get("align-top","Top") %> </option>
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
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= media_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= media_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>