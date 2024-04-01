using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; // Assuming UserService is a separate service
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/user/email/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            User user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int skip = 0, int take = 100)
        {
            List<User> users = await _userService.GetUsers(skip, take);
            return Ok(users);
        }
    }
}
