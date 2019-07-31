using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keycatch.Interfaces;
using Sampekey.Model;
using Sampekey.Contex;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Core
{
    public class UserRepo : IUserRepo
    {
        private readonly SampekeyDbContex context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserRepo(
            SampekeyDbContex _context,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await context.User
                .Include(i => i.Status)
                .Include(i => i.UserRoles)
                    .ThenInclude(i => i.Role)
                        .ThenInclude(i => i.RoleClaims)
                .Include(i => i.UserTokens)
            .ToListAsync();
        }

        public Task<User> FindUserByUserName(SampekeyUserAccountRequest userAccountRequest){
            return userManager.FindByNameAsync(userAccountRequest.UserName);
        }

        public async Task<bool> CreateUser(SampekeyUserAccountRequest userAccountRequest)
        {
            try
            {
                await Task.WhenAll(
                    userManager.CreateAsync(userAccountRequest, userAccountRequest.Password),
                    userManager.CreateSecurityTokenAsync(userAccountRequest)
                );
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}