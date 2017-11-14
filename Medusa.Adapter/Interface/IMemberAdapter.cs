using System.Collections.Generic;
using Medusa.Adapter.Dto;
using MemberDto = Medusa.Adapter.Dto.MemberDto;
using MemberSkillDto = Medusa.Adapter.Dto.MemberSkillDto;

namespace Medusa.Adapter.Interface
{
    public interface IMemberAdapter
    {
        MemberDto GetMember(int id);
        MemberDto UpdateMember(MemberDto memberDto);
        MemberDto AddMember(MemberDto memberDto);
        bool DeleteMember(int id);
        List<MemberDto> GetAllMembers();
        MemberSkillDto InsertSkill(MemberSkillDto msDto);
        MemberDto AssignToProject(int memberId, int projectId);
        MemberDto AddProject(string memberId, int projectId);
        List<ProjectDto> GetAllProjects(string memberId);
    }
}
