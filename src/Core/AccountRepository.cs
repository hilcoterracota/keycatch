using System;
using System.Threading.Tasks;
using keycatch.interfaces;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Core
{
    public class AccountRepository : IAccountRepository
    {
        public Task<IdentityResult> CreateAccount(SampekeyUserAccountRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> LoginAccount(SampekeyUserAccountRequest model)
        {
            throw new NotImplementedException();
        }
    }
}