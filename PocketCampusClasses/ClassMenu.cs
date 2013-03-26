using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassMenu : ClassCommsBase
    {

        public enum menuTypes{
            Breakfast = 1,
            Lunch =2,
            Dinner = 3
        }

        int c_ID;
        menuTypes c_Type;
        int c_Recurrence;
        
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
            get{
                //Set to Menu
                base.Category = new ClassCategory(14);

                return base.Category;
            }
        }

        public menuTypes Type
        {
            get{
                return c_Type;
            }
            set{
                c_Type = value;
            }
        }

        public int Recurrence
        {
            get
            {
                return c_Recurrence;
            }
            set
            {
                c_Recurrence = value;
            }
        }

        

        public ClassMenu()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassMenu(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From vw_Menus WHERE ID = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);
            RQ.RunQuery(Query);
            RQ.connection.connection.Close();

            if (RQ.numberofresults > 0)
            {

                LoadBaseFromDR(RQ.dataset.Tables[0].Rows[0]);

                Type = (menuTypes)Convert.ToInt16(RQ.dataset.Tables[0].Rows[0]["Menu_Type"]);
                Recurrence = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0]["Menu_Type"]);

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
            
            string Query = "INSERT INTO Menus (CommsBase_ID_LNK, Menu_Type, Menu_Recurrence) VALUES (" + c_BaseID + "," + c_Type.GetHashCode() + "," + c_Recurrence + ") SELECT @@IDENTITY;";

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

            string Query = "UPDATE Menus SET Menu_Type = " + c_Type.GetHashCode() + ", Menu_Recurrence = " + c_Recurrence + ";";

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

        public static DataSet loadDataset()
        {
            //Auto Update Recurrence
            ClassWriteQuery RQ0 = new ClassWriteQuery(ClassAppDetails.commscurrentconnection);

            string Query0 = "";

            Query0 = "UPDATE vw_Menus SET CommsBase_Title = Convert(varchar,DateAdd(d,(Menu_Recurrence * 7), CommsBase_DisplayFrom),103) + SubString(CommsBase_Title, 11,Len(CommsBase_Title) - 10) , CommsBase_DisplayFrom = DateAdd(d, (Menu_Recurrence * 7), CommsBase_DisplayFrom), CommsBase_DisplayTo = DateAdd(d, (Menu_Recurrence * 7), CommsBase_DisplayFrom) WHERE CommsBase_DisplayFrom < '" + DateTime.Now.ToShortDateString() + "';";

            RQ0.RunQuery(Query0);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.commscurrentconnection);

            string Query = "";

            Query = "SELECT * FROM vw_Menus;";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }
        


    }
}
