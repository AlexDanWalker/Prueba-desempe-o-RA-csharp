# Proyecto: TalentPlus S.A.S. – Sistema de Gestión de Empleados

## Pasos para correr la solución

Clonar el repositorio:


```bash
git clone [<link_del_repositorio>](https://github.com/AlexDanWalker/Prueba-desempe-o-RA-csharp/new/develop)
cd TalentPlus/src/TalentPlus.Api
```

Configurar la base de datos en appsettings.json:

```bash
"ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=talentplus;user=root;password=1234;"
}
```

Levantar la solución:

```bash
dotnet restore
dotnet build
dotnet run --project TalentPlus.Api
```

Abrir en el navegador:

https://localhost:5001/


Swagger estará en la raíz (/), ya listo para probar endpoints.

## Configuración de variables de entorno

Jwt:Key → Clave secreta para JWT.

Jwt:Issuer → Issuer de token.

Jwt:Audience → Audience de token.

EmailSettings:Host → Servidor SMTP (ej. smtp.gmail.com)

EmailSettings:Port → Puerto SMTP (ej. 587)

EmailSettings:User → Email de envío

EmailSettings:Password → Contraseña de email

EmailSettings:EnableSsl → true/false

Todo se puede configurar en appsettings.json o variables de entorno. (En este caso por falta de tiempo esta en el appsettings aunque se que es mala practica)

## Credenciales de acceso inicial

Email: TalentPlus@gmail.com

Password: Admin123!

Rol: Administrator

Repositorio

Link: https://github.com/AlexDanWalker/Prueba-desempe-o-RA-csharp
