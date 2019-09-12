# Sistema de autenticación dual (directorio activo, sistema propio) y generador de JWT

## Contenido:
- [Requerimientos](#requerimientos)
- [Implementación IIS](#implementacioniis)
- [Implementación Docker](#implementaciondocker)

## Requerimientos:

### Variables de entorno necesarias:
- SQL_SAMPEKEY : Cadena de conexión SQL donde se encuentre alojado la base de datos.
- SAMPEKEY_SECRET_KEY: Cadena string utilizada para la encriptación de tokens.
- AD_DDOMAIN : Nombre de dominio para la autenticación en directorio activo.
- AD_PORT : puerto para la autenticación en directorio activo.
- MAX_EXPIRATION_HOURS : Numero entero que representa la cantidad de horas para la vida de los tokens.
- BIND_DN
- LDAP_FILTER


Nota: Si se usa el sistema gateway las variables de entorno deben coincidir

### Para implementación en IIS:
- [net core hosting >= 2.2.6](https://dotnet.microsoft.com/download)

## Implementación Docker: