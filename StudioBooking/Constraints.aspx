<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Constraints.aspx.cs" Inherits="StudioBooking.Constraints" MasterPageFile="~/Blue.Master" %>

<%@ Register assembly="BasicFrame.WebControls.BasicDatePicker" namespace="BasicFrame.WebControls" tagprefix="BDP" %>

    <%@ Register src="ControlAdminMenu.ascx" tagname="ControlAdminMenu" tagprefix="uc1" %>

<asp:Content ID="StudioBookingLogo" ContentPlaceHolderID="HomeButtonCPH" runat="server"><a href="http://studiobooking.scar.hull.ac.uk"><label id="HomeButtonlbl" runat="server"><img src="http://pocketcampusimages.scar.hull.ac.uk/StudioBookingLogo.png" alt="Studio Booking Logo" /></label></a></asp:Content>

<asp:Content ID="mainform" ContentPlaceHolderID="MainArea" Runat="Server">
    
    <form id="constraintsform" runat="server" style="padding:10px;">

        <uc1:ControlAdminMenu ID="ControlAdminMenu1" runat="server" />

        <asp:MultiView ID="MultiView" runat="server">
        
            <asp:View ID="ConstaintsList" runat="server">
            
                 <h1>Constraints</h1>
            
                <p>Listed below are the Studio Booking Constaints.</p>

                <p><asp:LinkButton ID="AddConstraintlbtn" Text="Add Constraint" runat="server" onclick="AddConstraintlbtn_Click" 
                        /></p>

                <asp:GridView ID="ConstraintsGrid" runat="server" AutoGenerateColumns="False" 
                    EnableModelValidation="True" onrowcommand="ConstraintsGrid_RowCommand" 
                     Width="100%" AllowSorting="True" onsorting="ConstraintsGrid_Sorting" >
                    <Columns>
                        <asp:BoundField DataField="Constraint_Title" HeaderText="Constraint Name" 
                            SortExpression="Constraint_Title" />
                        <asp:BoundField DataField="Constraint_Desc" HeaderText="Type" 
                            SortExpression="Constraint_Desc" />
                        <asp:BoundField DataField="Constraint_StartDate" DataFormatString="{0:d}" 
                            HeaderText="Start Date" SortExpression="Constraint_StartDate" />
                        <asp:BoundField DataField="Constraint_EndDate" DataFormatString="{0:d}" 
                            HeaderText="End Date" SortExpression="Constraint_EndDate" />
                        <asp:ButtonField CommandName="ViewConstraint" Text="View" />
                        <asp:ButtonField CommandName="DeleteConstraint" Text="Delete" />
                    </Columns>
                </asp:GridView>

            </asp:View>

            <asp:View ID="AddEdit" runat="server">
            
                <h1><asp:Label ID="AddEditLabel" runat="server"></asp:Label> Constaint</h1>

                <p>Complete the form below, and click the add button.</p>

                <p>
                    <asp:Label ID="Errorlbl" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </p>

                <table ID="AddConstrainttbl">
            
                    <tr>
                        <td><b>Constraint Title:</b></td>
                        <td><asp:TextBox ID="ConstraintTitletxt" runat="server" Width="100px"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="ConstraintTitleVal" runat="server" 
                                ControlToValidate="ConstraintTitletxt" ErrorMessage="Please enter a Constraint Title"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><b>Start Date:</b></td>
                        <td><BDP:BDPLite ID="StartDateCal" runat="server" />
                            <asp:RequiredFieldValidator ID="StartDateVal" runat="server" ControlToValidate="StartDateCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                        </td>    
                    </tr>
                    <tr>
                        <td><b>End Date:</b></td>
                        <td><BDP:BDPLite ID="EndDateCal" runat="server" />
                            <asp:RequiredFieldValidator ID="EndDateVal" runat="server" ControlToValidate="EndDateCal" ErrorMessage="Required Field"></asp:RequiredFieldValidator>
                        </td>    
                    </tr>
                    <tr>
                        <td><b>Constraint Type:</b></td>
                        <td>
                            <asp:RadioButtonList ID="ConstraintTypelst" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ConstraintTypelst_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected=True>Studio Closed</asp:ListItem>
                                <asp:ListItem Value="1">Duration Restriction</asp:ListItem>
                                <asp:ListItem Value="2">Daily Allowance</asp:ListItem>
                                <asp:ListItem Value="3">Booking Range</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>    
                    </tr>
                    <asp:Panel ID="StartEndPanel" runat="server">
                        <tr>
                            <td><b>Start Booking Time:</b></td>
                            <td>
                                <asp:DropDownList ID="StartBookingDDL" runat="server">
                                </asp:DropDownList>
                            </td>    
                        </tr>
                        <tr>
                            <td><b>End Booking Time:</b></td>
                            <td>
                                <asp:DropDownList ID="EndBookingDDL" runat="server">
                                </asp:DropDownList>
                            </td>    
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="ValuePanel" runat="server" Visible=false>
                        <tr>
                            <td><b>Restriction <asp:Label ID="Restrictionlbl" runat="server" />:</b></td>
                            <td><asp:TextBox ID="Valuetxt" runat="server"></asp:TextBox></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td><b>Apply to:</b></td>
                        <td>
                            <asp:CheckBoxList ID="RoomList" runat="server">
                               <asp:ListItem>Recording Studio 1</asp:ListItem>
                               <asp:ListItem>Recording Studio 2</asp:ListItem>
                               <asp:ListItem>Music Room</asp:ListItem>
                               <asp:ListItem>Rehearsal Studio 1</asp:ListItem>
                               <asp:ListItem>Rehearsal Studio 2</asp:ListItem>
                               <asp:ListItem>Research Studio</asp:ListItem>
                               <asp:ListItem>Overdub Studio</asp:ListItem>
                               <asp:ListItem>Seminar Workstation 1</asp:ListItem>
                               <asp:ListItem>Seminar Workstation 2</asp:ListItem>
                               <asp:ListItem>Seminar Workstation 3</asp:ListItem> 
                               <asp:ListItem>Mixing Studio 1</asp:ListItem>
                               <asp:ListItem>Mixing Studio 2</asp:ListItem>
                               <asp:ListItem>Mixing Studio 3</asp:ListItem> 
                               <asp:ListItem>Seminar Room 2</asp:ListItem> 
                            </asp:CheckBoxList>
                        </td>
                    </tr>

                </table>

                <asp:Button ID="AddConstraintbtn"  runat="server" Text="Add Constraint" 
                    onclick="AddConstraintbtn_Click" />
            

                <asp:HiddenField ID="ConstraintIDHid" runat="server" />
            

                <br />
                <asp:LinkButton ID="Homebtn" runat="server" 
                    Text="Return to Constaint Management" onclick="Homebtn_Click" />

            </asp:View>

            <asp:View ID="Dialog" runat="server">
                <h1><asp:Label ID="DialogTitle" runat="server" /></h1>
                <p><asp:Label ID="DialogText" runat="server"/></p>
                
                <p>
                    <asp:LinkButton ID="Homebtn0" runat="server" onclick="Homebtn_Click" 
                        Text="Return to Constaint Management" />
                </p>
                
            </asp:View>

            <asp:View ID="AddGroup" runat="server">
            
            </asp:View>
        
        </asp:MultiView>

    </form>

</asp:Content>


