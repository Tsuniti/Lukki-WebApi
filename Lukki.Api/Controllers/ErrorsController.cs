﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        return Problem();
    }
}