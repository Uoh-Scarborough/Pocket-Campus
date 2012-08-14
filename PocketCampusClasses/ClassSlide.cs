using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;

namespace PocketCampusClasses
{
    class ClassSlide
    {

        private int c_ID, c_SlideShow, c_Order;
        private string c_URL;
        private Boolean c_Deleted;

        public int ID
        {
            get { return c_ID; }
        }

        public int SlideShowID
        {
            get { return c_SlideShow; }
            set { c_SlideShow = value; }
        }

        public int Order
        {
            get { return c_Order; }
            set { c_Order = value; }
        }

        public string URL{
            get { return c_URL.Trim(); }
            set { c_URL = value.Trim(); }
        }

        public Boolean Deleted
        {
            get { return c_Deleted; }
            set { c_Deleted = value; }
        }
           
        public ClassSlide(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From Slides WHERE Slide_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassSlide(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Slide_ID_LNK"].ToString());
            SlideShowID = Convert.ToInt32(DR["SlideShow_ID_LNK"].ToString());
            Order = Convert.ToInt32(DR["Slide_Order"].ToString());
            URL = DR["Slide_URL"].ToString();
            Deleted = Convert.ToBoolean(DR["SlideShow_Deleted"].ToString());
        }

    }
}
