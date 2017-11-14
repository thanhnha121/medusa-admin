using System;
using System.Collections.Generic;
using Medusa.Adapter.Interface;
using Medusa.Business;
using Medusa.Business.Entities;
using Medusa.Adapter.Dto;

namespace Medusa.Adapter
{
    public class ProjectAdapter : IProjectAdapter
    {
        private readonly ProjectManager _projectManager;

        public ProjectAdapter()
        {
            _projectManager = new ProjectManager();
        }

        public Project ConvertDtoToEntity(ProjectDto proDto)
        {
            SkillAdapter skillAdapter = new SkillAdapter();
            Project project = new Project()
            {
                Id = proDto.Id,
                Name = proDto.Name,
                Skills = skillAdapter.ConvertListDtosToEntities(proDto.Skills),
                EndDate = DateTime.Parse(proDto.EndDate),
                StartDate = DateTime.Parse(proDto.StartDate),
            };
            foreach (var member in proDto.Members)
            {
                project.Members.Add(new Member() {
                    Id = member.Id,
                    Name = member.Name
                });
            }
            foreach (var server in proDto.Servers)
            {
                project.Servers.Add(new Server() {
                    Id = server.Id,
                    Ip = server.Ip
                });
            }

            return project;
        }

        public ProjectDto ConvertEntityToDto(Project pro)
        {
            SkillAdapter skillAdapter = new SkillAdapter();
            ProjectDto proDto = new ProjectDto()
            {
                Id = pro.Id,
                Name = pro.Name,
                Skills = skillAdapter.ConvertListEntitiesToDtos(pro.Skills),
                EndDate = pro.EndDate.ToString("d"),
                StartDate = pro.StartDate.ToString("d"),
            };

            foreach (var memberDto in pro.Members)
            {
                proDto.Members.Add(new MemberDto()
                {
                    Id = memberDto.Id,
                    Name = memberDto.Name
                });
            }
            foreach (var serverDto in pro.Servers)
            {
                proDto.Servers.Add(new ServerDto()
                {
                    Id = serverDto.Id,
                    Ip = serverDto.Ip
                });
            }

            return proDto;
        }

        /// <summary>
        /// Add new Project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        public ProjectDto Add(ProjectDto projectDto)
        {
            return ConvertEntityToDto(_projectManager.Add(ConvertDtoToEntity(projectDto)));
        }

        /// <summary>
        /// Update exists project
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        public ProjectDto Update(ProjectDto projectDto)
        {
            return ConvertEntityToDto(_projectManager.Update(ConvertDtoToEntity(projectDto)));
        }

        /// <summary>
        /// Get Project by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ProjectDto GetProject(int projectId)
        {
            return ConvertEntityToDto(_projectManager.GetProject(projectId));
        }

        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>Return true if Project Exists and else</returns>
        public bool DeleteProject(int projectId)
        {
            return _projectManager.DeleteProject(projectId);
        }

        /// <summary>
        /// Get all Projects from DB
        /// </summary>
        /// <returns>Dto Project List</returns>
        public List<ProjectDto> GetAllProjects()
        {
            List<ProjectDto> projectDtos = new List<ProjectDto>();
            List<Project> projects = _projectManager.GetAllProjects();
            foreach (var project in projects)
            {
                projectDtos.Add(ConvertEntityToDto(project));
            }
            return projectDtos;
        }

        /// <summary>
        /// Add Skill for Exists Project
        /// </summary>
        /// <param name="projectId">Id of Project </param>
        /// <param name="skillId">Id of Skill</param>
        /// <returns></returns>
        public ProjectDto AddSkill(int projectId, int skillId)
        {
            return ConvertEntityToDto(_projectManager.AddSkill(projectId, skillId));
        }

        /// <summary>
        /// Add member for exists Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public ProjectDto AddMember(int projectId, int memberId)
        {
            return ConvertEntityToDto(_projectManager.AddMember(projectId, memberId));
        }

        /// <summary>
        /// Get all Skill of a Project by ProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>List of Skill</returns>
        public List<SkillDto> GetAllSkills(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public List<MemberDto> GetAllMembers(int projectId)
        {
            throw new System.NotImplementedException();
        }

    }
}
