using System;
using Medusa.Adapter.Interface;
using Medusa.Business;
using Medusa.Business.Entities;
using Medusa.Adapter.Dto;

namespace Medusa.Adapter
{
    public class LoginAdapter :ILoginAdapter
    {
        private readonly LoginManager _loginManager;

        public LoginAdapter()
        {
            _loginManager = new LoginManager();
        }

        /// <summary>
        /// Check a account
        /// </summary>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        public LoginResponseDto LogIn(AccountDto accountDto)
        {
            return ConvertEntityToDto(_loginManager.LogIn(ConvertDtoToEntity(accountDto)));
        }

        /// <summary>
        /// Member log out
        /// </summary>
        /// <returns></returns>
        public bool LogOut()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert entity to Data Transfer Object
        /// </summary>
        /// <param name="mem"></param>
        /// <returns></returns>
        public LoginResponseDto ConvertEntityToDto(Member mem)
        {
            LoginResponseDto result = new LoginResponseDto
            {
                Status = "Login Failed!",
                Time = DateTime.Now
            };
            if (mem != null)
            {
                result.Status = "Login Success!";
            }
            return result;
        }

        /// <summary>
        /// Convert Data Transfer Object to Entity
        /// </summary>
        /// <param name="accDto"></param>
        /// <returns></returns>
        public Member ConvertDtoToEntity(AccountDto accDto)
        {
            return new Member()
            {
               UserName = accDto.UserName,
               Password = accDto.Password
            };
        }
    }
}
