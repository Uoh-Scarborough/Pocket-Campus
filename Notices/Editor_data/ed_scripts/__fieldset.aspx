<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"fieldset",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<link rel="stylesheet" href="<%= Page.Request["css"] %>" media="all" />
</head>
<body style="overflow: hidden; margin: 0px; padding: 5px;" onload="start()">
<table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:50%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:1%; white-space:nowrap;">
<%= filler.Get("width","Width") %>:
</td>
<td style="width:100%">
<input type="text" id="widthField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap;">
<%= filler.Get("height","Height") %>:
</td>
<td style="width:100%">
<input type="text" id="heightField" style="width:100%" />
</td>
</tr>
</table>
</td>
<td style="width:50%">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:1%; white-space:nowrap;">
<%= filler.Get("padding","Padding") %>:
</td>
<td style="width:100%">
<input type="text" id="paddingField" style="width:100%" />
</td>
</tr>
<tr>
<td style="width:1%; white-space:nowrap;">
<%= filler.Get("margin","Margin") %>:
</td>
<td style="width:100%">
<input type="text" id="marginField" style="width:100%" />
</td>
</tr>
</table>
</td>
</tr>
</table>
</body>
</html>
