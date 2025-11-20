// Serverless Function que actúa como proxy a la API .NET
// Para usar esto, necesitas desplegar tu API .NET en Railway/Render/Azure
// y configurar la variable de entorno API_NET_URL en Vercel

export default async function handler(req, res) {
  // Obtener la URL de la API .NET desde variables de entorno
  const API_NET_URL = process.env.API_NET_URL;
  
  if (!API_NET_URL) {
    return res.status(500).json({
      error: 'API_NET_URL no configurada',
      message: 'Por favor, configura la variable de entorno API_NET_URL en Vercel con la URL de tu API .NET desplegada en Railway/Render/Azure',
      instructions: [
        '1. Despliega tu API .NET en Railway (https://railway.app) o Render (https://render.com)',
        '2. Obtén la URL de tu API desplegada',
        '3. Ve a Vercel Dashboard → Settings → Environment Variables',
        '4. Agrega: API_NET_URL = https://tu-api-dotnet.railway.app',
        '5. Redespliega en Vercel'
      ]
    });
  }
  
  try {
    // Construir la URL completa para el proxy
    const url = new URL(req.url.replace('/api', ''), API_NET_URL);
    
    // Preparar headers
    const headers = {
      'Content-Type': 'application/json',
    };
    
    // Copiar headers importantes del request original
    if (req.headers.authorization) {
      headers['Authorization'] = req.headers.authorization;
    }
    if (req.headers['content-type']) {
      headers['Content-Type'] = req.headers['content-type'];
    }
    
    // Preparar body para métodos que lo requieren
    let body = undefined;
    if (req.method !== 'GET' && req.method !== 'HEAD' && req.body) {
      body = JSON.stringify(req.body);
    }
    
    // Hacer la petición a la API .NET
    const response = await fetch(url.toString(), {
      method: req.method,
      headers: headers,
      body: body,
    });
    
    // Obtener la respuesta
    const contentType = response.headers.get('content-type');
    let data;
    
    if (contentType && contentType.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }
    
    // Copiar headers de respuesta importantes
    const responseHeaders = {};
    response.headers.forEach((value, key) => {
      if (key.toLowerCase() !== 'content-encoding') {
        responseHeaders[key] = value;
      }
    });
    
    // Responder con el mismo status y datos
    Object.keys(responseHeaders).forEach(key => {
      res.setHeader(key, responseHeaders[key]);
    });
    
    return res.status(response.status).json(data);
    
  } catch (error) {
    console.error('Error en proxy a API .NET:', error);
    return res.status(500).json({
      error: 'Error al conectar con la API .NET',
      message: error.message,
      apiUrl: API_NET_URL,
      help: 'Verifica que la API .NET esté desplegada y accesible en la URL configurada'
    });
  }
}

