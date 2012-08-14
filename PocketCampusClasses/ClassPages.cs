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

namespace PocketCampusClasses
{
    public class ClassPages : ClassBase
    {

        private int c_Parent_ID, c_NoticesCategoryID, c_EventsCategoryID;
        private string c_Title, c_Tag, c_StandardContent, c_MobileContent;
        private DateTime c_Updated;

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

        public ClassPages()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassPages(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Pages WHERE Page_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassPages(string Tag)
        {
            //Initialise New Class
            string Query = "SELECT * From Pages WHERE Page_Tag = '" + Tag + "';";
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
            {
                LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
            }
        }

        public ClassPages(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt16(DR["Page_IDLNK"].ToString());
            Parent_ID = Convert.ToInt16(DR["Page_Parent_IDLNK"].ToString());
            Title = DR["Page_Title"].ToString();
            Tag = DR["Page_Tag"].ToString();
            StandardContent = DR["Page_StandardContent"].ToString();
            MobileContent = DR["Page_MobileContent"].ToString();
            NoticesCateogryID = Convert.ToInt16(DR["Page_Notices_Category_ID_LNK"].ToString());
            EventsCateogryID = Convert.ToInt16(DR["Page_Events_Category_ID_LNK"].ToString());
            Updated = Convert.ToDateTime(DR["Page_Updated"].ToString());
            Deleted = Convert.ToBoolean(DR["Page_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "INSERT INTO Pages (Page_Parent_IDLNK, Page_Title, Page_Tag, Page_StandardContent, Page_MobileContent, Page_Notices_Category_ID_LNK, Page_Events_Category_ID_LNK, Page_Updated, Page_Deleted) VALUES (" + Parent_ID + ",'" + Title + "','" + Tag + "', @StandardContent, @MobileContent," + NoticesCateogryID + "," + EventsCateogryID + ",'" + Updated.ToShortDateString() + "',0) SELECT @@IDENTITY;";

            try
            {
                SqlCommand Command = new SqlCommand(Query, ClassAppDetails.commscurrentconnection.connection);

                SqlParameter StandardCont = new SqlParameter("@StandardContent", SqlDbType.Text);
                StandardCont.Value = StandardContent;

                SqlParameter MobileCont = new SqlParameter("@MobileContent", SqlDbType.Text);
                MobileCont.Value = MobileContent;

                Command.Parameters.Add(StandardCont);
                Command.Parameters.Add(MobileCont);

                ClassAppDetails.commscurrentconnection.connection.Open();

                c_ID = Convert.ToInt16(Command.ExecuteScalar());

                ClassAppDetails.commscurrentconnection.connection.Close();

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
            bool Result;

            string Query = "UPDATE Pages SET Page_Parent_IDLNK = " + Parent_ID + ", Page_Title = '" + Title + "', Page_Tag = '" + Tag  + "', Page_StandardContent = @StandardContent, Page_MobileContent = @MobileContent, Page_Notices_Category_ID_LNK = " + NoticesCateogryID + ", Page_Events_Category_ID_LNK = " + EventsCateogryID + ", Page_Updated = '" + Updated + "', Page_Deleted = " + Deleted.GetHashCode() +" Where Page_IDLNK = " + ID + ";";
            
            try
            {
                //qlConnection Connection = new SqlConnection(

                SqlCommand Command = new SqlCommand(Query, ClassAppDetails.commscurrentconnection.connection);

                SqlParameter StandardCont = new SqlParameter("@StandardContent",SqlDbType.Text);
                StandardCont.Value = StandardContent;

                SqlParameter MobileCont = new SqlParameter("@MobileContent", SqlDbType.Text);
                MobileCont.Value = MobileContent;

                Command.Parameters.Add(StandardCont);
                Command.Parameters.Add(MobileCont);

                ClassAppDetails.commscurrentconnection.connection.Open();

                Command.ExecuteNonQuery();

                ClassAppDetails.commscurrentconnection.connection.Close();

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

            Out += "<p>Return to <a href='?tag=" + Parent.Tag + "'>" + Parent.Title + "</a></p>";

            return Out;
        }

        public string outputMobilePage()
        {
            ClassPages Parent = new ClassPages(Parent_ID);

            string Out = MobileContent;

            Out += "<p><i>Updated: " + Updated.ToShortDateString() + "</i></p>";

            Out += "<p>Return to <a href='?tag=" + Parent.Tag + "'>" + Parent.Title + "</a></p>";

            return Out;
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
