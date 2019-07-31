using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keycatch.Interfaces;
using Sampekey.Model;
using Sampekey.Contex;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
            .ToListAsync();
        }

        public Task<User> FindUserByUserName(SampekeyUserAccountRequest userAccountRequest){
            return userManager.FindByNameAsync(userAccountRequest.UserName);
        }

        public Task<IdentityResult> CreateUser(SampekeyUserAccountRequest userAccountRequest)
        {
            return userManager.CreateAsync(userAccountRequest, userAccountRequest.Password);
        }

        public Task<IdentityResult> AddDefaultRoleToUser(User user)
        {
            return userManager.AddToRoleAsync(user, "default");
        }

        public Task<IList<string>> GetRolesFromUser(User user)
        {
            return userManager.GetRolesAsync(user);
        }

        public Task<IList<Claim>> GetClaimsFromUser(User user)
        {
            return userManager.GetClaimsAsync(user);
        }

    }
}