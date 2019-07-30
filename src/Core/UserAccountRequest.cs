
using System;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Core
{
    public partial class UserAccountRequest: IdentityUser
    {
        public string Password { get; set; }
    }
}