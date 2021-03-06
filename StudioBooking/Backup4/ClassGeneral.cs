﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StandardClasses
{
    public class ClassGeneral
    {
       

        public static int getAcademicWeek()
        {
            DateTime DT = DateTime.Now.Date;

            DateTime MOWO = new DateTime(2009, 9, 28);

            int WC = 0;

            while(MOWO.Date <= DT.Date)
            {
                //Found Week
                MOWO = MOWO.AddDays(7);
                WC++;
            }

            return WC;
        }

        public static int getWeekDay()
        {
            DateTime DT = DateTime.Now.Date;

            int DayID;

            if(DT.DayOfWeek == DayOfWeek.Monday){
                DayID = 0;
            } else if(DT.DayOfWeek == DayOfWeek.Tuesday) {
                DayID = 1;
            } else if(DT.DayOfWeek == DayOfWeek.Wednesday) {
                DayID = 2;
            } else if(DT.DayOfWeek == DayOfWeek.Thursday) {
                DayID = 3;
            } else if(DT.DayOfWeek == DayOfWeek.Friday) {
                DayID = 4;
            } else if(DT.DayOfWeek == DayOfWeek.Saturday) {
                DayID = 5;
            } else {
                DayID = 6;
            }

            return DayID;
        }

        public static string getAcademicWeekDetails(int Week)
        {
            int Days = (Week - 1) * 7;

            DateTime MOWO = new DateTime(2009, 9, 28);

            DateTime WeekStart = MOWO.AddDays(Days);

            DateTime WeekEnd = MOWO.AddDays(Days + 6);

            return "Week " + Week + " (" + WeekStart.ToShortDateString() + " - " + WeekEnd.ToShortDateString() + ")";
        }

        public static string getAcademicDate(int Week, int Day)
        {
            int Days = ((Week - 1) * 7) + Day;

            DateTime MOWO = new DateTime(2009, 9, 28);

            DateTime WeekStart = MOWO.AddDays(Days);

            return WeekStart.ToShortDateString();
        }

        public static string getTime(int Time)
        {
            int Hours = Time / 4;

            int Minutes = (Time - (Hours * 4)) * 15;

            string sHours, sMinutes;

            if (Hours <= 9)
            {
                sHours = "0" + Hours;
            }
            else
            {
                sHours = Hours.ToString();
            }

            if (Minutes <= 9)
            {
                sMinutes = "0" + Minutes;
            }
            else
            {
                sMinutes = Minutes.ToString();
            }

            return sHours + ":" + sMinutes;
        }

    }
}
