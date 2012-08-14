<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Directory.login" MasterPageFile="~/Red.Master" %>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

        <form id="studentservices" runat="server">
            <h1>Scarborough Directory</h1>

            <p>To add or edit a notice you must first login with your campus login to the Scarborough Communications website.</p>
            <p>&nbsp;</p>

            <table style="width:301px; margin-left:213px; padding:10px">
                <tr>
                    <td style="width:150px;">Username:</td>
                    <td class="style2">
                        <input id="UserName" type="text" runat="server" style="width: 160px" 
                            onclick="return UserName_onclick()" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" Display="Static" ErrorMessage="Required Field" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td style="width:150px" class="style5">Password:</td>
                    <td class="style4">
                        <input id="UserPass" type="password" runat="server" 
                            onclick="return UserPass_onclick()" style="width: 160px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" Display="Static" ErrorMessage="Required Field" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="style3">&nbsp;</td>
                    <td class="style2">
                        <asp:button id="cmdLogin" text="Login" OnClick="Login_Click"  runat="server" 
                            style="margin-left: 129px"/>
                    </td>
                </tr>
            </table>
            <p>&nbsp;<p>
            <asp:Label id="lblResults" ForeColor="red" Font-Size="10" runat="server" />
            
         </form>
         <p><a href="http://laurel.scar.hull.ac.uk/HelpManager/index.php?did=17">Login Help</a></p>
           
    </div>
	
</asp:Content>
  
  
