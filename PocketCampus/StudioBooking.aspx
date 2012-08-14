<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudioBooking.aspx.cs" Inherits="PocketCampus.StudioBooking" MasterPageFile="~/Red.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="form" runat="server">

                 <table cellpadding="4px" id="studiobookingtable">
                    <tr>
                        <td><a href="http://productionbooking.scar.hull.ac.uk/login.aspx"><img src="http://pocketcampusimages.scar.hull.ac.uk/PSStudioBookingButton.png" alt="Production Booking" /></a></td>
                        <td><h2>Production Studio Booking</h2><h3>Welcome to Production Studio Booking. To view or book a studio you must first log into the Production Booking System using your campus username and password. Click the button to the left to continue to the Production Booking System.</h3></td>
                    </tr>
                    <tr>
                        <td><a href="http://studiobooking.scar.hull.ac.uk/login.aspx"><img src="http://pocketcampusimages.scar.hull.ac.uk/84StudioBookingButton.png" alt="Studio Booking" /></a></td>
                        <td><h2>84 Filey Road Studio Booking</h2><h3>Welcome to 84 Filey Road Booking. To view or book a studio you must first log into the 84 Filey Road Studio Booking Ststem using your campus username and password. Click the button to the left to continue to the Studio Booking System.</h3></td>
                    </tr>
                </table> 
      
    </form>
</asp:Content>
