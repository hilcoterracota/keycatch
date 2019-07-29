
using System;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Core
{
    public partial class SampekeyUserAccountRequest: IdentityUser
    {
        public string Password { get; set; }
    }
}