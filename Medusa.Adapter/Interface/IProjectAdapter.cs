using System.Collections.Generic;
using MemberDto = Medusa.Adapter.Dto.MemberDto;
using ProjectDto = Medusa.Adapter.Dto.ProjectDto;
using SkillDto = Medusa.Adapter.Dto.SkillDto;

namespace Medusa.Adapter.Interface
{
    public interface IProjectAdapter
    {
        ProjectDto Add(ProjectDto projectDto);
        ProjectDto Update(ProjectDto projectDto);
        ProjectDto GetProject(int projectId);
        bool DeleteProject(int projectId);
        List<ProjectDto> GetAllProjects();
        ProjectDto AddSkill(int projectId, int skillId);
        ProjectDto AddMember(int projectId, int memberId);
        List<SkillDto> GetAllSkills(int projectId);
        List<MemberDto> GetAllMembers(int projectId);

    }
}
