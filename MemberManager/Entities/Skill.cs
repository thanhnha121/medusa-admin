using System;
using System.ComponentModel.DataAnnotations;

namespace Medusa.Business.Entities
{
    public class Skill
    {
        public int Id { get; set; }

        [MinLength(2), MaxLength(50)]
        public String Name { get; set; }

        [MaxLength(200)]
        public String Descriptions { get; set; }
    }
}
                                                                                                          