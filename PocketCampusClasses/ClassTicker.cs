using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassTicker : ClassCommsBase
    {

        int c_ID;

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

        public override ClassCategory Category
        {
            get
            {
                //Set to Menu
                base.Category = new ClassCategory(18);

                return base.Category;
            }
        }

        public ClassTicker()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassTicker(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From vw_Tickers WHERE ID = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            if (RQ.numberofresults > 0)
            {

                LoadBaseFromDR(RQ.dataset.Tables[0].Rows[0]);

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
            
            string Query = "INSERT INTO Tickers (CommsBase_ID_LNK) VALUES (" + this.BaseID + ") SELECT @@IDENTITY;";

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

            return SaveBase();

        }

        public static DataSet loadDataset(ClassUserInfo User)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            if (User.InGroup(ClassAppDetails.admingroup))
            {
                //Admin therefore show all
                
                    Query = "SELECT * FROM vw_Tickers;";

            }
            else
            {
                Query = "SELECT * FROM vw_Tickers Where CommsBase_PostedByID = '" + User.StudentID + "';";
            }

            RQ.RunQuery(Query);

            return RQ.dataset;
        }
        


    }
}
