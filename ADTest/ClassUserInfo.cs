using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.DirectoryServices;
using StandardClasses;

namespace ADTest
{
    public class ClassUserInfo
    {

        private string c_Username;
        private string c_DisplayName;
        private string c_StudentID;
        private string c_EmailAddress;
        private string c_Groups;

        public ClassUserInfo(string Username,string ldapserver)
        {
            DirectorySearcher search = new DirectorySearcher(ldapserver);
            search.Filter = "(SAMAccountName=" + Username + ")";
            search.PropertiesToLoad.Add("description");
            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("mail");
            search.PropertiesToLoad.Add("memberOf");
            SearchResult result = search.FindOne();

            c_Username = Username;
            c_DisplayName = (String)result.Properties["displayName"][0];
            try
            {
                c_StudentID = (String)result.Properties["description"][0];
            }
            catch
            {
                c_StudentID = "";
            }

            try
            {
                c_EmailAddress = (String)result.Properties["mail"][0];
            }
            catch
            {
                c_EmailAddress = "";
            }

            //Loop Through members

            for (int i = 0; i <= result.Properties["memberOf"].Count - 1; i++)
            {
                string[] groupdetails = result.Properties["memberOf"][i].ToString().Split(',');

                c_Groups += groupdetails[0].Substring(3) + ", ";
            }
        }

        public string Username
        {
            get {return c_Username; }
            set {c_Username = value; }
        }

        public string DisplayName
        {
            get { return c_DisplayName; }
            set { c_DisplayName = value; }
        }

        public string StudentID
        {
            get { return c_StudentID; }
            set { c_StudentID = value; }
        }

        public string Mail
        {
            get { return c_EmailAddress; }
            set { c_EmailAddress = value; }
        }

        public string Groups
        {
            get { return c_Groups; }
            set { c_Groups = value; }
        }

        public Boolean InGroup(string Group)
        {
            if (Groups.Contains(Group))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
