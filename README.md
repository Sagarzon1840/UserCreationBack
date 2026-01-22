# UserCreation - Backend API con Arquitectura Hexagonal

## 📝 Descripción

Backend RESTful API desarrollada con .NET 8 y PostgreSQL, implementando arquitectura hexagonal (puertos y adaptadores) con autenticación JWT.

## 🏛️ Arquitectura

El proyecto sigue el patrón de **Arquitectura Hexagonal**:

```
UserCreation/
├── UserCreation.Domain/          # Entidades del dominio (núcleo)
│   └── Entities/
│       ├── Persona.cs
│       └── Usuario.cs
├── UserCreation.Application/     # Casos de uso y DTOs (puertos)
│   ├── DTOs/
│   │   ├── Auth/
│   │   ├── Personas/
│   │   └── Usuarios/
│   ├── Ports/Out/               # Interfaces (puertos de salida)
│   └── UseCases/                # Lógica de negocio
├── UserCreation.Infrastructure/  # Adaptadores (implementaciones)
│   ├── Persistence/
│   │   ├── Configurations/
│   │   ├── Scripts/
│   │   └── AppDbContext.cs
│   ├── Repositories/
│   └── Services/
└── UserCreation.Api/            # Adaptador de entrada (REST API)
    └── Controllers/
```

## ✨ Características

- 🏗️ **Arquitectura Hexagonal** (Ports & Adapters)
- 🔐 **Autenticación JWT** con claims personalizados
- 🐘 **PostgreSQL** como base de datos
- 📦 **Entity Framework Core 8**
- 🔒 **BCrypt** para hash de contraseñas
- 🔍 **LINQ Queries** con EF Core (type-safe, testeable)
- 📊 **Columnas calculadas** en BD
- 📚 **Swagger/OpenAPI** para documentación
- 📮 **Colección de Postman** incluida

## 🛠️ Tecnologías

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- PostgreSQL
- Npgsql
- BCrypt.Net-Next
- JWT Bearer Authentication
- Swagger/Swashbuckle

## 🚀 Endpoints

### Auth

- `POST /api/auth/login` - Login con credenciales (retorna JWT + SessionId)

### Personas (requiere JWT)

- `POST /api/personas` - Crear una persona
- `GET /api/personas/creadas?desde=...&hasta=...` - Consultar personas creadas

### Usuarios (requiere JWT)

- `POST /api/usuarios` - Crear un usuario

## 🗄️ Modelo de Datos

### Tabla: `personas`

- `identificador` (UUID, PK)
- `nombres` (varchar 100)
- `apellidos` (varchar 100)
- `numero_identificacion` (varchar 50)
- `email` (varchar 200, único)
- `tipo_identificacion` (varchar 50)
- `fecha_creacion` (timestamptz)
- `id_completo` (computed: tipo + numero)
- `nombre_completo` (computed: nombres + apellidos)

### Tabla: `usuarios`

- `identificador` (UUID, PK)
- `nombre_usuario` (varchar 100, único)
- `pass_hash` (varchar 500)
- `fecha_creacion` (timestamptz)
- `persona_id` (UUID, FK opcional)

## 🎫 JWT Claims

El token JWT incluye:

- `sub`: UserId (UUID)
- `unique_name`: Nombre de usuario
- `jti`: SessionId (UUID único por login)
- `iat`: Fecha de emisión
- `exp`: Fecha de expiración

## ⚙️ Configuración

### appsettings.json

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=usercreation_db;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "UserCreation",
    "Audience": "UserCreation",
    "ExpirationMinutes": 60
  }
}
```

## 📦 Instalación y Ejecución

### 1. Prerequisitos

- .NET 8 SDK
- PostgreSQL 12+
- Visual Studio 2022 / VS Code / Rider

### 2. Clonar el repositorio

```bash
git clone <repository-url>
cd UserCreationBack
```

### 3. Configurar cadena de conexión

Editar `UserCreation.Api/appsettings.Development.json` con tu conexión PostgreSQL.

### 4. Aplicar migraciones

```bash
dotnet ef database update --project UserCreation.Infrastructure --startup-project UserCreation.Api
```

**Esto ejecutará 3 migraciones**:

1. ✅ `InitialCreate` - Crea tablas personas y usuarios
2. ✅ `AddPersonasCreadasFunction` - Crea función SQL
3. ✅ `SeedAdminUser` - Crea usuario administrador

**Usuario creado automáticamente**:

- 👤 Usuario: `admin`
- 🔑 Password: `Admin123!`

✅ **La función SQL se crea automáticamente con las migraciones**

### 5. Ejecutar la aplicación

```bash
cd UserCreation.Api
dotnet run
```

## 📮 Probar con Postman

### Colección Completa Incluida

Hemos incluido una colección completa de Postman que funciona como:

- 🧪 Suite de pruebas automatizadas
- 📘 Documentación interactiva
- 🎯 Guía de uso de la API
- ✅ Tests automáticos con scripts

### Importar la Colección

1. Abre Postman
2. Click en **Import**
3. Arrastra el archivo `UserCreation.postman_collection.json`
4. ¡Listo! Todos los endpoints están configurados

### Uso Rápido

1. Ejecuta `Auth → Login - Obtener JWT`
2. El token se guarda automáticamente
3. Prueba cualquier endpoint protegido
4. ¡Los headers de autenticación se configuran solos! ✨

📚 **Guía completa**: Ver `POSTMAN_GUIDE.md` para instrucciones detalladas

## 💡 Uso de la API

### 1. Crear un usuario inicial (necesitarás hacerlo directamente en BD o crear un endpoint seed)

```sql
INSERT INTO usuarios (identificador, nombre_usuario, pass_hash, fecha_creacion)
VALUES (
  gen_random_uuid(),
  'admin',
  '$2a$12$...',  -- Hash de BCrypt para tu contraseña
  NOW()
);
```

O puedes usar este endpoint sin autenticación (temporalmente, quita el `[Authorize]` del POST en UsuariosController solo para crear el primer usuario).

### 2. Login

```bash
POST /api/auth/login
Content-Type: application/json

{
  "usuario": "admin",
  "pass": "tupassword"
}
```

Respuesta:

```json
{
  "accessToken": "eyJhbGc...",
  "expiresAt": "2026-01-21T02:31:00Z",
  "sessionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "userId": "123e4567-e89b-12d3-a456-426614174000",
  "usuario": "admin"
}
```

### 3. Crear una persona (con JWT)

```bash
POST /api/personas
Authorization: Bearer {tu-token-jwt}
Content-Type: application/json

{
  "nombres": "Juan",
  "apellidos": "Pérez",
  "numeroIdentificacion": "12345678",
  "email": "juan@example.com",
  "tipoIdentificacion": "CC"
}
```

### 4. Consultar personas creadas

```bash
GET /api/personas/creadas?desde=2026-01-01T00:00:00Z&hasta=2026-12-31T23:59:59Z
Authorization: Bearer {tu-token-jwt}
```

## 🧪 Testing con Swagger

1. Navegar a `/swagger`
2. Hacer POST a `/api/auth/login`
3. Copiar el `accessToken` de la respuesta
4. Hacer clic en "Authorize" (candado verde)
5. Ingresar: `Bearer {accessToken}`
6. Ahora puedes probar los endpoints protegidos

## 🔒 Seguridad

- ✅ Contraseñas hasheadas con **BCrypt** (work factor 12)
- ✅ JWT con expiración configurable
- ✅ HTTPS en producción
- ✅ CORS configurado
- ✅ Validación de modelos server-side
- ✅ Índices únicos para prevenir duplicados
