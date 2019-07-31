using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sampekey.Contex;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Interfaces
{
    public interface IAccountRepo
    {
        Boolean LoginWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest);
        Task<SignInResult> LoginWithSampeKey(SampekeyUserAccountRequest userAccountRequest);
        Task UpdateForcePaswordAsync(SampekeyUserAccountRequest userAccountRequest);
    }
}

