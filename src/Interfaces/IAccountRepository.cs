
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using keycatch.Core;

namespace keycatch.interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAccount(SampekeyUserAccountRequest model);
        Task<SignInResult> LoginAccount(SampekeyUserAccountRequest model);
    }
}