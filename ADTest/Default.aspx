<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ADTest._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>ADTest</h1>
        <h2>LDAP Server</h2>
        <p>LDAP Address: <asp:TextBox runat="server" id="ldaptxt" />
        </p>
        <p>Domain: <asp:TextBox runat="server" id="domaintxt" />
        </p>
        <h2>Login Test</h2>
        <p>Username: <asp:TextBox runat="server" id="loginusernametxt" /></p>
        <p>Password: <asp:TextBox runat="server" ID="loginpasswordtxt" 
                TextMode="Password" /></p>
        <asp:Button runat="server" ID="logincmd" Text="Login" 
            onclick="logincmd_Click" />
        <p>Outcome: <asp:Label runat="server" ID="loginoutcomelbl" /></p>

        <h2>Details Test</h2>
        <p>Username: <asp:TextBox runat="server" id="detailsusernametxt" /></p>
        <asp:Button runat="server" ID="testcmd" Text="Test" onclick="testcmd_Click" />
        <p>Outcome: <asp:Label runat="server" ID="detailsoutcome"/></p>
        </div>
    </form>
</body>
</html>
