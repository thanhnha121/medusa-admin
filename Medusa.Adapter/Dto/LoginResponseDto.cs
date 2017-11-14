using System;
using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class LoginResponseDto
    {
        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public String Status { get; set; }
    }
}
