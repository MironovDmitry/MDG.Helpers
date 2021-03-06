﻿using System;
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

        /// <summary>
        /// Calculates number of business days, taking into account:
        ///  - weekends (Saturdays and Sundays)
        ///  - bank holidays in the middle of the week
        /// </summary>
        /// <param name="firstDay">First day in the time interval</param>
        /// <param name="lastDay">Last day in the time interval</param>
        /// <param name="bankHolidays">List of bank holidays excluding weekends</param>
        /// <returns>Number of business days during the 'span'</returns>
        public static int GetNumberOfWorkingDays(this DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }

        public static int GetMonthNumberFromName(string monthName)
        {
            int monthNumber = 1;

            switch (monthName.ToLower())
            { 
                case "january": 
                case "январь":
                    monthNumber = 1;
                    break;
                case "ferbruary":
                case "февраль":
                    monthNumber = 2;
                    break;
                case "march":
                case "март":
                    monthNumber = 3;
                    break;
                case "april":
                case "апрель":
                    monthNumber = 4;
                    break;
                case "may":
                case "май":
                    monthNumber = 5;
                    break;
                case "june":
                case "июнь":
                    monthNumber = 6;
                    break;
                case "july":
                case "июль":
                    monthNumber = 7;
                    break;
                case "august":
                case "август":
                    monthNumber = 8;
                    break;
                case "september":
                case "сентябрь":
                    monthNumber = 9;
                    break;
                case "october":
                case "октябрь":
                    monthNumber = 10;
                    break;
                case "november":
                case "ноябрь":
                    monthNumber = 11;
                    break;
                case "december":
                case "декабрь":
                    monthNumber = 12;
                    break;
                default:
                    break;
            }

            return monthNumber;
        }

        public static int GetWeekOfYear(DateTime date)
        {
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
