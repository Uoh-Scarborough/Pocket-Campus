<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mobile.Notices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Notices</title>
    <link rel="stylesheet" type="text/css" href="../iPhoneHeader.css" />
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
        <h1 class="secondheader">Notices</h1>
    </div>

    <div id="content">
        
        <asp:MultiView ID="MultiView" runat="server">
            <asp:View runat="server">
                <asp:Label ID="Noticeslbl" runat="server" Text="Label"></asp:Label>
            </asp:View>
        </asp:MultiView>
        
        <ul><li>&copy; University of Hull Scarborough Campus</li></ul>
        
    </div>
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
