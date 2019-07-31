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
    public class RoleRepo : IRoleRepo
    {
        private readonly SampekeyDbContex context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public RoleRepo(
            SampekeyDbContex _context,
            UserManager<User> _userManager,
            RoleManager<Role> _roleManager)
        {
            context = _context;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await context.Role.ToListAsync();
        }
        public Task<Role> FindRoleByName(Role role)
        {
            return roleManager.FindByNameAsync(role.Name);
        }
        public Task<IdentityResult> CreateRole(Role role)
        {
            return roleManager.CreateAsync(role);
        }

        public Task<IList<Claim>> GetClaimsFromRole(Role role)
        {
            return roleManager.GetClaimsAsync(role);
        }

        public Task<IdentityResult> AddClaimAsyncToRole(Role role, Claim claim)
        {
            return roleManager.AddClaimAsync(role, claim);
        }

    }
}