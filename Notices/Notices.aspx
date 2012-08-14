<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notices.aspx.cs" Inherits="Comms.Notices" MasterPageFile="~/Red.Master" ValidateRequest="false"%>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="roomdateform" runat="server">
   
                <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="ListView" runat="server">
                        <h1><asp:Label ID="Noticelbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="NoticesInstructionslbl" runat="server"></asp:Label></p>
                        <asp:Table ID="NoticesTable" runat="server" style="width:980px; Height:51px; padding:10px">
                            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tabletitle">Title</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledate">Display From</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledate">Display To</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablevalid">Valid</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>
                    <asp:View ID="AddEditView" runat="server">
                        <h1><asp:Label ID="AddEditlbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="AddEditInstructionlbl" runat="server"></asp:Label></p><br />
                        <table>
                            <tr>
                                <td valign="top">Title:</td>
                                <td><asp:TextBox ID="Titletxt" runat="server" Width="600px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="TitleValidator" runat="server" 
                                        ControlToValidate="Titletxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Notice:</td>
                                <td><asp:TextBox ID="Noticetxt" runat="server" Width="600px" TextMode="MultiLine" 
                                        Height="96px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NoticeValidator" runat="server" 
                                        ControlToValidate="Noticetxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Display From:</td>
                                <td>
                                    <BDP:BDPLite ID="DisplayFromCal" runat="server" />
                                    <asp:RequiredFieldValidator ID="DisplayFromValidator" runat="server" 
                                        ControlToValidate="DisplayFromCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Display To:</td>
                                <td>
                                    <BDP:BDPLite ID="DisplayToCal" runat="server" />
                                    <asp:RequiredFieldValidator ID="DisplayToValidator" runat="server" 
                                        ControlToValidate="DisplayToCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Priority Notice:</td>
                                <td>
                                    <asp:CheckBox ID="Urgentchk" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Category:</td>
                                <td>
                                    <asp:DropDownList ID="Categorylst" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Attachment:</td>
                                <td>
                                    <asp:FileUpload ID="Attachment" runat="server" Height="24px" Width="294px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan=2 style="text-align: right"><asp:Button ID="SaveEventcmd" runat="server" Text="Save Event" onclick="SaveEventcmd_Click" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="AdminView" runat="server">
                        <h1>Admin View</h1>
                        <p>The form below shows a notice. You can either:</p>
                        <ul>
                            <li>Validate a Notice - This will add the notice to the live system.</li>
                            <li>Invalidate a Notice - This will remove the notice from the live system, and 
                                email the person who posted the notice asking them to make changes.</li>
                        </ul>
                        <table>
                            <tr>
                                <td valign="top">Posted By:</td>
                                <td><asp:TextBox ID="AdminPostedBytxt" readonly="true" runat="server" Width="600px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top">Posted Date:</td>
                                <td><asp:TextBox ID="AdminPostedDatetxt" readonly="true" runat="server" Width="600px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top">Title:</td>
                                <td><asp:TextBox ID="AdminTitletxt" runat="server" Width="600px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminTitleValidator" runat="server" 
                                        ControlToValidate="AdminTitletxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td valign="top">Notice:</td>
                                <td><asp:TextBox ID="AdminNoticetxt" runat="server" Width="600px" TextMode="MultiLine" 
                                        Height="96px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminNoticeValidator" runat="server" 
                                        ControlToValidate="AdminNoticetxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td valign="top">Display From:</td>
                                <td>
                                    <BDP:BDPLite ID="AdminDisplayFromCal" runat="server" /><asp:RequiredFieldValidator ID="AdminDisplayFromValidator" runat="server" 
                                        ControlToValidate="AdminDisplayFromCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Display To:</td>
                                <td>
                                    <BDP:BDPLite ID="AdminDisplayToCal" runat="server" /><asp:RequiredFieldValidator ID="AdminDisplayToValidator" runat="server" 
                                        ControlToValidate="AdminDisplayToCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Priority Notice:</td>
                                <td>
                                    <asp:CheckBox ID="AdminUrgentchk" runat="server" />
                                </td>
                            </tr>
                             <tr>
                                <td valign="top">Category:</td>
                                <td>
                                    <asp:DropDownList ID="AdminCategorylst" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Attachment:</td>
                                <td>
                                    <asp:Label ID="AdminAttachmentURLlbl" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Valid:</td>
                                <td>
                                    <asp:CheckBox ID="AdminValidchk" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Invalid Reason:</td>
                                <td>
                                    <asp:TextBox ID="AdminInvalidReasontxt" TextMode="MultiLine" runat="server" Width="600px" Height="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan=2 style="text-align: right"><asp:Button ID="AdminSavecmd" runat="server" Text="Save" onclick="AdminSavecmd_Click" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
    
    </form>
    
</asp:Content>