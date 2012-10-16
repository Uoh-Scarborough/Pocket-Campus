<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudioBooking.aspx.cs" Inherits="PocketCampus.StudioBooking" MasterPageFile="~/Responsive.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

           <div id="bigbuttonrow">
        	
        		<div class="bigbutton" id="studiobooking" onclick="followLink('http://studiobooking.scar.hull.ac.uk')">
        			<a href="http://studiobooking.scar.hull.ac.uk"><span id="mapstext"><p>studio booking</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="psbooking" onclick="followLink('http://productionbooking.scar.hull.ac.uk')">
        			<a href="http://productionbooking.scar.hull.ac.uk"><span id="portaltext"><p>ps booking</p></span></a>
        		</div>
        		
        		<!--<div class="bigbutton" id="kdlbooking" onclick="followLink('http://kdlbooking.scar.hull.ac.uk')">
        			<a href="http://kdlbooking.scar.hull.ac.uk"><span id="findapctext"><p>kdl booking</p></span></a>
        		</div>-->

            </div>

</asp:Content>
