<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signs.aspx.cs" Inherits="ProductionBooking.Signs" MasterPageFile="~/Red.Master"%>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://productionbooking.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingLogo.png" alt="Production Booking" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    <form id="signsform" runat="server">
    

     <asp:MultiView ID="MultiView" runat="server">

                    <asp:View ID="ListView" runat="server">
                        <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
                        <h1>Performance Studio Signs</h1>
                        <p>The list below shows the signs which have been setup for use outside Performance 
                            Studios.</p>
                        <p><a href="?aid=1&sid=-1">Click to Add a new Signs</a></p>
                        <asp:Table ID="SignsTable" runat="server" Width="990px" Height="51px" style="padding:10px;">
                            <asp:TableHeaderRow BackColor="#ae2b30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tablestudio">Studio</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledatetime">Date / Time</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablesign">Sign</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>

                    <asp:View ID="AddEditView" runat="server">
                    
                        <h1><asp:Label ID="AddEditlbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="AddEditInstructionlbl" runat="server"></asp:Label></p><br />
                        <table>
                            <tr>
                                <td valign="top">Room:</td>
                                <td>
                                    <asp:DropDownList ID="Roomcmb" runat="server">
                                        <asp:ListItem>PS2</asp:ListItem>
                                        <asp:ListItem>PS3</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Display From:</td>
                                <td valign="top">Date: <BDP:BDPLite ID="DisplayFromDate" runat="server" /> Time: 
                                    <asp:DropDownList ID="DisplayFromTimecmb" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DisplayFromDate" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Display To:</td>
                                <td valign="top">Date: <BDP:BDPLite ID="DisplayToDate" runat="server" /> Time: 
                                    <asp:DropDownList ID="DisplayToTimecmb" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DisplayToDate" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Sign:</td>
                                <td>
                                    <asp:DropDownList ID="Signcmb" runat="server">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="AddSignTypecmd" runat="server" CausesValidation="False" 
                                        ImageUrl="~/Properties/script_add.png" onclick="AddSignTypecmd_Click" />
                                    <asp:ImageButton ID="SignTypeEditcmd" runat="server" CausesValidation="False" 
                                        ImageUrl="~/Properties/script_edit.png" onclick="SignTypeEditcmd_Click" />
                                </td>
                            </tr>

                            <tr>
                                <td colspan=2 style="text-align: right">
                                    <asp:Button ID="SaveSigncmd" runat="server" 
                                        Text="Save Sign" onclick="SaveSigncmd_Click" /></td>
                            </tr>
                        </table>
                        
                    </asp:View>

                    <asp:View ID="AddEditSignView" runat="server">

                        <h1><asp:Label ID="AddEditSignlbl" runat="server"></asp:Label> Sign</h1>

                        <p><asp:Label ID="AddEditSignInfolbl" runat="server"></asp:Label></p>

                        <table>
                            <tr>
                                <td valign="top">Title:</td>
                                <td>
                                    <asp:TextBox ID="SignTitletxt" runat="server" Width="382px"/>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Sign:</td>
                                <td>
                                    &nbsp;<asp:FileUpload ID="SignUpload" runat="server" Width="381px" />
                                </td>
                            </tr>
                             <tr>
                                <td colspan=2 style="text-align: right"><asp:Button ID="DeleteSignTypecmd" 
                                        runat="server" Text="Delete Sign" onclick="DeleteSignTypecmd_Click" />
                                    <asp:Button ID="SaveSignTypecmd" runat="server" onclick="SaveSignTypecmd_Click" 
                                        Text="Save Sign" />
                                 </td>
                            </tr>
                        </table>

                        <asp:HiddenField ID="TempID" runat="server" />

                    </asp:View>

                    <asp:View ID="MessageView" runat="server">

                        <h1><asp:Label ID="MessageTitlelbl" runat="server" /></h1>
                        <p><asp:Label ID="Messagelbl" runat="server" /></p>

                    </asp:View>

     </asp:MultiView>

    </form>
</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/ProductionBookingText.png" style="width:1000px; padding:5px 0 0 0; margin:0px; border:0px; position:relative; left:-12px;" alt="Production Booking"/>

</asp:Content>
