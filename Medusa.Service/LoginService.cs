using System.ServiceModel;
using System.ServiceModel.Web;
using Medusa.Adapter;
using Medusa.Adapter.Dto;
using Medusa.Adapter.Interface;

namespace Medusa.Service
{
    [ServiceContract]
    public class LoginService
    {
        private readonly ILoginAdapter _loginAdapter;
        public LoginService()
        {
            _loginAdapter = new LoginAdapter();
        }

        /// <summary>
        ///Operation Contract Serivce for member login
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = ""
            )]
        public LoginResponseDto Login(AccountDto acc)
        {
            return _loginAdapter.LogIn(acc);
        }

    }
}
