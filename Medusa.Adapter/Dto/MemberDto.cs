using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class MemberDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public String Address { get; set; }

        [DataMember]
        public String Birthday { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public String UserName { get; set; }

        [DataMember]
        public String Password { get; set; }

        [DataMember]
        public String Picture { get; set; }

        [DataMember]
        public List<MemberSkillDto> Skills { get; set; }

        [DataMember]
        public List<ProjectDto> Projects { get; set; }

        public MemberDto()
        {
            Skills = new List<MemberSkillDto>();
            Projects = new List<ProjectDto>();
        }
    }
}
