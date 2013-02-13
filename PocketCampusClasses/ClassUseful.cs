using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PocketCampusClasses
{
    public class ClassUseful
    {

        public static string FormatString(string inputStr)
        {
            inputStr = inputStr.Replace("&#39;", "'");
            inputStr = inputStr.Replace("&#34;", "'");

            return inputStr;
        }

        public static string FormatStringForDB(string inputStr)
        {
            inputStr = inputStr.Replace("'","&#39;");
            inputStr = inputStr.Replace("'", "&#34;");

            return inputStr;
        }

        public static string ConvertTo2DigitNumber(int NumberIn)
        {
            if (NumberIn <= 9)
            {
               return "0" + NumberIn.ToString();
            }
            else
            {
                return NumberIn.ToString();
            }
        }

        public static string ConvertTo2DigitNumber(string NumberIn)
        {
            if (Convert.ToInt32(NumberIn) <= 9)
            {
                return "0" + Convert.ToInt32(NumberIn);
            }
            else
            {
                return NumberIn;
            }
        }

        public static string CreateTimeStamp()
        {
            return DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Minute.ToString();
        }

   


    }
}
