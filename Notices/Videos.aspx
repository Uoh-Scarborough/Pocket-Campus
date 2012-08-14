<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Videos.aspx.cs" Inherits="Comms.Videos" MasterPageFile="~/Red.Master" %>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="roomdateform" runat="server">
   
                <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="ListView" runat="server">
                        <h1>Videos</h1>
                        <p>Below are the current Videos running on Screens and Kiosks across the campus.</p>
                        <p><a href="Videos.aspx?AID=1&amp;VID=-1">Add Video</a></p>
                        <asp:Table ID="VideoTable" runat="server" style="width:980px; Height:51px; padding:10px">
                            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tableurl">URL</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledisplayfrom">Display From</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledisplayto">Display To</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>
                    <asp:View ID="AddEditView" runat="server">
                        <h1><asp:Label ID="AddEditlbl" runat="server"></asp:Label></h1>
                        <p>Complete the form below to add a new video. <b>N.B.</b> The URL can be
                            http://localhost if the video is on the kiosk.</p><br />
                        <table>
                            <tr>
                                <td valign="top">Video URL:</td>
                                <td><asp:TextBox ID="URLtxt" runat="server" Width="600px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="TitleValidator" runat="server" 
                                        ControlToValidate="URLtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Show From:</td>
                                <td>
                                    <BDP:BDPLite ID="ShowFromCal" runat="server" />
                                    <asp:RequiredFieldValidator ID="ShowFromValidator" runat="server" 
                                        ControlToValidate="ShowFromCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Show To:</td>
                                <td>
                                    <BDP:BDPLite ID="ShowToCal" runat="server" />
                                    <asp:RequiredFieldValidator ID="ShowToValidator" runat="server" 
                                        ControlToValidate="ShowToCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan=2 style="text-align: right"><asp:HiddenField runat="server" ID="HiddenEditField"/>
                                    <asp:Button ID="SaveVideocmd" runat="server" Text="Save Video" 
                                        onclick="SaveVideocmd_Click" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="CompletedView" runat="server">
                        <h1><asp:Label ID="CompletedHeaderlbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="Completedlbl" runat="server"></asp:Label></p>
                        <p>Return to the <a href="Videos.aspx">Videos List.</a></p>
                    </asp:View>
                </asp:MultiView>
    
    </form>
    
</asp:Content>