using System.Collections.Generic;
using Medusa.Business;
using Medusa.Business.Entities;
using NUnit.Framework;

namespace Medusa.Test
{
    [TestFixture]
    class ServerManagerTest
    {
        private ServerManager _serverManager;

        [SetUp]
        public void SetUp()
        {
            _serverManager = new ServerManager();
        }

        [Test, TestCaseSource(nameof(TestAddCases))]
        public void AddServerTest(Server server, string ip)
        {
            Server s = _serverManager.Add(server);
            if (s != null)
            {
                Assert.AreEqual(s.Ip, ip);
            }
        }

        static readonly object[] TestAddCases =
        {
            new object[]
            {
                new Server() {
                    Ip = "10.10.10.10",
                    SystemOS = "MAC"
                },
                "10.10.10.10"
            },
            new object[]
            {
                new Server() {
                    Ip = "10.10.10.10",
                    SystemOS = "MAC"
                },
                null
            }
        };

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void GetServerTest(int id)
        {
            Server s = _serverManager.GetServer(id + "");
            if (s != null)
            {
                Assert.AreEqual(id, s.Id);
            }
        }

        [Test]
        [TestCase("10.10.10.10", 5)]
        [TestCase("10.10.10.10", 3)]
        [TestCase("10.10.10.101", 3)]
        public void AddProject(string serverIp, int projectId)
        {
            Server s = _serverManager.AddProject(serverIp, projectId);
            if (s != null)
            {
                Assert.AreEqual(s.Projects[s.Projects.Count - 1].Id, projectId);
            }
        }

        [Test]
        public void GetAllServerTest()
        {
            List<Server> servers = _serverManager.GetAllServers();
            Assert.AreEqual(2, servers.Count);
        }

        [Test]
        [TestCase(6, true)]
        [TestCase(100, false)]
        public void DeleteServerTest(int serverId, bool expect)
        {
            bool rs = _serverManager.Delete(serverId + "");
            Assert.AreEqual(expect, rs);
        }

        [Test, TestCaseSource(nameof(TestUpdateCases))]
        public void UpdateServerTest(Server server, string updatedInfo)
        {
            Server s = _serverManager.Update(server);
            if (s != null)
            {
                Assert.AreEqual(s.SystemOS, updatedInfo);
            }
        }

        static readonly object[] TestUpdateCases =
        {
            new object[]
            {
                new Server() {
                    Id = 1,
                    Ip = "127.0.0.1",
                    SystemOS = "Win 7"
                },
                "Win 7"
            },
            new object[]
            {
                new Server() {
                    Ip = "10.10.10.101",
                    SystemOS = "MAC"
                },
                null
            }
        };


    }
}
