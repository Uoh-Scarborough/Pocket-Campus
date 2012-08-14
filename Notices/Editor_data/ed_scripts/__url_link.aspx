<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"link",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<table border="0" cellspacing="0" cellpadding="2" style="margin: 2px;">
<tr>
<td align="left">
<%= filler.Get("protocol","Protocol") %>:
</td>
<td align="left">
<select id="protocol" style="width: 65px;">
<option value="http://">http:</option>
<option value="https://">https:</option>
<option value="file://">file:</option>
<option value="ftp://">ftp:</option>
<option value="gopher://">gopher:</option>
<option value="mailto://">mailto:</option>
<option value="news://">news:</option>
<option value="telnet://">telnet:</option>
<option value="wais://">wais:</option>
<option value="javascript:">javascript:</option>
<option value="#">anchor:</option>
</select>
</td>
<td align="right">
<div class='box' style='float:right; margin-right:7px;'>
<div title="<%= filler.Get("browse-title","Browse URL") %>" id='browsButton' style="font-size:7pt;padding-top:4px;width:48px;height:15px;width:expression('50px');height:expression('21px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_ab_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("browse","Browse") %></div>
</div>
</td>
</tr>
<tr>
<td align="left">
<%= filler.Get("url","URL") %>:
</td>
<td align="left" colspan="2">
<input id="url" type="text" maxlength="255"
 style="margin: 0px; padding: 0px; width: 350px; height:18px;" value=""/>
</td>
</tr>
<tr>
<td align="left">
<%= filler.Get("tooltip","ToolTip") %>:
</td>
<td align="left" colspan="2">
<input id="titleA" type="text" maxlength="255"
 style="margin: 0px; padding: 0px; width: 350px; height:18px;" value=""/>
</td>
</tr>
<tr>
<td align="left">
<%= filler.Get("target","Target") %>:
</td>
<td align="left" colspan="2">
<select id="target" style="width: 105px;">
<option value="_blank"><%= filler.Get("target-new-win","New window") %></option>
<option selected="selected" value="_self"><%= filler.Get("target-cur-win","Current window") %></option>
<option value="_parent"><%= filler.Get("target-par-win","Parent window") %></option>
<option value="_top"><%= filler.Get("target-top-win","Top window") %></option>
</select>
</td>
</tr>
</table>
<div style='margin-top:11px;'>
<div class='box' style='float:right; margin-right:5px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>
