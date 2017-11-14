using System.Collections.Generic;
using Medusa.Business.Entities;
using System.Data.Entity;
using System.Linq;

namespace Medusa.Business
{

    public class ProjectManager
    {
        private readonly MedusaManagerDbContext _ctx;

        public ProjectManager()
        {
            _ctx = new MedusaManagerDbContext();
        }

        /// <summary>
        /// Add new Project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Project Add(Project project)
        {
            Project returnProject = new Project()
            {
                EndDate = project.EndDate,
                Name = project.Name,
                StartDate = project.StartDate
            };
            foreach (var server in project.Servers)
            {
                returnProject.Servers.Add(_ctx.Servers.Find(server.Id));
            }
            foreach (var mem in project.Members)
            {
                returnProject.Members.Add(_ctx.Members.Find(mem.Id));
            }
            _ctx.Projects.Add(project);
            _ctx.SaveChanges();
            return returnProject;
        }

        /// <summary>
        /// Update exists project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Project Update(Project project)
        {
            Project returnProject = _ctx.Projects
                .Where(p => p.Id == project.Id)
                .Include(p => p.Servers)
                .Include(p => p.Members)
                .FirstOrDefault();

            returnProject.Name = project.Name;
            returnProject.EndDate = project.EndDate;
            returnProject.StartDate = project.StartDate;
            returnProject.Servers = new List<Server>();
            returnProject.Members = new List<Member>();

            foreach (var server in project.Servers)
            {
                returnProject.Servers.Add(_ctx.Servers.Find(server.Id));
            }
            foreach (var mem in project.Members)
            {
                returnProject.Members.Add(_ctx.Members.Find(mem.Id));
            }
            _ctx.SaveChanges();
            return returnProject;
        }

        /// <summary>
        /// Get Project by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Project GetProject(int projectId)
        {
            Project project = _ctx.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.Servers)
                .Include(p => p.Members)
                .FirstOrDefault();
            return project;
        }

        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>Return true if Project Exists and else</returns>
        public bool DeleteProject(int projectId)
        {
            Project returnProject = _ctx.Projects.Find(projectId);
            if (returnProject != null)
            {
                returnProject.Skills.Clear();
                returnProject.Members.Clear();
                _ctx.Projects.Remove(returnProject);
                _ctx.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all Projects from DB
        /// </summary>
        /// <returns>Dto Project List</returns>
        public List<Project> GetAllProjects()
        {
            List<Project> projects = 
                new List<Project>(_ctx.Set<Project>()
                .Include(p => p.Servers)
                .Include(p => p.Members));
            return projects;
        }

        /// <summary>
        /// Add Skill for Exists Project
        /// </summary>
        /// <param name="projectId">Id of Project </param>
        /// <param name="skillId">Id of Skill</param>
        /// <returns>Return a modified project if both skill and project found
        /// Return null if Skill not found</returns>
        public Project AddSkill(int projectId, int skillId)
        {
            Skill skill = _ctx.Skills.Find(skillId);
            Project project = _ctx.Projects.Where(p => p.Id == projectId)
                .Include(p => p.Skills)
                .FirstOrDefault();
            if (project != null && skill != null)
            {
                project.Skills.Add(skill);
                _ctx.SaveChanges();
                return project;
            }
            return null;
        }

        /// <summary>
        /// Add member for exists Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public Project AddMember(int projectId, int memberId)
        {
            Member member = _ctx.Members.Find(memberId);
            Project project = _ctx.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.Members)
                .FirstOrDefault();
            if (project != null && member != null)
            {
                project.Members.Add(member);
                _ctx.SaveChanges();
                return project;
            }

            return null;
        }

        /// <summary>
        /// Get all Skill of a Project by ProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>List of Skill</returns>
        public List<Skill> GetAllSkills(int projectId)
        {
            Project project = _ctx.Projects.Where(p => p.Id == projectId)
                .Include(p => p.Skills)
                .FirstOrDefault();
            return project.Skills;
        }

        /// <summary>
        /// Get all Members of a Project by ProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>List of Skill</returns>
        public List<Member> GetAllMembers(int projectId)
        {
            Project project = _ctx.Projects
                .Where(p => p.Id == projectId)
                .Include(p => p.Members)
                .FirstOrDefault();
            return project.Members;
        }
    }
}
