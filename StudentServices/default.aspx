<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="StudentServices._Default" MasterPageFile="~/Green.Master" %>

<asp:Content ID="SSLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studentservices.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudentServicesLogo.png" alt="Student Services Logo" /></label></a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <form id="studentservices" runat="server">

        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="Welcome" runat="server">
            
                <h1>Welcome to Scarborough Student Services</h1>
        
                <p>From the Scarborough Student Services Website you can request a Council Tax Letter or Status Letter.</p>

                </br>

                <table style="width:1000px;">
                    <tr>
                        <td align=center><asp:Button ID="CouncilTaxcmd" runat="server" CssClass="bigbutton" onclick="CouncilTaxcmd_Click" Text="Request Council Tax Letter" style="algin:center;"/></td>
                    </tr>
                    <tr>
                        <td align=center><asp:Button ID="StatusLettercmd" runat="server" CssClass="bigbutton" onclick="StatusLettercmd_Click" Text="Status Letter Request" /></td>
                    </tr>
                    <tr>
                        <td align=center><asp:Button ID="Logout" runat="server" CssClass="bigbutton" onclick="Logoutcmd_Click" Text="Log Out" /></td>
                    </tr>
                </table>         

           
                    
            </asp:View>
        
            <asp:View ID="CouncilTax" runat="server">
                <h1>Council Tax Letter Request</h1>
                <p>Thankyou you request is now being processed, you will recive an email to your 
                    email address (<asp:Label ID="CTEmaillbl" runat="server" Text="Label"></asp:Label>
                    ) informing you that you letter is ready to collect from Campus Connect.</p>
                    <br />
                <p>If you require any further information please don&#39;t hessitate to get in touch 
                    with Student Services.</p>
                    <br />
                <ul class="buttonmenu">
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
                <br /><br />
            </asp:View>
            
            <asp:View ID="StatusLetter" runat="server">
                <h1>Status Letter Request</h1>
                <p>Thankyou you request is now being processed, you will recive an email to your 
                    email address (<asp:Label ID="SLEmaillbl" runat="server" Text="Label"></asp:Label>
                    ) informing you that you letter is ready to collect from Campus Connect.</p>
                <p>If you require any further information please don&#39;t hessitate to get in touch 
                    with Student Services.</p>
                <ul class="buttonmenu">
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
            </asp:View>
            
            <asp:View ID="Transcript" runat="server">
                <h1>Status Letter Request</h1>
                <p>Thankyou you request is now being processed, you will recive an email to your 
                    email address (<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    ) informing you that you transcript is ready to collect from Campus Connect.</p>
                <p>If you require any further information please don&#39;t hessitate to get in touch 
                    with Student Services.</p>
                <ul class="buttonmenu">
                    <li><a href="Default.aspx">Request another service</a></li>
                    <li><a href="login.aspx">Log Out</a></li>
                </ul>
            </asp:View>
            
            <asp:View ID="Table" runat="server">
                <h1>List of Requests</h1>
                
                <p><asp:Label ID="ViewOptionslbl" runat="server" Text="&lt;a href=&quot;text.apsx&quot;&gt;Test&lt;/a&gt;"></asp:Label></p>

                <asp:Table ID="RequestTable" runat="server" Width="980px" Height="31px" style="padding:15px">
                    <asp:TableHeaderRow BackColor="#91a23d" ForeColor="White">
                        <asp:TableHeaderCell CssClass="tablename">Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablenumber">Student Number</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablerequest">Request</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablerequestdate">Request Date</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Controls</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>

            </asp:View>

            <asp:View ID="EditView" runat="server">
          
                <h1>View Request</h1>
            
                <table style="width:73%; height: 87px; margin-left: 39px;">
                    <tr>
                        <td style="width: 229px"><b>Student Name:</b></td>
                        <td>
                            <asp:TextBox ID="StudentName" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 229px"><b>Student Number:</b></td>
                        <td>
                            <asp:TextBox ID="StudentNumber" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td style="width: 229px"><b>Request Type:</b></td>
                        <td>
                            <asp:TextBox ID="RequestType" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td style="font-weight: 700; width: 229px">Request Date:</td>
                        <td>
                            <asp:TextBox ID="RequestDate" ReadOnly="true" runat="server" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td style="width: 229px"><b>Completed:</b></td>
                        <td>
                            <asp:CheckBox ID="Completed" runat="server" />
                        </td>
                    </tr>
                    <tr>
                       <td style="width: 229px"><b>Completed By:</b></td>
                        <td>
                            <asp:TextBox ID="CompletedBy" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td style="width: 229px"><b>Completed Date:</b></td>
                        <td>
                            <asp:TextBox ID="CompletedDate" ReadOnly="true" runat="server" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 229px">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="Savecmd" runat="server" onclick="Savecmd_Click" style="margin-left: 173px" Text="Save" />
                        </td>
                    </tr>
                </table>
 
            </asp:View>
        
            <asp:View ID="CompletedView" runat="server">
                <h1>Save Completed</h1>
                <p>The Request Details have been saved.</p>
                <p><a href="Default.aspx">Return to the Admin Home Page</a></p>
            </asp:View>
        
        </asp:MultiView>

    </form>
    
    

</asp:Content>

<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/StudentServicesText.png" style="width:1000px" alt="Student Services" />
</asp:Content>