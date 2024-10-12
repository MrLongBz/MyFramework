using Microsoft.AspNetCore.Mvc;
using MyF.Entities.BaseModels;
using MyF.Entities.DtoModesl;
using MyF.Services;

namespace MyF.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAllAsync();
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegistrationModel model)
        {
            if (await _userService.GetByUsernameAsync(model.UserName) != null)
            {
                return BadRequest("Username already exists");
            }

            if (await _userService.GetByEmailAsync(model.Email) != null)
            {
                return BadRequest("Email already exists");
            }
              
            var createdUser = await _userService.CreateUserAsync(model, model.Password);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }
    }
 
}
