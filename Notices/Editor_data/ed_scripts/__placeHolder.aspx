<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller placeholder_filler;

void Page_Load(object o, EventArgs e)
{
  placeholder_filler = new OboutInc.Editor.FieldsFiller(Page,"placeholder",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
</head>
<body>
<fieldset style="margin: 0px; margin-top: 2px; margin-left: 2px; padding: 4px; text-align: center;">
<%= placeholder_filler.Get("name","PlaceHolder name") %>:
<input id="placeholder_name" type="text" maxlength="20" size="20" value="" />
</fieldset>
<div style='margin-top:2px;'>
<div class='box' style='float:right; margin-right:5px;'>
<div id='cancel' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= placeholder_filler.Get("cancel","Cancel") %></div>
</div>
<div class='box' style='float:right; margin-right:5px;'>
<div id='ok' style="padding-top:5px;width:78px;height:18px;width:expression('80px');height:expression('25px');background-image: url('<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_submit_button.gif") %>');text-align:center;font-weight:bold;" class='button'><%= placeholder_filler.Get("ok","OK") %></div>
</div>
</div>
</body>
</html>
