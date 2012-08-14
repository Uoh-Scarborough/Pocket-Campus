using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using StandardClasses;

namespace Mobile
{
    public class ClassPages
    {

        private int c_ID, c_Parent_ID, c_NoticesCategoryID, c_EventsCategoryID;
        private string c_Title, c_Tag, c_StandardContent, c_MobileContent;
        private DateTime c_Updated;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
            set { c_ID = value; }
        }

        public int Parent_ID
        {
            get { return c_Parent_ID; }
            set { c_Parent_ID = value; }
        }

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public string Tag
        {
            get { return c_Tag.Trim(); }
            set { c_Tag = value.Trim(); }
        }

        public string StandardContent
        {
            get { return c_StandardContent.Trim(); }
            set { c_StandardContent = value.Trim(); }
        }

        public string MobileContent
        {
            get { return c_MobileContent.Trim(); }
            set { c_MobileContent = value.Trim(); }
        }

        public int NoticesCateogryID
        {
            get { return c_NoticesCategoryID; }
            set { c_NoticesCategoryID = value; }
        }

        public int EventsCateogryID
        {
            get { return c_EventsCategoryID; }
            set { c_EventsCategoryID = value; }
        }

        public DateTime Updated
        {
            get { return c_Updated; }
            set { c_Updated = value; }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }

        public ClassPages()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassPages(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Pages WHERE Page_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            Parent_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString());
            Title = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            Tag = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
            StandardContent = RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString();
            MobileContent = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
            NoticesCateogryID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString());
            EventsCateogryID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString());
            Updated = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString());
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[9]);
        }

        public ClassPages(string Tag)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Pages WHERE Page_Tag = '" + Tag + "';";
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
            {

                Parent_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString());
                Title = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
                Tag = RQ.dataset.Tables[0].Rows[0].ItemArray[3].ToString();
                StandardContent = RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString();
                MobileContent = RQ.dataset.Tables[0].Rows[0].ItemArray[5].ToString();
                NoticesCateogryID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[6].ToString());
                EventsCateogryID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[7].ToString());
                Updated = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[8].ToString());
                Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[9]);
            }
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            StandardContent = StandardContent.Replace("'", "&#39;");
            StandardContent = StandardContent.Replace("\"", "&#34;");

            MobileContent = MobileContent.Replace("'", "&#39;");
            MobileContent = MobileContent.Replace("\"", "&#34;");


            string Query = "INSERT INTO Pages (Page_Parent_IDLNK, Page_Title, Page_Tag, Page_StandardContent, Page_MobileContent, Page_Notices_Category_ID_LNK, Page_Events_Category_ID_LNK, Page_Updated, Page_Deleted) VALUES (" + Parent_ID + ",'" + Title + "','" + Tag + "','" + StandardContent.Replace('\"', '"') + "','" + MobileContent.Replace('\"', '"') + "'," + NoticesCateogryID + "," + EventsCateogryID + ",'" + Updated.ToShortDateString() + "',0) SELECT @@IDENTITY;";

            try
            {
                RQ.RunQuery(Query);
                c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            StandardContent = StandardContent.Replace("'", "&#39;");
            StandardContent = StandardContent.Replace("\"", "&#34;");

            MobileContent = MobileContent.Replace("'", "&#39;");
            MobileContent = MobileContent.Replace("\"", "&#34;");


            string Query = "UPDATE Pages SET Page_Parent_IDLNK = " + Parent_ID + ", Page_Title = '" + Title + "', Page_Tag = '" + Tag + "', Page_StandardContent = '" + StandardContent.Replace('\"', '"') + "', Page_MobileContent = '" + MobileContent.Replace('\"', '"') + "', Page_Notices_Category_ID_LNK = " + NoticesCateogryID + ", Page_Events_Category_ID_LNK = " + EventsCateogryID + ", Page_Updated = '" + Updated + "', Page_Deleted = " + Deleted.GetHashCode() + " Where Page_IDLNK = " + ID + ";";

            try
            {
                WQ.RunQuery(Query);
                Result = true;
            }
            catch (Exception ex)
            {
                Result = false;
            }
            return Result;
        }

        public string outputPage()
        {

            ClassPages Parent = new ClassPages(Parent_ID);

            string Out = StandardContent;

            Out += "<p><i>Updated: " + Updated.ToShortDateString() + "</i></p>";

            Out += "<p>Return to <a href='?page=" + Parent.Tag + "'>" + Parent.Title + "</a></p>";

            return Out;
        }

        public string outputMobilePage()
        {
            return MobileContent;
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            Query = "SELECT * FROM Pages_View;";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static bool PageExists(string Tag)
        {
            //Initialise New Class

            string Query = "SELECT * From Pages WHERE Page_Tag = '" + Tag + "';";
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
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
