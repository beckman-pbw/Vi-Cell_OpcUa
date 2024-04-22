using System;
using Google.Protobuf.WellKnownTypes;
using NUnit.Framework;

namespace ViCellBluOpcUaModelDesignTests
{
    public class DateTimeAutoMapperTests : BaseAutoMapperTests
    {
        [Test]
        public void DateTimeToTimestampTests()
        {
            var dateTime = DateTime.MinValue;
            var map = new Timestamp();

            dateTime = DateTime.MinValue.ToLocalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());

            dateTime = DateTime.MinValue.ToUniversalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());

            dateTime = DateTime.MaxValue.ToLocalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());

            dateTime = DateTime.MaxValue.ToUniversalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());

            dateTime = DateTime.Now.ToLocalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());

            dateTime = DateTime.Now.ToUniversalTime();
            map = Mapper.Map<Timestamp>(dateTime);
            Assert.IsNotNull(map);
            Assert.AreEqual(dateTime, map.ToDateTime());
        }

        [Test]
        public void TimestampToDateTimeTests()
        {
            var timestamp = new Timestamp();
            var map = DateTime.MinValue;

            timestamp = Timestamp.FromDateTime(DateTime.MinValue.ToUniversalTime());
            map = Mapper.Map<DateTime>(timestamp);
            Assert.IsNotNull(map);
            Assert.AreEqual(timestamp.ToDateTime(), map);

            timestamp = Timestamp.FromDateTime(DateTime.MaxValue.ToUniversalTime());
            map = Mapper.Map<DateTime>(timestamp);
            Assert.IsNotNull(map);
            Assert.AreEqual(timestamp.ToDateTime(), map);

            timestamp = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
            map = Mapper.Map<DateTime>(timestamp);
            Assert.IsNotNull(map);
            Assert.AreEqual(timestamp.ToDateTime(), map);
        }
    }
}