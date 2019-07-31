using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sampekey.Model;
using Sampekey.Contex;

namespace keycatch.Interfaces
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> FindUserByUserName(SampekeyUserAccountRequest userAccountRequest);
    }
}

