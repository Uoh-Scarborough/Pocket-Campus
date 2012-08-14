<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Directory.Default" MasterPageFile="~/Red.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="form1" runat="server">

    <asp:MultiView ID="MultiView" runat="server">
        <asp:View ID="StaffList" runat="server">
            <h1><asp:Label ID="StaffListlbl" runat="server">Staff List (A-Z)</asp:Label></h1>
            <p><asp:Label ID="StaffListInstructionslbl" runat="server"></asp:Label></p>
            <asp:Table ID="StaffListtbl" runat="server" Width="700px" Height="31px">
                <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                    <asp:TableHeaderCell CssClass="tabletitle">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tabletelephone">Department</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tabletelephone">Office</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tableoffice">Telephone</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
            <p>Return to <a href="Default.aspx">Homepage</a>.</p>
        </asp:View>
        
         <asp:View ID="Department" runat="server">
            <h1>Department</h1>
            <p><asp:Label ID="DepartmentDetailslbl" runat="server"></asp:Label></p>
            
            
             <table>
                
                 <tr>
                     <td valign="top">
                         <b>Department:</b></td>
                     <td>
                         <asp:Label ID="DeptDepartmentlbl" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td valign="top">
                         <b>Email:</b></td>
                     <td>
                         <asp:Label ID="DeptEmaillbl" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td valign="top">
                         <b>Telephone:</b></td>
                     <td>
                         <asp:Label ID="DeptPhonelbl" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                    <td valign="top">
                        <b>Fax:</b></td>
                    <td><asp:Label ID="DeptFaxlbl" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                     <td valign="top">
                         <b>Office:</b></td>
                     <td>
                         <asp:Label ID="DeptOfficelbl" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td valign="top">
                         <b>Office Hours:</b></td>
                     <td>
                         <asp:Label ID="DeptOfficeHourslbl" runat="server"></asp:Label>
                     </td>
                 </tr>
             </table>
            
            
            <asp:Table ID="DeptStaffListtbl" runat="server" Width="700px" Height="31px">
                <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                    <asp:TableHeaderCell CssClass="tabletitle">Name</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tabletelephone">Department</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tabletelephone">Office</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="tableoffice">Telephone</asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </asp:View>
        
        <asp:View ID="IndividualView" runat="server">
            <h1><asp:Label ID="Staffnamelbl" runat="server">Staff Details</asp:Label></h1>
            <p><asp:Label ID="StaffDetailslbl" runat="server"></asp:Label></p>
            
            <table>
                 <tr>
                     <td valign="top">
                         Name:</td>
                     <td>
                         <asp:Label ID="Namelbl" runat="server"></asp:Label>
                     </td>
                 </tr>
                <tr>
                    <td valign="top">Department:</td>
                    <td>
                        <asp:Label ID="Departmentlbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Email:</td>
                    <td><asp:Label ID="Emaillbl" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">Telephone:</td>
                    <td><asp:Label ID="Phonelbl" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">Office:</td>
                    <td>
                        <asp:Label ID="Officelbl" runat="server"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top">Office Hours:</td>
                    <td><asp:Label ID="OfficeHourslbl"  runat="server"></asp:Label></td>
                </tr>
            </table>
            <p>Return to <a href="Default.aspx">Homepage</a>.</p>
        </asp:View>
        
        <asp:View ID="SearchView" runat="server">
            <h1><asp:Label ID="Label3" runat="server">Staff Directory</asp:Label></h1>
            <p>To find a member of Staff on Campus, enter their name, or select a department to view departmental details and staff:</p>
            <table>
                <tr>
                    <td valign="top">Name:</td>
                    <td><asp:TextBox ID="SearchNametxt" runat="server" Width="399px"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="SearchStaffcmd" runat="server" onclick="SearchStaffcmd_Click" 
                            Text="Search Staff" />
                    </td>
                </tr>
                <tr>
                    <td valign="top">Department:</td>
                    <td>
                        <asp:DropDownList ID="Departmentscmb" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="ViewDepartmentcmd" runat="server" Text="View Department" 
                            onclick="ViewDepartmentcmd_Click" />
                    </td>
                </tr>
             </table>
             <p><a href="DirectoryManager.aspx">Staff Login</a></p>
        </asp:View>
        
   </asp:MultiView>
                
  
    </form>
                
  
</asp:Content>