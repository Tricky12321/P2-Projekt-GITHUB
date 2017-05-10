using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class IPHandlerTests
    {
        [Test()]
        [TestCase("127.0.0.1",ExpectedResult = true)]
        [TestCase("192.168.0.1",ExpectedResult = true)]
        [TestCase("999.999.999.999",ExpectedResult = true)]
        [TestCase("172.25.11.120",ExpectedResult = true)]
        [TestCase("asdf",ExpectedResult = false)]
        [TestCase("fisk",ExpectedResult = false)]
        [TestCase("a:b:c:d:e",ExpectedResult = false)]
        [TestCase("1234:1234:1234:1234",ExpectedResult = false)]
        public bool IsIPV4Test(string IP)
        {
            return IPHandler.IsIPV4(IP);
        }

        [Test()]
        [TestCase("127.0.0.1", ExpectedResult = false)]
        [TestCase("192.168.0.1", ExpectedResult = false)]
        [TestCase("999.999.999.999", ExpectedResult = false)]
        [TestCase("172.25.11.120", ExpectedResult = false)]
        [TestCase("asdf", ExpectedResult = false)]
        [TestCase("fisk", ExpectedResult = false)]
        [TestCase("a:b:c:d:e", ExpectedResult = true)]
        [TestCase("1234:1234:1234:1234", ExpectedResult = true)]
        public bool IsIPV6Test(string IP)
        {
            return IPHandler.IsIPV6(IP);
        }
    }
}