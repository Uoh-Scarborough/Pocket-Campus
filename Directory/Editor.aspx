<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Directory.Editor"
    MasterPageFile="~/Red.Master" %>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" runat="Server">
    <form id="form1" runat="server">
    <asp:MultiView ID="Multiview" runat="server">
        <asp:View ID="EditView" runat="server">
            <h1>
                Edit your Details</h1>
            <p>
                &nbsp;</p>
            <table>
                <tr>
                    <td>
                        Name:
                    </td>
                    <td>
                        <asp:TextBox ID="Nametxt" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Department:
                    </td>
                    <td>
                        <asp:DropDownList ID="Departmentcmb" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="Emailtxt" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Telephone:
                    </td>
                    <td>
                        +44 (0)1723 35<asp:TextBox ID="Telephonetxt" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Room:
                    </td>
                    <td>
                        <asp:TextBox ID="Roomtxt" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Office Hours:
                    </td>
                    <td>
                        <asp:TextBox ID="OfficeHourstxt" runat="server" Width="500px" Height="110px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Savecmd" runat="server" Text="Save Details" OnClick="Savecmd_Click" />
        </asp:View>
        <asp:View ID="SaveView" runat="server">
            <h1>
                Details Saved</h1>
            <p>
                Your details have been successfully updated, and will take immediate affect.</p>
            <p>
                Please return to the <a href="Default.aspx">Directory Home</a></p>
        </asp:View>
    </asp:MultiView>
    </form>
</asp:Content>
