<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"input",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<link rel="stylesheet" href="<%= Page.Request["css"] %>" media="all" />
</head>
<body style="overflow: hidden; margin: 0px; padding: 5px;">
<input type="hidden" id="input_text" value="<%= filler.Get("text","Textbox") %>"/>
<input type="hidden" id="input_checkbox" value="<%= filler.Get("checkbox","CheckBox") %>"/>
<input type="hidden" id="input_radio" value="<%= filler.Get("radio","Radio button") %>"/>
<input type="hidden" id="input_hidden" value="<%= filler.Get("hidden","Hidden element") %>"/>
<input type="hidden" id="input_button" value="<%= filler.Get("button","Button") %>"/>
<input type="hidden" id="input_submit" value="<%= filler.Get("submit","Submit button") %>"/>
<input type="hidden" id="input_reset" value="<%= filler.Get("reset","Reset button") %>"/>
<input type="hidden" id="input_password" value="<%= filler.Get("password","Password field") %>"/>
<span  id="inputType" style="font:11px Verdana; color:#0033cc; font-weight: bold;">None</span>
<table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:50%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("width","Width") %>:
</td>
<td style="width:100%">
<input type="text" id="widthField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("height","Height") %>:
</td>
<td style="width:100%">
<input type="text" id="heightField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("value","Value") %>:
</td>
<td style="width:100%" valign="middle">
<span style="white-space:nowrap;"><input type="text" id="valueField" /><input type="checkbox" id="checkField" /></span>
</td>
</tr>
</table>
</td>
<td style="width:50%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("id","ID") %>:
</td>
<td style="width:100%">
<input type="text" id="idField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("name","Name") %>:
</td>
<td style="width:100%">
<input type="text" id="nameField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap">
<%= filler.Get("tooltip","ToolTip") %>:
</td>
<td style="width:100%">
<input type="text" id="titleField" style="width:100%" />
</td>
</tr>
</table>
</td>
</tr>
</table>
</body>
</html>
