<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Mobile.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Student Services</title>
    <link rel="stylesheet" type="text/css" href="../RoundedRectangle.css" />
</head>
<body onload="hideAddressbar();">

     <script type="text/javascript">
        function hideAddressbar()
        {
	        window.scrollTo(0, 1);
        }
    </script>

    <div id="banner">
        <a href="../Default.aspx" class="first"><img src="../Images/PocketCampusHome.png" alt="Hello"/></a>
        <a href="Default.aspx"><img class="second" src="../Images/NoticesHome.png" alt="Hello"/></a>
        <h1 class="secondheader">Student Services</h1>
    </div>

    <div id="content">
    <h1>Login</h1>
    
        <form id="studentservices" runat="server">

            <ul>
                <li>To make a request to Student Services you must first login with your campus username and password.</li>
            </ul>

            <ul>
                <li>Username: <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Static" ErrorMessage="Required Field" runat="server"/><br /><input id="UserName" type="text" runat="server" style="width:180px;margin-left:90px"/></li>
                <li>Password: <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" Display="Static" ErrorMessage="Required Field" runat="server"/><br /><input id="UserPass" type="password" runat="server" style="width:180px;margin-left:90px"/></li>
                <li><asp:button id="cmdLogin" text="Login" OnClick="Login_Click"  runat="server" style="margin-left: 220px"/></li>
            </ul>
            <asp:Label id="lblResults" ForeColor="red" Font-Size="10" runat="server" />
        
        </form>
     </div>
    
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try {
var pageTracker = _gat._getTracker("UA-3108410-12");
pageTracker._trackPageview();
} catch(err) {}</script>
    
</body>
</html>
