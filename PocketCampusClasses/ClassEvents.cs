using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassEvents : ClassCommsBase
    {

        int c_ID;
        string c_Location;
        string c_EventTime;

        public int ID
        {
            get
            {
                return c_ID;
            }
            set
            {
                c_ID = value;
            }
        }

        public string Location
        {
            get{
                return c_Location;
            }
            set{
                c_Location = value;
            }
        }

        public string EventTime
        {
            get{
                return c_EventTime;
            }
            set{
                c_EventTime = value;
            }
        }

        public DateTime  EventDate
        {
            get {
                return DisplayTo;
            }
            set {
                DisplayTo = value;
            }
        }

        public ClassEvents()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassEvents(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From vw_Events WHERE ID = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            if (RQ.numberofresults > 0)
            {

                LoadBaseFromDR(RQ.dataset.Tables[0].Rows[0]);

                Location = RQ.dataset.Tables[0].Rows[0]["Event_Location"].ToString();
                EventTime = RQ.dataset.Tables[0].Rows[0]["Event_Time"].ToString();

            }
        }

        public void SetBase(ClassCommsBase Base)
        {
            LoadBaseFromBase(Base);
        }

        public bool Create()
        {
            bool Result  = CreateBase();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            
            string Query = "INSERT INTO Events (CommsBase_ID_LNK, Event_Location, Event_Time) VALUES (" + this.BaseID + ",'" + Location + "','" + EventTime + "') SELECT @@IDENTITY;";

            try
            {
                if (Result)
                {
                    RQ.RunQuery(Query);
                    c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
                    Result = true;
                }
            }
            catch
            {
                Result = false;
            }

            return Result;
        }

        public bool Save()
        {

            bool Result = SaveBase();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "UPDATE Events SET Event_Location = '" + Location + "', Event_Time = '" + EventTime + "' WHERE Event_ID_LNK = " + c_ID + ";";

            try
            {
                if (Result)
                {
                    RQ.RunQuery(Query);
                    Result = true;
                }
            }
            catch
            {
                Result = false;
            }

            return Result;
        }

        public static DataSet loadDataset(ClassUserInfo User)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            if (User.InGroup(ClassAppDetails.admingroup))
            {
                //Admin therefore show all
                
                    Query = "SELECT * FROM vw_Events;";

            }
            else
            {
                Query = "SELECT * FROM vw_Events Where CommsBase_PostedByID = '" + User.StudentID + "';";
            }

            RQ.RunQuery(Query);

            return RQ.dataset;
        }
        


    }
}
