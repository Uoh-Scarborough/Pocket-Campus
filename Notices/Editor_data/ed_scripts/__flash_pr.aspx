<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller flash_filler;

void Page_Load(object o, EventArgs e)
{
  flash_filler = new OboutInc.Editor.FieldsFiller(Page,"flash",Page.Request["localization_path"],Page.Request["language"]);
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
<td align="left" style="white-space:nowrap;">
<%= flash_filler.Get("url","Flash URL") %>:
</td>
<td>
<input id="txtFileName" type="text" size="40" />
</td>
<td align="left" style="white-space:nowrap;">
<div class='box' style='float:right; margin-right:7px;'>
<div title="<%= flash_filler.Get("browse-title","Browse Flashes") %>" id='browsButton' style="font-size:7pt;padding-top:4px;width:48px;height:15px;width:expression('50px');height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= flash_filler.Get("browse","Browse") %></div>
</div>
</td>
</tr>
<tr>
<td colspan="3" align="left" style="white-space:nowrap;" valign="middle">
<%= flash_filler.Get("loop","Loop") %>:
<input id='loop' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
<%= flash_filler.Get("autoplay","Autoplay") %>:
<input id='autoplay' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
<%= flash_filler.Get("transparency","Transparency") %>:
<input id='transparency' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox'/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px; width: 100%">
<tr>
<td valign="top" style="">
<fieldset style="padding: 0px;">
<legend><%= flash_filler.Get("properties","Main properties") %></legend>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td>
<%= flash_filler.Get("width","Width") %>:
</td>
<td>
<input type="text" id="flashWidth"  size="3" style='vertical-align:middle;' />
</td>
</tr>
<tr>
<td>
<%= flash_filler.Get("height","Height") %>:
</td>
<td>
<input type="text" id="flashHeight" size="3" style='vertical-align:middle;' />
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left">
<%= flash_filler.Get("background-color","Background Color") %>
</td>
<td style="white-space:nowrap;" align="left" valign="top">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="bgColor" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= flash_filler.Get("bg-color-not-set","Not set") %>"
 title="<%= flash_filler.Get("bg-color-popup","Flash background color") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='clearColor' style='margin:0px; padding:0px;margin-left:3px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= flash_filler.Get("remove-bg-color","Remove background color") %>" />
</span>
</td>
</tr>
<tr>
<td>
<%= flash_filler.Get("quality","Quality") %>:
</td>
<td>
<select size="1" id="selQuality" style="margin:0px;">
<option value="high" selected="selected"> <%= flash_filler.Get("quality-high","High") %> </option>
<option value="medium"> <%= flash_filler.Get("quality-medium","Medium") %> </option>
<option value="low"> <%= flash_filler.Get("quality-low","Low") %> </option>
</select>
</td>
</tr>
<tr>
<td>
<%= flash_filler.Get("scale","Scale") %>:
</td>
<td>
<select size="1" id="selScale" style="margin:0px;">
<option value="" selected="selected"> <%= flash_filler.Get("scale-default","Default") %> </option>
<option value="noborder"> <%= flash_filler.Get("scale-noborder","No border") %> </option>
<option value="exactfit"> <%= flash_filler.Get("scale-exactfit","Exact fit") %> </option>
</select>
</td>
</tr>
</table>
</fieldset>
</td>
<td valign="top" style="">
<fieldset style="padding: 0px;margin-right:2px;">
<legend><%= flash_filler.Get("layout","Layout") %></legend>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td>
<%= flash_filler.Get("alignment","Alignment") %>:
</td>
<td>
<select size="1" id="selAlignment" style="margin:0px;">
<option value=""> <%= flash_filler.Get("align-not-set","Not set") %> </option>
<option value="left"> <%= flash_filler.Get("align-left","Left") %> </option>
<option value="right"> <%= flash_filler.Get("align-right","Right") %> </option>
<option value="texttop"> <%= flash_filler.Get("align-texttop","Texttop") %> </option>
<option value="absmiddle"> <%= flash_filler.Get("align-absmiddle","Absmiddle") %> </option>
<option value="baseline" selected="selected"> <%= flash_filler.Get("align-baseline","Baseline") %> </option>
<option value="absbottom"> <%= flash_filler.Get("align-absbottom","Absbottom") %> </option>
<option value="bottom"> <%= flash_filler.Get("align-bottom","Bottom") %> </option>
<option value="middle"> <%= flash_filler.Get("align-middle","Middle") %> </option>
<option value="top"> <%= flash_filler.Get("align-top","Top") %> </option>
</select>
</td>
</tr>
<tr>
<td>
<%= flash_filler.Get("horizontal","Horizontal") %>:
</td>
<td>
<input id="txtHorizontal" type="text" size="3" maxlength="3" value=""  style="margin:0px;" />
</td>
</tr>
<tr>
<td>
<%= flash_filler.Get("vertical","Vertical") %>:
</td>
<td>
<input id="txtVertical" type="text" size="3" maxlength="3" value="" />
</td>
</tr>
<tr>
<td colspan="2">
<select size="1" style="visibility:hidden">
<option value="" selected="selected">xx</option>
</select>
</td>
</tr>
<tr>
<td colspan="2">
<select size="1" style="visibility:hidden">
<option value="" selected="selected">xx</option>
</select>
</td>
</tr>
</table>
</fieldset>
</td>
</tr>
</table>
</td>
</tr>
</table>

<div style='margin-top:2px;'>
<div class='box' style='float:right; margin-right:5px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= flash_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= flash_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>