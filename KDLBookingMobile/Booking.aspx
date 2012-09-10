<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="KDLBookingMobile.Booking" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Timetables</title>
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">
    <form id="form1" runat="server">
    <div>
        <div id="banner">
            <a href="../Default.aspx" class="first">
                <img src="Images/PocketCampusHome.png" alt="Pocket Campus Home" /></a> <a href="Default.aspx">
                    <img class="second" src="Images/StudioBookingHome.png" alt="Studio Booking Home" /></a>
            <h1 class="secondheader">
                Studio Booking</h1>
        </div>
        <div id="content">
            <asp:MultiView ID="MultiView" runat="server">
                <asp:View ID="AddEditView" runat="server">
                    <h1>
                        <asp:Label ID="Bookinglbl" runat="server" Text="Label"></asp:Label>
                        Booking</h1>
                    <ul>
                        <li>Name:
                            <asp:TextBox ID="Nametxt" runat="server" Width="150px" ReadOnly="true" Style="position: absolute;
                                left: 130px;"></asp:TextBox>
                        </li>
                        <li>Room:
                            <asp:TextBox ID="Roomtxt" runat="server" Width="150px" ReadOnly="true" Style="position: absolute;
                                left: 130px;"></asp:TextBox>
                        </li>
                        <li>Date:
                            <asp:TextBox ID="Datetxt" runat="server" Width="102px" ReadOnly="true" Style="position: absolute;
                                left: 130px;"></asp:TextBox>
                        </li>
                        <li>Start Time:
                            <asp:TextBox ID="StartTimetxt" runat="server" Width="101px" ReadOnly="true" Style="position: absolute;
                                left: 130px;"></asp:TextBox>
                        </li>
                        <li>End Time:
                            <asp:DropDownList ID="EndTimecmb" runat="server" Style="position: absolute; left: 130px;">
                            </asp:DropDownList>
                        </li>
                        <li>Tel / Mobile:
                            <asp:TextBox ID="PhoneNumber" runat="server" Width="150px" Style="position: absolute;
                                left: 130px;"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="errorposition" ID="rfvTelephone" ControlToValidate="PhoneNumber"
                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </li>
                        <li>Vehicle Reg:*
                            <asp:TextBox ID="VehicleRegtxt" runat="server" Style="position: absolute; left: 130px;"></asp:TextBox>
                        </li>
                        <li>Additional Group Members:<br />
                            1.
                            <asp:TextBox ID="Member1Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member1Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            2.
                            <asp:TextBox ID="Member2Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member2Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            3.
                            <asp:TextBox ID="Member3Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member3Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            4.
                            <asp:TextBox ID="Member4Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member4Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            5.
                            <asp:TextBox ID="Member5Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member5Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            6.
                            <asp:TextBox ID="Member6Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member6Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            7.
                            <asp:TextBox ID="Member7Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member7Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            8.
                            <asp:TextBox ID="Member8Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member8Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            9.
                            <asp:TextBox ID="Member9Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member9Typecmb" runat="server" Style="position: absolute; left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            10.
                            <asp:TextBox ID="Member10Nametxt" runat="server" Style="position: absolute; left: 40px;"></asp:TextBox>
                            <asp:DropDownList ID="Member10Typecmb" runat="server" Style="position: absolute;
                                left: 190px;">
                                <asp:ListItem Text="Student" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Guest" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="Images/MakeBookingButton.png"
                                OnClick="Savecmd_Click" CssClass="alignbutton" />
                        </li>
                    </ul>
                    <ul>
                        <li><i>* Required if you intend to use a vehicle to transport and unload / load on university
                            premises. <u>No Parking on Site</u>. Parking permitted on main campus after 6pm.</i>
                            <asp:HiddenField ID="WeekHidden" runat="server" />
                            <asp:HiddenField ID="DayHidden" runat="server" />
                            <asp:HiddenField ID="StartHidden" runat="server" />
                            <asp:HiddenField ID="EditHidden" runat="server" />
                        </li>
                    </ul>
                    <ul>
                        <li>
                            <asp:Label ID="ReturnLinklbl" runat="server" Text="Return to"></asp:Label></li></ul>
                </asp:View>
                <asp:View ID="Completed" runat="server">
                    <h1>
                        Booking
                        <asp:Label ID="SaveEditlbl" runat="server" Text="Label"></asp:Label></h1>
                    <ul>
                        <li>Your booking has been succesfully saved in the system. You will recieve an email
                            shortly confirming the booking. If you need to change the booking you can return
                            to the booking and change the details or delete the booking if you dont need to
                            use the space.</li>
                    </ul>
                    <ul>
                        <li><asp:Label ID="CalendarLinklbl" runat="server" Text="Return to"></asp:Label></li></ul>
                </asp:View>
                <asp:View ID="Delete" runat="server">
                    <h1>
                        Confirm Deletion</h1>
                    <ul>
                        <li>
                        Are you sure you want to delete the following booking?</li>
                        <li><asp:Label ID="DeleteInfolbl" runat="server" Text="Label"></asp:Label></li>
                        <li><asp:ImageButton ID="Deletedcmb" runat="server" 
                                ImageUrl="Images/DeleteBookingButton.png" CssClass="alignbutton" 
                                onclick="Deletedcmb_Click"/></li>
                    </ul>
               
                    <asp:HiddenField ID="BIDHidden" runat="server" />
                    
                </asp:View>
                <asp:View ID="Deleted" runat="server">
                    <h1>
                        Booking Deleted</h1>
                    <ul>
                        <li>The booking has been succesfully deleted.</li></ul>
                    <ul>
                        <li><a href="Default.aspx">Return to Studio Booking</a></li></ul>
                </asp:View>
                <asp:View ID="ErrorView" runat="server">
                    <h1>
                        Booking Error</h1>
                    <ul>
                        <li>You booking cannot be processed becuase
                        <asp:Label ID="Errorlbl" runat="server" Text="Label"></asp:Label></li></ul>
                    <ul>
                        <li><a href="Default.aspx?View=true">Return to Calendar</a></li></ul>
                </asp:View>
            </asp:MultiView>
            <ul><li>&copy; University of Hull Scarborough Campus</li></ul>
        </div>
    </form>
</body>
</html>
