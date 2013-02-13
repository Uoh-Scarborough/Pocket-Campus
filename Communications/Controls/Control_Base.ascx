<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_Base.ascx.cs" Inherits="Communications.Control_Base" %>
<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>
<%@ Register src="Control_CategoryDropDown.ascx" tagname="Control_CategoryDropDown" tagprefix="UC" %>

<asp:HiddenField ID="hfBaseID" runat="server" />

<asp:Panel ID="pnl_PostedInfo" runat="server">
    <h2>Information:</h2>
    <table id="validinformationtable" class="informationtable">
        
        <tr>
            <td class="formtitle">Posted By:</td>
            <td><asp:TextBox ID="txt_PostedBy" Columns="40" runat="server" CssClass="titletext"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="formtitle">Posted Date:</td>
            <td><asp:TextBox ID="txt_PostedDate" Columns="10" runat="server" CssClass="datetext"></asp:TextBox></td>
        </tr>
        <asp:Panel ID="pnl_Validated" runat="server" Visible=false>
            <tr>
                <td class="formtitle">Validated By:</td>
                <td><asp:TextBox ID="txt_Validated" Columns="10" runat="server" CssClass="shorttext"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="formtitle">Validated Date:</td>
                <td><asp:TextBox ID="txt_ValidatedDate" Columns="10" runat="server" CssClass="datetext"></asp:TextBox></td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnl_Reason" runat="server" Visible=false>
            <tr>
                <td class="formtitle">
                    Invalid Reason:</td>
                <td>
                    <asp:TextBox ID="txt_InvalidReason" runat="server" Columns="80" Rows="4" 
                        TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="rfv_InvalidReason" runat="server" ErrorMessage="InvalidReason" Text="*" ControlToValidate="txt_InvalidReason" ValidationGroup="Reasons"></asp:RequiredFieldValidator>
                    
                </td>
            </tr>
        </asp:Panel>

    </table>

    <asp:Button ID="Validcmd" runat="server" Text="Validate" 
        CausesValidation="False" CommandName="Validate" onclick="Validcmd_Click" 
        ToolTip="This will validate the communication, however any changes must be saved first."/> 
    <asp:Button ID="Returncmd" runat="server" Text="Return to Creator" 
        ValidationGroup="Reasons" 
        ToolTip="Returns the communication to the creator with the invalid reason."/>

</asp:Panel>
    <h2>Detail:</h2>
<table id="detailtable" class="informationtable">
    
        
    <asp:Panel ID="pnl_Title" runat="server" Visible=true>
        <tr>
            <td class="formtitle">Title:</td>
            <td><asp:TextBox ID="txt_Title" runat="server" CssClass="titletext" MaxLength="80"></asp:TextBox><asp:RequiredFieldValidator ID="rfv_Title" runat="server" ErrorMessage="Title" Text="*" ControlToValidate="txt_Title"></asp:RequiredFieldValidator></td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_Content" runat="server" Visible=true>
        <tr>
            <td class="formtitle"><asp:label ID="Contentlbl" runat="server" CssClass="visible">Content:</asp:label></td>
            <td class="style1">
                <asp:TextBox ID="txt_Content" TextMode="MultiLine" Rows="4" CssClass="titletext" MaxLength="500"
                    runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="rfv_Notice" runat="server" ErrorMessage="Notice" Text="*" ControlToValidate="txt_Content" ValidationGroup="Base"></asp:RequiredFieldValidator></td>
                    
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_MenuContent" runat="server" Visible=false>
        <tr>
            <td class="formtitle"><asp:label ID="Label1" runat="server" CssClass="visible">Menu:</asp:label></td>
            <td>
                <table>
                    <tr>
                        <td class="formtitle">Item 1:</td>
                        <td><asp:TextBox ID="txt_Item1" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Description:</td>
                        <td><asp:TextBox ID="txt_Desc1" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Item 2:</td>
                        <td><asp:TextBox ID="txt_Item2" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Description:</td>
                        <td><asp:TextBox ID="txt_Decs2" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Item 3:</td>
                        <td><asp:TextBox ID="txt_Item3" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Description:</td>
                        <td><asp:TextBox ID="txt_Desc3" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Item 4:</td>
                        <td><asp:TextBox ID="txt_Item4" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Description:</td>
                        <td><asp:TextBox ID="txt_Desc4" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Item 5:</td>
                        <td><asp:TextBox ID="txt_Item5" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formtitle">Description:</td>
                        <td><asp:TextBox ID="txt_Desc5" runat="server" CssClass="menutext"></asp:TextBox></td>
                    </tr>
                
                </table>
             </td>
        </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_DisplayFrom" runat="server" Visible="true">
        <tr>
            <td class="formtitle">Display From:</td>
            <td><BDP:BDPLite ID="Cal_DisplayFrom" runat="server" TextBoxStyle-CssClass="datetext"/><asp:RequiredFieldValidator ID="rfv_DisplayFrom" runat="server" ErrorMessage="Display From" Text="*" ControlToValidate="Cal_DisplayFrom" ValidationGroup="Base"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </asp:Panel>
    <tr>
        <td class="formtitle"><asp:label ID="DisplayTolbl" runat="server" CssClass="visible">Display To:</asp:label></td>
        <td><BDP:BDPLite ID="Cal_DisplayTo" runat="server" TextBoxStyle-CssClass="datetext"/><asp:RequiredFieldValidator ID="rfv_DisplayTo" runat="server" ErrorMessage="Display To" Text="*" ControlToValidate="Cal_DisplayTo" ValidationGroup="Base"></asp:RequiredFieldValidator></td>
    </tr>
    <asp:Panel ID="pnl_Time" runat="server">
    <tr>
        <td class="formtitle">Time:</td>
        <td><asp:TextBox ID="txtTime" runat="server" CssClass="shorttext" ></asp:TextBox></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_Location" runat="server">
    <tr>
        <td class="formtitle">Location:</td>
        <td><asp:TextBox ID="txtLocation" runat="server" CssClass="shorttext" ></asp:TextBox></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_General" runat="server" Visible=true>
    <tr>
        <td class="formtitle">High Priority:</td>
        <td><asp:CheckBox ID="chk_PriorityNotice" runat="server" /></td>
    </tr>
    <tr>
        <td class="formtitle">Category:</td>
        <td>
            <UC:Control_CategoryDropDown ID="Control_CategoryDropDown1" runat="server" CssClass="texttitle"  />
            </td>
    </tr>
    <tr>
        <td class="formtitle">Attachement:</td>
        <td><asp:FileUpload ID="Attachement" runat="server" ToolTip="Upload your image" CssClass="titletext"/>
            <asp:Label ID="Attachementlbl" runat="server" CssClass="visiblespan"></asp:Label><asp:HiddenField ID="hdAttachement" runat="server" /></td>
    </tr>
    <tr>
        <td class="formtitle">Use Attachement Image:</td>
        <td><asp:CheckBox ID="chk_UserImage" runat="server" /></td>
    </tr>
    </asp:Panel>
    <asp:Panel ID="pnl_MenuType" runat="server" Visible=false>
    <tr>
        <td class="formtitle">Menu Type:</td>
        <td><asp:DropDownList ID="MenuTypeddl" runat="server" CssClass="titletext" ><asp:ListItem Text="Breakfast" Value=1></asp:ListItem><asp:ListItem Text="Lunch" Value=2></asp:ListItem><asp:ListItem Text="Dinner" Value=3></asp:ListItem></asp:DropDownList></td>
    </tr>
    <tr>
        <td class="formtitle">Menu Recurrence:</td>
        <td><asp:DropDownList ID="Recurrenceddl" runat="server" CssClass="titletext" ><asp:ListItem Text="1 Week" Value=1></asp:ListItem><asp:ListItem Text="2 Weeks" Value=2></asp:ListItem><asp:ListItem Text="3 Weeks" Value=3></asp:ListItem><asp:ListItem Text="4 Weeks" Value=4></asp:ListItem></asp:DropDownList></td>
    </tr>
    </asp:Panel>

</table>




