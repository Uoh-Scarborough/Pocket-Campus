<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Comms.Events" MasterPageFile="~/Red.Master" ValidateRequest="false"%>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="eventsform" runat="server">
   
                <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="ListView" runat="server">
                        <h1><asp:Label ID="Eventlbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="EventsInstructionslbl" runat="server"></asp:Label></p>
                        <asp:Table ID="EventsTable" runat="server" style="width:980px; Height:51px; padding:10px">
                            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tabletitle">Title</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tableeventdatetime">Date / Time</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablelocation">Location</asp:TableHeaderCell>
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
                                <td valign="top">Event:</td>
                                <td><asp:TextBox ID="Eventtxt" runat="server" Width="600px" TextMode="MultiLine" 
                                        Height="96px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EventsValidator" runat="server" 
                                        ControlToValidate="Eventtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Location:</td>
                                <td><asp:TextBox ID="Locationtxt" runat="server" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="LocationValidator" runat="server" 
                                        ControlToValidate="Locationtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Event Date:</td>
                                <td>
                                    <BDP:BDPLite ID="EventDateCal" runat="server" />
                                    <asp:RequiredFieldValidator ID="EventDateValidator" runat="server" 
                                        ControlToValidate="EventDateCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Event Time:</td>
                                <td>
                                    <asp:TextBox ID="EventTimetxt" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EventTimeValidator" runat="server" 
                                        ControlToValidate="EventTimetxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ID="EventTimeExpValidator" runat="server" ErrorMessage="Must be in the format HH:MM" ControlToValidate="EventTimetxt" ValidationExpression="^(([0-1]?[0-9])|([2][0-3])):([0-5]?[0-9])(:([0-5]?[0-9]))?$"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Duration:</td>
                                <td>
                                    <asp:TextBox ID="Durationtxt" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="DurationValidator" runat="server" 
                                        ControlToValidate="Durationtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
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
                                <td colspan=2 style="text-align: right"><asp:Button ID="SaveNoticecmd" runat="server" Text="Save Notice" onclick="SaveNoticecmd_Click" /></td>
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
                                <td valign="top">Event:</td>
                                <td><asp:TextBox ID="AdminEventtxt" runat="server" Width="600px" TextMode="MultiLine" 
                                        Height="96px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminEventValidator" runat="server" 
                                        ControlToValidate="AdminEventtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td valign="top">Location:</td>
                                <td><asp:TextBox ID="AdminLocationstxt" runat="server" Width="400px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminLocationsValidator" runat="server" 
                                        ControlToValidate="AdminLocationstxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td valign="top">Event Date:</td>
                                <td>
                                    <BDP:BDPLite ID="AdminEventDateCal" runat="server" /><asp:RequiredFieldValidator ID="AdminEventDateValidator" runat="server" 
                                        ControlToValidate="AdminEventDateCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Event Time:</td>
                                <td>
                                    <asp:TextBox ID="AdminEventTimetxt" runat="server" Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminEventTimeValidator" runat="server" 
                                        ControlToValidate="AdminEventTimetxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Event Duration:</td>
                                <td>
                                    <asp:TextBox ID="AdminDurationtxt" runat="server" Width="50px"></asp:TextBox><asp:RequiredFieldValidator ID="AdminDurationValidator" runat="server" 
                                        ControlToValidate="AdminDurationtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
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