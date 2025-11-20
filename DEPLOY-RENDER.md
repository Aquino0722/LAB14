# 🚀 Guía Completa: Desplegar LabLINQ en Render

## 📋 Requisitos Previos

1. Cuenta en GitHub (ya tienes el repositorio)
2. Cuenta en Render (gratis en https://render.com)
3. Base de datos MySQL (puedes usar la de Render o una externa)

## 🔧 Paso 1: Configurar el Proyecto

El proyecto ya está configurado con:
- ✅ `render.yaml` - Configuración para Render
- ✅ `Program.cs` - Configurado para usar el puerto de Render
- ✅ Swagger habilitado en producción

## 📝 Paso 2: Crear Cuenta en Render

1. Ve a https://render.com
2. Haz clic en "Get Started for Free"
3. Crea una cuenta (puedes usar GitHub para registrarte más rápido)
4. Verifica tu email si es necesario

## 🌐 Paso 3: Crear Base de Datos MySQL en Render (Opcional)

Si no tienes una base de datos MySQL, puedes crear una en Render:

1. En Render Dashboard, haz clic en "New +" → "PostgreSQL" (Render no tiene MySQL gratis)
2. O mejor: crea una MySQL gratuita en https://www.freemysqlhosting.net/ o https://www.db4free.net/

**Alternativa: Usar PostgreSQL en Render**
- Render ofrece PostgreSQL gratis
- Necesitarías cambiar tu conexión de MySQL a PostgreSQL

## 🚀 Paso 4: Crear Web Service en Render

1. En Render Dashboard, haz clic en "New +" → "Web Service"

2. **Conectar Repositorio:**
   - Conecta tu cuenta de GitHub si no lo has hecho
   - Selecciona el repositorio: `Aquino0722/LAB14`
   - Haz clic en "Connect"

3. **Configurar el Servicio:**
   - **Name**: `lablinq-api` (o el nombre que prefieras)
   - **Region**: Selecciona la región más cercana (ej: Oregon)
   - **Branch**: `main`
   - **Root Directory**: `LabLINQ` (IMPORTANTE: el proyecto está en esta subcarpeta)
   - **Runtime**: Render detectará automáticamente `.NET`
   - **Build Command**: 
     ```
     dotnet restore && dotnet publish -c Release -o ./publish
     ```
   - **Start Command**:
     ```
     dotnet ./publish/LabLINQ.dll
     ```
   - **Plan**: Selecciona **Free** (gratis)

4. **Configurar Variables de Entorno:**
   Haz clic en "Advanced" y agrega estas variables:

   | Key | Value |
   |-----|-------|
   | `ASPNETCORE_ENVIRONMENT` | `Production` |
   | `ASPNETCORE_URLS` | `http://0.0.0.0:10000` |
   | `ConnectionStrings__MySQLConnection` | Tu cadena de conexión MySQL |

   **Ejemplo de cadena de conexión MySQL:**
   ```
   Server=tu-servidor;Database=linqexample;User=tu-usuario;Password=tu-password;
   ```

   **Nota**: En Render, las variables de entorno con `__` (doble guión bajo) se convierten en configuración anidada.
   `ConnectionStrings__MySQLConnection` se convierte en `ConnectionStrings:MySQLConnection` en .NET.

5. **Configuración Adicional:**
   - **Health Check Path**: `/swagger` (opcional, para verificar que la app está funcionando)
   - **Auto-Deploy**: Mantén activado "Auto-Deploy" para que se despliegue automáticamente en cada push a `main`

6. **Crear el Servicio:**
   - Haz clic en "Create Web Service"
   - Render comenzará a construir y desplegar tu aplicación

## ⏳ Paso 5: Esperar el Despliegue

- Render tardará aproximadamente **5-10 minutos** en construir y desplegar tu aplicación
- Puedes ver el progreso en tiempo real en la pestaña "Logs"
- Una vez completado, obtendrás una URL como: `https://lablinq-api.onrender.com`

## ✅ Paso 6: Verificar el Despliegue

1. **Ver Swagger:**
   - Ve a `https://tu-api.onrender.com/swagger`
   - Deberías ver la documentación de Swagger

2. **Probar los Endpoints:**
   - Prueba los endpoints de tu API
   - Ejemplo: `https://tu-api.onrender.com/api/exercise/clients`

## 🔗 Paso 7: Configurar Vercel como Proxy (Opcional)

Si quieres usar Vercel como punto de entrada:

1. Ve a Vercel Dashboard
2. Selecciona tu proyecto LAB14
3. Ve a Settings → Environment Variables
4. Agrega:
   - **Key**: `API_NET_URL`
   - **Value**: `https://tu-api.onrender.com` (tu URL de Render)
5. Redespliega en Vercel

## 🔧 Solución de Problemas

### Error: "dotnet: command not found"
- Asegúrate de que el Root Directory esté configurado como `LabLINQ`
- Verifica que el repositorio esté conectado correctamente

### Error de conexión a la base de datos
- Verifica que la cadena de conexión esté correcta en Variables de Entorno
- Asegúrate de usar `ConnectionStrings__MySQLConnection` (con doble guión bajo)
- Verifica que tu base de datos MySQL permita conexiones externas

### La aplicación no inicia
- Revisa los logs en Render Dashboard → Logs
- Verifica que el Start Command sea correcto
- Asegúrate de que el puerto sea dinámico (usando variable PORT o 10000)

### El servicio se suspende después de inactividad
- En el plan gratuito, Render suspende servicios inactivos después de 15 minutos
- Esto es normal y el servicio se reactivará automáticamente cuando recibas una petición
- La primera petición después de la suspensión puede tardar 30-60 segundos

## 📊 Monitoreo

- **Logs**: Render Dashboard → Tu Servicio → Logs
- **Métricas**: Render Dashboard → Tu Servicio → Metrics (en planes de pago)
- **Health Checks**: Render verificará automáticamente la ruta `/swagger`

## 🎉 ¡Listo!

Tu API .NET estará disponible en:
- **URL de Render**: `https://lablinq-api.onrender.com`
- **Swagger**: `https://lablinq-api.onrender.com/swagger`
- **API Endpoints**: `https://lablinq-api.onrender.com/api/...`

## 📝 Notas Importantes

1. **Plan Gratuito de Render:**
   - Servicios se suspenden después de 15 minutos de inactividad
   - La primera petición después de suspensión puede tardar 30-60 segundos
   - 750 horas gratis por mes

2. **Base de Datos:**
   - Render ofrece PostgreSQL gratis, pero tu proyecto usa MySQL
   - Puedes usar una base de datos MySQL externa gratuita
   - O modificar el proyecto para usar PostgreSQL

3. **Variables de Entorno:**
   - Render convierte `__` (doble guión bajo) en configuración anidada
   - `ConnectionStrings__MySQLConnection` → `ConnectionStrings:MySQLConnection` en .NET

## 🔄 Actualizaciones Automáticas

Con "Auto-Deploy" activado:
- Cada push a `main` desplegará automáticamente en Render
- No necesitas hacer nada manualmente
- Puedes ver el progreso en Render Dashboard → Deployments

