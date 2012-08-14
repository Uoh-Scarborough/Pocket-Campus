
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="StudioBooking.Booking" MasterPageFile="~/Blue.Master" %>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>
<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studiobooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingLogo.png" alt="Studio Booking Logo" /></label></a></asp:Content>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">
    <form id="bookingform" runat="server" style="padding:10px;">

<uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
        <div>
            <asp:MultiView ID="MultiView" runat="server">
                <asp:View ID="AddEditView" runat="server">
                    <h1><asp:Label ID="Bookinglbl" runat="server" Text="Label"></asp:Label> Booking</h1>
                    <table>
                        <tr>
                            <td>Name:</td>
                            <td><asp:TextBox ID="Nametxt" runat="server" Width="358px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Room:</td>
                            <td><asp:TextBox ID="Roomtxt" runat="server" Width="200px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Date:</td>
                            <td><asp:TextBox ID="Datetxt" runat="server" Width="102px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Start Time:</td>
                            <td><asp:TextBox ID="StartTimetxt" runat="server" Width="101px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>End Time:</td>
                            <td>
                                <asp:DropDownList ID="EndTimecmb" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tel / Mobile no:</td>
                            <td><asp:TextBox ID="PhoneNumber" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td valign="top">Additional Group Members:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>1</td>
                                        <td><asp:TextBox ID="Member1Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member1Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                        <td>2</td>
                                        <td><asp:TextBox ID="Member2Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member2Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td><asp:TextBox ID="Member3Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member3Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                        <td>4</td>
                                        <td><asp:TextBox ID="Member4Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member4Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td><asp:TextBox ID="Member5Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member5Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                        <td>6</td>
                                        <td><asp:TextBox ID="Member6Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member6Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td><asp:TextBox ID="Member7Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member7Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                        <td>8</td>
                                        <td><asp:TextBox ID="Member8Nametxt" runat=server></asp:TextBox></td>
                                        <td><asp:DropDownList ID="Member8Typecmb" runat=server><asp:ListItem Text="Student" Value=1></asp:ListItem><asp:ListItem Text="Guest" Value=2></asp:ListItem></asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Vehicle Registration*</td>
                            <td><asp:TextBox ID="VehicleRegtxt" runat=server></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2"><i>* Required if you intend to use a vehicle to transport and unload / load on university premises. <u>No Parking on Site</u>. Parking permitted on main campus after 6pm.</i></td>
                        </tr>

                        <tr>
                            <td>
                                <asp:HiddenField ID="WeekHidden" runat="server" />
                                <asp:HiddenField ID="DayHidden" runat="server" />
                                <asp:HiddenField ID="StartHidden" runat="server" />
                                <asp:HiddenField ID="EditHidden" runat="server" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table> 
                    <asp:Button ID="Savecmd" runat="server" Text="Make Booking" onclick="Savecmd_Click"/>
                    <p>Return to <asp:Label ID="ReturnLinklbl" runat="server" Text="Label"></asp:Label></p>
                </asp:View>
                <asp:View ID="Completed" runat="server">
                    <h1>Booking <asp:Label ID="SaveEditlbl" runat="server" Text="Label"></asp:Label></h1>
                    <p>Your booking has been succesfully saved in the system. You will recieve an email 
                        shortly confirming the booking. If you need to change the booking you can return 
                        to the booking and change the details or delete the booking if you dont need to 
                        use the space.</p>
                    <br />
                    <p>Return to <asp:Label ID="CalendarLinklbl" runat="server" Text="Label"></asp:Label></p>
                </asp:View>
                <asp:View ID="Delete" runat="server">
                    <h1>Confirm Deletion</h1>
                    <p>Are you sure you want to delete the following booking?</p>
                    <p>Booking: <asp:Label ID="DeleteInfolbl" runat="server" Text="Label"></asp:Label></p>
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
                    <p>Your booking cannot be processed because <asp:Label ID="Errorlbl" runat="server" Text="Label"></asp:Label></p>
                    <p><a href="Default.aspx">Return to Calendar</a></p>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</asp:Content>

<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingText.png" alt="Studio Booking" />
</asp:Content>