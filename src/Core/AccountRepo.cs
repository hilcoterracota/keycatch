using System;
using keycatch.Interfaces;
using Sampekey.Contex;
using Sampekey.Model;
using Novell.Directory.Ldap;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace keycatch.Core
{
    public class AccountRepo : IAccountRepo
    {
        private readonly SampekeyDbContex context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountRepo(
            SampekeyDbContex _context,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public Boolean LoginCnsfWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest)
        {
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect("cnsf.gob.mx", 389);
                    connection.Bind($"{userAccountRequest.UserName}@cnsf.gob.mx", userAccountRequest.Password);
                    return connection.Bound;
                }
            }
            catch
            {
                return false;
            }
        }

        public Task<SignInResult> LoginCnsfWithSampeKey(SampekeyUserAccountRequest userAccountRequest)
        {
            return signInManager.PasswordSignInAsync(userAccountRequest.UserName, userAccountRequest.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task UpdateForcePaswordAsync(SampekeyUserAccountRequest userAccountRequest)
        {
            await userManager.RemovePasswordAsync(userAccountRequest);
            await userManager.AddPasswordAsync(userAccountRequest, userAccountRequest.Password);
        }

    }
}