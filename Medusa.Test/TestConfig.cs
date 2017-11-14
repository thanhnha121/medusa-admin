
using NUnit.Framework;

namespace Medusa.Test
{
    [TestFixture]
    class TestConfig
    {
        private const string FileName = "D:\\MockProject\\trunk\\Medusa.Test\\MedusaTest.xml";


        [Test]
        public void GetConnStringTest()
        {
            MedusaTestConfiguration m = new MedusaTestConfiguration();
            string rs = m.GetConnString(FileName);
            Assert.IsNotNull(rs);
        }

    }
}
