<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductionBooking._Default" MasterPageFile="~/Red.Master"%>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://productionbooking.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingLogo.png" alt="Production Booking" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="roomdateform" runat="server">
    
        <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <table>
            <tr>
                <td><asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>
                    
                        <asp:DropDownList ID="Roomcmb" runat="server" 
                            onselectedindexchanged="Roomcmb_SelectedIndexChanged">
                            <asp:ListItem>PS1</asp:ListItem>
                            <asp:ListItem>PS2</asp:ListItem>
                            <asp:ListItem>PS3</asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList ID="Weekscmb" runat="server" Width="270px">
                        </asp:DropDownList>
                    
                    </ContentTemplate>
                    
                
                </asp:UpdatePanel></td>

                <td>
                       <asp:Button ID="Gocmd" runat="server" Text="Go" Width="59px" onclick="Gocmd_Click" />
                </td>
            
            </tr>
        </table>

    <div>
        
        <asp:Table ID="Timetable" runat="server"></asp:Table>  
    </div>
    </form>
</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingText.png" style="width:1000px; padding:5px 0 0 0; margin:0px; border:0px; position:relative; left:-12px;" alt="Production Booking"/>

</asp:Content>