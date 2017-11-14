using System.Linq;
using Medusa.Business.Entities;

namespace Medusa.Business
{
    public class LoginManager
    {
        private readonly MedusaManagerDbContext _ctx;

        public LoginManager()
        {
            _ctx = new MedusaManagerDbContext();
        }
        /// <summary>
        /// Check Exits account of member 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public Member LogIn(Member member)
        {
            Member mem = _ctx.Members.FirstOrDefault(x => x.UserName.Equals(member.UserName)
                                                          && x.Password.Equals(member.Password));
            return mem;
        }
        /// <summary>
        /// Member log out
        /// </summary>
        /// <returns></returns>
        public bool LogOut()
        {
            return true;
        }
    }
}
