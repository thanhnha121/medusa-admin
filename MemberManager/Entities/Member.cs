using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medusa.Business.Entities
{
    public class Member
    {
        public int Id { get; set; }

        [MinLength(2), MaxLength(50)]
        public String Name { get; set; }

        [Required]
        public int Type { get; set; }

        [MinLength(5), MaxLength(100)]
        public String Address { get; set; }

        public DateTime Birthday { get; set; }

        [MinLength(10), MaxLength(50)]
        public String Email { get; set; }

        [MinLength(6), MaxLength(20)]
        public String UserName { get; set; }

        [MinLength(4), MaxLength(20)]
        public String Password { get; set; }

        public String Picture { get; set; }

        public List<MemberSkill> Skills { get; set; }

        public List<Project> Projects { get; set; }

        public Member()
        {
            Skills = new List<MemberSkill>();
            Projects = new List<Project>();
        }
    }
}
