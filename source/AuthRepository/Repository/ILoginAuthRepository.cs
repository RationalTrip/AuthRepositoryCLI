using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthRepository
{
    public interface ILoginAuthRepository
    {
        Task<LoginAuth> CreateUserAsync(LoginAuth userAuth);
        Task<LoginAuth> SignInUserAsync(LoginAuth userToAuth);
        Task<bool> IsLoginExistsAsync(string login);
    }
}
