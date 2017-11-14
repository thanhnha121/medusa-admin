using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using Medusa.Business.Entities;
using System.Linq;

namespace Medusa.Business
{

    public class MemberManager
    {
        private readonly MedusaManagerDbContext _ctx;

        public MemberManager()
        {
            _ctx = new MedusaManagerDbContext();
        }
        /// <summary>
        /// Add a member 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member AddMember(Member member)
        {
            if (!string.IsNullOrEmpty(member.Picture))
            {
                using (Image image = Image.FromFile(member.Picture))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        member.Picture = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            Member mem = new Member()
            {
                Name = member.Name,
                Address = member.Address,
                Birthday = member.Birthday,
                Email = member.Email,
                Password = member.Password,
                Picture = member.Picture,
                Type = member.Type,
                UserName = member.UserName
            };
            foreach (var skill in member.Skills)
            {
                mem.Skills.Add(_ctx.MemberSkills.Find(skill.Id));
            }
            foreach (var project in member.Projects)
            {
                mem.Projects.Add(_ctx.Projects.Find(project.Id));
            }
            Member memReturn = _ctx.Members.Add(mem);
            _ctx.SaveChanges();
            return memReturn;
        }
        /// <summary>
        /// Assign a member to project by memberId and projectId
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Member AssignToProject(int memberId, int projectId)
        {
            Member member = _ctx.Members.Where(m => m.Id == memberId)
                .Include(m => m.Projects)
                .FirstOrDefault();
            member.Projects.Add(_ctx.Projects.Find(projectId));
            _ctx.SaveChanges();
            return member;
        }
        /// <summary>
        /// Insert a skill
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public MemberSkill InsertSkill(MemberSkill ms)
        {
            MemberSkill memberSkill = new MemberSkill();
            memberSkill.Member = _ctx.Members.Find(ms.Member.Id);
            memberSkill.Skill = _ctx.Skills.Find(ms.Skill.Id);
            return _ctx.MemberSkills.Add(ms);
        }
        /// <summary>
        /// Delete a member by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMember(int id)
        {
            Member mem = _ctx.Members.Find(id);
            mem.Skills.Clear();
            mem.Projects.Clear();
            _ctx.SaveChanges();
            return mem != null;
        }

        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns></returns>
        public List<Member> GetAllMembers()
        {
            List<Member> mems = new List<Member>(_ctx.Set<Member>().Include(s => s.Projects));
            return mems;
        }

        // Get member by id
        public Member GetMember(int id)
        {
            return _ctx.Members.Where(m => m.Id == id)
                .Include(m => m.Projects)
                .Include(m => m.Skills)
                .FirstOrDefault();
        }

        /// <summary>
        /// Update a member 
        /// </summary>
        /// <param name="newMember">New member</param>
        /// <returns></returns>
        public Member UpdateMember(Member newMember)
        {
            Member member = _ctx.Members.Find(newMember.Id);
            if (member != null)
            {
                member.Name = newMember.Name;
                member.Address = newMember.Address;
                member.Type = newMember.Type;
                member.Birthday = newMember.Birthday;
                member.Email = newMember.Email;
                member.UserName = newMember.UserName;
            }
            member.Projects = new List<Project>();
            foreach (var project in member.Projects)
            {
                member.Projects.Add(_ctx.Projects.Find(project.Id));
            }
            _ctx.SaveChanges();
            return _ctx.Members.Find(newMember.Id);
        }

        /// <summary>
        /// Add project to member
        /// </summary>
        /// <param name="memberId">Id of member</param>
        /// <param name="projectId">Id of project</param>
        /// <returns></returns>
        public Member AddProject(string memberId, int projectId)
        {
            int id = int.Parse(memberId);
            Member member = _ctx.Members.Where(m => m.Id == id)
                .Include(m => m.Projects)
                .FirstOrDefault();
            Project p = _ctx.Projects.Find(projectId);
            if (member != null && p != null)
            {
                member.Projects.Add(p);
                _ctx.SaveChanges();
                return member;
            }
            return null;
        }
        /// <summary>
        /// List all project
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public List<Project> GetAllProjects(string memberId)
        {
            int id = int.Parse(memberId);
            Member member = _ctx.Members.Where(m => m.Id == id)
                .Include(m => m.Projects)
                .FirstOrDefault();
            return member.Projects;
        }
    }
}
