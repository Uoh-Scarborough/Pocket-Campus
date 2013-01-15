<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Groups.aspx.cs" Inherits="KDLBooking.Groups" MasterPageFile="~/Responsive.Master" EnableSessionState="True" %>

<%@ Register  TagPrefix="Tools" TagName="SearchUsers" Src="~/Controls/Control_SearchUsers.ascx" %>

<%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">


    <asp:MultiView ID="MultiView" runat="server">
    
        <asp:View ID="ViewGroupList" runat="server" onload="ViewGroupList_Load">
        
            <h1>Groups</h1>
            
            <p>Listed below are the groups managed by the Studio Booking system.</p>

            <p><asp:LinkButton ID="AddGrouplbtn" Text="Add Group" runat="server" 
                    onclick="AddGrouplbtn_Click" /></p>

            <asp:GridView ID="GroupsGrid" runat="server" AutoGenerateColumns="False" 
                EnableModelValidation="True" onrowcommand="GroupsGrid_RowCommand" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Group_Name" HeaderText="Group Name" ControlStyle-Width="50%" />
                    <asp:ButtonField CommandName="ViewGroup" Text="View Group" />
                    <asp:ButtonField CommandName="GroupConstraints" Text="Group Constraints" />
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
                    <asp:BoundField DataField="Name" HeaderText="Name" />
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

         <asp:View ID="ViewGroupConstraints" runat="server">
            
            <h1><asp:Label ID="GroupNamelbl1" runat="server"/></h1>

            <p>Select from the list below which constraints you want to apply to the group.</p>
            
            <h2 style="color:Black">Studio Closures</h2>

            <asp:CheckBoxList ID="StudioClosureList" runat="server" RepeatColumns="5">

            </asp:CheckBoxList>

            <h2 style="color:Black">Duration Restrictions</h2>

            <asp:CheckBoxList ID="DurationList" runat="server" RepeatColumns="5">

            </asp:CheckBoxList>

            <h2 style="color:Black">Daily Allowance</h2>

            <asp:CheckBoxList ID="WeeklyAllowanceList" runat="server" RepeatColumns="5">

            </asp:CheckBoxList>

            <h2 style="color:Black">Booking Range</h2>

            <asp:CheckBoxList ID="BookingRangeList" runat="server" RepeatColumns="5">

            </asp:CheckBoxList>

             <br />
             <asp:Button ID="SaveConstraintsbtn" runat="server" 
                 onclick="SaveConstraintsbtn_Click" Text="Save Constraints" />
             <br />

            <asp:LinkButton ID="HomeButton1" runat="server" 
                onclick="Homebt_OnClick" Text="Return to Group Management" />

        </asp:View>
    
    </asp:MultiView>

</asp:Content>
