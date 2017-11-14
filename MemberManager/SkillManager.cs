using System.Collections.Generic;
using System.Linq;
using Medusa.Business.Entities;

namespace Medusa.Business
{

    public class SkillManager
    {
        private readonly MedusaManagerDbContext _ctx;

        public SkillManager()
        {
            _ctx = new MedusaManagerDbContext();
        }

        /// <summary>
        /// Check exists Skill
        /// </summary>
        /// <param name="name">Name of a skill</param>
        /// <returns>Return true if had a skill name found </returns>
        public bool Exists(string name)
        {
            var skill = _ctx.Skills.FirstOrDefault(x => x.Name.Equals(name));
            if (skill != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add new Skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public Skill AddSkill(Skill skill)
        {
            if (!Exists(skill.Name))
            {
                Skill rsSkill = _ctx.Skills.Add(skill);
                _ctx.SaveChanges();
                return rsSkill;
            }
            return null;
        }

        /// <summary>
        /// Update Exists Skill
        /// </summary>
        /// <param name="newSkill"></param>
        /// <returns></returns>
        public Skill UpdateSkill(Skill newSkill)
        {
            Skill skill = _ctx.Skills.Find(newSkill.Id);
            if (skill != null)
            {
                skill.Name = newSkill.Name;
                skill.Descriptions = newSkill.Descriptions;
                _ctx.SaveChanges();
                return skill;
            }
            return null;
        }

        /// <summary>
        /// Get All Skills from DB
        /// </summary>
        /// <returns>List of Skills</returns>
        public List<Skill> GetAllSkill()
        {
            List<Skill> skills = new List<Skill>(_ctx.Set<Skill>());
            return skills;
        }

        /// <summary>
        /// Get a skill by SkillId
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public bool DeleteSkill(int skillId)
        {

            Skill skill = _ctx.Skills.Find(skillId);
            if (skill != null)
            {
                _ctx.Skills.Remove(skill);
                _ctx.SaveChanges();
            }
            return skill != null;
        }

        /// <summary>
        /// Delete a skill by SkillId
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns>Return true if Skill found and success delete, 
        /// return false if skill not found</returns>
        public Skill GetSkill(int skillId)
        {
            return _ctx.Skills.Find(skillId);
        }
    }
}
