<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PocketCampus.Default" MasterPageFile="~/Red.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="form1" runat="server">

                 <table cellpadding="5px" id="buttonstable">
                    <tr>
                        <td><asp:Label ID="Mapslbl" runat="server"></asp:Label></td>
                        <td><a href="http://info.scar.hull.ac.uk?page=Library"><img src="http://pocketcampusimages.scar.hull.ac.uk/LibraryButton.png" alt="Library" /></a></td>
                        <td><a href="http://studentservices.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudentServicesButton.png" alt="Student Services" /></a></td>
                        <td><a href="http://portal.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/PortalButton.png" alt="Portal" /></a></td>
                        <td><a href="FaPC.aspx"><img src="http://pocketcampusimages.scar.hull.ac.uk/FindAPCButton.png" alt="Find a Computer" /></a></td>
                        <td><a href="http://timetables.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/TimetablesButton.png" alt="Timetables" /></a></td>
                    </tr>
                    <tr>
                        <td><a href="http://info.scar.hull.ac.uk/"><img src="http://pocketcampusimages.scar.hull.ac.uk/CampusInfoButton.png" alt="Campus Info" /></a></td>
                        <td><a href="http://ebridge.hull.ac.uk/"><img src="http://pocketcampusimages.scar.hull.ac.uk/eBridgeButton.png" alt="eBridge" /></a></td>
                        <td><a href="http://info.scar.hull.ac.uk?Page=CampusConnect"><img src="http://pocketcampusimages.scar.hull.ac.uk/CampusConnect.png" alt="Campus Connect" /></a></td>
                        <td><a href="http://mail.scar.hull.ac.uk/"><img src="http://pocketcampusimages.scar.hull.ac.uk/EmailButton.png" alt="Email" /></a></td>
                        <td><a href="http://info.scar.hull.ac.uk?page=IT"><img src="http://pocketcampusimages.scar.hull.ac.uk/ITServicesButton.png" alt="IT Services" /></a></td>
                        <td><a href="studiobooking.aspx"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingButton.png" alt="Studio Booking" /></a></td>
                    </tr>
                </table>

                <table cellpadding="4px" id="noticeseventstable" width="100px">
                
                    <tr>
                    
                        <td valign=top align=left width=50%><asp:Label ID="Eventslbl" runat="server" Text="Label"></asp:Label></td>
                        <td valign=top align=left width=50%><asp:Label ID="Noticeslbl" runat="server" Text="Label"></asp:Label></td>
                    
                    </tr>
                
                </table>
            
                
          
           
                 
      
    </form>
</asp:Content>