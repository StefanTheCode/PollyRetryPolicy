using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [Route("getUser/{id}")]
    [HttpGet]
    public ActionResult GetUser(int id)
    {
        Random random = new Random();

        var failEdge = random.Next(1, 50);

        if (id < failEdge)
        {
            Console.WriteLine("I'm returning Success - 200");
            return Ok();
        }

        Console.WriteLine("I'm returning Error - 500");
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}