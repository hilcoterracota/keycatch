using System;
using keycatch.Interfaces;

namespace keycatch.Core
{
    public class SystemRepo : ISystemRepo
    {
        public Object GetUnauthorizedMenssageFromCnsfActiveDirectory()
        {
            return new
            {
                title = "Error de autenticación",
                message = "No se ha podido autentificar ante el directorio activo de la CNSF. Por favor compruebe si las credenciales son correctas."
            };
        }

        public Object GetUnauthorizedMenssageFromSampeKey()
        {
            return new
            {
                title = "Error de autenticación",
                message = "Su cuenta no esta dada de alta en el sistema. Por favor contacte al administrador para mayor imformación."
            };
        }
    }
}