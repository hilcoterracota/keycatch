using System;

namespace keycatch.Interfaces
{
    public interface ISystemRepo
    {
        Object GetUnauthorizedMenssageFromCnsfActiveDirectory();
        Object GetUnauthorizedMenssageFromSampeKey();

    }
}

