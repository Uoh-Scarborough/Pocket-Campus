<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller cell_filler;

void Page_Load(object o, EventArgs e)
{
  cell_filler = new OboutInc.Editor.FieldsFiller(Page,"cell",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<fieldset id="m_fieldset" style="margin: 0px; margin-left: 2px; padding: 0px; text-align: center;">
<legend><%= cell_filler.Get("legend","Cell properties") %></legend>
<table id="m_table" border="0" cellspacing="0" cellpadding="0" style="margin: 2px; width: 300px; height: 50px;">
<tr>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px;">
<tr>
<td style="white-space:nowrap;" align="right">
<%= cell_filler.Get("alignment","Alignment") %>
</td>
<td style="white-space:nowrap;" align="left">
<select id="align" style="width: 65px;">
<option value=""><%= cell_filler.Get("align-not-set","Not set") %></option>
<option value="left"><%= cell_filler.Get("align-left","Left") %></option>
<option value="right"><%= cell_filler.Get("align-right","Right") %></option>
<option value="center"><%= cell_filler.Get("align-center","Center") %></option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="right">
<%= cell_filler.Get("v-alignment","Vertical Alignment") %>
</td>
<td style="white-space:nowrap;" align="left">
<select
 id="vAlign" style="width: 65px;">
<option value="top"><%= cell_filler.Get("valign-top","Top") %></option>
<option value="middle"><%= cell_filler.Get("valign-middle","Middle") %></option>
<option value="bottom"><%= cell_filler.Get("valign-bottom","Bottom") %></option>
<option value="baseline"><%= cell_filler.Get("valign-baseline","Baseline") %></option>
<option value=""><%= cell_filler.Get("valign-not-set","Not set") %></option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="right">
<%= cell_filler.Get("background-color","Background Color") %>
</td>
<td style="white-space:nowrap;" align="left" valign="top">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="bgColor" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= cell_filler.Get("bg-color-not-set","Not set") %>"
 title="<%= cell_filler.Get("bg-color-popup","Cell's background color") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='clearColor' style='margin:0px; padding:0px;margin-left:3px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= cell_filler.Get("remove-bg-color","Remove background color") %>" />
</span>
</td>
</tr>
</table>
</td>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px;">
<tr>
<td style="white-space:nowrap;" align="right">
<%= cell_filler.Get("width","Width") %>
</td>
<td style="white-space:nowrap;" align="left">
<span style="margin: 0px; padding: 0px;">
<input id="cellwidth" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="" />
<input id="cellwidthUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="right">
<%= cell_filler.Get("height","Height") %>
</td>
<td style="white-space:nowrap;" align="left">
<span style="margin: 0px; padding: 0px;">
<input id="cellheight" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="" />
<input id="cellheightUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left">
<%= cell_filler.Get("background-image","Background Image") %>
</td>
<td align="left" valign="top" style="white-space:nowrap;">
<span style='margin: 0px; padding: 0px;'>
<input readonly="readonly" id='bgImage' type='text' style='margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;' value="<%= cell_filler.Get("bg-image-not-set","Not set") %>"
 title="<%= cell_filler.Get("bg-image-popup","Cell's background image") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='clearImage' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= cell_filler.Get("remove-bg-image","Remove background image") %>" />
</span>
</td>
</tr>
</table>
</td>
</tr>
</table>
</fieldset>
<div style='margin-top:2px;'>
<div class='box' style='float:right; margin-right:5px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= cell_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= cell_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>
