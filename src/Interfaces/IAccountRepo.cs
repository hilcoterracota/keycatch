using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sampekey.Contex;
using Microsoft.AspNetCore.Identity;

namespace keycatch.Interfaces
{
    public interface IAccountRepo
    {
        Boolean LoginCnsfWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest);
        Task<SignInResult> LoginCnsfWithSampeKey(SampekeyUserAccountRequest userAccountRequest);
        Task UpdateForcePaswordAsync(SampekeyUserAccountRequest userAccountRequest);
    }
}

