<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ProductionBooking.login" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://productionbooking.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingLogo.png" alt="Production Booking" />

</a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <table>
        <tr>
            <td width="660px">
            <h1>Welcome to Production Booking</h1>

        <form id="studentservices" runat="server">

            <p>To view Production Bookings you must first log into the Production Booking System using your campus username and password.</p>
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
            </td>
            <td valign="top" style="background:white; width:300px; padding: 10px;">
                <h2 style="color:Black; padding-left:0px;">Peformance Space Documents</h2>

                <h3>Documents</h3>

                <ul>
                    <li><a href="Documents/SANM%20Event-Use%20of%20Space%20Guidelines%20V4.0.pdf">Use of Space Guidelines V4.0</a></li>
                    <li><a href="Documents/SANM%20Pre-Show%20H%20&%20S%20checklist.pdf">Pre-Show
                        H &amp; S checklist</a></li>
                    <li><a href="Documents/SANM%20Risk%20Assessment%20Template.doc">ANM Risk
                        Assessment Template.doc</a></li>
                    <li><a href="Documents/UHSC%20PS1%20PS2%20Technical%20Specifications%20091009.pdf">
                        PS1 PS2 Technical Specifications 091009</a></li>
                </ul>

                <h3>Plans</h3>

                <ul>
                    <li><a href="Documents/PS1%20Plan%20view.pdf">PS1 Plan view</a></li>
                    <li><a href="Documents/PS2%20Plan%20view.pdf">PS2 Plan view</a></li>
                    <li><a href="Documents/PS3%20Plan%20view.pdf">PS3 Plan view</a></li>
                </ul>
            </td>
        
        </tr>
    </table>

</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingText.png" style="width:1000px; padding:5px 0 0 0; margin:0px; border:0px; position:relative; left:-12px;" alt="Production Booking"/>

</asp:Content>
  
  
