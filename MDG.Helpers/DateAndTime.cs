using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDG.Helpers
{
    public static class DateAndTime
    {
        /// <summary>
        /// Get date of the first day of the week for the current year
        /// </summary>
        /// <param name="weekNumber">Ween number in the year.</param>
        /// <param name="firstDayOfWeek">1 - Monday, 0 - Sunday</param>
        /// <returns>DateTime of the first day with requested week</returns>
        public static DateTime GetDateFromWeekNumberAndDayOfWeek(int weekNumber, int firstDayOfWeek)
        {
            DateTime jan1 = new DateTime(DateTime.Now.Year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekNumber;
            if (firstWeek <= 1)
            {
                weekNum -= 1; 
            }

            var result = firstMonday.AddDays(weekNum * 7 + firstDayOfWeek - 1);
            return result;
        }

        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
    }
}
