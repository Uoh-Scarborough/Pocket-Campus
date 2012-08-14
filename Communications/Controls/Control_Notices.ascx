<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_Notices.ascx.cs" Inherits="Communications.Control_Notices" %>
<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>
<%@ Register src="Control_CategoryDropDown.ascx" tagname="Control_CategoryDropDown" tagprefix="UC" %>

<h1><asp:Label ID="lbl_Header" runat="server" />
    <asp:HiddenField ID="hf_NoticeID" runat="server" />
</h1>

<asp:ValidationSummary ID="ValidationSummary" runat="server" 
    HeaderText="You must enter a value in the following fields:" />
<table>

    <asp:Panel ID="pnl_PostedInfo" runat="server">
        <tr>
            <td class="label">Posted By:</td>
            <td><asp:TextBox ID="txt_PostedBy" Columns="40" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="label">Posted Date:</td>
            <td><asp:TextBox ID="txt_PostedDate" Columns="10" runat="server"></asp:TextBox></td>
        </tr>
    </asp:Panel>
    <tr>
        <td class="label">Title:</td>
        <td><asp:TextBox ID="txt_Title" Columns="80" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfv_Title" runat="server" ErrorMessage="Title" Text="*" ControlToValidate="txt_Title"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="label">Notice:</td>
        <td><asp:TextBox ID="txt_Notice" TextMode="MultiLine" Rows="4" Columns="80" 
                runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="rfv_Notice" runat="server" ErrorMessage="Notice" Text="*" ControlToValidate="txt_Notice"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="label">Display From:</td>
        <td><BDP:BDPLite ID="Cal_DisplayFrom" runat="server" /><asp:RequiredFieldValidator ID="rfv_DisplayFrom" runat="server" ErrorMessage="Display From" Text="*" ControlToValidate="Cal_DisplayFrom"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="label">Display To:</td>
        <td><BDP:BDPLite ID="Cal_DisplayTo" runat="server" /><asp:RequiredFieldValidator ID="rfv_DisplayTo" runat="server" ErrorMessage="Display To" Text="*" ControlToValidate="Cal_DisplayTo"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="label">Priority Notice:</td>
        <td><asp:CheckBox ID="chk_PriorityNotice" runat="server" /></td>
    </tr>
    <tr>
        <td class="label">Category:</td>
        <td>
            <UC:Control_CategoryDropDown ID="Control_CategoryDropDown1" runat="server" />
            </td>
    </tr>
    <tr>
        <td class="label">Attachement:</td>
        <td><asp:FileUpload ID="Attachement" runat="server" /></td>
    </tr>
    <tr>
        <td class="label">Use Attachement Image:</td>
        <td><asp:CheckBox ID="chk_UserImage" runat="server" /></td>
    </tr>
    <asp:Panel ID="pnl_ValidInfo" runat="server">
        <tr>
            <td class="label">Valid:</td>
            <td><asp:CheckBox ID="chk_Valid" runat="server" /></td>
        </tr>
        <td class="label">Invalid Reason:</td>
           <td><asp:TextBox ID="txt_InvalidReason" TextMode="MultiLine" Rows="4" Columns="80" 
                    runat="server"></asp:TextBox>
        </td>
    </asp:Panel>

</table>

<asp:Button ID="btn_Action" runat="server" Text="Add Notice" 
    onclick="btn_Action_Click" />


