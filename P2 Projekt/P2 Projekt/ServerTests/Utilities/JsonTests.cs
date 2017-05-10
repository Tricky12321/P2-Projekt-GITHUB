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
            string JsonCompare = "object,Bus|{\"StoppeStederMTid\":[{\"Stop\":{\"StoppestedName\":\"Skydebanevej v/ Væddeløbsbanen\",\"StoppestedID\":1,\"StoppestedLok\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309}},\"AfPåTidComb\":[{\"ID\":0,\"Tidspunkt\":{\"hour\":13,\"minute\":0},\"afstigninger\":0,\"påstigninger\":0},{\"ID\":0,\"Tidspunkt\":{\"hour\":14,\"minute\":0},\"afstigninger\":0,\"påstigninger\":0}]}],\"busName\":\"TestBus\",\"BusID\":9999,\"placering\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309},\"CapacitySitting\":22,\"CapacityStanding\":222,\"Rute\":{\"RuteName\":\"TestRute\",\"RuteID\":999,\"StoppeSteder\":[{\"StoppestedName\":\"Skydebanevej v/ Væddeløbsbanen\",\"StoppestedID\":1,\"StoppestedLok\":{\"xCoordinate\":57.054779,\"yCoordinate\":9.882309}}]},\"PassengerUpdate\":null,\"PassengersTotal\":0}";
            Assert.AreEqual(JsonCompare, Json.Serialize(TestObj1));
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
        [TestCase("object,Bus|",ExpectedResult = typeof(Bus))]
        [TestCase("object,Stoppested|", ExpectedResult = typeof(Stoppested))]
        [TestCase("object,StoppestedMTid|", ExpectedResult = typeof(StoppestedMTid))]
        [TestCase("object,Rute|", ExpectedResult = typeof(Rute))]
        public Type GetTypeFromStringTest(string JsonString)
        {
            return Json.GetTypeFromString(JsonString);
        }
    }
}