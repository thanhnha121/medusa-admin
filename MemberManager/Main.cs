using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MemberManager.Models;

namespace MemberManager
{

    class MemberManagerDbContext : DbContext
    {

        static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            using (var context = new MemberManagerDbContext())
            {
               // new MemberManagerDbContext().SeedData(context);
                context.Members.FirstOrDefault();
            }
        }

        public void SeedData(MemberManagerDbContext context)
        {
            List<Skill> skills = new List<Skill>();
            List<Member> members = new List<Member>();
            List<Project> projects = new List<Project>();
            List<MemberSkill> memberSkills = new List<MemberSkill>();
            for (int i = 0; i < 10; i++)
            {
                skills.Add(new Skill() { SkillName = "SkillName" + i });
            }
            foreach (var skill in skills)
            {
                context.Skills.AddOrUpdate(skill);
            }

            for (int i = 0; i < 10; i++)
            {
                members.Add(new Member() {MemberName = "MemberName" + i,
                    Address = "Address"+i,
                    Birthday = DateTime.Today,
                    Email = "email"+i+"@fsoft.com.vn",
                    AccountId = "account" + i,
                    Password = "password" + i,
                });
            }

            for (int i = 0; i < 10; i++)
            {
                projects.Add(new Project()
                {
                    ProjectName = "ProjectName" + i,
                    StartDate = new DateTime(2015, 1 + i, 1),
                    EndDate = new DateTime(2016, 1 + i, 1),
                });
            }

            for (int i = 0; i < 2; i++)
            {
                List<Member> projectMembers = new List<Member>() {members[i], members[2]};
                projects[i].Members = projectMembers;
                context.Projects.AddOrUpdate(projects[i]);

                List<Project> memProjects = new List<Project>() {projects[i], projects[2]};
                members[i].Projects = projects;
                members[i].Skills = new List<MemberSkill>() {new MemberSkill()
                {
                    Member = members[i],
                    Skill = skills[i],
                    YearsOfExp = 1
                } };
                context.Members.AddOrUpdate(members[i]);
            }

            context.SaveChanges();
            //int dice = rnd.Next(1, 7);
        }

        public MemberManagerDbContext() : base("TestEFCF")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MemberManagerDbContext, Migrations.Configuration>());
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberSkill> MemberSkills { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
