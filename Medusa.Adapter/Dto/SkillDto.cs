using System;
using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class SkillDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String Descriptions { get; set; }
    }
}
