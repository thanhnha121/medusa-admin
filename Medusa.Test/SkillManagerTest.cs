using System;
using Medusa.Business;
using Medusa.Business.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Medusa.Test
{
    [TestFixture]
    class SkillManagerTest
    {
        private SkillManager _skillManager;

        [SetUp]
        public void SetUp()
        {
            _skillManager = new SkillManager();
        }

        [Test]
        [TestCase("Java Core1", true)]
        [TestCase("AngularJS", false)]
        public void CheckSkillExistsTest(String a, bool expect)
        {
            bool result = _skillManager.Exists(a);
            Assert.AreEqual(expect, result);
        }

        [Test]
        [TestCase(".NET", ".NET")]
        [TestCase("Unit Test", null)]
        public void AddSkillTest(string skillName, string expect)
        {
            Skill reSkill = _skillManager.AddSkill(new Skill() { Name = skillName });
            var result = reSkill?.Name;
            Assert.AreEqual(expect, result);
        }

        [Test, TestCaseSource(nameof(TestUpdateCases))]
        public void UpdateSkill(Skill skill, string expect)
        {
            skill.Name += "1";
            Skill reSkill = _skillManager.UpdateSkill(skill);
            if (reSkill != null)
            {
                Assert.AreEqual(reSkill.Name, expect);
            }
        }

        static readonly object[] TestUpdateCases =
        {
            new object[]
            {
                new Skill(){Id = 1, Name = "Java Core", Descriptions = "Advance in Java"},
                "Java Core1"
            },
            new object[]
            {
                new Skill(){Id = 100, Name = ""},
                null
            }
        };

        [Test]
        [TestCase(3, true)]
        [TestCase(200, false)]
        public void DeleteSkillTest(int id, bool expect)
        {
            bool result = _skillManager.DeleteSkill(id);
            Assert.AreEqual(expect, result);
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void GetSkill(int id)
        {
            Skill skill = _skillManager.GetSkill(id);
            if (skill != null)
            {
                Assert.AreEqual(skill.Id, id);
            }
        }

        [Test]
        public void GetAllSkillsTest()
        {
            List<Skill> skills = _skillManager.GetAllSkill();
            Assert.AreEqual(skills.Count, 2);
        }

    }
}
