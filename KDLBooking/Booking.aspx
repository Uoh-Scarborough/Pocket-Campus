
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="KDLBooking.Booking" MasterPageFile="~/Responsive.Master" %>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">

<uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
        <div>
            <asp:MultiView ID="MultiView" runat="server">
                <asp:View ID="AddEditView" runat="server">
                    <h1><asp:Label ID="Bookinglbl" runat="server" Text="Label"></asp:Label> Booking</h1>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

                    <table id="bookingtable">
                        <tr>
                            <td class="formtitle">Name:</td>
                            <td><asp:TextBox ID="Nametxt" runat="server" ReadOnly=true CssClass="inputtext"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formtitle">Room:</td>
                            <td><asp:TextBox ID="Roomtxt" runat="server" ReadOnly=true CssClass="inputtext"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="formtitle">Date:</td>
                            <td><asp:TextBox ID="Datetxt" runat="server" ReadOnly=true CssClass="inputtext"></asp:TextBox></td>
                        </tr>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                        <tr>
                            <td class="formtitle">Start Time:</td>
                            <td><asp:DropDownList ID="StartTimecmb" runat="server" AutoPostBack="true" OnSelectedIndexChanged="StartTimecmd_SelectedIndexChanged" CssClass="inputtext">
                                </asp:DropDownList>
                                
                                <!--<asp:TextBox ID="StartTimetxt" runat="server" Width="101px" ReadOnly=true></asp:TextBox>--></td>
                        </tr>
                        <tr>
                            <td class="formtitle">End Time:</td>
                            <td>
                                <asp:DropDownList ID="EndTimecmb" runat="server" CssClass="inputtext">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        <tr>
                            <td class="formtitle">Tel / Mobile no:</td>
                            <td><asp:TextBox ID="PhoneNumber" runat="server" CssClass="inputtext"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="WeekHidden" runat="server" />
                                <asp:HiddenField ID="DayHidden" runat="server" />
                                <asp:HiddenField ID="StartHidden" runat="server" />
                                <asp:HiddenField ID="LengthHidden" runat="server" />
                                <asp:HiddenField ID="EditHidden" runat="server" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td id="submitrow"><asp:Button ID="Savecmd" runat="server" Text="Make Booking" onclick="Savecmd_Click"/></td>
                        </tr>
                    </table> 
                    <p>Return to <asp:Label ID="ReturnLinklbl" runat="server" Text="Label" CssClass=visiblespan></asp:Label></p>
                </asp:View>
                <asp:View ID="Completed" runat="server">
                    <h1>Booking <asp:Label ID="SaveEditlbl" runat="server" Text="Label" CssClass=visiblespan></asp:Label></h1>
                    <p>Your booking has been succesfully saved in the system. You will recieve an email 
                        shortly confirming the booking. If you need to change the booking you can return 
                        to the booking and change the details or delete the booking if you dont need to 
                        use the space.</p>
                    <br />
                    <p>Return to <asp:Label ID="CalendarLinklbl" runat="server" Text="Label" CssClass=visiblespan></asp:Label></p>
                </asp:View>
                <asp:View ID="Delete" runat="server">
                    <h1>Confirm Deletion</h1>
                    <p>Are you sure you want to delete the following booking?</p>
                    <p>Booking: <asp:Label ID="DeleteInfolbl" runat="server" Text="Label" CssClass=visiblespan></asp:Label></p>
                    <asp:HiddenField ID="BIDHidden" runat="server" />
                    <asp:Button ID="Deletedcmb" runat="server" Text="Delete Booking" onclick="Deletecmd_Click"/>
                </asp:View>
                <asp:View ID="Deleted" runat="server">
                    <h1>Booking Deleted</h1>
                    <p>The booking has been succesfully deleted.</p>
                    <p><a href="Default.aspx">Return to Calendar</a></p>
                </asp:View>
                <asp:View ID="ErrorView" runat="server">
                    <h1>Booking Error</h1>
                    <p>Your booking cannot be processed because <asp:Label ID="Errorlbl" runat="server" Text="Label" CssClass=visiblespan></asp:Label></p>
                    <p><a href="Default.aspx">Return to Calendar</a></p>
                </asp:View>
                <asp:View ID="MyBookingsView" runat="server">
                    <h1>My Bookings</h1>
                    <asp:Label ID="BookingsList" runat="server" CssClass=visiblespan></asp:Label>
                    <p><a href="Default.aspx">Return to Calendar</a></p>
                </asp:View>
            </asp:MultiView>
        </div>
</asp:Content>