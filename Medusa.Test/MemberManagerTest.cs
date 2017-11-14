using System;
using Medusa.Business.Entities;
using NUnit.Framework;

namespace Medusa.Test
{
    [TestFixture]
    internal class MemberManagerTest
    {
        [SetUp]
        public void Setup()
        {
            _memberManager = new Business.MemberManager();
        }

        [Test]
        [TestCase(3, 3)]
        public void TestGetOneMember(int idTest, int idActual)
        {


            Member tempMember = _memberManager.GetMember(idTest);

            Assert.AreEqual(tempMember.Id, idActual);

        }

        private Business.MemberManager _memberManager;

        [Test]
        [TestCase("MemberTest1", "MemberTest1")]
        public void TestAddMember(string memberName, string expect)
        {
            Member reMember = _memberManager.AddMember(new Member() {Birthday = DateTime.Now, Name = memberName, Type = 1});
            var result = reMember?.Name;
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void TestGetAllMember()
        {
            //int count;
            var actual = _memberManager.GetAllMembers();
            Assert.AreEqual(2, actual.Count);           
        }


        [Test, TestCaseSource(nameof(TestUpdateCases))]
        public void TestUpdateMember(Member memberResult, Member memberExpect)
        {

            Member reMember = _memberManager.GetMember(memberResult.Id);
            reMember.Name = memberExpect.Name;
            memberResult = _memberManager.UpdateMember(reMember);
            Assert.AreEqual(memberResult.Name, memberExpect.Name);
        }

        private static readonly object[] TestUpdateCases =
        {
            new object[]
            {
                new Member()
                {
                    Id = 3,
                    Name = "Phuong Nguyen",
                    Type = 1,
                    Birthday = DateTime.Now
                },
                new Member()
                {
                    Id = 3,
                    Name = "Phuong Nguyen 1",
                    Type = 1,
                    Birthday = DateTime.Now
                }
            },
             new object[]
            {
                new Member()
                {
                    Id = 3,
                    Name = "Phuong Nguyen 2",
                    Type = 1,
                    Birthday = DateTime.Now
                },
                new Member()
                {
                    Id = 3,
                    Name = "Phuong Nguyen 3",
                    Type = 1,
                    Birthday = DateTime.Now
                }
            }

        };

    }
    }

