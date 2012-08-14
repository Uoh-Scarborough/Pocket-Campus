<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller cell_filler;
OboutInc.Editor.FieldsFiller table_filler;

void Page_Load(object o, EventArgs e)
{
  cell_filler  = new OboutInc.Editor.FieldsFiller(Page,"cell" ,Page.Request["localization_path"],Page.Request["language"]);
  table_filler = new OboutInc.Editor.FieldsFiller(Page,"table",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<style type="text/css">
.ctable td { font-size:1px;}
</style>
</head>
<body>
<input id="rows" type="hidden" value="3" />
<input id="cols" type="hidden" value="4" />
<table border="0" cellspacing="0" cellpadding="0" style=" margin: 2px; padding-left:8px;">
<tr>
<td>
<br />
<table border="0" cellspacing="0" cellpadding="0" style="margin: 2px; width: 460px;">
<tr style="width: 100%; font-size: 12px;">
<td colspan="3" align="left" valign="middle" style="overflow: hidden; width: 100%; white-space:nowrap;">
        <table border="0" cellspacing="0" cellpadding="0" style='margin: 0px; padding: 0px;'>
        <tr>
        <td align="left" valign="middle" style="padding-left: 2px; width:80px; height: 40px; white-space:nowrap;">
        <%= table_filler.Get("insert-remove","insert, remove") %>
        </td>
        <td align="center" valign="middle" style="padding-left: 2px; white-space:nowrap;">
        <div class='box'>
        <div id='after' style="padding-top:3px;width:48px;height:16px;width:expression('50px');height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= table_filler.Get("after-before","after/before") %></div>
        </div>
        </td>
        <td align="left" valign="middle" style="padding-left: 2px; width:80px; white-space:nowrap;">&#160;
        <%= table_filler.Get("current-cell","current cell") %>
        </td>
        <td align="left" valign="middle" style="width: 256px; margin:0px;padding:0px; white-space:nowrap;">
        <span class='box'><label><%= table_filler.Get("columns","Columns") %> :</label><input id='incrCells' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_plus.gif") %>' class='button' /><input id='decrCells' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_minus.gif") %>' class='button' /></span>
        <span class='box'><label><%= table_filler.Get("span","Span") %> :</label><input id='Icolspan'  type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_plus.gif") %>' class='button' /><input id='Dcolspan'  type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_minus.gif") %>' class='button' /></span>
        </td>
        </tr>
        </table>
</td>
</tr>
<tr style="width: 100%; font-size: 12px;">
<td style="width: 180px;">
<table border="0" cellspacing="3" cellpadding="0">
<tr>
<td align="left" style="width: 70px;">
<%= table_filler.Get("width","Width") %>:
</td>
<td align="left" style="white-space:nowrap;" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input id="width" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="100" />
<input id="widthUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td align="left">
<%= table_filler.Get("height","Height") %>:
</td>
<td align="left" style="white-space:nowrap;" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input id="height" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="100" />
<input id="heightUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td align="left" style="white-space:nowrap;">
<%= table_filler.Get("cell-padding","Cell Padding") %>:
</td>
<td align="left">
<span style="margin: 0px; padding: 0px;">
<input id="padding" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="0" />&#160;px
</span>
</td>
</tr>
<tr>
<td align="left" style="white-space:nowrap;">
<%= table_filler.Get("cell-spacing","Cell Spacing") %>:
</td>
<td align="left">
<span style="margin: 0px; padding: 0px;">
<input  id="spacing" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="0" />&#160;px
</span>
</td>
</tr>
<tr>
<td align="left" style="white-space:nowrap;">
<%= table_filler.Get("border-width","Border width") %>:
</td>
<td align="left">
<span style="margin: 0px; padding: 0px;">
<input id="borderWidth" type="text" maxlength="2" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="0" />&#160;px
</span>
</td>
</tr>
<tr>
<td align="left" style="white-space:nowrap;">
<%= table_filler.Get("border-color","Border color") %>:
</td>
<td align="left">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="borderColor" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= table_filler.Get("bg-color-not-set","Not set") %>"
 title="<%= table_filler.Get("border-color-popup","Border color") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='borderClearColor' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= table_filler.Get("remove-border-color","Remove border color") %>" />
</span>
</td>
</tr>
<tr>
<td align="left" style="white-space:nowrap;">
<%= table_filler.Get("border-style","Border style") %>:
</td>
<td align="left">
<select id="borderStyle" style="width: 65px;">
<option value="none"><%= table_filler.Get("border-style-none","None") %></option>
<option value="dotted"><%= table_filler.Get("border-style-dotted","Dotted") %></option>
<option value="dashed"><%= table_filler.Get("border-style-dashed","Dashed") %></option>
<option value="solid"><%= table_filler.Get("border-style-solid","Solid") %></option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left">
<%= table_filler.Get("background-color","Background Color") %>:
</td>
<td align="left" valign="top" style="white-space:nowrap;">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="tableBgColor" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= table_filler.Get("bg-color-not-set","Not set") %>"
 title="<%= table_filler.Get("bg-color-popup","Table's background color") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='tableClearColor' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= table_filler.Get("remove-bg-color","Remove background color") %>" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left">
<%= table_filler.Get("background-image","Background Image") %>:
</td>
<td align="left" valign="top" style="white-space:nowrap;">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="tableBgImage" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= table_filler.Get("bg-image-not-set","Not set") %>"
 title="<%= table_filler.Get("bg-image-popup","Table's background image") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='tableClearImage' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title="<%= table_filler.Get("remove-bg-image","Remove background image") %>" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left">
<%= table_filler.Get("show-shadow","Show Shadow") %>:
</td>
<td align="left" valign="top" style="white-space:nowrap;">
<span class='box' style='margin:0px; padding:0px;'>
<input id='showShadow' style='margin:0px; padding:0px;vertical-align:top;' type='checkbox' title="<%= table_filler.Get("shadow-title","Set/reset shadow effect") %>" />
</span>
</td>
</tr>
</table>
</td>
<td valign="top">
<div id="tablePlace" style="width: 220px; height: 200px; overflow: auto;"></div>
</td>
<td align="center" valign="middle">
<div class='box'>
<label><%= table_filler.Get("rows","Rows") %>:</label>
</div>
<div class='box'><input id='incrRows' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_plus.gif") %>' class='button' /><br /><input id='decrRows' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_minus.gif") %>' class='button' /></div>
<br />
<br />
<div class='box'>
<label><%= table_filler.Get("span","Span") %>:</label>
</div>
<div class='box'><input id='Irowspan' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_plus.gif") %>' class='button' /><br /><input id='Drowspan' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_minus.gif") %>' class='button' /></div>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td>
<fieldset style="margin: 5px; padding: 2px; text-align: center; width: 465px;">
<legend><%= cell_filler.Get("legend","Cell properties") %></legend>
<center>
<table border="0" cellspacing="0" cellpadding="5" style="margin: 2px;height: 75px;">
<tr>
<td colspan="2">
   <table border="0" cellspacing="0" cellpadding="0" style='margin: 0px; padding: 0px;'>
   <tr>
   <td align="center" valign="middle" style="padding-left: 2px; white-space:nowrap">
   <div class='box'>
   <div id='apply' style="padding-top:3px;height:16px;height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_stretch.gif") %>');text-align:center;font-weight:bold;" class='button'>&nbsp;<%= table_filler.Get("apply","Apply") %>&nbsp;</div>
   </div>
   </td>
   <td align="left" valign="middle" style="padding-left: 2px; white-space:nowrap">
      <%= table_filler.Get("apply-properties","checked properties to") %>
      <select id="applyTo" style="width: 65px;">
      <option value="column"><%= table_filler.Get("apply-column","Column") %></option>
      <option value="row"><%= table_filler.Get("apply-row","Row") %></option>
      <option value="table"><%= table_filler.Get("apply-table","Table") %></option>
      </select>
   </td>
   </tr>
   </table>
</td>
</tr>
<tr>
<td>
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px;">
<tr>
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='alignCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("alignment","Alignment") %>:
</td>
<td style="white-space:nowrap;" align="left" valign="middle">
<select
 id="align" style="width: 65px;">
<option value=""><%= cell_filler.Get("align-not-set","Not set") %></option>
<option value="left"><%= cell_filler.Get("align-left","Left") %></option>
<option value="right"><%= cell_filler.Get("align-right","Right") %></option>
<option value="center"><%= cell_filler.Get("align-center","Center") %></option>
</select>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='vAlignCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("v-alignment","Vertical Alignment") %>:
</td>
<td style="white-space:nowrap;" align="left" valign="middle">
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
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='bgColorCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("background-color","Background Color") %>:
</td>
<td style="white-space:nowrap;" align="left" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id="bgColor" type="text" style="margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;" value="<%= cell_filler.Get("bg-color-not-set","Not set") %>"
 title="<%= cell_filler.Get("bg-color-popup","Cell's background color") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='clearColor' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title='<%= cell_filler.Get("remove-bg-color","Remove background color") %>' />
</span>
</td>
</tr>
</table>
</td>
<td>
<table border="0" cellspacing="2" cellpadding="0">
<tr>
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='cellwidthCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("width","Width") %>:
</td>
<td style="white-space:nowrap;" align="left" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input id="cellwidth" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="" />
<input id="cellwidthUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='cellheightCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("height","Height") %>:
</td>
<td style="white-space:nowrap;" align="left" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input id="cellheight" type="text" maxlength="3" style="margin: 0px; padding: 0px; width: 30px; height:18px;" value="" />
<input id="cellheightUnit" type="button" style="background-color:transparent;" value="px" />
</span>
</td>
</tr>
<tr>
<td style="white-space:nowrap;" align="left" valign="middle">
<input id='bgImageCheck' style='margin:0px; padding:0px;vertical-align:middle;' type='checkbox' />
<%= cell_filler.Get("background-image","Background Image") %>:
</td>
<td align="left" style="white-space:nowrap;" valign="middle">
<span style="margin: 0px; padding: 0px;">
<input readonly="readonly" id='bgImage' type='text' style='margin: 0px; padding: 0px;padding-left:1px; width: 42px; height:17px; cursor: pointer;' value='<%= cell_filler.Get("bg-image-not-set","Not set") %>'
 title="<%= cell_filler.Get("bg-image-popup","Cell's background image") %>" />
</span>
<span class='box' style='margin:0px; padding:0px;'>
<input id='clearImage' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button' title='<%= cell_filler.Get("remove-bg-image","Remove background image") %>' />
</span>
</td>
</tr>
</table>
</td>
</tr>
</table>
</center>
</fieldset>
</td>
</tr>
</table>
<div style="margin-top:11px;">
<div class='box' style='float:right; margin-right:12px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= table_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:12px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= table_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>
