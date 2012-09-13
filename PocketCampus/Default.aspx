<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PocketCampus.Default" MasterPageFile="~/Responsive.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

  

           <div id="bigbuttonrow">
        	
        		<div class="bigbutton" id="maps" onclick="followLink('http://maps.scar.hull.ac.uk')">
        			<a href="http://maps.scar.hull.ac.uk"><span id="mapstext"><p>Maps</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="portal" onclick="followLink('http://portal.hull.ac.uk')">
        			<a href="http://portal.hull.ac.uk"><span id="portaltext"><p>Portal</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="findapc" onclick="followLink('http://maps.scar.hull.ac.uk/index.php?newaction=showfindapc')">
        			<a href="http://maps.scar.hull.ac.uk"><span id="findapctext"><p>Find a PC</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="email" onclick="followLink('http://mail.scar.hull.ac.uk')">
        			<a href="http://mail.scar.hull.ac.uk"><span id="emailtext"><p>Email</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="timetables" onclick="followLink('http://timetables.scar.hull.ac.uk')">
        			<a href="http://timetables.scar.hull.ac.uk"><span id="timetablestext"><p>Timetables</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="roombooking" onclick="followLink('studiobooking.aspx')">
        			<a href="studiobooking.aspx"><span id="roombookingtext"><p>Room Booking</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="ebridge" onclick="followLink('http://ebridge.hull.ac.uk')">
        			<a href="http://ebridge.hull.ac.uk"><span id="ebridgetext"><p>eBridge</p></span></a>
        		</div>
        		
        		<div class="bigbutton" id="studentservices" onclick="followLink('http://studentservices.scar.hull.ac.uk')">
        			<a href="http://studentservices.scar.hull.ac.uk"><span id="studentservicestext"><p>Student Services</p></span></a>
        		</div>
        	
        	</div>
        	
        	<div id="campusinforowtop" class="campusinforow">
        	
        		<div id="campusinfotext">
        		
        		</div>
        		
        		<div class="smallbutton" id="accomodation" onclick="followLink('http://campusinfo.scar.hull.ac.uk/accomodation')">
        			<a href="http://campusinfo.scar.hull.ac.uk/accomodation/"><span id="accomodationtext">accommodation</span></a>
        		</div>
        		
        		<div class="smallbutton" id="blackwells" onclick="followLink('http://campusinfo.scar.hull.ac.uk/blackwells')">
        			<a href="http://campusinfo.scar.hull.ac.uk/blackwells"><span id="blackwellstext">blackwells</span></a>
        		</div>
        		
        		<div class="smallbutton" id="calvinos" onclick="followLink('http://campusinfo.scar.hull.ac.uk/calvinos')">
        			<a href="http://campusinfo.scar.hull.ac.uk/calvinos"><span id="calvinostext">calvinos</span></a>
        		</div>
        		
        		<div class="smallbutton" id="campusconnect" onclick="followLink('http://campusinfo.scar.hull.ac.uk/campus-connect')">
        			<a href="http://campusinfo.scar.hull.ac.uk/campus-connect"><span id="campusconnecttext">campus connect</span></a>
        		</div>
        		
        		<div class="smallbutton" id="careers" onclick="followLink('http://campusinfo.scar.hull.ac.uk/careers')">
        			<a href="http://campusinfo.scar.hull.ac.uk/careers"><span id="careersext">careers</span></a>
        		</div>
        		        		
        		<div class="smallbutton" id="dates" onclick="followLink('http://campusinfo.scar.hull.ac.uk/dates')">
        			<a href="http://campusinfo.scar.hull.ac.uk/dates"><span id="datestext">dates</span></a>
        		</div>
        		
        		<div class="smallbutton" id="diningroom" onclick="followLink('http://campusinfo.scar.hull.ac.uk/dining-room')">
        			<a href="http://campusinfo.scar.hull.ac.uk/dining-room"><span id="wavestext">waves</span></a>
        		</div>
        		
        		<div class="smallbutton" id="estates" onclick="followLink('http://campusinfo.scar.hull.ac.uk/estates')">
        			<a href="http://campusinfo.scar.hull.ac.uk/estates"><span id="estatestext">estates</span></a>
        		</div>
        	
        	</div>
        	
        	<div id="campusinforowbottom" class="campusinforow">
        	       	
	       		<div class="smallbutton" id="faqs" onclick="followLink('http://campusinfo.scar.hull.ac.uk/faqs')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/faqs"><span id="faqstext">faqs</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="freeelective" onclick="followLink('http://free-electives.scar.hull.ac.uk')">
	       			<a href="http://free-electives.scar.hull.ac.uk"><span id="freeelectivetext">free elective</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="healthandwellbeing" onclick="followLink('http://campusinfo.scar.hull.ac.uk/health-and-wellbeing')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/health-and-wellbeing"><span id="healthandwellbeingtext">health and wellbeing</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="itservices" onclick="followLink('http://campusinfo.scar.hull.ac.uk/it-services')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/it-services"><span id="itservicestext">it services</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="library" onclick="followLink('http://campusinfo.scar.hull.ac.uk/library')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/library"><span id="librarytext">library</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="ote" onclick="followLink('http://campusinfo.scar.hull.ac.uk/on-the-edge')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/on-the-edge"><span id="ontheedgetext">on the edge</span></a>
	       		</div>
	       		        		
	       		<div class="smallbutton" id="publictransport" onclick="followLink('http://campusinfo.scar.hull.ac.uk/public-transport')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/public-transport"><span id="publictransporttext">public transport</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="studentsunion" onclick="followLink('http://campusinfo.scar.hull.ac.uk/students-union')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/student-union"><span id="studentsuniontext">students union</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="studyadvice" onclick="followLink('http://campusinfo.scar.hull.ac.uk/study-advice')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/study-advice"><span id="studyadvicetext">study advice</span></a>
	       		</div>
	       		
	       		<div class="smallbutton" id="townregion" onclick="followLink('http://campusinfo.scar.hull.ac.uk/town-and-region')">
	       			<a href="http://campusinfo.scar.hull.ac.uk/town-and-region"><span id="townregiontext">town and region</span></a>
	       		</div>
	       	
	       	</div>
	       	
	       	<div id="eventsandnotices">
	       		       		
	       		<div id="events" class="eventsandnoticesblock">
	       			
	       			<asp:Label ID="Eventslbl" runat="server" Text="Label" 
                        CssClass="eventsandnoticesspan"></asp:Label>
	       		
	       		</div>
	       		
	       		<div id="notices" class="eventsandnoticesblock">

	       			<asp:Label ID="Noticeslbl" runat="server" Text="Label" 
                        CssClass="eventsandnoticesspan"></asp:Label>
	       		
	       		</div>
	       		
	       		
	       	
	       	</div>

</asp:Content>