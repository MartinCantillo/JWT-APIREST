using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configurar JWT
builder.Configuration.AddJsonFile("appsettings.json");
// Obtener la clave secreta de la configuración
string? secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();

// Verificar si secretkey es null antes de convertirlo a bytes
var keyBytes = Encoding.UTF8.GetBytes(secretkey ?? "");
//configurar el sistema de jwt en el contenedor
builder.Services.AddAuthentication(config =>
{ //Configurar el jwt 

    // Configurar JWT como esquema de autenticación predeterminado
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // Configurar JWT como esquema de desafío predeterminado
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    // Desactivar la validación de HTTPS para simplificar el desarrollo
    config.RequireHttpsMetadata = false;
    // Habilitar la persistencia del token en el contexto de la solicitud
    config.SaveToken = true;
    // Configurar los parámetros de validación del token JWT
    config.TokenValidationParameters = new TokenValidationParameters
    {
        // Validar la clave de firma del emisor
        ValidateIssuerSigningKey = true,
        // Establecer la clave de firma del emisor
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        // No validar el emisor del token (puede ser útil en entornos de desarrollo)
        ValidateIssuer = false,
        // No validar el destinatario del token (puede ser útil en entornos de desarrollo)
        ValidateAudience = false,
    };
});

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    // Definir una política CORS llamada "AllowAnyOrigin"
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        // Permitir solicitudes desde cualquier origen
        builder.AllowAnyOrigin()
               // Permitir cualquier método HTTP
               .AllowAnyMethod()
               // Permitir cualquier encabezado HTTP
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Aplicar la política CORS "AllowAnyOrigin" a todas las solicitudes entrantes
app.UseCors("AllowAnyOrigin");
// Agregar middleware de autenticación al pipeline de solicitudes HTTP, despues de haber configurado el jwt 
app.UseAuthentication();
// Agregar middleware de autorización al pipeline de solicitudes HTTP
app.UseAuthorization();

app.MapControllers();

app.Run();
