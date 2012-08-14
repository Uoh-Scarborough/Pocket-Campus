<%@ Page Language="C#" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<script runat="server">

OboutInc.Editor.FieldsFiller filler;

void Page_Load(object o, EventArgs e)
{
  filler = new OboutInc.Editor.FieldsFiller(Page,"div",Page.Request["localization_path"],Page.Request["language"]);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
<link rel="stylesheet" href="<%= Page.Request["css"] %>" media="all" />
</head>
<body style="overflow: hidden; margin: 0px; padding: 5px;">
<table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
<tr>
<td style="width:50%" valign="top">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("width","Width") %>
  </td>
  <td style="width:100%" align="left">
  <input type="text" id="widthField" style="width:100%" />
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("border-width","Border width") %>
  </td>
  <td style="width:100%" align="left">
  <input type="text" id="borderWidth" style="width:100%" />
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("border-style","Border style") %>
  </td>
  <td style="width:100%" align="left">
  <select id="borderStyle">
  <option value=""><%= filler.Get("border-style-none","None") %></option>
  <option value="dotted"><%= filler.Get("border-style-dotted","Dotted") %></option>
  <option value="dashed"><%= filler.Get("border-style-dashed","Dashed") %></option>
  <option value="solid"><%= filler.Get("border-style-solid","Solid") %></option>
  </select>
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("overflow","Overflow") %>
  </td>
  <td style="width:100%" align="left">
  <select id="overflow">
  <option value=""><%= filler.Get("overflow-none","None") %></option>
  <option value="visible"><%= filler.Get("overflow-visible","Visible") %></option>
  <option value="hidden"><%= filler.Get("overflow-hidden","Hidden") %></option>
  <option value="scroll"><%= filler.Get("overflow-scroll","Scroll") %></option>
  <option value="auto"><%= filler.Get("overflow-auto","Auto") %></option>
  </select>
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("id","ID") %>
  </td>
  <td style="width:100%">
  <input type="text" id="idField" style="width:100%" />
  </td>
  </tr>
</table>
</td>
<td style="width:50%" valign="top">
<table border="0" cellspacing="2" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("height","Height") %>
  </td>
  <td style="width:100%" align="left">
  <input type="text" id="heightField" style="width:100%" />
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("border-color","Border color") %>
  </td>
  <td style="width:100%" align="left">
  <table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
    <tr>
    <td style="width:100%; white-space:nowrap;">
    <input readonly="readonly" type="text" id="borderColor" maxlength="30" style="width:100%; cursor: pointer;"
           value="<%= filler.Get("border-color-not-set","Not set") %>"
           title="<%= filler.Get("border-color-popup","Border color") %>" />
    </td>
    <td style="width:1%; white-space:nowrap;">
    <span class='box' style='margin:0px; padding:0px;margin-left:2px;margin-right:2px;'>
    <input id='clearBorderColor' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button'
           title="<%= filler.Get("remove-border-color","Remove border color") %>"
    />
    </span>
    </td>
    </tr>
  </table>
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("background-color","Background Color") %>
  </td>
  <td style="width:100%; white-space:nowrap;" align="left">
  <table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
    <tr>
    <td style="width:100%; white-space:nowrap;">
    <input type="text" readonly="readonly" id="bgColor" style="width:100%; cursor: pointer;"
           value="<%= filler.Get("bg-color-not-set","Not set") %>"
           title="<%= filler.Get("bg-color-popup","DIV's background color") %>" />
    </td>
    <td style="width:1%; white-space:nowrap;">
    <span class='box' style='margin:0px; padding:0px;margin-left:2px;margin-right:2px;'>
    <input id='clearColor' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button'
           title="<%= filler.Get("remove-bg-color","Remove background color") %>"
    />
    </span>
    </td>
    </tr>
  </table>
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("background-image","Background Image") %>
  </td>
  <td style="width:100%; white-space:nowrap;" align="left">
  <table border="0" cellspacing="0" cellpadding="0" style="margin: 0px; padding: 0px; width: 100%;">
    <tr>
    <td style="width:100%; white-space:nowrap;">
    <input type="text" readonly="readonly" id="bgImage" style="width:100%; cursor: pointer;"
           value="<%= filler.Get("bg-image-not-set","Not set") %>"
           title="<%= filler.Get("bg-image-popup","DIV's background image") %>" />
    </td>
    <td style="width:1%; white-space:nowrap;">
    <span class='box' style='margin:0px; padding:0px;margin-left:2px;margin-right:2px;'>
    <input id='clearImage' style='margin:0px; padding:0px;vertical-align:top;' type='image' src='<%= Page.ClientScript.GetWebResourceUrl(typeof(OboutInc.Editor.Editor), "OboutInc.Editor.Resources.Images.ed_unformat.gif") %>' class='button'
           title="<%= filler.Get("remove-bg-image","Remove background image") %>"
    />
    </span>
    </td>
    </tr>
  </table>
  </td>
  </tr>
  <tr>
  <td style="width:1%; white-space:nowrap;" align="right">
  <%= filler.Get("css-class","CSS Class") %>
  </td>
  <td style="width:100%" align="left">
  <input type="text" id="cssClass" style="width:100%" />
  </td>
  </tr>
</table>
</td>
</tr>
</table>
</body>
</html>
