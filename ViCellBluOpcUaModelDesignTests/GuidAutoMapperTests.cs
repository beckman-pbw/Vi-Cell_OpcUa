using System;
using NUnit.Framework;
using Opc.Ua;

namespace ViCellBluOpcUaModelDesignTests
{
    public class GuidAutoMapperTests : BaseAutoMapperTests
    {

        [Test]
        public void StringToGuidTests()
        {
            var str = string.Empty;
            var map = Guid.Empty;

            str = Guid.NewGuid().ToString();
            map = Mapper.Map<Guid>(str);

            Assert.IsNotNull(map);
            Assert.AreEqual(str, map.ToString());

            str = string.Empty;
            map = Mapper.Map<Guid>(str);
            Assert.IsNotNull(map);
            Assert.AreEqual(Guid.Empty, map);

            str = null;
            map = Mapper.Map<Guid>(str);
            Assert.IsNotNull(map);
            Assert.AreEqual(Guid.Empty, map);
        }

        [Test]
        public void GuidToStringTests()
        {
            var guid = Guid.Empty;
            var map = string.Empty;

            guid = Guid.Empty;
            map = Mapper.Map<string>(guid);
            Assert.IsNotNull(map);
            Assert.AreEqual(Guid.Empty.ToString().ToUpper(), map.ToUpper());

            guid = Guid.NewGuid();
            map = Mapper.Map<string>(guid);
            Assert.IsNotNull(map);
            Assert.AreEqual(guid.ToString().ToUpper(), map.ToUpper());
        }

        [Test]
        public void StringToUuidTests()
        {
            var str = string.Empty;
            var map = Guid.Empty;

            str = new Uuid(Guid.NewGuid()).ToString();
            map = Mapper.Map<Uuid>(str);

            Assert.IsNotNull(map);
            Assert.AreEqual(str, map.ToString());

            str = string.Empty;
            map = Mapper.Map<Uuid>(str);
            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty, map);

            str = null;
            map = Mapper.Map<Uuid>(str);
            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty, map);
        }

        [Test]
        public void UuidToStringTests()
        {
            var uuid = Uuid.Empty;
            var map = string.Empty;

            uuid = Uuid.Empty;
            map = Mapper.Map<string>(uuid);
            Assert.IsNotNull(map);
            Assert.AreEqual(Uuid.Empty.ToString().ToUpper(), map.ToUpper());

            uuid = new Uuid(Guid.NewGuid());
            map = Mapper.Map<string>(uuid);
            Assert.IsNotNull(map);
            Assert.AreEqual(uuid.ToString().ToUpper(), map.ToUpper());
        }

    }
}