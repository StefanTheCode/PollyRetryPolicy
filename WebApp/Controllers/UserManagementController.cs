using Microsoft.AspNetCore.Mvc;
using WebApp.Policies;

namespace WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserManagementController : ControllerBase
{
    public readonly HttpClient _client;
    private readonly ClientRetryPolicy _retryPolicy;

    public UserManagementController(HttpClient client, ClientRetryPolicy retryPolicy)
    {
        _client = client;
        _retryPolicy = retryPolicy;
    }

    [HttpGet]
    [Route("returnUser/{id}")]
    public async Task<ActionResult> ReturnUser(int id)
    {
        string apiURL = $"https://localhost:7071/api/User/getUser/{id}";

        var response = await _retryPolicy.ExponentialHttpRetry.ExecuteAsync(() => _client.GetAsync(apiURL));

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Success 200");
            
            return Ok(response);
        }
        else
        {
            Console.WriteLine("Error 500");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}