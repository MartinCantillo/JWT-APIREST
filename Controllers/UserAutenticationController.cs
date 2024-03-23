using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

//Api
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelsPizza.Models;
using ModelsUser.User;
using PizzaServices.Services;

namespace ControllersControllerr.Controllers;

[ApiController]

[Route("[controller]")]
public class UserController : ControllerBase
{
    //Creacion de la llave secreta
    private readonly string? secretkey;
    //Constructor
    public UserController(IConfiguration config)
    {
        this.secretkey = config.GetSection("settings").GetSection("secretkey").ToString();

    }
    
[HttpPost("/user")]
public IActionResult Validar([FromBody] User user)


{
    Console.WriteLine(user.Username);
    // Verificar si las credenciales del usuario son válidas
    if (user.Username == "martin@gmail.com" && user.Password == "123")
    {
        // Verificar si la clave secreta está disponible
        if (secretkey != null)
        {
            // Convertir la clave secreta a bytes utilizando ASCII
            var keyBytes = Encoding.ASCII.GetBytes(secretkey);

            // Crear un conjunto de reclamaciones (claims) para el usuario autenticado
            var claims = new ClaimsIdentity();
            
            // Agregar reclamaciones adicionales si es necesario (por ejemplo, roles de usuario)
            // claims.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            // Agregar una reclamación con el nombre de usuario
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));

            // Configurar el descriptor del token JWT, es como un resumen de lo que tiene el token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Definir las reclamaciones del token (en este caso, las reclamaciones del usuario)
                Subject = claims,
                // Definir la fecha de expiración del token (5 minutos a partir de ahora)
                Expires = DateTime.UtcNow.AddMinutes(5),
                // Definir las credenciales de firma con la clave secreta
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            // Crear un manejador de tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Crear el token JWT utilizando el descriptor configurado
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            // Escribir el token JWT como una cadena
            var tokenCreado = tokenHandler.WriteToken(tokenConfig);

            // Retornar una respuesta exitosa (200 OK) con el token JWT en el cuerpo de la respuesta
            return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
        }
    }
    
    // Si las credenciales son inválidas o la clave secreta no está disponible, retornar un error (400 Bad Request)
    return StatusCode(StatusCodes.Status400BadRequest, new { token = "Error , no estas registrado en la bd" });
}


}