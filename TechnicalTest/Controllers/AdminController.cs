using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Data;
using TechnicalTest.Interfaces;
using TechnicalTest.ViewModels;

namespace TechnicalTest.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var token = Guid.NewGuid().ToString();
            var result = await _authService.RegisterAsync(model, token);

            if (!result.Success) return BadRequest(new { message = result.Message ?? "Error al registrar usuario" });

            var activationLink = Url.Action("Welcome", "Auth", new { token }, Request.Scheme);

            return Ok(new { message = result.Message, link = activationLink });
        }
    }
}
