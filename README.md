# LabLINQ - API REST con .NET 8.0

Proyecto ASP.NET Core Web API con Entity Framework Core y MySQL.

## 🚀 Despliegue

### 🚀 Desplegar en Render (GRATIS) - RECOMENDADO

Render tiene un **plan gratuito** perfecto para desplegar aplicaciones .NET. Tu proyecto ya está configurado para Render.

#### ✅ Archivos Configurados

- ✅ `render.yaml` - Configuración para Render
- ✅ `Program.cs` - Configurado para usar el puerto de Render
- ✅ Swagger habilitado en producción

#### 📝 Pasos Rápidos:

1. **Crear cuenta en Render:**
   - Ve a https://render.com
   - Crea una cuenta gratuita (puedes usar GitHub)

2. **Crear Web Service:**
   - Haz clic en "New +" → "Web Service"
   - Conecta tu GitHub y selecciona `Aquino0722/LAB14`
   - Configura:
     - **Name**: `lablinq-api`
     - **Root Directory**: `LabLINQ` ⚠️ IMPORTANTE
     - **Build Command**: `dotnet restore && dotnet publish -c Release -o ./publish`
     - **Start Command**: `dotnet ./publish/LabLINQ.dll`
     - **Plan**: **Free** (gratis)

3. **Configurar Variables de Entorno:**
   - `ASPNETCORE_ENVIRONMENT` = `Production`
   - `ASPNETCORE_URLS` = `http://0.0.0.0:10000`
   - `ConnectionStrings__MySQLConnection` = Tu cadena de conexión MySQL
     - ⚠️ Usa `__` (doble guión bajo) para configuración anidada

4. **Desplegar:**
   - Haz clic en "Create Web Service"
   - Espera 5-10 minutos
   - Obtén tu URL: `https://lablinq-api.onrender.com`

📖 **Guía Completa**: Ver `DEPLOY-RENDER.md` para instrucciones detalladas paso a paso

#### 🎯 Opción Alternativa: Vercel como Proxy

Si quieres usar Vercel como punto de entrada (proxy a Render):

1. **Despliega en Render** (pasos arriba) ✅
2. **Configura Vercel:**
   - Ve a Vercel Dashboard → Settings → Environment Variables
   - Agrega: `API_NET_URL` = `https://tu-api.onrender.com`
   - Redespliega en Vercel

El proxy serverless (`api/index.js`) redirigirá todas las peticiones a tu API .NET en Render.

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

