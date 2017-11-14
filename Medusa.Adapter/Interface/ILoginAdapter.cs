using AccountDto = Medusa.Adapter.Dto.AccountDto;
using LoginResponseDto = Medusa.Adapter.Dto.LoginResponseDto;

namespace Medusa.Adapter.Interface
{
    public interface ILoginAdapter
    {
        LoginResponseDto LogIn(AccountDto accountDto);
        bool LogOut();
    }
}
