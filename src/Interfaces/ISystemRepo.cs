using System;

namespace keycatch.Interfaces
{
    public interface ISystemRepo
    {
        Object GetUnauthorizedMenssageFromActiveDirectory();
        Object GetUnauthorizedMenssageFromSampeKey();
        Object GetUnauthorizedMenssage();
        Object GetValidationProblemMenssage();
    }
}

