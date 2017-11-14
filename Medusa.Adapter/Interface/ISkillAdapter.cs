using System.Collections.Generic;
using SkillDto = Medusa.Adapter.Dto.SkillDto;

namespace Medusa.Adapter.Interface
{
    public interface ISkillAdapter
    {
        bool Exists(string name);
        SkillDto AddSkill(SkillDto skillDto);
        SkillDto UpdateSkill(SkillDto newSkillDto);
        List<SkillDto> GetAllSkill();
        SkillDto GetSkill(int skillId);
        bool DeleteSkill(int skillId);
    }
}
