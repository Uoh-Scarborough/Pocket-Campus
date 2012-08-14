<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restrict.aspx.cs" Inherits="KDLBooking.Restrict" MasterPageFile="~/Blue.Master" %>

<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studiobooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingLogo.png" alt="Studio Booking Logo" /></label></a></asp:Content>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">
    <form id="bookingform" runat="server">
        <div>
            <asp:MultiView ID="MultiView" runat="server">
                <asp:View ID="ListView" runat="server">
                    <h1>Restricted Users</h1>
                    <p><b>Username:</b> <asp:TextBox ID="UserIDtxt" runat="server"></asp:TextBox>
                        <asp:Button ID="Restrictcmd" runat="server" Text="Restrict User" 
                            onclick="Restrictcmd_Click" /></p>

                    <asp:Table ID="RestrictedUsersTable" runat="server" Width="980px" Height="31px" style="padding:10px;">
                        <asp:TableHeaderRow BackColor="#006699" ForeColor="White">
                        <asp:TableHeaderCell CssClass="tablename" Width="300px">Name</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablerestricted" Width="300px">Restricted Rooms</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2 Width="100px">Controls</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
            </asp:Table>
                    
                </asp:View>
                <asp:View ID="UserView" runat="server">
                    <h1>User Details</h1>
                    <table>
                        <tr>
                            <td style="width: 136px"><b>Name:</b></td>
                            <td style="width: 417px"><asp:TextBox ID="Nametxt" runat="server" Width="300px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 136px"><b>Email:</b></td>
                            <td style="width: 417px"><asp:TextBox ID="Emailtxt" runat="server" Width="300px" ReadOnly=true></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td valign=top style="width: 136px"><b>Room Resctrictions:</b></td>
                            <td style="width: 417px">
                            
                                <table>

                                    <tr>
                                        <td style="width: 174px">Recording Studio 1</td>
                                        <td style="width: 243px"><asp:CheckBox ID="RecordingStudio1chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Recording Studio 2</td>
                                        <td style="width: 243px"><asp:CheckBox ID="RecordingStudio2chk" runat="server" /></td>
                                    </tr>
                                
                                    <tr>
                                        <td style="width: 174px">Music Room</td>
                                        <td style="width: 243px"><asp:CheckBox ID="MusicRoomchk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Rehearsal Studio 1</td>
                                        <td style="width: 243px"><asp:CheckBox ID="RehearsalStudio1chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Rehearsal Studio 2</td>
                                        <td style="width: 243px"><asp:CheckBox ID="RehearsalStudio2chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Research Studio</td>
                                        <td style="width: 243px"><asp:CheckBox ID="ResearchStudiochk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Overdub Studio</td>
                                        <td style="width: 243px"><asp:CheckBox ID="OverdubStudiochk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Seminar Workstation 1</td>
                                        <td style="width: 243px"><asp:CheckBox ID="SeminarWorkstation1chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Seminar Workstation 2</td>
                                        <td style="width: 243px"><asp:CheckBox ID="SeminarWorkstation2chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Seminar Workstation 3</td>
                                        <td style="width: 243px"><asp:CheckBox ID="SeminarWorkstation3chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Mixing Studio 1</td>
                                        <td style="width: 243px"><asp:CheckBox ID="MixingStudio1chk" runat="server" /></td>
                                    </tr>
                                
                                    <tr>
                                        <td style="width: 174px">Mixing Studio 2</td>
                                        <td style="width: 243px"><asp:CheckBox ID="MixingStudio2chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Mixing Studio 3</td>
                                        <td style="width: 243px"><asp:CheckBox ID="MixingStudio3chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Electronica Studio</td>
                                        <td style="width: 243px"><asp:CheckBox ID="ElectronicaStudiochk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td style="width: 174px">Seminar Room 2</td>
                                        <td style="width: 243px"><asp:CheckBox ID="SeminarRoom2chk" runat="server" /></td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">
                                            <asp:HiddenField ID="UserIDHidden" runat="server" />
                                            <asp:Button ID="Savecmd" runat="server" Text="Save Restrictions" 
                                                onclick="Savecmd_Click"/></td>
                                    </tr>

                                </table>


                            
                            </td>
                        </tr>
                       
                    </table> 
                    
                </asp:View>
                 <asp:View ID="UserNotFoundView" runat="server">
                    <h1>User Not Found</h1>
                    <p>The username you entered was not found on the system.</p>
                    <p><a href="Restrict.aspx">Return to the restricted users page.</a></p>
                    <br />
                    <p>Return to <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></p>
                </asp:View>
                <asp:View ID="Completed" runat="server">
                    <h1>User Saved</h1>
                    <p>The restricted user details have been saved. These restrictions will take instant 
                        effect.</p>
                    <br />
                    <p><a href="Restrict.aspx">Return to the restricted users page.</a></p>
                </asp:View>
                <asp:View ID="DeleteView" runat="server">
                    <h1>Confirm Deletion</h1>
                    <p>Are you sure you want to delete the following user restrictions?</p>
                    <p>User: <asp:Label ID="DeleteUserlbl" runat="server" Text="Label"></asp:Label></p>
                    <p>Restrictions: <asp:Label ID="DeleteRestrictionslbl" runat="server" Text="Label"></asp:Label></p>
                    <asp:HiddenField ID="RIDHidden" runat="server" />
                    <asp:Button ID="Deletedcmb" runat="server" Text="Delete Booking" onclick="Deletecmd_Click"/>
                </asp:View>
                <asp:View ID="Deleted" runat="server">
                    <h1>Restriction Deleted</h1>
                    <p>The restriction has been succesfully deleted.</p>
                    <p><a href="Restrict.aspx">Return to the restricted users page</a></p>
                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</asp:Content>