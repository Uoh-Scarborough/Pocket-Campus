<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KDLBooking.Default" MasterPageFile="~/Responsive.Master"%>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">


    <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    

    <table>
    
        <tr>
            <td>    
        
            <asp:DropDownList ID="Roomcmb" runat="server" AutoPostBack="True" 
                onselectedindexchanged="Roomcmb_SelectedIndexChanged" class="dropdown" 
                AppendDataBoundItems="True">
                <asp:ListItem>KDL 1</asp:ListItem>
                <asp:ListItem>KDL 2</asp:ListItem>
               <asp:ListItem>KDL 3</asp:ListItem>
               <asp:ListItem>All</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="Weekscmb" runat="server"  class="dropdown" 
                AppendDataBoundItems="True" AutoPostBack="True" 
                onselectedindexchanged="Weekscmb_SelectedIndexChanged">
            
            </asp:DropDownList>

            <asp:DropDownList ID="Dayscmb" runat="server"  class="dropdown" Visible="False" 
                AutoPostBack="True" AppendDataBoundItems="True" 
                    onselectedindexchanged="Dayscmb_SelectedIndexChanged1">
                <asp:ListItem>Monday</asp:ListItem>
                <asp:ListItem>Tuesday</asp:ListItem>
                <asp:ListItem>Wednesday</asp:ListItem>
                <asp:ListItem>Thursday</asp:ListItem>
                <asp:ListItem>Friday</asp:ListItem>
                <asp:ListItem>Saturday</asp:ListItem>
                <asp:ListItem>Sunday</asp:ListItem>
            </asp:DropDownList>

        

    </td>
        </tr>

    </table>



    

    <div>
        
        
        
    </div>

    <asp:Table ID="Timetable" runat="server"></asp:Table>  

</asp:Content>
