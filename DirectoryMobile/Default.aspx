<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DirectoryMobile._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Campus Directory</title>
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">
    <form id="form1" runat="server">
    <script type="text/javascript">
        function hideAddressbar() {
            window.scrollTo(0, 1);
        }
    </script>
    <div id="banner">
        <a href="../Default.aspx" class="first">
            <img src="Images/PocketCampusHome.png" alt="Pocket Campus Home" /></a> <a href="Default.aspx">
                <img class="second" src="Images/StudioBookingHome.png" alt="Studio Booking Home" /></a>
        <h1 class="secondheader">
            Directory</h1>
    </div>
    <div id="content">
        <asp:MultiView ID="MultiView" runat="server">
            <asp:View ID="DefaultView" runat="server">
                <h1>
                    Select a task</h1>
                <ul>
                    <li><a href="?aid=1">
                        <img class="arrow" src="Images/ArrowButton.jpg" />View by Department</li>
                    <li><a href="?aid=2">
                        <img class="arrow" src="Images/ArrowButton.jpg" />Search Directory</li>
                </ul>
            </asp:View>
            <asp:View ID="DepartmentListView" runat="server">
                <h1>
                    Please select a department.</h1>
                <asp:Label ID="DepartmentList" runat="server"></asp:Label>
                <ul>
                    <li><a href="Default.aspx">Return to Directory Home</a></li>
                </ul>
            </asp:View>
            <asp:View ID="DepartmentDirectoryView" runat="server">
                <h1>
                    Department Details</h1>
                <ul>
                    <li>Department:
                        <asp:Label ID="Departmentlbl" runat="server"></asp:Label></li>
                    <li>Telephone:
                        <asp:Label ID="DeptTelephonelbl" runat="server"></asp:Label></li>
                    <li>Fax:
                        <asp:Label ID="DeptFaxlbl" runat="server"></asp:Label></li>
                    <li>Email:
                        <asp:Label ID="DeptEmailbl" runat="server"></asp:Label></li>
                    <li>Office: <asp:Label ID="DeptOfficelbl" runat="server"></asp:Label></li>
                    <li>Office Opening:
                        <asp:Label ID="DeptOpeninglbl" runat="server"></asp:Label></li>
                </ul>
                <h1>Department Staff</h1>
                <asp:Label ID="DepartmentDirectoryList" runat="server"></asp:Label>
                <ul>
                    <li><a href="Default.aspx">Return to Directory Home</a></li>
                </ul>
            </asp:View>
            <asp:View ID="SearchView" runat="server">
                <h1>
                    Search the Directory</h1>
                <ul>
                    <li>You can search by name for anyone in the directory. This can be firstname or surname.</li>
                    <li>Search: 
                        <asp:TextBox ID="Searchtxt" runat="server" Width="193px"></asp:TextBox>
                    </li>   
                    <li><asp:ImageButton ID="Searchbtn" runat="server" 
                                    ImageUrl="Images/SearchButton.png" onclick="Searchbtn_Click" /></li>             
                </ul>
                
                <ul>
                    <li><a href="Default.aspx">Return to Directory Home</a></li>
                </ul>
            </asp:View>
            <asp:View ID="DirectoryView" runat="server">
                <asp:Label ID="DirectoryListViewLabel" runat="server"></asp:Label>
                <ul>
                    <li><a href="Default.aspx">Return to Directory Home</a></li>
                </ul>
            </asp:View>
            <asp:View ID="IndividualView" runat="server">
                 <ul>
                    <li>Name:
                        <asp:Label ID="IndNamelbl" runat="server"></asp:Label></li>
                    <li>Telephone:
                        <asp:Label ID="IndTellbl" runat="server"></asp:Label></li>
                    <li>Fax:
                        <asp:Label ID="IndFaxlbl" runat="server"></asp:Label></li>
                    <li>Email:
                        <asp:Label ID="IndEmaillbl" runat="server"></asp:Label></li>
                    <li>Office: <asp:Label ID="IndOfficelbl" runat="server"></asp:Label></li>
                    <li>Office Opening:
                        <asp:Label ID="IndOfficeOpeninglbl" runat="server"></asp:Label></li>
                </ul>

                <ul>
                    <li><asp:Label ID="DeptIDlbl" runat="server"/></li>
                    <li><a href="Default.aspx">Return to Directory Home</a></li>
                </ul>
            </asp:View>
        </asp:MultiView>
        <ul>
            <li>&copy; University of Hull Scarborough Campus</li></ul>
    </div>
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <script type="text/javascript">
        try {
            var pageTracker = _gat._getTracker("UA-3108410-12");
            pageTracker._trackPageview();
        } catch (err) { }</script>
    </form>
</body>
</html>
