<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ProductionBooking.login" MasterPageFile="~/GoldMenu.Master" %>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    
        <h1>Welcome to Production Booking</h1>

        <form id="studentservices" runat="server">

            <p>To view Production Bookings you must first log into the Production Booking System using your campus username and password.</p>
            <p>&nbsp;</p>

            <table style="width:301px; margin-left:200px; padding:10px">
                <tr>
                    <td class="formtitle">Username:</td>
                    <td>
                        <input id="UserName" type="text" runat="server" style="width: 160px" 
                            onclick="return UserName_onclick()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Static" ErrorMessage="*" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="formtitle">Password:</td>
                    <td>
                        <input id="UserPass" type="password" runat="server" 
                            onclick="return UserPass_onclick()" style="width: 160px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" Display="Static" ErrorMessage="*" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:button id="cmdLogin" text="Login" OnClick="Login_Click"  runat="server" 
                            style="margin-left: 129px"/>
                    </td>
                </tr>
            </table>
            <p><asp:Label id="lblResults" ForeColor="red" Font-Size="10" runat="server" /></p>
        </form>
	
</asp:Content>
  
  
