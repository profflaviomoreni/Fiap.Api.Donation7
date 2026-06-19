using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fiap.Api.Donation7.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int GetUserId()
        {
            var idValue = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(idValue, out var userId) ? userId : 0;
        }

        protected ClaimsPrincipal? CurrentUser => HttpContext?.User;
    }
}
