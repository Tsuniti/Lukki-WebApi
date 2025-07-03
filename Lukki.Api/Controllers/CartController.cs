using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

[Route("[controller]")]
public class CartController : ApiController
{
    [HttpGet]
    public IActionResult ListCartItems()
    {
        return Ok(Array.Empty<string>());
    }
}