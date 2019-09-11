using System;
using keycatch.Interfaces;
using Sampekey.Contex;
using Sampekey.Model;
using Novell.Directory.Ldap;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public Boolean LoginWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest)
        {
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(Environment.GetEnvironmentVariable("AD_DDOMAIN"), int.Parse(Environment.GetEnvironmentVariable("AD_PORT")));
                    connection.Bind($"{userAccountRequest.UserName}@{Environment.GetEnvironmentVariable("AD_DDOMAIN")}", userAccountRequest.Password);
                    return connection.Bound;
                }
            }
            catch
            {
                return false;
            }
        }

        public HashSet<string> GetUsersWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest)
        {
            var users = new HashSet<string>();

            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(Environment.GetEnvironmentVariable("AD_DDOMAIN"), int.Parse(Environment.GetEnvironmentVariable("AD_PORT")));
                    connection.Bind($"{userAccountRequest.UserName}@{Environment.GetEnvironmentVariable("AD_DDOMAIN")}", userAccountRequest.Password);
                    LdapSearchResults searchResults = connection.Search(
                    Environment.GetEnvironmentVariable("BIND_DN"),
                    LdapConnection.SCOPE_SUB,
                    Environment.GetEnvironmentVariable("LDAP_FILTER"),
                    null,
                    false
                    );
                    while (searchResults.hasMore())
                    {

                        LdapEntry nextEntry = null;

                        nextEntry = searchResults.next();
                        nextEntry.getAttributeSet();
                        var attr = nextEntry.getAttribute("mail");

                        if (attr == null)
                        {
                            users.Add(nextEntry.getAttribute("distinguishedName").StringValue);
                        }
                        else
                        {
                            users.Add(nextEntry.getAttribute("mail").StringValue);
                        }
                    }
                    return users;
                }
            }
            catch
            {
                return users;
            }
        }

        public Task<SignInResult> LoginWithSampeKey(SampekeyUserAccountRequest userAccountRequest)
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