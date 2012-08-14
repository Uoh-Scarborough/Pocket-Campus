<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Timetables._Default" MasterPageFile="~/Yellow.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://timetables.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/TimetableLogo.png" alt="Campus Timetables" />

</a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

<form id="courseform" runat="server">

    <div id="maincontent">

            <asp:DropDownList id="CourseList" CssClass="courselist" runat="server"></asp:DropDownList><asp:Button ID="CourseButton" CssClass="coursebutton" runat="server" Text="Load Timetable" onclick="CourseButton_Click"/>
            <br />


                <asp:Panel ID="NoTimetablePanel" runat="server"><asp:Label ID="NoTimetablelbl" runat="server" style="color:#FF3300; font-size: large"></asp:Label></asp:Panel>
                <asp:Panel ID="WeekPanel" runat="server" style="width:900px;  position:relative; top:-20px; left: 30px;"><asp:Label ID="Weeklbl" runat="server" style="font-size: small; text-align:right;"></asp:Label>. <asp:DropDownList id="WeekList" CssClass="weeklist" runat="server"></asp:DropDownList><asp:Button ID="WeekButton" CssClass="weekbutton" runat="server" Text="Load Week" onclick="WeekButton_Click"/></p></asp:Panel>    

      
        <div>
            <asp:Table ID="Timetable" runat="server" Width="970px"></asp:Table>  
            
            <br />

            <p>*Please note that these timetables may be subject to change.</p>
            
            <asp:Panel ID="TIDPanel" runat="server"><asp:Label ID="TIDlbl" runat="server" style="color:#000000; font-size: small"></asp:Label></asp:Panel>
         </div>

</form>

</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/TimetablesText.png" style="width:1000px; padding:0px; margin:0px; border:0px; position:relative; left:-12px;" alt="Timetables"/>

</asp:Content>


