using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassNotice : ClassCommsBase
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

        public ClassNotice()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassNotice(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From vw_Notices WHERE ID = " + ID;
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
            
            string Query = "INSERT INTO Notices (CommsBase_ID_LNK) VALUES (" + this.BaseID + ") SELECT @@IDENTITY;";

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

            //Send Email
            

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
                
                    Query = "SELECT * FROM vw_Notices;";

            }
            else
            {
                Query = "SELECT * FROM vw_Notices Where CommsBase_PostedByID = '" + User.StudentID + "';";
            }

            RQ.RunQuery(Query);

            return RQ.dataset;
        }
        


    }
}
