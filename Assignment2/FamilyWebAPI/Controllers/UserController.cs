using System;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FamilyWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser([FromQuery] string? userName, [FromQuery] string? password){
            try
            {
                var user = await _userService.ValidateUserAsync(userName,password);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}