<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ProductionBookingMobile.Login"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Studio Booking</title>
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
</head>
<body onload="hideAddressbar();">

     <script type="text/javascript">
         function hideAddressbar() {
             window.scrollTo(0, 1);
         }
    </script>

    <div id="banner">
        <a href="../Default.aspx" class="first"><img src="Images/PocketCampusHome.png" alt="Pocket Campus Home"/></a>
        <a href="Default.aspx"><img class="second" src="Images/StudioBookingHome.png" alt="Studio Booking Home"/></a>
        <h1 class="secondheader">Production Booking</h1>
    </div>

    <div id="content">
    <h1>Login</h1>
    
        <form id="studentservices" runat="server">

            <ul>
                <li>To make a new booking or edit a previous booking you must first login to the Production Booking System.</li>
            </ul>

            <ul>
                <li>Username: <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        ControlToValidate="UserName" ErrorMessage="Required Field" runat="server" 
                        meta:resourcekey="RequiredFieldValidator1Resource1"/><input id="UserName" type="text" runat="server" style="width:150px;margin-left:90px"/></li>
                <li>Password: <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        ControlToValidate="UserPass" ErrorMessage="Required Field" runat="server" 
                        meta:resourcekey="RequiredFieldValidator2Resource1"/><input id="UserPass" type="password" runat="server" style="width:150px;margin-left:90px"/></li>
                <li><asp:ImageButton id="cmdLogin" OnClick="Login_Click" runat="server" 
                        ImageUrl="Images/LoginButton.png" CssClass="alignbutton" 
                        meta:resourcekey="cmdLoginResource1" /></li>
            </ul>
            <asp:Label id="lblResults" ForeColor="Red" Font-Size="10pt" runat="server" 
                meta:resourcekey="lblResultsResource1" />
        
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
    } catch (err) { }</script>
    
</body>
</html>
