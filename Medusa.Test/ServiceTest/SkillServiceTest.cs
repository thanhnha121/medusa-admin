using System;
using System.Net;
using System.ServiceModel;
using Medusa.Adapter.Dto;
using Medusa.Service;
using Medusa.Service.Utils;
using NUnit.Framework;

namespace Medusa.Test.ServiceTest
{
    [TestFixture]
    public class SkillServiceTest
    {
        private const String GetSkillUrl = "http://localhost:6069/groups";

        [SetUp]
        public void SetUp()
        {
        }
        /// <summary>
        /// Opent host
        /// </summary>
        public void OpenHost()
        {
            Uri baseAddress = new Uri("http://localhost/Medusa/Skill");
            var host = new ServiceHost(typeof(SkillService), baseAddress);
            host.Open();
        }
        /// <summary>
        /// Test for get skill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expectRequest"></param>
        [Test]
        [TestCase(1, true)]
        [TestCase(1000, false)]
        public void GetSkillTest(int id, bool expectRequest)
        {
            int statusCode;
            OpenHost();
            var requestResult = HttpUtils.MakeJsonRequest(GetSkillUrl + id,
                null,
                "GET",
                "",
                typeof(SkillDto),
                out statusCode);
            Assert.AreEqual(expectRequest, requestResult);
            Assert.AreEqual((HttpStatusCode)statusCode, HttpStatusCode.OK);
        }
    }
}
