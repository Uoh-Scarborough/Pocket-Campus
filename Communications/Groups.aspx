<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="KDLBooking.Groups" MasterPageFile="~/Responsive.Master" EnableSessionState="True" %>

<%@ Register  TagPrefix="Tools" TagName="SearchUsers" Src="~/Controls/Control_SearchUsers.ascx" %>

<%@ Register src="~/Controls/Control_Menu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">

    <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />
&nbsp;<asp:MultiView ID="MultiView" runat="server">
    
        <asp:View ID="ViewGroupList" runat="server" onload="ViewGroupList_Load">
        
            <h1>Groups</h1>
            
            <p>Listed below are the groups managed by the Communications system.</p>

            <p><asp:LinkButton ID="AddGrouplbtn" Text="Add Group" runat="server" 
                    onclick="AddGrouplbtn_Click" /></p>

            <asp:GridView ID="GroupsGrid" runat="server" AutoGenerateColumns="False" 
                EnableModelValidation="True" onrowcommand="GroupsGrid_RowCommand" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Group_Name" HeaderText="Group Name" 
                        ControlStyle-Width="50%" >
                    <ControlStyle Width="50%" />
                    </asp:BoundField>
                    <asp:ButtonField CommandName="ViewGroup" Text="View Group" />
                    <asp:TemplateField><ItemTemplate>
                            <asp:LinkButton ID="DeleteGroupbtn" runat="server" Text="Delete Group" CommandName="DeleteGroup" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClientClick="return confirm('Are you sure you want to delete?');"/>
                        </ItemTemplate></asp:TemplateField>
                </Columns>
            </asp:GridView>

        </asp:View>

        <asp:View ID="ViewAddGroup" runat="server">
        
            <h1>Add Group</h1>

            <p>Complete the form below, and click the add button.</p>

            <p>
                <asp:Label ID="Errorlbl" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </p>

            <table ID="AddGrouptbl">
            
                <tr>
                    <td><b>Group name:</b></td>
                    <td><asp:TextBox ID="GroupNametxt" runat="server" Width="100px"></asp:TextBox> 
                        <asp:RequiredFieldValidator ID="GroupNameVal" runat="server" 
                            ControlToValidate="GroupNametxt" ErrorMessage="Please enter a Group Name"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td><b>Add All UCSNET Users:</b></td>
                    <td>
                        <asp:CheckBox ID="AddAll" runat="server" 
                            Text="N.B. This will take some time." />
                    </td>
                </tr>
                <tr>
                    <td colspan=2><asp:Button ID="AddGroupbtn"  runat="server" onclick="AddGroupbtn_Click" Text="Add Group" /></td>
                </tr>

            </table>

            

            <br />
            <asp:LinkButton ID="GroupManagement1btn" runat="server" 
                onclick="Homebt_OnClick" Text="Return to Group Management" />

            

        </asp:View>

        <asp:View ID="ViewGroupMembers" runat="server">
        
            <h1><asp:Label ID="GroupNamelbl" runat="server"></asp:Label></h1>

            <p>List below are the group members.</p>

            <asp:LinkButton ID="AddMemberlbtn" runat="Server" 
                OnClick="AddMemberlbtn_OnClick" >Add Group Member</asp:LinkButton>

            <asp:GridView ID="MembersGrid" runat="server" AutoGenerateColumns="False" 
                EnableModelValidation="True" onrowcommand="MembersGrid_RowCommand">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="User ID" />
                    <asp:ButtonField CommandName="DeleteUser" Text="Delete User" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:LinkButton ID="GroupManagement2btn0" runat="server" 
                onclick="Homebt_OnClick" Text="Return to Group Management" />

        </asp:View>

        <asp:View ID="ViewAddGroupMember" runat="server">
        
            <Tools:SearchUsers ID="SearchCtl" runat="Server" ControlText="Pick User" OnObjectChossen="SearchCtl_ObjectChossen" />

            <asp:Label ID="UNlbl" runat="server" />
        
        </asp:View>

        <asp:View ID="ViewSuccess" runat="server">
            
            <h1><asp:Label ID="SuccessHeaderlbl" runat="server"/></h1>

            <p><asp:Label ID="SuccessMainlbl" runat="server"/></p>

            <asp:LinkButton ID="GroupManagement3btn" runat="server" 
                onclick="Homebt_OnClick" Text="Return to Group Management" />

        </asp:View>

    </asp:MultiView>

</asp:Content>
