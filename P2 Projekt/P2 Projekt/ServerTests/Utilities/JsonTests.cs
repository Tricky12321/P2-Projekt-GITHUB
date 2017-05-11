using NUnit.Framework;
using JsonSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer.Tests
{
    [TestFixture()]
    public class JsonTests
    {
        [Test()]
        public void TestSerializer()
        {
            Bus TestObj1 = new Bus();
            TestObj1.BusID = 9999;
            TestObj1.GetUpdate();
            string json = Json.Serialize(TestObj1);
            Bus TestObj2 = Json.Deserialize(json).First() as Bus;
            Assert.AreEqual(TestObj1.BusID, TestObj2.BusID);
        }
        [Test()]
        public void TestSerializer2()
        {
            Stoppested TestObj1 = new Stoppested();
            TestObj1.StoppestedID = 1;
            TestObj1.GetUpdate();
            string json = Json.Serialize(TestObj1);
            Stoppested TestObj2 = Json.Deserialize(json).First() as Stoppested;
            Assert.AreEqual(TestObj1.StoppestedID, TestObj2.StoppestedID);
        }
        [Test()]
        public void TestSerializer3()
        {
            Rute TestObj1 = new Rute();
            TestObj1.RuteID = 1;
            TestObj1.GetUpdate();
            string json = Json.Serialize(TestObj1);
            Rute TestObj2 = Json.Deserialize(json).First() as Rute;
            Assert.AreEqual(TestObj1.RuteID, TestObj2.RuteID);
        }
        [Test()]
        public void TestDeserializer()
        {
            Bus TestObj1 = new Bus();
            TestObj1.BusID = 9999;
            TestObj1.GetUpdate();
            string JsonCompare = "object,Bus|{\"StoppeStederMTid\":[{\"Stop\":{\"StoppestedName\":\"Skydebanevej v/ Væddeløbsbanen\",\"StoppestedID\":1,\"StoppestedLok\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309}},\"AfPåTidComb\":[{\"ID\":0,\"Tidspunkt\":{\"hour\":13,\"minute\":0},\"afstigninger\":0,\"påstigninger\":0},{\"ID\":0,\"Tidspunkt\":{\"hour\":14,\"minute\":0},\"afstigninger\":0,\"påstigninger\":0}]}],\"busName\":\"TestBus\",\"BusID\":9999,\"placering\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309},\"CapacitySitting\":22,\"CapacityStanding\":222,\"Rute\":{\"RuteName\":\"TestRute\",\"RuteID\":999,\"StoppeSteder\":[{\"StoppestedName\":\"Skydebanevej v/ Væddeløbsbanen\",\"StoppestedID\":1,\"StoppestedLok\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309}}]},\"PassengerUpdate\":null,\"PassengersTotal\":0}";
            Assert.AreEqual(TestObj1.BusID, (Json.Deserialize(JsonCompare).First() as Bus).BusID);
        }
        [Test()]
        public void TestDeserializer1()
        {
            Rute TestObj1 = new Rute();
            TestObj1.RuteID = 1;
            TestObj1.GetUpdate();
            string JsonCompare = Json.Serialize(TestObj1);
            Assert.AreEqual(TestObj1.RuteID, (Json.Deserialize(JsonCompare).First() as Rute).RuteID);
        }
        [Test()]
        public void TestDeserializer2()
        {
            Stoppested TestObj1 = new Stoppested();
            TestObj1.StoppestedID = 1;
            TestObj1.GetUpdate();
            string JsonCompare = Json.Serialize(TestObj1);
            Assert.AreEqual(TestObj1.StoppestedID, (Json.Deserialize(JsonCompare).First() as Stoppested).StoppestedID);
        }

        [Test()]
        [TestCase("object,Bus|", ExpectedResult = typeof(Bus))]
        [TestCase("object,Stoppested|", ExpectedResult = typeof(Stoppested))]
        [TestCase("object,StoppestedMTid|", ExpectedResult = typeof(StoppestedMTid))]
        [TestCase("object,Rute|", ExpectedResult = typeof(Rute))]
        public Type GetTypeFromStringTest(string JsonString)
        {
            return Json.GetTypeFromString(JsonString);
        }

        [Test()]
        [TestCase("asdf<EOF>",ExpectedResult = "asdf")]
        [TestCase("hejmeddig<EOF>",ExpectedResult = "hejmeddig")]
        [TestCase("hej med dig<EOF>",ExpectedResult = "hej med dig")]
        [TestCase("<EOF>asdf<EOF>",ExpectedResult = "asdf")]
        public string TrimDataTest(string text)
        {
             Json.TrimData(ref text);
            return text;
        }
    }
}