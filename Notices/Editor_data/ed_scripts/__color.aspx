<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"color",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<span id="colorSet"><%= filler.Get("set","Set") %></span>
<span id="colorSelected"><%= filler.Get("selected","Selected") %></span>
<span id="colorOK"><%= filler.Get("ok","OK") %></span>
<span id="colorCancel"><%= filler.Get("cancel","Cancel") %></span>
</body>
</html>

