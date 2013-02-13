<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_List.ascx.cs" Inherits="Communications.Controls.Control_List" %>


<asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" DataKeyNames="ID" 
    onrowcommand="GridView_RowCommand" CssClass="list" 
    onrowdatabound="GridView_RowDataBound" >
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-Width="350px" >
<HeaderStyle Width="350px"></HeaderStyle>
        </asp:BoundField>
        <asp:BoundField DataField="Category" HeaderText="Category" 
            HeaderStyle-Width="100px" >
<HeaderStyle Width="100px" CssClass="categoryfield"></HeaderStyle>
        <ItemStyle CssClass="categoryfield" />
        </asp:BoundField>
        <asp:BoundField DataField="DisplayRange" HeaderText="Display Range" 
            HeaderStyle-Width="150px" >
<HeaderStyle Width="150px" CssClass="displayrangefield"></HeaderStyle>
        <ItemStyle CssClass="displayrangefield" />
        </asp:BoundField>
        <asp:ButtonField Text="Edit" CommandName="EditItem" HeaderStyle-Width="50px" >
<HeaderStyle Width="50px"></HeaderStyle>
        </asp:ButtonField>
        <asp:ButtonField Text="Delete" CommandName="DeleteItem" 
            HeaderStyle-Width="50px" >
<HeaderStyle Width="50px"></HeaderStyle>
        </asp:ButtonField>
    </Columns>
</asp:GridView>



