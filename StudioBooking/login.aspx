<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="StudioBooking.login" MasterPageFile="~/Blue.Master" %>

<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studiobooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingLogo.png" alt="Studio Booking Logo" /></label></a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    
        <h1>Welcome to 84 Filey Road Studio Booking</h1>

        <form id="studentservices" runat="server">

            <p>To view Studio Bookings you must first log into the 84 Filey Road Studio Booking System using your campus username and password.</p>
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
    <img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingText.png" alt="Studio Booking" />
</asp:Content>
  
  
