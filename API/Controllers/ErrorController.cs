using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models.Error;
using Models.Result;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [Produces("application/json")]
        public IActionResult Error([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return exceptionHandlerFeature.Error is NotFoundException ?
                NotFound(exceptionHandlerFeature.Error.Message) : Problem(exceptionHandlerFeature.Error.Message);
        }

    }
}