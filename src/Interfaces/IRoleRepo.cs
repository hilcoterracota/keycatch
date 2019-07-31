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
        Task<Role> FindRoleByName(Role role);
        Task<IdentityResult> AddClaimAsyncToRole(Role role, Claim claim);
    }
}

