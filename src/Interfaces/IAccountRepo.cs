using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sampekey.Contex;

namespace keycatch.Interfaces
{
    public interface IAccountRepo
    {
        Boolean LoginCnsfWithActiveDirectory(SampekeyUserAccountRequest userAccountRequest);
        Task<Boolean> CreateUser(SampekeyUserAccountRequest userAccountRequest);
        Task<Boolean> LoginCnsfWithSampeKey(SampekeyUserAccountRequest userAccountRequest);
    }
}

