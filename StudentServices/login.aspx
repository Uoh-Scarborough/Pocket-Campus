<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="StudentServices.login" MasterPageFile="~/Green.Master" %>

<asp:Content ID="SSLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studentservices.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudentServicesLogo.png" alt="Student Services Logo" /></label></a></asp:Content>


<asp:Content ID="first" ContentPlaceHolderID="mainarea" Runat="Server">

    <div id="maincontent">
    
        <h1>Scarborough Student Services <asp:Label ID="Adminlbl" runat="server" Text="Admin"></asp:Label></h1>

        <p><b>Please note: </b>Students living within the Scarborough Borough Council area or Hull City Council area do not need to request a Council tax exemption letter as information has been passed to the councils indicating you are registered students at the University.</p>

        <form id="studentservices" runat="server">

            <p><asp:Label id="Infolbl" runat="server" /></p>

            <p>&nbsp;</p>

            <table id="logindetailstable">
                <tr>
                    <td class="formtitle">Username:</td>
                    <td>
                        <input id="UserName" type="text" runat="server" onclick="return UserName_onclick()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Static" ErrorMessage="*" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="formtitle">Password:</td>
                    <td>
                        <input id="UserPass" type="password" runat="server" onclick="return UserPass_onclick()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" Display="Static" ErrorMessage="*" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td id="submitrow">
                        <asp:button id="cmdLogin" text="Login" OnClick="Login_Click"  runat="server" />
                    </td>
                </tr>
            </table>
            <p id="errorlbl"><asp:Label id="lblResults" runat="server" /></p>
        </form>
    </div>
	
</asp:Content>


<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/StudentServicesText.png" style="width:1000px" alt="Student Services" />
</asp:Content>
  
  
