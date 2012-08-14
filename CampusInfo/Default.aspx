<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CampusInfo._Default"
    MasterPageFile="~/GreenNoMenu.Master" %>

<asp:Content ID="InfoLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://info.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/CampusInfoLogo.png" alt="Campus Info Logo" /></label></a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" runat="Server">
    <asp:MultiView ID="Multiview" runat="server">
        <asp:View ID="DefaultView" runat="server">
            <table width="980px" style="padding-left:20px">
                
                <tr style="vertical-align:top; height:185px">
                    <td>
                        <a href="?page=Careers"><img src="http://pocketcampusimages.scar.hull.ac.uk/CareersButton.png" alt="Careers" /></a>
                    </td>
                    <td>
                        <a href="?page=StudyAdvice"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudyAdviceButton.png" alt="Study Advice" /></a>
                    </td>
                    <td>
                        <a href="?page=IT"><img src="http://pocketcampusimages.scar.hull.ac.uk/ITServicesInfoButton.png" alt="Careers" /></a>
                    </td>
                    <td>
                        <a href="http://free-electives.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/FreeElectiveButton.png" alt="Free Electives" /></a>
                    </td>
                    <td>
                        <a href="?page=Calvinos"><img src="http://pocketcampusimages.scar.hull.ac.uk/CalvinosButton.png" alt="Calvinos" /></a>
                    </td>
                    <td>
                        <a href="?page=OTE"><img src="http://pocketcampusimages.scar.hull.ac.uk/OnTheEdgeButton.png" alt="On The Edge" /></a>
                    </td>
                </tr>

                <tr>
                
                    <td>
                        <a href="?page=StudentSupport"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudentSupportButton.png" alt="Student Support" /></a>
                    </td>
                    <td>
                        <a href="?page=Estates"><img src="http://pocketcampusimages.scar.hull.ac.uk/EstatesButton.png" alt="Estates" /></a>
                    </td>
                    <td>
                        <a href="?page=Library"><img src="http://pocketcampusimages.scar.hull.ac.uk/LibraryInfoButton.png" alt="Library" /></a>
                    </td>
                    <td>
                        <a href="?page=DiningRoom"><img src="http://pocketcampusimages.scar.hull.ac.uk/DiningRoomButton.png" alt="DiningRoom" /></a>
                    </td>
                    <td>
                        <a href="?page=SU"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudentsUnionButton.png" alt="Students Union" /></a>
                    </td>
                    <td>
                        <a href="?page=UsefulDates"><img src="http://pocketcampusimages.scar.hull.ac.uk/UsefulDatesButton.png" alt="Useful Dates" /></a>
                    </td>
                
                </tr>
            
            </table>


           
        </asp:View>

        <asp:View ID="ContentNoticesView" runat="server">
        
            <table height="440px" valign="top">
                <tr>
                    <td valign="top" class="infotext">
            
                        <asp:label runat="server" ID="contentnoticeslbl" />

                    </td>
                    <td id="notices" valign="top" class="infoeventnotice">
                            <asp:Label ID="NoticesOnlylbl" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </asp:View>

        <asp:View ID="ContentEventsView" runat="server">
        
            <table height="440px" valign="top" >
                <tr>
                    <td valign="top" class="infotext">
            
                        <asp:label runat="server" ID="contenteventslbl" />

                    </td>
                    <td id="events" valign="top" class="infoeventnotice">
                            <asp:Label ID="EventsOnlylbl" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </asp:View>

        <asp:View ID="ContentView" runat="server">
        
            <table height="440px" valign="top">
                <tr>
                    <td valign="top" rowspan="2" class="infotext">
            
                        <asp:label runat="server" ID="contentlbl" />

                    </td>
                    <td id="notices" valign="top" class="infoeventnotice">
                        
                        <asp:Label ID="Noticeslbl" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="events" valign="top" class="infoeventnotice">
                         <asp:Label ID="Eventslbl" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>

        </asp:View>

        <asp:View ID="ContentPlainView" runat="server">

            <table>
                <tr>
                    <td class="infotextfull">
                    <asp:label runat="server" ID="plaincontentlbl" />
                    </td>
                </tr>
            </table>
        
        </asp:View>

    </asp:MultiView>
</asp:Content>

<asp:Content ID="bottomtext" ContentPlaceHolderID="BottomText" runat="server">
    <img src="http://pocketcampusimages.scar.hull.ac.uk/CampusInfo.png" style="width:1000px" alt="Campus Info" />
</asp:Content>
