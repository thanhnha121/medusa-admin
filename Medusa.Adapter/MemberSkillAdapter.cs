using System.Collections.Generic;
using Medusa.Business.Entities;
using Medusa.Adapter.Dto;

namespace Medusa.Adapter
{
    public class MemberSkillAdapter 
    {
        public MemberSkill ConvertDtoToEntity(MemberSkillDto msDto)
        {
            SkillAdapter skillAdapter = new SkillAdapter();
            MemberSkill ms = new MemberSkill()
            {
                Skill = skillAdapter.ConvertDtoToEntity(msDto.Skill),
                YearsOfExp = msDto.YearsOfExp
            };
            ms.Member = new Member()
            {
                Id = msDto.Member.Id,
                Name = msDto.Member.Name
            };
            return ms;
        }

        public MemberSkillDto ConvertEntityToDto(MemberSkill ms)
        {
            SkillAdapter skillAdapter = new SkillAdapter();
            MemberSkillDto msDto = new MemberSkillDto()
            {
                Skill = skillAdapter.ConvertEntityToDto(ms.Skill),
                YearsOfExp = ms.YearsOfExp
            };
            msDto.Member = new MemberDto()
            {
                Id = ms.Member.Id,
                Name = ms.Member.Name
            };
            return msDto;
        }

        public List<MemberSkillDto> ConvertListEntitiesToDtos(List<MemberSkill> memberSkills)
        {
            List<MemberSkillDto> memberSkillDtos = new List<MemberSkillDto>();

            foreach (var ms in memberSkills)
            {
                MemberSkillDto msDto = ConvertEntityToDto(ms);
                memberSkillDtos.Add(msDto);
            }

            return memberSkillDtos;
        }

        public List<MemberSkill> ConvertListDtosToEntities(List<MemberSkillDto> msDtos)
        {
            List<MemberSkill> mss = new List<MemberSkill>();

            foreach (var msDto in msDtos)
            {
                MemberSkill ms  = ConvertDtoToEntity(msDto);
                mss.Add(ms);
            }


            return mss;
        }

    }
}
