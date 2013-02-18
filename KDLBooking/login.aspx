<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="KDLBooking.login" MasterPageFile="~/Responsive.Master" %>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    
        <h1>Welcome to KDL Room Booking</h1>

            <p>To view KDL Bookings you must first log into the Booking System using your campus username and password.</p>

            <p><b>Please note:</b> KDL rooms are for student use only. Staff should use Campus Roombookings. Bookings are checked daily and any made by staff will be cancelled.</p>

            <table id="logindetailstable">
                <tr>
                    <td class="formtitle">Username:</td>
                    <td>
                        <input id="UserName" class="inputtext" type="text" runat="server" onclick="return UserName_onclick()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Static" ErrorMessage="*" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="formtitle">Password:</td>
                    <td>
                        <input id="UserPass" class="inputtext" type="password" runat="server" onclick="return UserPass_onclick()" />
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
	
</asp:Content>

  
  
