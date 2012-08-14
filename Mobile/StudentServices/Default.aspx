<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mobile.Default" %>

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
    <form id="form1" runat="server">
        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0">
        
            <asp:View ID="Welcome" runat="server">
            
                <ul><li>From the Scarborough Student Services Website you can request a Council Tax Letter or Status Letter.</li></ul>
                
                <ul>
                    <li><asp:Button ID="StatusLettercmd" runat="server" CssClass="bigbutton" onclick="StatusLettercmd_Click" Text="Status Letter Request" style="width:280px"/></li>
                    <li><asp:Button ID="CouncilTaxcmd" runat="server" CssClass="bigbutton" onclick="CouncilTaxcmd_Click" Text="Request Council Tax Letter" style="width:280px"/></li>
                    <li><asp:Button ID="Logout" runat="server" CssClass="bigbutton" onclick="Logoutcmd_Click" Text="Log Out" style="width:280px" /></li>
                
                </ul>
                    
            </asp:View>
        
            <asp:View ID="CouncilTax" runat="server">
                <h1>Council Tax Letter Request</h1>
                <ul>
                    <li>
                        <p>Thankyou you request is now being processed, you will recive an email to your 
                            email address (<asp:Label ID="CTEmaillbl" runat="server" Text="Label"></asp:Label>
                            ) informing you that you letter is ready to collect from Student Services in 
                            Quad 3.</p>
                        <p>If you require any further information please don&#39;t hessitate to get in touch 
                            with Student Services.</p>
                    </li>
                </ul>
                <ul>
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
                <br /><br />
            </asp:View>
            
            <asp:View ID="StatusLetter" runat="server">
                <h1>Status Letter Request</h1>
                <ul>
                    <li>
                        <p>Thankyou you request is now being processed, you will recive an email to your 
                        email address (<asp:Label ID="SLEmaillbl" runat="server" Text="Label"></asp:Label>
                        ) informing you that you letter is ready to collect from Student Services in 
                        Quad 3.</p>
                        <p>If you require any further information please don&#39;t hessitate to get in touch 
                        with Student Services.</p>
                    </li>
                </ul>
                <ul>
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
            </asp:View>
            
            <asp:View ID="Transcript" runat="server">
                <h1>Status Letter Request</h1>
                <ul>
                    <li>
                        <p>Thankyou you request is now being processed, you will recive an email to your 
                        email address (<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        ) informing you that you transcript is ready to collect from Student Services in 
                        Quad 3.</p>
                        <p>If you require any further information please don&#39;t hessitate to get in touch 
                        with Student Services.</p>
                    </li>
                </ul>
                <ul>
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
            </asp:View>
        
        </asp:MultiView>
    </form>
    
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
