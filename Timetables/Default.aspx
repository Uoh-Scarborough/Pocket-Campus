<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Timetables._Default" MasterPageFile="~/Responsive.Master" %>



<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">



    <div id="maincontent">

            <div id="breadcrumb"><p><a href="http://pocketcampus.scar.hull.ac.uk">Pocket Campus</a> > <a href="http://timetables.scar.hull.ac.uk">Timetables</a> > <asp:Label ID="courselabelbread" runat="server"></asp:Label> </p></div>

            <asp:DropDownList id="CourseList" CssClass="courselist" runat="server" 
                AutoPostBack="True" onselectedindexchanged="CourseList_SelectedIndexChanged"></asp:DropDownList>
            
            <asp:DropDownList id="WeekList" CssClass="weeklist" runat="server" AutoPostBack="True"  onselectedindexchanged="CourseList_SelectedIndexChanged"></asp:DropDownList>    
            <asp:Label ID="Courselbl" runat="server" CssClass="courselabel"></asp:Label>
            <asp:Label ID="Weeklbl" runat="server" CssClass="weeklabel"></asp:Label> 
      
        <div>
            <asp:Table ID="Timetable" runat="server"></asp:Table>  
            
            <br />

            <p>*Please note that these timetables may be subject to change.</p>
            
            <asp:Panel ID="TIDPanel" runat="server"><asp:Label ID="TIDlbl" runat="server" style="color:#000000; font-size: small"></asp:Label></asp:Panel>
         </div>


</asp:Content>




