<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="KDLBooking.login" MasterPageFile="~/Green.Master" %>

<asp:Content ID="KDLBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://kdlbooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/KDLBookingLogo.png" alt="KDL Booking Logo" /></label></a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    
        <h1>Welcome to KDL Room Booking</h1>

        <form id="studentservices" runat="server">

            <p>To view KDL Bookings you must first log into the Booking System using your campus username and password.</p>
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
	
</asp:Content>

<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/KDLBookingText.png" alt="Studio Booking" />
</asp:Content>
  
  
