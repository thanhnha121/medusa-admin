using System.Collections.Generic;
using Medusa.Adapter.Interface;
using Medusa.Business;
using Medusa.Business.Entities;
using SkillDto = Medusa.Adapter.Dto.SkillDto;

namespace Medusa.Adapter
{
    public class SkillAdapter : ISkillAdapter
    {
        private readonly SkillManager _skillManager;

        public SkillAdapter()
        {
            _skillManager = new SkillManager();
        }

        public Skill ConvertDtoToEntity(SkillDto skillDto)
        {
            return new Skill()
            {
                Id = skillDto.Id,
                Name = skillDto.Name,
                Descriptions = skillDto.Descriptions
            };
        }

        public SkillDto ConvertEntityToDto(Skill skill)
        {
            return new SkillDto()
            {
                Id = skill.Id,
                Name = skill.Name,
                Descriptions = skill.Descriptions
            };
        }

        /// <summary>
        /// Check exists Skill
        /// </summary>
        /// <param name="name">Name of a skill</param>
        /// <returns>Return true if had a skill name found </returns>
        public bool Exists(string name)
        {
            return _skillManager.Exists(name);
        }

        /// <summary>
        /// Add new Skill
        /// </summary>
        /// <param name="skillDto"></param>
        /// <returns></returns>
        public SkillDto AddSkill(SkillDto skillDto)
        {
            return ConvertEntityToDto(_skillManager.AddSkill(ConvertDtoToEntity(skillDto)));
        }

        /// <summary>
        /// Update Exists Skill
        /// </summary>
        /// <param name="newSkillDto"></param>
        /// <returns></returns>
        public SkillDto UpdateSkill(SkillDto newSkillDto)
        {
            return ConvertEntityToDto(_skillManager.UpdateSkill(ConvertDtoToEntity(newSkillDto)));
        }

        /// <summary>
        /// Get All Skills from DB
        /// </summary>
        /// <returns>List of Skills</returns>
        public List<SkillDto> GetAllSkill()
        {
            List<Skill> skills = _skillManager.GetAllSkill();
            List<SkillDto> skillDtos = new List<SkillDto>();
            foreach (var skill in skills)
            {
                skillDtos.Add(ConvertEntityToDto(skill));
            }
            return skillDtos;
        }

        /// <summary>
        /// Get a skill by SkillId
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public SkillDto GetSkill(int skillId)
        {
            return ConvertEntityToDto(_skillManager.GetSkill(skillId));
        }

        /// <summary>
        /// Delete a skill by SkillId
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns>Return true if Skill found and success delete, 
        /// return false if skill not found</returns>
        public bool DeleteSkill(int skillId)
        {
            return _skillManager.DeleteSkill(skillId);
        }

        public List<SkillDto> ConvertListEntitiesToDtos(List<Skill> skills)
        {
            List<SkillDto> skillDtos = new List<SkillDto>();

            foreach (var skill in skills)
            {
                SkillDto skillDto = ConvertEntityToDto(skill);
                skillDtos.Add(skillDto);
            }

            return skillDtos;
        }

        public List<Skill> ConvertListDtosToEntities(List<SkillDto> skillDtos)
        {
            List<Skill> skills= new List<Skill>();

            foreach (var skillDto in skillDtos)
            {
                Skill skill= ConvertDtoToEntity(skillDto);
                skills.Add(skill);
            }


            return skills;
        }


    }
}
