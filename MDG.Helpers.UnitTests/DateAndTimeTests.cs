using System;

using NUnit.Framework;

using MDG.Helpers;


namespace MDG.Helpers.UnitTests
{
    [TestFixture]
    public class DateAndTimeTests
    {
        [Test]
        public void GetDateFromWeekNumberAndDayOfWeek_GivenWeek1AndDayOfWeek1_Return29_Dec_2014()
        {
            DateTime expectedDate = new DateTime(2014,12,29);
            DateTime actualDate = DateAndTime.GetDateFromWeekNumberAndDayOfWeek(1, 1);

            Assert.AreEqual(expectedDate, actualDate, "Дата для первой недели 2015 года не равна 29 декабря 2014 года");
        }

        [Test]
        public void FirstDateOfWeekISO8601_GivenYear2015AndWeek1_Return29_Dec_2014()
        { 
            DateTime expectedDate = new DateTime(2014,12,29);
            DateTime actualDate = DateAndTime.FirstDateOfWeekISO8601(2015,1);

            Assert.AreEqual(expectedDate, actualDate, "Дата для первой недели 2015 года не равна 29 декабря 2014 года");
        }

        [Test]
        public void GetDateFromWeekNumberAndDayOfWeek_GivenWeek18AndDayOfWeek1_Return27_Apr_2015()
        {
            DateTime expectedDate = new DateTime(2015, 04, 27);
            DateTime actualDate = DateAndTime.GetDateFromWeekNumberAndDayOfWeek(18, 1);

            Assert.AreEqual(expectedDate, actualDate, "Дата для 18 недели 2015 года не равна 27 апреля 2015 года");
        }

        [Test]
        public void FirstDateOfWeekISO8601_GivenYear2015AndWeek18_ReturnReturn27_Apr_2015()
        {
            DateTime expectedDate = new DateTime(2015, 04, 27);
            DateTime actualDate = DateAndTime.FirstDateOfWeekISO8601(2015, 18);

            Assert.AreEqual(expectedDate, actualDate, "Дата для 18 недели 2015 года не равна 27 апреля 2015 года"); 
        }

        [Test]
        public void GetNumberOfWorkingDays_Given20150330And20150321_Returns2()
        {
            DateTime startDate = new DateTime(2015, 3, 30);
            DateTime endDate = new DateTime(2015, 3, 31);
            DateTime[] holidays = new DateTime[] { };

            int actualWorkingDays = DateAndTime.GetNumberOfWorkingDays(startDate, endDate, holidays);

            Assert.AreEqual(2, actualWorkingDays);
        }

        [Test]
        public void GetNumberOfWorkingDays_Given20150325And20150321_Returns5()
        {
            DateTime startDate = new DateTime(2015, 3, 25);
            DateTime endDate = new DateTime(2015, 3, 31);
            DateTime[] holidays = new DateTime[] { };

            int actualWorkingDays = DateAndTime.GetNumberOfWorkingDays(startDate, endDate, holidays);

            Assert.AreEqual(5, actualWorkingDays);
        }

        [Test]
        public void GetMonthNumberFromName_GivenJanuary_Returns1()
        {
            Assert.AreEqual(1, DateAndTime.GetMonthNumberFromName("January"));
        }

        [Test]
        public void GetMonthNumberFromName_GivenЯнварь_Returns1()
        {
            Assert.AreEqual(1, DateAndTime.GetMonthNumberFromName("Январь"));
        }

        [Test]
        public void GetMonthNumberFromName_GivenMarch_Returns3()
        {
            Assert.AreEqual(3, DateAndTime.GetMonthNumberFromName("March"));
        }

        [Test]
        public void GetMonthNumberFromName_GivenАпрель_Returns4()
        {
            Assert.AreEqual(4, DateAndTime.GetMonthNumberFromName("Апрель"));
        }
    
    }
}
