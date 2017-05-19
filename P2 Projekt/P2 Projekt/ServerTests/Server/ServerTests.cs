using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture()]
    public class ServerTests
    {
        [Test()]
        [TestCase(",ALL", ExpectedResult = "ALL")]
        [TestCase(",Tast,", ExpectedResult = "Tast,")]
        [TestCase(",None", ExpectedResult = "None")]
        public string GetWhereConditionTest(string Test)
        {
            Server TestServer = new Server(12943, ServerType.Ipv4);
            string WhereCondition = TestServer.GetWhereCondition(ref Test);
            return WhereCondition;
        }
    }
}
