using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class ServerDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Ip { get; set; }

        [DataMember]
        public string SystemOS { get; set; }

        [DataMember]
        public string Descriptons { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public List<ProjectDto> Projects { get; set; }

        [DataMember]
        public string Status { get; set; }

        public ServerDto()
        {
            Projects = new List<ProjectDto>();
        }
    }
}
