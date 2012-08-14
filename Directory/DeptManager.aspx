<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptManager.aspx.cs" Inherits="Directory.DeptManager" MasterPageFile="~/Red.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="form1" runat="server">
    
    
     <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="ListView" runat="server">
                        <h1><asp:Label ID="Departmentlbl" runat="server"></asp:Label></h1>
                        <p><a href="DirectoryManager.aspx">Manage Directory</a></p>
                        <p><asp:Label ID="DepartmentInstructionslbl" runat="server"></asp:Label></p>
                        <asp:Table ID="DepartmentTable" runat="server" Width="700px" Height="31px">
                            <asp:TableHeaderRow BackColor="#AE2B30" ForeColor="White">
                                <asp:TableHeaderCell CssClass="tabletitle">Department</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabletelephone">Telephone</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tabletelephone">Fax</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tableoffice">Office</asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:View>
                    <asp:View ID="AddEditView" runat="server">
                        <h1><asp:Label ID="AddEditlbl" runat="server"></asp:Label></h1>
                        <p><asp:Label ID="AddEditInstructionlbl" runat="server"></asp:Label></p><br />
                        <table>
                            <tr>
                                <td valign="top">Department:</td>
                                <td><asp:TextBox ID="Departmenttxt" runat="server" Width="600px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="DepartmentValidator" runat="server" 
                                        ControlToValidate="Departmenttxt" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Email:</td>
                                <td><asp:TextBox ID="Emailtxt" runat="server" Width="400px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top">Telephone:</td>
                                <td><asp:TextBox ID="Phonetxt" runat="server" Width="100px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top">Fax:</td>
                                <td><asp:TextBox ID="Faxtxt" runat="server" Width="100px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">Office:</td>
                                <td><asp:TextBox ID="Officetxt" runat="server" Width="400px"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td valign="top">Opening:</td>
                                <td><asp:TextBox ID="Opeingtxt"  runat="server" Width="595px" Height="94px" 
                                        TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                    
                            <tr>
                                <td colspan=2 style="text-align: right"><asp:Button ID="Savecmd" runat="server" Text="Save" onclick="Savecmd_Click" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    
               </asp:MultiView>
    
    </form>
    
 </asp:Content>
