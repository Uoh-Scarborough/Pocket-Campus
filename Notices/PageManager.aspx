<%@ Page Title="" Language="C#" MasterPageFile="~/Red.Master" AutoEventWireup="true" CodeBehind="PageManager.aspx.cs" Inherits="Comms.PageManager" %>

<%@ Register assembly="obout_Editor" namespace="OboutInc.Editor" tagprefix="obout" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ContentPlaceHolderID="MainArea" runat="server">

    <form id="pagemanagerform" runat="server">
    
    <asp:MultiView ID="Multiview" runat="server">
    
    <asp:View ID="PageListView" runat="server">
        <h1>Pages</h1>
        <p><a href="?aid=1&amp;pid=-1">Add new page</a></p>
        <asp:Table ID="PagesTable" runat="server" style="width:980px; Height:51px; padding:10px">
            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                <asp:TableHeaderCell CssClass="tabletitle">Title</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="tabletitle">Parent</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="tabledate">Updated</asp:TableHeaderCell>
                <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </asp:View>

    <asp:View ID="ContentListView" runat="server">
    
        <table>
            <tr>
                <td valign="top">Page Title:</td>
                <td style="width: 636px"><asp:TextBox ID="Titletxt" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TitleValidator" runat="server" 
                        ControlToValidate="Titletxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td valign="top" style="height: 29px">Page Tag:</td>
                <td style="width: 636px; height: 29px;"><asp:TextBox ID="Tagtxt" runat="server" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TagValidator" runat="server" 
                        ControlToValidate="Tagtxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top">Parent:</td>
                <td style="width: 636px"><asp:DropDownList ID="Parentddl" runat="server" 
                        ondatabound="Parentddl_DataBound"></asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="top">Content:</td>
                <td style="width: 636px">
                    <obout:Editor ID="ContentEditor" runat="server" Appearance="custom" 
                        PathPrefix="Editor_data/" Submit="False" Width="630">
                        <Buttons>
                            <obout:Toggle Name="Bold" />
                            <obout:Toggle Name="Italic" />
                            <obout:Toggle Name="Underline" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="OrderedList" />
                            <obout:Method Name="BulletedList" />
                            <obout:Method Name="CreateLink" />
                        </Buttons>
                    </obout:Editor>
                </td>
            </tr>
            <tr>
                <td valign="top">Mobile Content:</td>
                <td style="width: 636px">
                    <obout:Editor ID="ContentEditorMobile" runat="server" Appearance="custom" 
                        PathPrefix="Editor_data/" Submit="False"  Width="630">
                        <Buttons>
                            <obout:Toggle Name="Bold" />
                            <obout:Toggle Name="Italic" />
                            <obout:Toggle Name="Underline" />
                            <obout:HorizontalSeparator />
                            <obout:Method Name="OrderedList" />
                            <obout:Method Name="BulletedList" />
                            <obout:Method Name="CreateLink" />
                        </Buttons>
                    </obout:Editor>
                </td>
            </tr>
            <tr>
                <td valign="top">Notices Category:</td>
                <td style="width: 636px"><asp:DropDownList ID="NoticesCategoryddl" runat="server" 
                        ondatabound="NoticesCategoryddl_DataBound"></asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="top">Events Category:</td>
                <td style="width: 636px"><asp:DropDownList ID="EventsCategoryddl" runat="server" 
                        ondatabound="EventsCategoryddl_DataBound"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">
                <asp:Button ID="SavePagecmd" runat="server" Text="Save Page" 
                        onclick="SavePagecmd_Click" />
                </td>
            </tr>

        </table>
    </asp:View>

    <asp:View ID="SavedView" runat="server">
        <h1>Page Saved</h1>

        <p>The page has been succefully saved, and is now live on the comms system.</p>

        <p><a href="PageManager.aspx">Return to Page List</a></p>
    </asp:View>

    <asp:View ID="DeletedView" runat="server">
        <h1>Page Deleted</h1>

        <p>The page has been succefully deleted.</p>

        <p><a href="PageManager.aspx">Return to Page List</a></p>
    </asp:View>

    </asp:MultiView>

   
        

    </form>

</asp:Content>
