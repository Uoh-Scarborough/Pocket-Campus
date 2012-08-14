
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
    public class ClassConstraint : ClassBase
    {
        private string c_Title;
        private DateTime c_StartDate, c_EndDate;
        private int c_BookableStart, c_BookableEnd, c_Type, c_Value;

        private ArrayList c_Rooms;

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public DateTime StartDate
        {
            get { return c_StartDate; }
            set { c_StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return c_EndDate; }
            set { c_EndDate = value; }
        }

        public int BookableStart
        {
            get { return c_BookableStart; }
            set { c_BookableStart = value; }
        }

        public int BookableEnd
        {
            get { return c_BookableEnd; }
            set { c_BookableEnd = value; }
        }

        public int Type
        {
            get { return c_Type; }
            set { c_Type = value; }
        }

        public int Value
        {
            get { return c_Value; }
            set { c_Value = value; }
        }

        public ArrayList Rooms
        {
            get { return c_Rooms; }
            set { c_Rooms = value; }
        }

        public ClassConstraint()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassConstraint(int ID)
        {
            //Initialise New Class
            
            string Query = String.Format("SELECT * From Constraints WHERE Constraint_ID_LNK = {0}",ID);
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassConstraint(DataRow DR)
        {
            LoadFromDR(DR);
        }

        public ClassConstraint(DataRow DR, int Start, int End)
        {
            
            //DR["Constraint_BookableStart"] = Start;
            //DR["Constraint_BookableEnd"] = End;

            LoadFromDR(DR);

            this.BookableStart = Start;
            this.BookableEnd = End;
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["Constraint_ID_LNK"]);
            Title = DR["Constraint_Title"].ToString();
            StartDate = Convert.ToDateTime(DR["Constraint_StartDate"].ToString());
            EndDate = Convert.ToDateTime(DR["Constraint_EndDate"].ToString());
            BookableStart = Convert.ToInt16(DR["Constraint_BookableStart"]);
            BookableEnd = Convert.ToInt16(DR["Constraint_BookableEnd"]);
            Type = Convert.ToInt16(DR["Constraint_Type"]);
            Value = Convert.ToInt16(DR["Constraint_Value"]);
            Deleted = Convert.ToBoolean(DR["Constraint_Deleted"]);

            //Load Rooms
            c_Rooms = new ArrayList();

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            string Query = String.Format("SELECT * FROM Constraint_Room WHERE Constraint_ID_LNK = {0} AND Constraint_Room_Deleted = 0;", c_ID);

            RQ.RunQuery(Query);

            foreach (DataRow DR1 in RQ.dataset.Tables[0].Rows)
            {
                c_Rooms.Add(DR1["Constraint_Room"].ToString().Trim());
            }

        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("INSERT INTO Constraints (Constraint_Title, Constraint_StartDate, Constraint_EndDate, Constraint_BookableStart, Constraint_BookableEnd, Constraint_Type, Constraint_Value, Constraint_Deleted) VALUES ('{0}','{1}','{2}',{3},{4},{5},{6},0) SELECT @@IDENTITY; ", Title,StartDate,EndDate,BookableStart,BookableEnd,Type,Value);

            RQ.RunQuery(Query);
            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);

            Result = true;

            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);

            //Save Rooms
            foreach (string Room in c_Rooms)
            {
                string Q2 = String.Format("INSERT INTO Constraint_Room (Constraint_Room, Constraint_ID_LNK, Constraint_Room_Deleted) VALUES ('{0}',{1},0)", Room, c_ID);
                WQ.RunQuery(Q2);
            }

            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("Update Constraints SET Constraint_Title = '{0}', Constraint_StartDate = '{1}', Constraint_EndDate = '{2}', Constraint_BookableStart = {3}, Constraint_BookableEnd = {4}, Constraint_Type =  {5}, Constraint_Value = {6}, Constraint_Deleted = {7} WHERE Constraint_ID_LNK = {8};", Title, StartDate, EndDate, BookableStart,BookableEnd,Type,Value, Deleted.GetHashCode(),ID);

            WQ.RunQuery(Query);

            Result = true;

            //Update Rooms
            ClassWriteQuery WQ1 = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);

            string Q1 = String.Format("UPDATE Constraint_Room SET Constraint_Room_Deleted = 1 WHERE Constraint_ID_LNK = '{0}'", c_ID);
            WQ1.RunQuery(Q1);

            foreach (string Room in c_Rooms)
            {
                string Q2 = String.Format("INSERT INTO Constraint_Room (Constraint_Room, Constraint_ID_LNK, Constraint_Room_Deleted) VALUES ('{0}',{1},0)", Room, c_ID);
                WQ1.RunQuery(Q2);
            }

            return Result;
        }

        public static DataSet loadDataset()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT * FROM Constraints_View WHERE Constraint_Deleted = 0 ORDER BY Constraint_Title;");
            
            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static DataSet loadDataset(int Constraint_Type)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT * FROM Constraints_View WHERE Constraint_Type = {0} AND Constraint_Deleted = 0 ORDER BY Constraint_Title;",Constraint_Type);

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        public static Boolean Exists(string Name)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = String.Format("SELECT Constraint_ID_LNK FROM Constraints WHERE Constraint_Title = '{0}' AND Constraint_Deleted = 0;",Name);

            RQ.RunQuery(Query);

            if (RQ.numberofresults >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return BookableStart.ToString();
        }

       
    }
}
