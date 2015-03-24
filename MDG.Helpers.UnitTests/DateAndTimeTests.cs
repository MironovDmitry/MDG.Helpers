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
    }
}
