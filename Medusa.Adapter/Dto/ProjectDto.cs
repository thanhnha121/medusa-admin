using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class ProjectDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public List<MemberDto> Members { get; set; }

        [DataMember]
        public List<SkillDto> Skills { get; set; }

        [DataMember]
        public List<ServerDto> Servers { get; set; }

        public ProjectDto()
        {
            Skills = new List<SkillDto>();
            Servers = new List<ServerDto>();
            Members = new List<MemberDto>();
        }
    }

}
