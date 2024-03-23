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
//Proteger las rutas
[Authorize]
//Api controller para decirle que va hacer un controlador tipo API
[ApiController]
//route define la asignacion del token controller
[Route("[controller]")]
//ControllerBase es para trabajar con solicitudes http
public class PizzaController : ControllerBase
{
    //Creacion de la llave secreta
    private readonly string? secretkey;
    //Constructor
    public PizzaController(IConfiguration config)
    {
        this.secretkey = config.GetSection("settings").GetSection("secretkey").ToString();

    }
    //Obtener todos las pizzas get
    [HttpGet("GetAllPizzas")]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    //Consultar por id
    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id)
    {
        //Inyecto el servicio
        var pizza = PizzaService.GetById(id);
        if (pizza is null)
        {
            return NotFound();
        }
        else
        {
            return pizza;
        }
    }

    //Agregar una pizza a la lista
    [HttpPost]
    public AcceptedResult Create(Pizza pizza)
    {

        PizzaService.Add(pizza);
        return Accepted(pizza);
    }
    //Eliminar una pizza
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {

        if (id == 0)
        {
            return NotFound();
        }
        PizzaService.Delete(id);
        return Accepted();
    }
    //Actualizar una pizza
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (pizza is null || id == 0)
        {
            return NotFound();
        }
        else
        {
            PizzaService.Update(pizza);
            return Accepted(pizza);
        }

    }
}