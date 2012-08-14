using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StandardClasses;
using System.Data;
using System.Collections;

namespace PocketCampusClasses
{
    public class ClassBooking : ClassBase
    {
        private string c_Title, c_Location, c_Week, c_Name, c_Username, c_Email, c_GroupMembers;
        private int c_Day, c_StartTime, c_EndTime, c_Number;
        private DateTime c_Date;

        public enum BookingType
        {
            StudioBooking,
            ProductionBooking,
            KDLBooking,
        }

        public string Title
        {
            get { return c_Title.Trim(); }
            set { c_Title = value.Trim(); }
        }

        public int Day
        {
            get { return c_Day; }
            set { c_Day = value; }
        }

        public int StartTime
        {
            get { return c_StartTime; }
            set { c_StartTime = value; }
        }

        public string StartTimeOut
        {
            get
            {
                int ST = StartTime;
                int Hours = ST / 4;
                int Minutes = 15 * (ST - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public int EndTime
        {
            get { return c_EndTime; }
            set { c_EndTime = value; }
        }

        public string EndTimeOut
        {
            get
            {
                int ET = EndTime;
                int Hours = ET / 4;
                int Minutes = 15 * (ET - Hours * 4);
                if (Minutes == 0)
                {
                    return Hours + ":0" + Minutes;
                }
                else
                {
                    return Hours + ":" + Minutes;
                }
            }
        }

        public string Location
        {
            get { return c_Location.Trim(); }
            set { c_Location = value.Trim(); }
        }

        public string Week
        {
            get { return c_Week.Trim(); }
            set { c_Week = value.Trim(); }
        }

        public string Name
        {
            get { return c_Name.Trim(); }
            set { c_Name = value.Trim(); }
        }

        public string Username
        {
            get { return c_Username.Trim(); }
            set { c_Username = value.Trim(); }
        }

        public string Email
        {
            get { return c_Email.Trim(); }
            set { c_Email = value.Trim(); }
        }

        public int Number
        {
            get { return c_Number; }
            set { c_Number = value; }
        }

        public DateTime Date
        {
            get { return c_Date; }
            set { c_Date = value; }
        }

        public string GroupMembers
        {
            get { return c_GroupMembers.Trim(); }
            set { c_GroupMembers = value.Trim(); }
        }

        public override string ToString()
        {
            return StartTime.ToString();
        }

        public static ArrayList GenerateDaySet(int d, int Week, string Room)
        {
            string sWeek;

            if (Week <= 9)
            {
                sWeek = "0" + Week.ToString();
            }
            else
            {
                sWeek = Week.ToString();
            }

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQ.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS Activity_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks FROM Activities WHERE (Activity_Location LIKE '%" + Room + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            RQ1.RunQuery("SELECT * FROM Bookings WHERE (Booking_Day = " + d + ") AND (Booking_Location LIKE '%" + Room + "%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");

            //Merge Together

            ArrayList MergeList = new ArrayList();

            foreach (DataRow DR in RQ.dataset.Tables[0].Rows)
            {
                Object Obj = new Object();
                Obj = new ClassActivities(DR);
                MergeList.Add(Obj);
            }

            foreach (DataRow DR in RQ1.dataset.Tables[0].Rows)
            {
                Object Obj = new Object();
                Obj = new ClassProductionBookings(DR);
                MergeList.Add(Obj);
            }

            MergeList.Sort();

            return MergeList;
        }

        public static DataSet GenerateBookingsDS(string UserID)
        {
            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            RQ.RunQuery("SELECT * FROM Bookings WHERE Booking_Username = '" + UserID + "' AND Booking_Deleted = 0;");

            return RQ.dataset;
        }

        public static string GenerateBookingsList(string UserID)
        {
            DataSet DS = GenerateBookingsDS(UserID);

            string returnStr = "<ul>";

            foreach (DataRow DR in DS.Tables[0].Rows)
            {
                ClassProductionBookings Booking = new ClassProductionBookings(DR);

                returnStr += "<li>" + Booking.Location + "<a href=\"?bid=" + Booking.ID + "\"><img class=\"arrow\" src=\"Images/ArrowButton.jpg\"/></a><br/>" + ClassGeneral.getAcademicDate(Convert.ToInt16(Booking.Week), Booking.Day) + " - " + Booking.StartTimeOut + "</li>";
            }

            returnStr += "</ul>";

            return returnStr;
        }


        public static int FindStartofNext(string Room, string Week, int Day, int Start, string UserID, int BookingID)
        {
            //Find Next Event

            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            ClassReadQuery RQ3 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            int StartOfNext = 97;
            Boolean FoundNext = false;

            string Query1 = string.Format("SELECT Activity_StartTime FROM Activities WHERE Activity_Day = {0} AND Activity_Location LIKE '%{1}%' AND Activity_Weeks LIKE '%{2}%' AND Activity_StartTime >= {3} ORDER BY Activity_StartTime;", Day, Room, Week, Start);
            RQ1.RunQuery(Query1);


            string Query2 = string.Format("SELECT Booking_StartTime FROM Bookings WHERE Booking_Day = {0} AND Booking_Location LIKE '%{1}%' AND Booking_Week = '{2}' AND Booking_StartTime >= {3} AND Booking_Deleted = 0 AND Booking_ID_LNK != {4} ORDER BY Booking_StartTime;", Day, Room, Convert.ToInt16(Week), Start, BookingID);
            RQ2.RunQuery(Query2);


            string Query3 = string.Format("SELECT * FROM Room_User_Constraints_View WHERE GroupMembers_UserID = '{0}' AND Constraint_Type = 0 AND Constraint_Room = '{1}' AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_Value ASC;", UserID, Room, ClassGeneral.getAcademicDate(Convert.ToInt16(Week), Day), ClassGeneral.getAcademicDate(Convert.ToInt16(Week), Day));
            RQ3.RunQuery(Query3);


            ArrayList StartTimes = new ArrayList();

            if (RQ1.dataset.Tables[0].Rows.Count > 0)
            {
                DataRow DR1 = RQ1.dataset.Tables[0].Rows[0];
                StartTimes.Add(Convert.ToInt16(DR1["Activity_StartTime"]));
                FoundNext = true;
            }

            if (RQ2.dataset.Tables[0].Rows.Count > 0)
            {
                DataRow DR2 = RQ2.dataset.Tables[0].Rows[0];
                StartTimes.Add(Convert.ToInt16(DR2["Booking_StartTime"]));
                FoundNext = true;
            }

            if (RQ3.dataset.Tables[0].Rows.Count > 0)
            {
                DataRow DR3 = RQ3.dataset.Tables[0].Rows[0];
                StartTimes.Add(Convert.ToInt16(DR3["Constraint_BookableEnd"]));
                FoundNext = true;
            }

            StartTimes.Sort();

            if (FoundNext)
            {
                StartOfNext = Convert.ToInt16(StartTimes[0]);
            }

            return StartOfNext;
        }

        public static int CalculateEndTime(int StartTime, int Remain, int NextBooking)
        {
            Remain = Remain / 15;

            if (StartTime + Remain > NextBooking)
            {
                return NextBooking;
            }
            else
            {
                return StartTime + Remain - 1;
            }
        }

        public static ArrayList GenerateDaySet(int d, int Week, string Room, string UserID, BookingType BT)
        {
            string sWeek;

            if (Week <= 9)
            {
                sWeek = "0" + Week.ToString();
            }
            else
            {
                sWeek = Week.ToString();
            }

            ClassReadQuery RQ2 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            string Query2 = string.Format("SELECT * FROM Studio_Closure_View WHERE (GroupMembers_UserID = '{0}' AND Constraint_room = '{1}') AND ((Constraint_StartDate >= '{2}' AND Constraint_EndDate <= '{3}') OR (Constraint_StartDate <= '{2}' AND Constraint_EndDate >= '{3}') OR (Constraint_StartDate >= '{2}' AND Constraint_StartDate <= '{3}') OR (Constraint_EndDate >= '{2}' AND Constraint_EndDate <= '{3}')) ORDER BY Constraint_BookableStart;", UserID, Room, ClassGeneral.getAcademicDate(Week, 0), ClassGeneral.getAcademicDate(Week, 6));

            RQ2.RunQuery(Query2);

            ClassReadQuery RQ = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            RQ.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS Activity_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks FROM Activities WHERE (Activity_Day = " + d + ") AND (Activity_Location LIKE '%" + Room + "%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

            ClassReadQuery RQ1 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);

            RQ1.RunQuery("SELECT * FROM Bookings WHERE (Booking_Day = " + d + ") AND (Booking_Location LIKE '%" + Room + "%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");


            //ClassReadQuery RQ3 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            //ClassReadQuery RQ4 = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);
            //ClassReadQuery RQ5 = new ClassReadQuery(ClassAppDetails.bookingcurrentconnection);
            //ClassReadQuery RQ6 = new ClassReadQuery(ClassAppDetails.ttcurrentconnection);

            //if (Room == "Rehearsal Studio 1" || Room == "Rehearsal Studio 2")
            //{

            //    RQ3.RunQuery("SELECT * FROM Bookings WHERE (Booking_Day = " + d + ") AND (Booking_Location LIKE '%Music Room%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");
            //    RQ4.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS Activity_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks FROM Activities WHERE (Activity_Day = " + d + ") AND (Activity_Location LIKE '%Music Room%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

            //    RQ5.RunQuery("SELECT * FROM Bookings WHERE (Booking_Day = " + d + ") AND (Booking_Location LIKE '%Seminar Room 2%') AND (Booking_Week = " + Week + ") AND Booking_Deleted = 0 ORDER BY Booking_Day, Booking_StartTime, Booking_EndTime DESC;");
            //    RQ6.RunQuery("SELECT MIN(DISTINCT Activity_ID_LNK) AS Activity_ID_LNK, Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks FROM Activities WHERE (Activity_Day = " + d + ") AND (Activity_Location LIKE '%Seminar Room 2%') AND (Activity_Weeks LIKE '%" + sWeek + "%') GROUP BY Activity_Module, Activity_Title, Activity_Day, Activity_StartTime, Activity_EndTime, Activity_Location, Activity_Academic, Activity_Pattern, Activity_Weeks ORDER BY Activity_Day, Activity_StartTime, Activity_EndTime DESC;");

            //}

            Boolean FullDayClosure = false;

            int DayStartBooking = 24;
            int DayEndBooking = 96;

            DataRow[] dayRestricts = RQ2.dataset.Tables[0].Select("Constraint_StartDate <= '" + ClassGeneral.getAcademicDate(Week, 0) + "' AND Constraint_EndDate >= '" + ClassGeneral.getAcademicDate(Week, d) + "'");

            //Merge Together

            ArrayList MergeList = new ArrayList();

            foreach (DataRow DR in dayRestricts)
            {
                if (!FullDayClosure)
                {

                    int Start1, End1, Start2, End2;

                    Start1 = DayStartBooking;
                    End2 = DayEndBooking;

                    End1 = Convert.ToInt32(DR["Constraint_BookableStart"]);
                    Start2 = Convert.ToInt32(DR["Constraint_BookableEnd"]);

                    if (End1 == Start2)
                    {
                        //Full Day
                        Object Obj = new Object();
                        Obj = new ClassConstraint(DR, Start1, End2);
                        MergeList.Add(Obj);
                        FullDayClosure = true;
                    }
                    else
                    {

                        if (End1 > DayStartBooking)
                        {
                            Object Obj = new Object();
                            Obj = new ClassConstraint(DR, Start1, End1);
                            MergeList.Add(Obj);

                            DayStartBooking = End1;
                        }

                        if (Start2 < DayEndBooking)
                        {
                            Object Obj2 = new Object();
                            Obj2 = new ClassConstraint(DR, Start2, End2);
                            MergeList.Add(Obj2);

                            DayEndBooking = Start2;
                        }

                    }

                }
            }

            if (!FullDayClosure)
            {

                //DataRow[] day

                DataRow[] dayActivities = RQ.dataset.Tables[0].Select();
                DataRow[] dayBookings = RQ1.dataset.Tables[0].Select();

                MergeList = ClassBooking.MergeActivities(dayActivities, DayStartBooking, DayEndBooking, MergeList);
                MergeList = ClassBooking.MergeBookings(dayBookings, DayStartBooking, DayEndBooking, MergeList, BT);

                /*if (Room == "Rehearsal Studio 1" || Room == "Rehearsal Studio 2")
                {

                    ArrayList MergeList2 = new ArrayList();

                    if (RQ4.numberofresults != 0)
                    {
                        DataRow[] musicActivities = RQ4.dataset.Tables[0].Select("Activity_Day = " + d);
                        MergeList2 = ClassBooking.MergeActivities(musicActivities, DayStartBooking, DayEndBooking, MergeList2);
                    }

                    if (RQ3.numberofresults != 0)
                    {
                        DataRow[] musicBookings = RQ3.dataset.Tables[0].Select("Booking_Day = " + d);
                        MergeList2 = ClassBooking.MergeBookings(musicBookings, DayStartBooking, DayEndBooking, MergeList2, BT);
                    }

                    if (RQ6.numberofresults != 0)
                    {
                        DataRow[] seminarActivities = RQ6.dataset.Tables[0].Select("Activity_Day = " + d);
                        MergeList2 = ClassBooking.MergeActivities(seminarActivities, DayStartBooking, DayEndBooking, MergeList2);
                    }
                    if (RQ5.numberofresults != 0)
                    {
                        DataRow[] seminarBookings = RQ5.dataset.Tables[0].Select("Booking_Day = " + d);
                        MergeList2 = ClassBooking.MergeBookings(seminarBookings, DayStartBooking, DayEndBooking, MergeList2, BT);
                    }

                    //Sort Merge List 2

                    MergeList2.Sort();

                    string ObjType = "";

                    switch (BT)
                    {
                        case BookingType.ProductionBooking:
                            ObjType = "PocketCampusClasses.ClassProductionBookings";
                            break;
                        case BookingType.StudioBooking:
                            ObjType = "PocketCampusClasses.ClassStudioBookings";
                            break;
                        case BookingType.KDLBooking:
                            ObjType = "PocketCampusClasses.ClassKDLBookings";
                            break;
                    }

                    int Counter = 0;

                    while (Counter <= MergeList2.Count - 1)
                    {
                        Object Obj = MergeList2[Counter];

                        if (Obj.GetType().ToString() == ObjType)
                        {

                            ClassStudioBookings Booking = (ClassStudioBookings)Obj;

                            Booking.Title = "Studio Closed";

                            if (MergeList2.Count > (Counter + 1))
                            {
                                Object NObj = MergeList2[Counter + 1];

                                if (NObj.GetType().ToString() == ObjType)
                                {

                                    if (BT == BookingType.StudioBooking)
                                    {
                                        ClassStudioBookings NBooking = (ClassStudioBookings)NObj;
                                        if (NBooking.StartTime <= Booking.EndTime)
                                        {
                                            Booking.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }
                                    else if (BT == BookingType.ProductionBooking)
                                    {
                                        ClassProductionBookings NBooking = (ClassProductionBookings)NObj;
                                        if (NBooking.StartTime <= Booking.EndTime)
                                        {
                                            Booking.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }
                                    else
                                    {
                                        ClassKDLBookings NBooking = (ClassKDLBookings)NObj;
                                        if (NBooking.StartTime <= Booking.EndTime)
                                        {
                                            Booking.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }

                                }
                                else
                                {

                                    ClassActivities NActivity = (ClassActivities)NObj;

                                    if (NActivity.StartTime <= Booking.EndTime)
                                    {
                                        Booking.EndTime = NActivity.EndTime;
                                        MergeList2.RemoveAt(Counter + 1);
                                        Counter -= 1;
                                    }

                                }
                            }


                        }
                        else
                        {
                            ClassActivities Activity = (ClassActivities)Obj;

                            Activity.Title = "Studio Closed";

                            if (MergeList2.Count > (Counter + 1))
                            {
                                Object NObj = MergeList2[Counter + 1];

                                if (NObj.GetType().ToString() == ObjType)
                                {
                                    if (BT == BookingType.StudioBooking)
                                    {
                                        ClassStudioBookings NBooking = (ClassStudioBookings)NObj;
                                        if (NBooking.StartTime <= Activity.EndTime)
                                        {
                                            Activity.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }
                                    else if (BT == BookingType.ProductionBooking)
                                    {
                                        ClassProductionBookings NBooking = (ClassProductionBookings)NObj;
                                        if (NBooking.StartTime <= Activity.EndTime)
                                        {
                                            Activity.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }
                                    else
                                    {
                                        ClassKDLBookings NBooking = (ClassKDLBookings)NObj;
                                        if (NBooking.StartTime <= Activity.EndTime)
                                        {
                                            Activity.EndTime = NBooking.EndTime;
                                            MergeList2.RemoveAt(Counter + 1);
                                            Counter -= 1;
                                        }
                                    }

                                }
                                else
                                {

                                    ClassActivities NActivity = (ClassActivities)NObj;

                                    if (NActivity.StartTime <= Activity.EndTime)
                                    {
                                        Activity.EndTime = NActivity.EndTime;
                                        MergeList2.RemoveAt(Counter + 1);
                                        Counter -= 1;
                                    }

                                }
                            }
                        }

                        Counter += 1;
                    }

                    MergeList.AddRange(MergeList2);


                }*/

            }

            MergeList.Sort();

            int MergeListCounter = 0;

            foreach (Object Obj in MergeList)
            {

                int StartTime, EndTime;

                string TT = Obj.GetType().ToString();

                if (Obj.GetType().ToString() == "PocketCampusClasses.ClassProductionBookings")
                {
                    //Studio Booking

                    ClassProductionBookings Booking = (ClassProductionBookings)Obj;

                    //Check if closure

                    if (Booking.Title == "Studio Closed")
                    {
                        try
                        {
                            Object nObj = MergeList[MergeListCounter + 1];

                            if (nObj.GetType().ToString() == "PocketCampusClasses.ClassProductionBookings")
                            {
                                ClassProductionBookings nBooking = (ClassProductionBookings)nObj;

                                if (nBooking.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }

                            }
                            else
                            {
                                ClassActivities nActivity = (ClassActivities)nObj;

                                if (nActivity.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nActivity.StartTime;

                                    //nActivity.EndTime = nActivity.StartTime;
                                }

                            }


                        }
                        catch
                        {
                            //Do Nothing
                        }
                    }
                }
                else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassStudioBookings")
                {
                    ClassStudioBookings Booking = (ClassStudioBookings)Obj;

                    //Check if closure

                    if (Booking.Title == "Studio Closed")
                    {
                        try
                        {
                            Object nObj = MergeList[MergeListCounter + 1];

                            if (nObj.GetType().ToString() == "PocketCampusClasses.ClassStudioBookings")
                            {

                                ClassStudioBookings nBooking = (ClassStudioBookings)nObj;

                                if (nBooking.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }

                            }
                            else
                            {
                                ClassActivities nActivity = (ClassActivities)nObj;

                                if (nActivity.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nActivity.StartTime;

                                    //nActivity.EndTime = nActivity.StartTime;
                                }

                            }
                        }
                        catch
                        {
                            //Do Nothing
                        }
                    }
                }
                else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                {
                    ClassKDLBookings Booking = (ClassKDLBookings)Obj;

                    //Check if closure

                    if (Booking.Title == "Studio Closed")
                    {
                        try
                        {
                            Object nObj = MergeList[MergeListCounter + 1];

                            if (nObj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                            {

                                ClassKDLBookings nBooking = (ClassKDLBookings)nObj;

                                if (nBooking.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }

                            }
                            else
                            {
                                ClassActivities nActivity = (ClassActivities)nObj;

                                if (nActivity.StartTime < Booking.EndTime)
                                {
                                    Booking.EndTime = nActivity.StartTime;

                                    //nActivity.EndTime = nActivity.StartTime;
                                }

                            }
                        }
                        catch
                        {
                            //Do Nothing
                        }
                    }
                }
                else if (Obj.GetType().ToString() == "PocketCampusClasses.ClassAcitivities")
                {
                    //Activity

                    ClassActivities Activity = (ClassActivities)Obj;

                    //Check if closure

                    if (Activity.Title == "Studio Closed")
                    {
                        try
                        {
                            Object nObj = MergeList[MergeListCounter + 1];

                            if (nObj.GetType().ToString() == "PocketCampusClasses.ClassStudioBookings")
                            {

                                ClassStudioBookings nBooking = (ClassStudioBookings)nObj;

                                if (nBooking.StartTime < Activity.EndTime)
                                {
                                    Activity.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }

                            }
                            else if (nObj.GetType().ToString() == "PocketCampusClasses.ClassProductionBookings")
                            {

                                ClassProductionBookings nBooking = (ClassProductionBookings)nObj;

                                if (nBooking.StartTime < Activity.EndTime)
                                {
                                    Activity.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }
                            }
                            else if (nObj.GetType().ToString() == "PocketCampusClasses.ClassKDLBookings")
                            {
                                ClassKDLBookings nBooking = (ClassKDLBookings)nObj;

                                if (nBooking.StartTime < Activity.EndTime)
                                {
                                    Activity.EndTime = nBooking.StartTime;

                                    //nBooking.EndTime = nBooking.StartTime;
                                }
                            }
                            else
                            {
                                ClassActivities nActivity = (ClassActivities)nObj;

                                if (nActivity.StartTime < Activity.EndTime)
                                {
                                    Activity.EndTime = nActivity.StartTime;

                                    //nActivity.EndTime = nActivity.StartTime;
                                }

                            }

                        }
                        catch
                        {
                            //Do Nothing
                        }
                    }

                }
                else
                {

                    ClassConstraint Const = (ClassConstraint)Obj;

                    //StartTime = Const.BookableStart;
                    //EndTime = Const.BookableEnd;
                }

                MergeListCounter += 1;

            }

            return MergeList;
        }

        public static ArrayList MergeActivities(DataRow[] DRS, int DayStartBooking, int DayEndBooking, ArrayList MergeList)
        {
            foreach (DataRow DR in DRS)
            {
                Object Obj = new Object();

                if (Convert.ToInt32(DR["Activity_StartTime"]) < DayStartBooking)
                {
                    DR["Activity_StartTime"] = DayStartBooking;
                }

                if (Convert.ToInt32(DR["Activity_EndTime"]) > DayEndBooking)
                {
                    DR["Activity_EndTime"] = DayEndBooking;
                }

                if (Convert.ToInt32(DR["Activity_StartTime"]) < Convert.ToInt32(DR["Activity_EndTime"]))
                {
                    Obj = new ClassActivities(DR);
                    MergeList.Add(Obj);
                }

            }

            return MergeList;
        }

        public static ArrayList MergeBookings(DataRow[] DRS, int DayStartBooking, int DayEndBooking, ArrayList MergeList)
        {
            foreach (DataRow DR in DRS)
            {
                Object Obj = new Object();

                if (Convert.ToInt32(DR["Booking_StartTime"]) < DayStartBooking)
                {
                    DR["Booking_StartTime"] = DayStartBooking;
                }

                if (Convert.ToInt32(DR["Booking_EndTime"]) > DayEndBooking)
                {
                    DR["Booking_EndTime"] = DayEndBooking;
                }

                if (Convert.ToInt32(DR["Booking_StartTime"]) < Convert.ToInt32(DR["Booking_EndTime"]))
                {
                    Obj = new ClassStudioBookings(DR);
                    MergeList.Add(Obj);
                }
            }

            return MergeList;
        }

        public static ArrayList MergeBookings(DataRow[] DRS, int DayStartBooking, int DayEndBooking, ArrayList MergeList, BookingType BT)
        {

            foreach (DataRow DR in DRS)
            {

                Object Obj = new Object();

                if (Convert.ToInt32(DR["Booking_StartTime"]) < DayStartBooking)
                {
                    DR["Booking_StartTime"] = DayStartBooking;
                }

                if (Convert.ToInt32(DR["Booking_EndTime"]) > DayEndBooking)
                {
                    DR["Booking_EndTime"] = DayEndBooking;
                }

                if (Convert.ToInt32(DR["Booking_StartTime"]) < Convert.ToInt32(DR["Booking_EndTime"]))
                {

                    switch (BT)
                    {
                        case BookingType.StudioBooking:
                            Obj = new ClassStudioBookings(Convert.ToInt32(DR[0]));
                            break;
                        case BookingType.ProductionBooking:
                            Obj = new ClassProductionBookings(Convert.ToInt32(DR[0]));
                            break;
                        case BookingType.KDLBooking:
                            Obj = new ClassKDLBookings(Convert.ToInt32(DR[0]));
                            break;
                    }

                    MergeList.Add(Obj);

                }
            }

            return MergeList;

        }


    }




}
