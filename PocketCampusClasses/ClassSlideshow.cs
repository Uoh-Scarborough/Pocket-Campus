
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
    public class ClassSlideshow : ClassBase
    {

        private int c_ID;
        private string c_Title;
        private DateTime c_DisplayFrom, c_DisplayTo;

        private ArrayList c_Slides;

        public int ID
        {
            get { return c_ID; }
        }
      
        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public DateTime DisplayFrom
        {
            get { return c_DisplayFrom; }
            set { c_DisplayFrom = value; }
        }

        public DateTime DisplayTo
        {
            get { return c_DisplayTo; }
            set { c_DisplayTo = value; }
        }

        public ArrayList Slide
        {
           get { return c_Slides; }
            set { c_Slides = value; }
        }

        public int NumberofSlides
        {
            get { return c_Slides.Count; }
        }

        public void LoadSlides()
        {
            c_Slides = new ArrayList();
            
            string Query = "SELECT * FROM Slides Where Slide_Slideshow_ID_LNK = " + ID + " AND Deleted = 0";

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            RQ.RunQuery(Query);

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                c_Slides.Add(new ClassSlide(DR));
            }

        }

        public ClassSlideshow()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassSlideshow(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Slideshows WHERE Slideshow_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassSlideshow(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["SlideShow_ID_LNK"].ToString());
            Title = DR["SlideShow_Title"].ToString();
            DisplayFrom = Convert.ToDateTime(DR["SlideShow_DisplayFrom"]);
            DisplayTo = Convert.ToDateTime(DR["SlideShow_DisplayTo"]);
            Deleted = Convert.ToBoolean(DR["SlideShow_Deleted"].ToString());
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            bool Result;

            string Query = "INSERT INTO SlideShows (SlideShow_Title, SlideShow_DisplayFrom, SlideShow_DisplayTo, SlideShow_Deleted) VALUES ('" + ClassUseful.FormatStringForDB(Title) + "','" + ClassUseful.FormatStringForDB(Title) + "','" + DisplayFrom.ToShortDateString() + "','" + DisplayTo.ToShortDateString() + "',0) SELECT @@IDENTITY;";

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

            string Query = "UPDATE SlideShows SET SlideShow_Title = '" + ClassUseful.FormatStringForDB(Title) + "', SlideShow_DisplayFrom = '" + DisplayFrom.ToShortDateString() + "', SlideShow_DisplayTo = '" + DisplayTo + "', Notice_Deleted = " + Deleted.GetHashCode() + " WHERE Notice_ID_LNK = " + ID + ";";
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
        
        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            Query = "SELECT * FROM SlideShows WHERE SlideShow_Deleted = 0 ORDER BY SlideShow_Title";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

    }
}
