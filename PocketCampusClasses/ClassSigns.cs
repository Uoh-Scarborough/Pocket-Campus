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
    public class ClassSign : ClassBase
    {

        private ClassSignType c_Sign;
        private DateTime c_DisplayFrom, c_DisplayTo;
        private string c_Room;
        private int c_SignID;

        public int SignID
        {
            get
            {
                if (c_Sign == new ClassSignType())
                {
                    return c_SignID;
                }
                else
                {
                    return c_Sign.ID;
                }
            }
            set
            {
                c_SignID = value;
            }
        }

        public ClassSignType Sign
        {
            get
            {
                if (c_Sign == new ClassSignType())
                {
                    c_Sign = new ClassSignType(c_SignID);
                }
                return c_Sign;
            }
            set
            {
                c_Sign = value;
                c_SignID = value.ID;
            }
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

        public string Room
        {
            get { return c_Room.Trim(); }
            set { c_Room = value.Trim(); }
        }

        public ClassSign()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassSign(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Signs WHERE Signs_ID_LNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            Sign = new ClassSignType((int)RQ.dataset.Tables[0].Rows[0].ItemArray[1]);
            DisplayFrom = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[2]);
            DisplayTo = Convert.ToDateTime(RQ.dataset.Tables[0].Rows[0].ItemArray[3]);
            Room = RQ.dataset.Tables[0].Rows[0].ItemArray[4].ToString();
            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[5]);
        }

        public ClassSign(DataRow DR)
        {
            CreateFromDR(DR);
        }

        private void CreateFromDR(DataRow DR)
        {
            ID = Convert.ToInt32(DR["Signs_ID_LNK"]);
            SignID = Convert.ToInt32(DR["Signs_SignType_ID_LNK"]);
            DisplayFrom = Convert.ToDateTime(DR["Signs_DisplayFrom"]);
            DisplayTo = Convert.ToDateTime(DR["Signs_DisplayTo"]);
            Room = DR["Signs_Room"].ToString();
            Deleted = Convert.ToBoolean(DR["Signs_Deleted"]);

            if (DR.Table.Columns.Contains("SignType_Name") && DR.Table.Columns.Contains("SignType_FileType"))
            {
                Sign = new ClassSignType();
                Sign.ID = Convert.ToInt32(DR["Signs_SignType_ID_LNK"]);
                Sign.Name = DR["SignType_Name"].ToString();
                Sign.FileType = DR["SignType_FileType"].ToString();
            }
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Signs (Signs_SignType_ID_LNK, Signs_DisplayFrom, Signs_DisplayTo, Signs_Room, Signs_Deleted) VALUES (" + Sign.ID + ",'" + DisplayFrom.ToShortDateString() + " " + DisplayFrom.ToShortTimeString() + "','" + DisplayTo.ToShortDateString() + " " + DisplayTo.ToShortTimeString() + "','" + Room + "',0) SELECT @@IDENTITY;";
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
            string Query = "UPDATE Signs SET Signs_SignType_ID_LNK = " + Sign.ID + ", Signs_DisplayFrom = '" + DisplayFrom.ToShortDateString() + " " + DisplayFrom.ToShortTimeString() + "', Signs_DisplayTo = '" + DisplayTo.ToShortDateString() + " " + DisplayTo.ToShortTimeString() + "', Signs_Room = '" + Room + "', Signs_Deleted = " + Deleted.GetHashCode() + " WHERE Signs_ID_LNK = " + ID + ";";
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
           
            Query = "SELECT * FROM Signs_View WHERE Signs_Deleted = 0 AND Signs_DisplayTo >= '" + DateTime.Now.ToShortDateString() + "'  ORDER BY Signs_DisplayFrom, Signs_Room";

            RQ.RunQuery(Query);

            return RQ.dataset;
        }

        
    }
}

