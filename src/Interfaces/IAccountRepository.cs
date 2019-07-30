
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using keycatch.Core;

namespace keycatch.interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAccount(UserAccountRequest userAccountRequest);
        Task<SignInResult> LoginAccount(UserAccountRequest userAccountRequest);
    }
}