using System;
using System.Threading.Tasks;
using keycatch.interfaces;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Core
{
    public class AccountRepository : IAccountRepository
    {
        public Task<IdentityResult> CreateAccount(UserAccountRequest userAccountRequest)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> LoginAccount(UserAccountRequest userAccountRequest)
        {
            throw new NotImplementedException();
        }
    }
}