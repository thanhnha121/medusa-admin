using System;
using Medusa.Business;
using Medusa.Business.Entities;
using Moq;
using NUnit.Framework;

namespace Medusa.Test
{
    [TestFixture]
    class LoginManagerTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        [TestCase("username1", "123")]
        [TestCase("username1", "12345")]
        [TestCase("username1", " ")]
        [TestCase(" ", "12345")]
        [TestCase(" ", " ")]
        public void MemberLoginTest(String userName, String passWord)
        {
            var memberLogin = new LoginManager();
            //memberLogin.LogIn(x => x.(It.IsAny<String>(), It.IsAny<String>())).Returns(true);
            Member a = memberLogin.LogIn(new Member() { UserName = userName, Password = passWord });
            if (a != null)
            {
                
            }
            else
            {
                Assert.AreEqual(a.UserName, userName);
                Assert.AreEqual(a.Password, passWord);
            }
           
        }
    }
}
