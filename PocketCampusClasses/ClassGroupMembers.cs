
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
using System.DirectoryServices;

namespace PocketCampusClasses
{
    public class ClassGroupMembers : ClassBase, IComparable
    {
        int c_GroupID;
        string c_UserID;
        

        public int GroupID
        {
            get { return c_GroupID; }
            set { c_GroupID = value; }
        }

        public string UserID
        {
            get { return c_UserID.Trim(); }
            set { c_UserID = value.Trim(); }
        }

        public ClassUserInfo UserInfo
        {
            get { return new ClassUserInfo(UserID); }
        }

        public ClassGroupMembers()
        {
             //Initialise New Class
            Deleted = false;
        }

        public ClassGroupMembers(int ID)
        {
            //Initialise New Class
            
            string Query = String.Format("SELECT * FROM GroupMembers WHERE GroupMembers_ID_LNK = {0}",ID);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            RQ.RunQuery(Query);

            LoadFromDR(RQ.dataset.Tables[0].Rows[0]);
        }

        public ClassGroupMembers(DataRow DR)
        {
            LoadFromDR(DR);
        }

        private void LoadFromDR(DataRow DR)
        {
            c_ID = Convert.ToInt32(DR["GroupMembers_ID_LNK"]);
            c_GroupID = Convert.ToInt16(DR["GroupMembers_GroupIDLNK"]);
            c_UserID = DR["GroupMembers_UserID"].ToString();
            Deleted = Convert.ToBoolean(DR["GroupMembers_Deleted"]);
        }

        public bool Create()
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("INSERT INTO GroupMembers (GroupMembers_GroupIDLNK, GroupMembers_UserID, GroupMembers_Deleted) VALUES ({0},'{1}',0) SELECT @@IDENTITY; ",GroupID,UserID);

            RQ.RunQuery(Query);
            c_ID = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0]);

            Result = true;
            
            return Result;
        }

        public bool Save()
        {
            ClassWriteQuery WQ = new ClassWriteQuery(ClassAppDetails.bookingcurrentconnection);
            bool Result;
            string Query = String.Format("Update GroupMembers SET GroupMembers_GroupIDLNK = {0}, GroupMembers_UserID = '{1}', GroupMembers_Deleted = {2} WHERE GroupMembers_ID_LNK = {3};", GroupID, UserID, Deleted.GetHashCode(), ID);

            WQ.RunQuery(Query);

            Result = true;
            
            return Result;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            ClassGroupMembers otherMember = (ClassGroupMembers)obj;

            if (otherMember != null)
                return this.UserID.ToString().CompareTo(otherMember.UserID.ToString());
            //return this.UserInfo.DisplayName.CompareTo(otherMember.UserInfo.DisplayName);
            else
                throw new ArgumentException("Object not comparible");
        }

        public static void Delete(int ID)
        {
            ClassGroupMembers Member = new ClassGroupMembers(ID);

            Member.Deleted = true;

            Member.Save();
        }

        public static DataSet LoadDataset(int GroupID)
        {
            DataSet DS = new DataSet();

            DS.Tables.Add();

            //DS.Tables[0].Columns.Add(new DataColumn("Name"));
            DS.Tables[0].Columns.Add(new DataColumn("UserID"));
            DS.Tables[0].Columns.Add(new DataColumn("ID"));

            ClassGroup Grp = new ClassGroup(GroupID);
            Grp.LoadMembers();

            foreach(ClassGroupMembers Member in Grp.Members){
                DataRow DR = DS.Tables[0].NewRow();

                //DR[0] = Member.UserInfo.DisplayName;
                DR[0] = Member.UserID;
                DR[1] = Member.ID;

                DS.Tables[0].Rows.Add(DR);
            }

            return DS;
        }

        public static Boolean IsActiveUser(string UserID)
        {

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = string.Format("SELECT * FROM Active_Members_View WHERE GroupMembers_UserID = '{0}';", UserID);

            RQ.RunQuery(Query);

            if (RQ.dataset.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static int RemainingUseage(string UserID, string Room, string Week, int Day)
        {
            int Rem = 0;

            //Find User Constraints Relating to Room (Finds Most Restrictive First)

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            //Find Daily Useage

            string Query1 = string.Format("SELECT * FROM Room_User_Constraints_View WHERE GroupMembers_UserID = '{0}' AND Constraint_Type = 2 AND Constraint_Room = '{1}' AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_Value ASC;", UserID, Room, ClassGeneral.getAcademicDate(Convert.ToInt16(Week), Day), ClassGeneral.getAcademicDate(Convert.ToInt16(Week), Day));

            RQ.RunQuery(Query1);

            if (RQ.dataset.Tables[0].Rows.Count > 0)
            {
                //Load from DR

                ClassConstraint Constraint = new ClassConstraint(RQ.dataset.Tables[0].Rows[0]);

                string RoomList = "";

                foreach ( String room in Constraint.Rooms ){
                    RoomList += String.Format(",'{0}'",room);
                }

                RoomList = RoomList.Substring(1);

                string Query2 = string.Format("SELECT SUM(Booking_Duration) FROM Booking_Duration_View WHERE Booking_Username = '{0}' AND Booking_Location IN({1}) AND Booking_Week = '{2}' AND Booking_Day = {3} AND Booking_Deleted = 0", UserID, RoomList, Week, Day);
                //string Query2 = string.Format("SELECT SUM(Booking_Duration) FROM Booking_Duration_View WHERE Booking_Username = '{0}' AND Booking_Location IN({1}) AND Booking_Week = '{2}'", UserID, Constraint.Rooms.ToString(), Week);

                RQ.RunQuery(Query2);

                int BookingDuration;

                try
                {
                    BookingDuration = Convert.ToInt16(RQ.dataset.Tables[0].Rows[0].ItemArray[0].ToString());
                }
                catch
                {
                    BookingDuration = 0;
                }

                Rem = Constraint.Value - BookingDuration;

                string Query3 = string.Format("SELECT * FROM Room_User_Constraints_View WHERE GroupMembers_UserID = '{0}' AND Constraint_Type = 1 AND Constraint_Room = '{1}' AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_Value ASC;", UserID, Room, ClassGeneral.getAcademicDate(Convert.ToInt16(Week), 0), ClassGeneral.getAcademicDate(Convert.ToInt16(Week), 6));

                RQ.RunQuery(Query3);

                int RoomMax;

                if (RQ.dataset.Tables[0].Rows.Count > 0)
                {
                    ClassConstraint Constraint2 = new ClassConstraint(RQ.dataset.Tables[0].Rows[0]);

                    RoomMax = Constraint2.Value;
                } else {
                    RoomMax = 0;
                }

                if(RoomMax < Rem){
                    Rem = RoomMax;
                }

            }
            else
            {
                Rem = 0;
            }

            return Rem;
        }

        public static int BookingRange(string UserID, string Room, string Week)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query1 = string.Format("SELECT * FROM Room_User_Constraints_View WHERE GroupMembers_UserID = '{0}' AND Constraint_Type = 3 AND Constraint_Room = '{1}' AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_Value ASC;", UserID, Room, ClassGeneral.getAcademicDate(Convert.ToInt16(Week), 0), ClassGeneral.getAcademicDate(Convert.ToInt16(Week), 6));

            RQ.RunQuery(Query1);

            if (RQ.numberofresults > 0)
            {
                ClassConstraint Constraint = new ClassConstraint(RQ.dataset.Tables[0].Rows[0]);
                return Constraint.Value;
            }
            else
            {
                return 0;
            }

        }

        public static Boolean IsAdmin(string UserID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = string.Format("SELECT * FROM Group_Members_View WHERE Group_Name = '{0}' AND GroupMembers_UserID = '{1}'", "Admin", UserID);

            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean IsMember(string UserID, string Group)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = string.Format("SELECT * FROM Group_Members_View WHERE Group_Name = '{0}' AND GroupMembers_UserID = '{1}'", Group, UserID);

            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean SendAdminEmail(string UserID)
        {
            //Send email based on the group the user is in.

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query = string.Format("SELECT * FROM Group_Members_View WHERE Group_Name IN ('{0}','{1}') AND GroupMembers_UserID = '{2}'", "Non Theatre STAFF", "Non Theatre STUDENT", UserID);

            RQ.RunQuery(Query);

            if (RQ.numberofresults > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddAllUsers(int GroupID)
        {

                DirectorySearcher search = new DirectorySearcher(ClassAppDetails.ldapserver);

                for (int i = 0; i <= 25; i++)
                {
                    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                    search.Filter = String.Format("(&(displayName={0}*)(objectCategory=person)((objectClass=user)))", alphabet.Substring(i,1));
                    search.PropertiesToLoad.Add("samaccountname");
                    search.PropertiesToLoad.Add("displayName");
                    SearchResultCollection results = search.FindAll();

                    foreach (SearchResult result in results)
                    {

                        ClassGroupMembers Member = new ClassGroupMembers();

                        Member.GroupID = GroupID;
                        Member.UserID = (String)result.Properties["samaccountname"][0];

                        string Description = (String)result.Properties["displayName"][0];

                        if (Description.Trim().Contains(" "))
                        {
                            Member.Create();
                        }

                        

                    }
                }

        }



      
    }


  
}
