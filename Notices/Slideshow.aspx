<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Slideshow.aspx.cs" Inherits="Comms.Slideshow"  MasterPageFile="~/Red.Master"%>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainArea" runat="server">

    <form id="roomdateform" runat="server">
   
                <asp:MultiView ID="MultiView" runat="server">
                    
                    <asp:View ID="ListView" runat="server">
                        
                        <h1><asp:Label ID="Slideshowlbl" runat="server">Slideshows</asp:Label></h1>
                        
                        <p><asp:Label ID="SlideshowInstructionslbl" runat="server">The list below shows slideshows currently running on campus.</asp:Label></p>

                        <p>
                            <a href="Welcomes.aspx?AID=1&WID=-1">Add a new Slideshow</a>
                        &nbsp;</p>

                        <asp:Table ID="SlideshowTable" runat="server" style="width:980px; Height:51px; padding:10px">
                            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tabletitle">Title</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledate">Display From</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabledate">Display To</asp:TableHeaderCell>
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
                                <td valign="top">Slides:</td>
                                <td>
                                
                                   <asp:Table ID="Table1" runat="server" style="width:980px; Height:51px; padding:10px">
                                        <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                            <asp:TableHeaderCell CssClass="tabletitle">Slide</asp:TableHeaderCell>
                                            <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=3>Controls</asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                    </asp:Table>

                                    <asp:Panel ID="UploadPanel" runat="server" Visible=false>
                                        <p>Add Slide <asp:FileUpload ID="NewSlideUpld" runat="server" /> <asp:Button ID="AddSlidecmd" runat="server" /></p>
                                    </asp:Panel>
                                    <asp:Panel ID="HowtoUploadPanel" runat="server" Visible=true>
                                        <p>To upload slides you must first save the slideshow.</p>
                                    </asp:Panel>



                                </td>
                            </tr>
                            <tr>
                                <td colspan=2 style="text-align: right"><asp:Button ID="SaveSlideShowcmd" runat="server" Text="Save Slideshow" onclick="SaveSlideshowcmd_Click" /></td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
    
    </form>

</asp:Content>