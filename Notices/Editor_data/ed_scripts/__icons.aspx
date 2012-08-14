<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<table id="iconFolders" border="0" cellspacing="0" cellpadding="0" style="padding:0px;margin: 4px 4px 0px 4px;">
</table>
<div id="iconsPano" style="width: 485px;height:275px;margin:0px 4px 4px 4px; padding:0px;">
</div>
</body>
</html>
