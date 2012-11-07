<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudioBooking.Default" MasterPageFile="~/Blue.Master"%>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studiobooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingLogo.png" alt="Studio Booking Logo" /></label></a></asp:Content>


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
                <asp:ListItem>Recording Studio 1</asp:ListItem>
                <asp:ListItem>Recording Studio 2</asp:ListItem>
               <asp:ListItem>Music Room</asp:ListItem>
               <asp:ListItem>Rehearsal Studio 1</asp:ListItem>
               <asp:ListItem>Rehearsal Studio 2</asp:ListItem>
               <asp:ListItem>Research Studio</asp:ListItem>
               <asp:ListItem>Overdub Studio</asp:ListItem>
               <asp:ListItem>Seminar Workstation 1</asp:ListItem>
               <asp:ListItem>Seminar Workstation 2</asp:ListItem>
               <asp:ListItem>Seminar Workstation 3</asp:ListItem> 
               <asp:ListItem>Mixing Studio 1</asp:ListItem>
               <asp:ListItem>Mixing Studio 2</asp:ListItem>
               <asp:ListItem>Postgraduate Studio</asp:ListItem>
               <asp:ListItem>Mixing Studio 3</asp:ListItem> 
               <asp:ListItem>Seminar Room 2</asp:ListItem> 
            </asp:DropDownList>
            <asp:DropDownList ID="Weekscmb" runat="server"  class="dropdown">
            </asp:DropDownList>
 
       </ContentTemplate>

    </asp:UpdatePanel></td>

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
    <img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingText.png" style="width:1000px" alt="Studio Booking" />
</asp:Content>