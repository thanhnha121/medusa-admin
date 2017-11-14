using System;
using System.Collections.Generic;
using Medusa.Business;
using Medusa.Business.Entities;
using NUnit.Framework;

namespace Medusa.Test
{
    [TestFixture]
    class ProjectManagerTest
    {
        private ProjectManager _projectManager;

        [SetUp]
        public void SetUp()
        {
            _projectManager = new ProjectManager();
        }

        [Test, TestCaseSource(nameof(TestAddCases))]
        public void AddProjectTest(Project project, string expect)
        {
            Project p = _projectManager.Add(project);
            if (p != null)
            {
                Assert.AreEqual(p.Name, expect);
            }
        }

        static readonly object[] TestAddCases =
        {
            new object[]
            {
                new Project() {
                    Name = "Slogan",
                    EndDate = DateTime.Parse("2017-11-11 13:00:00.000"),
                    StartDate = DateTime.Parse("2016-11-11 13:00:00.000")
                },
                "Slogan"
            },
            new object[]
            {
                new Project(),
                null
            }
        };

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void GetProjectTest(int id)
        {
            Project p = _projectManager.GetProject(id);
            if (p != null)
            {
                Assert.AreEqual(id, p.Id);
            }
        }

        [Test]
        public void GetAllProjectsTest()
        {
            List<Project> projects = _projectManager.GetAllProjects();
            Assert.AreEqual(2, projects.Count);
        }

        [Test]
        [TestCase(1, true)]
        [TestCase(100, false)]
        public void DeleteProjectTest(int id, bool expect)
        {
            bool rs = _projectManager.DeleteProject(id);
            Assert.AreEqual(rs, expect);
        }


        [Test, TestCaseSource(nameof(TestUpdateCases))]
        public void UpdateProjectTest(Project project, string expect)
        {
            Project p = _projectManager.Update(project);

            if (p != null)
            {
                Assert.AreEqual(p.Name, expect);
            }
        }

        static readonly object[] TestUpdateCases =
        {
            new object[]
            {
                new Project() {
                    Id = 5,
                    Name = "Slogan1",
                    EndDate = DateTime.Parse("2017-11-11 13:00:00.000"),
                    StartDate = DateTime.Parse("2016-11-11 13:00:00.000")
                },
                "Slogan1"
            },
            new object[]
            {
                new Project(),
                null
            }
        };

        [Test]
        [TestCase(5, 2)]
        [TestCase(5, 100)]
        public void AddSkillTest(int projectId, int skillId)
        {
            Project p = _projectManager.AddSkill(projectId, skillId);
            if (p != null)
            {
                Assert.AreEqual(p.Id, projectId);
                Assert.AreEqual(skillId, p.Skills[p.Skills.Count - 1].Id);
            }
        }

        [Test]
        [TestCase(5, 4)]
        [TestCase(5, 100)]
        public void AddMember(int projectId, int memberId)
        {
            Project p = _projectManager.AddMember(projectId, memberId);
            if (p != null)
            {
                Assert.AreEqual(p.Id, projectId);
                Assert.AreEqual(memberId, p.Members[p.Members.Count - 1].Id);
            }
        }

        [Test]
        [TestCase(5, 1)]
        [TestCase(100, 0)]
        public void GetAllSkillsTest(int projectId, int expect)
        {
            List<Skill> skills = _projectManager.GetAllSkills(projectId);
            if (skills != null)
            {
                Assert.AreEqual(skills.Count, expect);
            }
        }

        [Test]
        [TestCase(5, 1)]
        [TestCase(100, 0)]
        public void GetAllMembersTest(int projectId, int expect)
        {
            List<Member> members = _projectManager.GetAllMembers(projectId);
            if (members != null)
            {
                Assert.AreEqual(members.Count, expect);
            }
        }
    }
}
