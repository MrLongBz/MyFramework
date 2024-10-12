using Microsoft.AspNetCore.Mvc;
using MyF.Entities.DtoModesl; 
using MyF.Services;

namespace MyF.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {

        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] LoginModel model)
        {
            var (user, roles,permissions) = await _userService.LoginAsync(model.Account, model.Password);

            if (user == null)
            {
                return BadRequest("Invalid account or password");
            }

            return Ok(new 
            {
                User = user,
                Roles = roles,
                Permissions = permissions
            });
        }
    }
}
