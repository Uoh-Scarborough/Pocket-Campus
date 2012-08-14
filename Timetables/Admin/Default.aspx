<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Timetables.Admin.Default" MasterPageFile="~/Yellow.Master"%>


<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://timetables.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/TimetableLogo.png" alt="Campus Timetables" />

</a></asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <div id="maincontent">
    
    <h1>Timetables Admin</h1>
    
    <p>Welcome to the Timetables Admin. To upload a new timetable follow the steps below:</p>
    
    <ol>
        <li>From Scienta export the timetables as an excel file.</li>
        <li>Open the excel file and add the <a href="timetableschema.xsd">XML Schema</a> to define each column.</li>
        <li>Save the file as an XML file.</li>
        <li>Upload the XML file using the form below.</li>
        <li>After a few minutes the upload complete message will show, and then the new timetable will be live.</li>
    </ol>
    
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
      
    
    <asp:FileUpload ID="FileUpload1" runat="server" Width="223px" />
    <asp:Button ID="Uploadcmd" runat="server" onclick="Uploadcmd_Click" 
        Text="Upload" />
    <br /><br />
    <asp:Label ID="Errorlbl" runat="server" Text="Label" Font-Names="Georgia,Arial"></asp:Label>
      
    
    </form>
    
    </div>
    

</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/TimetablesText.png" style="width:1000px; padding:0px; margin:0px; border:0px; position:relative; left:-12px;" alt="Timetables"/>

</asp:Content>