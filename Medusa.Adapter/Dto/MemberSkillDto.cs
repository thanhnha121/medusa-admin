using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class MemberSkillDto
    {
        [DataMember]
        public MemberDto Member { get; set; }

        [DataMember]
        public SkillDto Skill { get; set; }

        [DataMember]
        public int YearsOfExp { get; set; }
    }
}
