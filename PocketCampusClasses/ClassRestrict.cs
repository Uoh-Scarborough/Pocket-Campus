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
    public class ClassRestrict : ClassBase
    {

        private string c_UserID;
        private Boolean c_RecordingStudio1, c_RecordingStudio2, c_TheMusicRoom, c_RehearsalStudio1, c_RehearsalStudio2, c_ResearchStudio, c_OverdubStudio, c_SeminarWorkstation1, c_SeminarWorkstation2, c_SeminarWorkstation3, c_MixingStudio1, c_MixingStudio2, c_ElectronicaStudio, c_MixingStudio3, c_SeminarRoom2;

        public string UserID
        {
            get { return c_UserID.Trim(); }
            set { c_UserID = value.Trim(); }
        }

        public Boolean RecordingStudio1
        {
            get { return c_RecordingStudio1; }
            set { c_RecordingStudio1 = value; }
        }

        public Boolean RecordingStudio2
        {
            get { return c_RecordingStudio2; }
            set { c_RecordingStudio2 = value; }
        }

        public Boolean TheMusicRoom
        {
            get { return c_TheMusicRoom; }
            set { c_TheMusicRoom = value; }
        }

        public Boolean RehearsalStudio1
        {
            get { return c_RehearsalStudio1; }
            set { c_RehearsalStudio1 = value; }
        }

        public Boolean RehearsalStudio2
        {
            get { return c_RehearsalStudio2; }
            set { c_RehearsalStudio2 = value; }
        }

        public Boolean ResearchStudio
        {
            get { return c_ResearchStudio; }
            set { c_ResearchStudio = value; }
        }

        public Boolean OverdubStudio
        {
            get { return c_OverdubStudio; }
            set { c_OverdubStudio = value; }
        }

        public Boolean SeminarWorkstation1
        {
            get { return c_SeminarWorkstation1; }
            set { c_SeminarWorkstation1 = value; }
        }

        public Boolean SeminarWorkstation2
        {
            get { return c_SeminarWorkstation2; }
            set { c_SeminarWorkstation2 = value; }
        }

        public Boolean SeminarWorkstation3
        {
            get { return c_SeminarWorkstation3; }
            set { c_SeminarWorkstation3 = value; }
        }

        public Boolean MixingStudio1
        {
            get { return c_MixingStudio1; }
            set { c_MixingStudio1 = value; }
        }

        public Boolean MixingStudio2
        {
            get { return c_MixingStudio2; }
            set { c_MixingStudio2 = value; }
        }

        public Boolean ElectronicaStudio
        {
            get { return c_ElectronicaStudio; }
            set { c_ElectronicaStudio = value; }
        }

        public Boolean MixingStudio3
        {
            get { return c_MixingStudio3; }
            set { c_MixingStudio3 = value; }
        }

        public Boolean SeminarRoom2
        {
            get { return c_SeminarRoom2; }
            set { c_SeminarRoom2 = value; }
        }

        public ClassRestrict()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassRestrict(int ID)
        {
            //Initialise New Class
            c_ID = ID;
            string Query = "SELECT * From Restricts WHERE Restrict_IDLNK = " + ID;
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            //Name = RQ.dataset.Tables[0].Rows[0].ItemArray[2].ToString();
            //Code = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();

            UserID = RQ.dataset.Tables[0].Rows[0].ItemArray[1].ToString();

            RecordingStudio1 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[2]);
            RecordingStudio2 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[3]);
            TheMusicRoom = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[4]);
            RehearsalStudio1 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[5]);
            RehearsalStudio2 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[6]);
            ResearchStudio = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[7]);
            OverdubStudio = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[8]);
            SeminarWorkstation1 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[9]);
            SeminarWorkstation2 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[10]);
            SeminarWorkstation3 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[11]);
            MixingStudio1 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[12]);
            MixingStudio2 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[13]);
            ElectronicaStudio = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[14]);
            MixingStudio3 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[15]);
            SeminarRoom2 = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[16]);

            Deleted = Convert.ToBoolean(RQ.dataset.Tables[0].Rows[0].ItemArray[17]);
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Restricts (Restrict_UserID, Restrict_RecordingStudio1, Restrict_RecordingStudio2, Restrict_TheMusicRoom, Restrict_RehearsalStudio1, Restrict_RehearsalStudio2, Restrict_ResearchStudio, Restrict_OverdubStudio, Restrict_SeminarWorkstation1, Restrict_SeminarWorkstation2, Restrict_SeminarWorkstation3, Restrict_MixingStudio1, Restrict_MixingStudio2, Restrict_ElectronicaStudio, Restrict_MixingStudio3, Restrict_SeminarRoom2, Restrict_Deleted) VALUES ('" + UserID + "'," + RecordingStudio1.GetHashCode()  + "," + RecordingStudio2.GetHashCode() + "," + TheMusicRoom.GetHashCode() + "," + RehearsalStudio1.GetHashCode() + "," + RehearsalStudio2.GetHashCode() + "," + ResearchStudio.GetHashCode() + "," + OverdubStudio.GetHashCode() + "," + SeminarWorkstation1.GetHashCode() + "," + SeminarWorkstation2.GetHashCode() + "," + SeminarWorkstation3.GetHashCode() + "," + MixingStudio1.GetHashCode() + "," + MixingStudio2.GetHashCode() + "," + ElectronicaStudio.GetHashCode() + "," + MixingStudio3.GetHashCode() + "," + SeminarRoom2.GetHashCode() + ",0) SELECT @@IDENTITY;";
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

        public bool Create(SqlTransaction Trans, ClassConnection TransConn)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = "INSERT INTO Restricts (Restrict_UserID, Restrict_RecordingStudio1, Restrict_RecordingStudio2, Restrict_TheMusicRoom, Restrict_RehearsalStudio1, Restrict_RehearsalStudio2, Restrict_ResearchStudio, Restrict_OverdubStudio, Restrict_SeminarWorkstation1, Restrict_SeminarWorkstation2, Restrict_SeminarWorkstation3, Restrict_MixingStudio1, Restrict_MixingStudio2, Restrict_ElectronicaStudio, Restrict_MixingStudio3, Restrict_SeminarRoom2, Restrict_Deleted) VALUES ('" + UserID + "'," + RecordingStudio1.GetHashCode() + "," + RecordingStudio2.GetHashCode() + "," + TheMusicRoom.GetHashCode() + "," + RehearsalStudio1.GetHashCode() + "," + RehearsalStudio2.GetHashCode() + "," + ResearchStudio.GetHashCode() + "," + OverdubStudio.GetHashCode() + "," + SeminarWorkstation1.GetHashCode() + "," + SeminarWorkstation2.GetHashCode() + "," + SeminarWorkstation3.GetHashCode() + "," + MixingStudio1.GetHashCode() + "," + MixingStudio2.GetHashCode() + "," + ElectronicaStudio.GetHashCode() + "," + MixingStudio3.GetHashCode() + "," + SeminarRoom2.GetHashCode() + ",0) SELECT @@IDENTITY;";
            try
            {
                RQ.RunQuery(Query,Trans,TransConn);
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
            string Query = "UPDATE Restricts SET Restrict_UserID = '" + UserID + "', Restrict_RecordingStudio1 = " + RecordingStudio1.GetHashCode() + ", Restrict_RecordingStudio2 = " + RecordingStudio2.GetHashCode() + ", Restrict_TheMusicRoom = " + TheMusicRoom.GetHashCode() + ", Restrict_RehearsalStudio1 = " + RehearsalStudio1.GetHashCode() + ", Restrict_RehearsalStudio2 = " + RehearsalStudio2.GetHashCode() + ", Restrict_ResearchStudio = " + ResearchStudio.GetHashCode() + ", Restrict_OverdubStudio = " + OverdubStudio.GetHashCode() + ", Restrict_SeminarWorkstation1 = " + SeminarWorkstation1.GetHashCode() + ", Restrict_SeminarWorkstation2 = " + SeminarWorkstation2.GetHashCode() + ", Restrict_SeminarWorkstation3 = " + SeminarWorkstation3.GetHashCode() + ", Restrict_MixingStudio1 = " + MixingStudio1.GetHashCode() + ", Restrict_MixingStudio2 = " + MixingStudio2.GetHashCode() + ", Restrict_ElectronicaStudio = " + ElectronicaStudio.GetHashCode() + ", Restrict_MixingStudio3 = " + MixingStudio3.GetHashCode() + ", Restrict_SeminarRoom2 = " + SeminarRoom2.GetHashCode() + ", Restrict_Deleted = " + Deleted.GetHashCode() + " WHERE Restrict_IDLNK = " + ID + ";";
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

        public static int Exists(string User)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            string Query = "SELECT Restrict_IDLNK FROM Restricts WHERE Restrict_UserID = '" + User + "' AND Restrict_Deleted = 0;";

            RQ.RunQuery(Query);

            if(RQ.numberofresults > 0)
            {
                return Convert.ToInt32(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);
            } else {
                return 0;
            }
        }

        public static string GenerateRestrictedRooms(DataRow DR)
        {
            String outputstr = "";

            if(Convert.ToBoolean(DR[2])){
                outputstr += " ,Recording Studio 1";
            }

            if(Convert.ToBoolean(DR[3])){
                outputstr += ", Recording Studio 2";
            }

            if(Convert.ToBoolean(DR[4])){
                outputstr += ", Music Room";
            }

            if(Convert.ToBoolean(DR[5])){
                outputstr += ", Rehearsal Studio 1";
            }

            if(Convert.ToBoolean(DR[6])){
                outputstr += ", Rehearsal Studio 2";
            }

            if(Convert.ToBoolean(DR[7])){
                outputstr += ", Research Studio";
            }

            if(Convert.ToBoolean(DR[8])){
                outputstr += ", Overdub Studio";
            }

            if(Convert.ToBoolean(DR[9])){
                outputstr += ", Seminar Workstation 1";
            }

            if(Convert.ToBoolean(DR[10])){
                outputstr += ", Seminar Workstation 2";
            }

            if(Convert.ToBoolean(DR[11])){
                outputstr += ", Seminar Workstation 3";
            }

            if(Convert.ToBoolean(DR[12])){
                outputstr += ", Mixing Studio 1";
            }

            if(Convert.ToBoolean(DR[13])){
                outputstr += ", Mixing Studio 2";
            }

            if(Convert.ToBoolean(DR[14])){
                outputstr += ", Electronica Studio";
            }

            if(Convert.ToBoolean(DR[15])){
                outputstr += ", Mixing Studio 3";
            }

            if(Convert.ToBoolean(DR[16])){
                outputstr += ", Seminar Room 2";
            }

            try
            {
                return outputstr.Substring(2);
            }
            catch
            {
                return "";
            }
            
        }


        public static Boolean Restricted(String Room, String Username)
        {
            Boolean isRestricted = false;

            int UserID = ClassRestrict.Exists(Username);

            if (UserID > 0)
            {
                //Found

                ClassRestrict Restricted = new ClassRestrict(UserID);

                if (Room == "Recording Studio 1" && Restricted.RecordingStudio1) { isRestricted = true; }
                if (Room == "Recording Studio 2" && Restricted.RecordingStudio2) { isRestricted = true; }
                if (Room == "Music Room" && Restricted.TheMusicRoom) { isRestricted = true; }
                if (Room == "Rehearsal Studio 1" && Restricted.RehearsalStudio1) { isRestricted = true; }
                if (Room == "Rehearsal Studio 2" && Restricted.RehearsalStudio2) { isRestricted = true; }
                if (Room == "Research Studio" && Restricted.ResearchStudio) { isRestricted = true; }
                if (Room == "Overdub Studio" && Restricted.OverdubStudio) { isRestricted = true; }
                if (Room == "Seminar Workstation 1" && Restricted.SeminarWorkstation1) { isRestricted = true; }
                if (Room == "Seminar Workstation 2" && Restricted.SeminarWorkstation2) { isRestricted = true; }
                if (Room == "Seminar Workstation 3" && Restricted.SeminarWorkstation3) { isRestricted = true; }
                if (Room == "Mixing Studio 1" && Restricted.MixingStudio1) { isRestricted = true; }
                if (Room == "Mixing Studio 2" && Restricted.MixingStudio2) { isRestricted = true; }
                if (Room == "Electronica Studio" && Restricted.ElectronicaStudio) { isRestricted = true; }
                if (Room == "Mixing Studio 3" && Restricted.MixingStudio3) { isRestricted = true; }
                if (Room == "Seminar Room 2" && Restricted.SeminarRoom2) { isRestricted = true; }
            }

            return isRestricted;
        }

      
    }


   
}
