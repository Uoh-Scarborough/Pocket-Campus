<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller image_filler;

void Page_Load(object o, EventArgs e)
{
  image_filler = new OboutInc.Editor.FieldsFiller(Page,"image",Page.Request["localization_path"],Page.Request["language"]);
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
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("alt-text","Alternate Text") %>:
</td>
<td colspan="2">
<input type="text" id="txtAltText" size="40" />
</td>
</tr>
<tr>
<td style="white-space:nowrap;" valign="middle">
<%= image_filler.Get("show-shadow","Show Shadow") %>:
</td>
<td align="left" style="white-space:nowrap;" valign="middle" colspan="2">
<input id='showShadow' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' title="<%= image_filler.Get("shadow-title","Set/reset shadow effect") %>" />
<%= image_filler.Get("width","Width") %>:
<input type="text" id="imgWidth"  size="3" style='vertical-align:middle;' />
<img alt="" style="vertical-align:middle;" src="<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.scale.gif") %>" /> <input id='scale' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= image_filler.Get("height","Height") %>:
<input type="text" id="imgHeight" size="3" style='vertical-align:middle;' />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px; width: 100%">
<tr>
<td valign="top" style="height: 99%">
<fieldset style="padding: 0px;height:90%;">
<legend><%= image_filler.Get("layout","Layout") %></legend>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td>
<%= image_filler.Get("alignment","Alignment") %>:
</td>
<td>
<select size="1" id="selAlignment" style="margin:0px;">
<option id="optNotSet" value=""> <%= image_filler.Get("align-not-set","Not set") %> </option>
<option id="optLeft" value="left"> <%= image_filler.Get("align-left","Left") %> </option>
<option id="optRight" value="right"> <%= image_filler.Get("align-right","Right") %> </option>
<option id="optTexttop" value="texttop"> <%= image_filler.Get("align-texttop","Texttop") %> </option>
<option id="optAbsMiddle" value="absmiddle"> <%= image_filler.Get("align-absmiddle","Absmiddle") %> </option>
<option id="optBaseline" value="baseline" selected="selected"> <%= image_filler.Get("align-baseline","Baseline") %> </option>
<option id="optAbsBottom" value="absbottom"> <%= image_filler.Get("align-absbottom","Absbottom") %> </option>
<option id="optBottom" value="bottom"> <%= image_filler.Get("align-bottom","Bottom") %> </option>
<option id="optMiddle" value="middle"> <%= image_filler.Get("align-middle","Middle") %> </option>
<option id="optTop" value="top"> <%= image_filler.Get("align-top","Top") %> </option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;">
<%= image_filler.Get("border-width","Border Thickness") %>:
</td>
<td>
<input id="txtBorder" type="text" size="3" maxlength="3" value="" style="_margin-bottom:2px;" />
</td>
</tr>
</table>
</fieldset>
</td>
<td valign="top" style="height: 99%">
<fieldset style="padding: 0px;height:90%;margin-right:2px;">
<legend><%= image_filler.Get("spacing","Spacing") %></legend>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 2px; padding: 0px;">
<tr>
<td>
<%= image_filler.Get("horizontal","Horizontal") %>:
</td>
<td>
<input id="txtHorizontal" type="text" size="3" maxlength="3" value=""  style="margin:0px;" />
</td>
</tr>
<tr>
<td>
<%= image_filler.Get("vertical","Vertical") %>:
</td>
<td>
<input id="txtVertical" type="text" size="3" maxlength="3" value="" />
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
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= image_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= image_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>