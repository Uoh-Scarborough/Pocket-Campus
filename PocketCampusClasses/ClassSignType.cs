using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using StandardClasses;

namespace PocketCampusClasses
{
    public class ClassSignType : ClassBase
    {

        private string c_Name;
        private string c_FileType;
        private string c_URL;
        
        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string FileType
        {
            get { return c_FileType.Trim(); }
            set { c_FileType = value.Trim(); }
        }

        public string URL
        {
            get { return "http://productionboooking.scar.hull.ac.uk/Signs/" + ID + FileType; }
        }

        public ClassSignType()
        {
            //Initialise New Class
            Deleted = false;
        }

        public ClassSignType(int ID)
        {
            //Initialise New Class
            string Query = "SELECT * From SignType WHERE SignType_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            CreateFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassSignType(DataRow DR)
        {
            CreateFromDR(DR);
        }

        private void CreateFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["SignType_ID_LNK"]);
            Name = DR["SignType_Name"].ToString();
            FileType = DR["SignType_FileType"].ToString();
            Deleted = Convert.ToBoolean(DR["SignType_Deleted"]);
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = "INSERT INTO SignType (SignType_Name, SignType_FileType, SignType_Deleted) VALUES ('" + Name + "','" + FileType + "',0) SELECT @@IDENTITY;";
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
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = "UPDATE SignType SET SignType_Name = '" + Name + "', SignType_FileType = '" + FileType + "', SignType_Deleted = " + Deleted.GetHashCode() + " WHERE SignType_ID_LNK = " + ID + ";";
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
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = "";

            Query = "SELECT * FROM SignType WHERE SignType_Deleted = 0 ORDER BY SignType_Name";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }


    }
}

