<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questionnaire.aspx.cs" Inherits="PocketCampus.Questionnaire" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://pocketcampus.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/EventsLogo.png" alt="Events" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

<h1>Student Satisfaction Survey</h1>

   <iframe src="http://www.formstack.com/forms/hubs-scarb_satisfaction_questionnaire" width="1000" height="800" frameborder=0 scrolling=no></iframe>

</asp:Content>