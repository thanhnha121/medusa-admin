using System.Runtime.Serialization;

namespace Medusa.Adapter.Dto
{
    [DataContract]
    public class AccountDto
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
