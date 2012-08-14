<%@ Page Language="C#" maintainScrollPositionOnPostBack="false" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<script runat="server">
    string result = "";
    void Page_Load(object o, EventArgs e)
    {
        Page.Server.ScriptTimeout = 2 * 60;
        if (!String.IsNullOrEmpty(Page.Request["addword"]))
        {
            result = OboutInc.Editor.Library.AddWordToCustom(Page.Request["addword"], Page.Request["dictionaries_path"]);
        }
        else
        {
            result = OboutInc.Editor.Library.SpellHTML(Page, Page.Request["dictionaries_path"], Page.Request["dictionaries"], Page.Request["diclangs"], Page.Request["maxsugg"], Page.Request["customonserver"]);
        }
    }
</script>
<%= result %>
<body></body>
</html>