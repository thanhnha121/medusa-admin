using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medusa.Business.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [MaxLength(100), MinLength(5)]
        public String Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Member> Members { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Server> Servers { get; set; }

        public Project()
        {
            Servers = new List<Server>();
            Skills = new List<Skill>();
            Members = new List<Member>();
        }
    }
}
