<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KDLBooking.Default" MasterPageFile="~/Green.Master"%>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://kdlbooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/KDLBookingLogo.png" alt="KDL Booking Logo" /></label></a></asp:Content>


<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">



    <form id="roomdateform" runat="server">

    <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    

    <table>
    
        <tr>
            <td>    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              

       <ContentTemplate>
        
            <asp:DropDownList ID="Roomcmb" runat="server" AutoPostBack="True" 
                onselectedindexchanged="Roomcmb_SelectedIndexChanged" class="dropdown">
                <asp:ListItem>KDL 1</asp:ListItem>
                <asp:ListItem>KDL 2</asp:ListItem>
               <asp:ListItem>KDL 3</asp:ListItem>
               <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="Weekscmb" runat="server"  class="dropdown">
            
            </asp:DropDownList>

            <asp:DropDownList ID="Dayscmb" runat="server"  class="dropdown" Visible="False">
                <asp:ListItem>Monday</asp:ListItem>
                <asp:ListItem>Tuesday</asp:ListItem>
                <asp:ListItem>Wednesday</asp:ListItem>
                <asp:ListItem>Thursday</asp:ListItem>
                <asp:ListItem>Friday</asp:ListItem>
                <asp:ListItem>Saturday</asp:ListItem>
                <asp:ListItem>Sunday</asp:ListItem>
            </asp:DropDownList>

       </ContentTemplate>

    </asp:UpdatePanel>
   

    </td>

    <td>
    <asp:Button ID="Gocmd" runat="server" Text="Go" Width="59px" 
            onclick="Gocmd_Click" />

            

    </td>
        </tr>

    </table>



    

    <div>
        
        
        
    </div>

    
        <asp:Table ID="Timetable" runat="server"></asp:Table>  
    
    </form>
</asp:Content>

<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/KDLBookingText.png" style="width:1000px" alt="Studio Booking" />
</asp:Content>