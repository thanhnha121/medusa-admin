using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medusa.Business.Entities
{
    /// <summary>
    /// MemberSkill give extra YearsOfExp info
    /// </summary>
    public class MemberSkill
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        public Member Member { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        [Required]
        public int YearsOfExp { get; set; }
    }
}
