using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sampekey.Model;
using System.Security.Claims;
using System.Collections.Generic;

namespace keycatch.Interfaces
{
    public interface IRoleRepo
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<IdentityResult> CreateRole(Role role);
        Task<Role> FindRoleByName(string roleName);
        Task<Role> FindRoleById(string roleId);
        Task<IdentityResult> AddClaimAsyncToRole(Role role, Claim claim);
        Task<IList<Claim>> GetClaimsFromRole(Role role);
    }
}

