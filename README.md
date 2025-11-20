# LabLINQ - API REST con .NET 8.0

Proyecto ASP.NET Core Web API con Entity Framework Core y MySQL.

## 🚀 Despliegue

### 🚀 Desplegar en Vercel (Solución con Proxy)

Aunque Vercel NO soporta .NET directamente, hemos configurado una **solución con proxy serverless** que te permite usar Vercel.

#### ¿Cómo funciona?

1. **Despliega tu API .NET en Railway/Render** (gratis y fácil):
   - Railway: https://railway.app
   - Render: https://render.com
   - Azure: https://portal.azure.com

2. **Obtén la URL de tu API .NET desplegada**:
   ```
   https://tu-api-dotnet.railway.app
   ```

3. **Configura la variable de entorno en Vercel**:
   - Ve a Vercel Dashboard → Tu Proyecto → Settings → Environment Variables
   - Agrega: `API_NET_URL` = `https://tu-api-dotnet.railway.app`
   - Aplica a Production, Preview y Development

4. **Redespliega en Vercel**:
   - El proxy serverless (`api/index.js`) redirigirá todas las peticiones a tu API .NET

#### Ventajas:
- ✅ Funciona con Vercel
- ✅ Mantienes las ventajas de Vercel (CDN, edge functions)
- ✅ Tu API .NET corre en una plataforma nativa (eficiente)

#### Pasos detallados:

**Paso 1: Desplegar API .NET en Railway**
1. Ve a https://railway.app
2. Conecta tu GitHub
3. Selecciona el repositorio LAB14
4. Railway detectará .NET automáticamente
5. Configura la variable de entorno `MySQLConnection`
6. Espera a que se despliegue y copia la URL

**Paso 2: Configurar Vercel**
1. Ve a https://vercel.com/dashboard
2. Selecciona el proyecto LAB14
3. Ve a Settings → Environment Variables
4. Agrega: `API_NET_URL` con la URL de Railway
5. Guarda y redespliega

**Paso 3: ¡Listo!**
- Tu API estará disponible en Vercel
- Todas las peticiones se redirigen automáticamente a Railway

### ✅ Alternativas Recomendadas para .NET

#### 1. **Azure App Service** (Recomendado para .NET)
- **URL**: https://portal.azure.com
- **Ventajas**: 
  - Soporte oficial de Microsoft para .NET
  - Integración directa con GitHub
  - Despliegue automático
  - Escalado automático
- **Pasos**:
  1. Crear cuenta en Azure
  2. Crear un App Service (Web App)
  3. Conectar repositorio de GitHub
  4. Seleccionar .NET 8.0 como runtime
  5. Desplegar automáticamente

#### 2. **Railway**
- **URL**: https://railway.app
- **Ventajas**:
  - Detección automática de proyectos .NET
  - Despliegue desde GitHub
  - Base de datos MySQL incluida
- **Pasos**:
  1. Conectar cuenta de GitHub
  2. Importar repositorio
  3. Railway detectará automáticamente .NET
  4. Configurar variables de entorno
  5. Desplegar

#### 3. **Render**
- **URL**: https://render.com
- **Ventajas**:
  - Soporte nativo para .NET
  - Despliegue desde GitHub
  - Base de datos MySQL disponible
- **Pasos**:
  1. Conectar cuenta de GitHub
  2. Crear nuevo "Web Service"
  3. Seleccionar .NET como runtime
  4. Configurar variables de entorno
  5. Desplegar

#### 4. **Fly.io**
- **URL**: https://fly.io
- **Ventajas**:
  - Soporte para aplicaciones .NET
  - Despliegue global
  - Configuración con Docker

#### 5. **Heroku** (con buildpack de .NET)
- **URL**: https://heroku.com
- **Ventajas**:
  - Soporte para .NET mediante buildpacks
  - Integración con GitHub
- **Requisito**: Necesita buildpack de .NET

## 📦 GitHub Actions CI/CD

Este proyecto incluye un workflow de GitHub Actions configurado que:

- ✅ Restaura dependencias
- ✅ Compila la aplicación en modo Release
- ✅ Publica la aplicación
- ✅ Genera artefactos descargables

El workflow se ejecuta automáticamente en cada push a la rama `main`.

**Verificar el workflow**:
1. Ve a https://github.com/Aquino0722/LAB14/actions
2. Revisa que el workflow "Build and Deploy .NET Application" se ejecute correctamente

## 🛠️ Configuración Local

### Requisitos
- .NET 8.0 SDK
- MySQL Server
- Visual Studio / Rider / VS Code

### Variables de Entorno
Configurar la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "MySQLConnection": "Server=tu-servidor;Database=tu-db;User=tu-usuario;Password=tu-password;"
  }
}
```

### Ejecutar Localmente
```bash
dotnet restore
dotnet build
dotnet run
```

La API estará disponible en:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger: https://localhost:5001/swagger

## 📝 Estructura del Proyecto

```
LabLINQ/
├── Controllers/          # Controladores de la API
├── DTOs/                # Data Transfer Objects
├── Models/              # Modelos de Entity Framework
├── Mappings/            # Perfiles de AutoMapper
├── Repositories/        # Repositorios y Unit of Work
├── .github/workflows/   # Workflows de GitHub Actions
└── Program.cs           # Punto de entrada de la aplicación
```

## 🔧 Tecnologías Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- MySQL (Pomelo.EntityFrameworkCore.MySql)
- AutoMapper
- Swagger/OpenAPI
- GitHub Actions (CI/CD)

## 📚 Documentación

- [Documentación de ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [GitHub Actions](https://docs.github.com/en/actions)

