<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="ProductionBooking.Booking" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://productionbooking.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingLogo.png" alt="Production Booking" /></a>
    
</asp:Content>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">
    <form id="bookingform" runat="server" style="padding:10px;">
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
                            <td><asp:TextBox ID="Roomtxt" runat="server" Width="103px" ReadOnly=true></asp:TextBox></td>
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
                            <td>Module / Group:</td>
                            <td><asp:TextBox ID="ModuleGrouptxt" runat="server" Width="358px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="Required" ControlToValidate="ModuleGrouptxt"></asp:RequiredFieldValidator>
                             </td>
                        </tr>
                        <!--
                         <tr>
                            <td>Additional Info:</td>
                            <td><asp:TextBox ID="AdditionalInfotxt" runat="server" Width="358px" Height="92px" 
                                    TextMode="MultiLine"></asp:TextBox>
                                
                             </td>
                        </tr>
                        -->
                        <tr>
                            <td>Group Members:</td>
                            <td><asp:TextBox ID="GroupMemberstxt" runat="server" Width="358px"></asp:TextBox>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>Workshop Access:</td>
                            <td>
                                <asp:CheckBox ID="Workshopchk" runat="server" />
                            </td>
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
                    <p>You booking has been succesfully saved in the system. You will recieve an email shortly confirming the booking. If you need to change the booking you can return to the booking and change the details or delete the booking if you dont need to use the space.</p>
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
                    <p>You booking cannot be processed becuase <asp:Label ID="Errorlbl" runat="server" Text="Label"></asp:Label></p>
                    <p><a href="Default.aspx">Return to Calendar</a></p>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingText.png" style="width:1000px; padding:5px 0 0 0; margin:0px; border:0px; position:relative; left:-12px;" alt="Production Booking"/>

</asp:Content>