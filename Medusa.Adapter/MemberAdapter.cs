using System;
using System.Collections.Generic;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;
using Medusa.Business;
using Medusa.Business.Entities;

namespace Medusa.Adapter
{
    public class MemberAdapter : IMemberAdapter
    {
        private readonly Business.MemberManager _memberManager;
        private readonly MemberSkillAdapter _memberSkillAdapter;
        private readonly ProjectAdapter _projectAdapter;

        public MemberAdapter()
        {
            _projectAdapter = new ProjectAdapter();
            _memberSkillAdapter = new MemberSkillAdapter();
            _memberManager = new Business.MemberManager();
        }
        /// <summary>
        /// Convert data transfer object to entity
        /// </summary>
        /// <param name="memDto"></param>
        /// <returns></returns>
        public Member ConvertDtoToEntity(MemberDto memDto)
        {
            MemberSkillAdapter memberSkillAdapter = new MemberSkillAdapter();
            Member member = new Member() { 
                Id = memDto.Id,
                Name = memDto.Name,
                Skills = memberSkillAdapter.ConvertListDtosToEntities(memDto.Skills),
                Password = memDto.Password,
                UserName = memDto.UserName,
                Address = memDto.Address,
                Birthday = DateTime.Parse(memDto.Birthday),
                Email = memDto.Email,
                Type = memDto.Type,
                Picture = memDto.Picture
            };
            foreach (var project in memDto.Projects)
            {
                member.Projects.Add(new Project()
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }
            return member;
        }

        /// <summary>
        /// Convert entity to data transfer object
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public MemberDto ConvertEntityToDto(Member mem)
        {
            MemberSkillAdapter msk = new MemberSkillAdapter();
            MemberDto memDto = new MemberDto()
            {
                Id = mem.Id,
                Name = mem.Name,
                Skills = msk.ConvertListEntitiesToDtos(mem.Skills),
                Password = mem.Password,
                UserName = mem.UserName,
                Address = mem.Address,
                Birthday = mem.Birthday.ToString("d"),
                Email = mem.Email,
                Type = mem.Type,
                Picture = mem.Picture
            };
            foreach (var project in mem.Projects)
            {
                memDto.Projects.Add(new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }

            return memDto;
        }

        /// <summary>
        /// Get member by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MemberDto GetMember(int id)
        {
            return ConvertEntityToDto(_memberManager.GetMember(id));
        }
        /// <summary>
        /// Update a member 
        /// </summary>
        /// <param name="memberDto"></param>
        /// <returns></returns>
        public MemberDto UpdateMember(MemberDto memberDto)
        {
            return ConvertEntityToDto(_memberManager.UpdateMember(ConvertDtoToEntity(memberDto)));
        }
        /// <summary>
        /// add a member
        /// </summary>
        /// <param name="memberDto"></param>
        /// <returns></returns>
        public MemberDto AddMember(MemberDto memberDto)
        {
            return ConvertEntityToDto(_memberManager.AddMember(ConvertDtoToEntity(memberDto)));
        }
        /// <summary>
        /// Delete one member by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMember(int id)
        {
            return _memberManager.DeleteMember(id);
        }
        /// <summary>
        /// Get all members 
        /// </summary>
        /// <returns></returns>
        public List<MemberDto> GetAllMembers()
        {
            List<MemberDto> memberDtos = new List<MemberDto>();
            List<Member> members = _memberManager.GetAllMembers();
            foreach (var member in members)
            {
                memberDtos.Add(ConvertEntityToDto(member));
            }
            return memberDtos;
        }
        /// <summary>
        /// Insert skill for the member
        /// </summary>
        /// <param name="msDto"></param>
        /// <returns></returns>
        public MemberSkillDto InsertSkill(MemberSkillDto msDto)
        {
            return _memberSkillAdapter.ConvertEntityToDto(_memberManager.InsertSkill(_memberSkillAdapter.ConvertDtoToEntity(msDto)));
        }
        /// <summary>
        /// Assign a member to project
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public MemberDto AssignToProject(int memberId, int projectId)
        {
            return ConvertEntityToDto(_memberManager.AssignToProject(memberId, projectId));
        }
        /// <summary>
        /// Add a project into exists  
        /// </summary>
        /// <param name="memberId">Id of member</param>
        /// <param name="projectId">ID of Project that want to add</param>
        /// <returns></returns>
        public MemberDto AddProject(string memberId, int projectId)
        {
            return ConvertEntityToDto(_memberManager.AddProject(memberId, projectId));
        }

        /// <summary>
        /// Get All Projects of a member by memberId
        /// </summary>
        /// <param name="serverId">Id of member</param>
        /// <returns>List of Projects</returns>
        public List<ProjectDto> GetAllProjects(string serverId)
        {
            List<ProjectDto> projectDtos = new List<ProjectDto>();
            List<Project> projects = new ProjectManager().GetAllProjects();
            foreach (var project in projects)
            {
                projectDtos.Add(_projectAdapter.ConvertEntityToDto(project));
            }
            return projectDtos;
        }

    }
}
