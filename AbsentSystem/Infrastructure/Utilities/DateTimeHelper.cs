using System;
using System.Globalization;

namespace AbsentSystem.Infrastructure.Utilities
{
    public static class DateTimeHelper
    {

        public static string ToPersionDate(this DateTime date)
        {

            if (date.Year != 1)
            {
                PersianCalendar jc = new PersianCalendar();

                return string.Format("{0}/{1}/{2}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));
            }
            return "هنوز مشخص نشد";
        }

        public static string ToPersionDayOfWeekName(this DateTime date)
        {
            if (date != null)
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Saturday: return "شنبه";
                    case DayOfWeek.Sunday: return "يکشنبه";
                    case DayOfWeek.Monday: return "دوشنبه";
                    case DayOfWeek.Tuesday: return "سه‏ شنبه";
                    case DayOfWeek.Wednesday: return "چهارشنبه";
                    case DayOfWeek.Thursday: return "پنجشنبه";
                    case DayOfWeek.Friday: return "جمعه";
                    default: return "";
                }
            return string.Empty;
        }

        public static DateTime GetTime(this DateTime d)
        {
            return new DateTime(d.TimeOfDay.Ticks);
        }
    }
}