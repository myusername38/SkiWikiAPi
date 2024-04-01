using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VailInstructorWikiApi.Auth;
using VailInstructorWikiApi.DTOs.User;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public AuthenticationController(
            IAuthService authService,
            IUserService userService,
            IConfiguration configuration,
            IEmailService emailService
        )
        {
            this._authService = authService;
            this._userService = userService;
            this._emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<string> Register([FromBody] CreateUserDto dto)
        {

            var userData = new User
            {
                Email = dto.Email,
            };
            var user = await _authService.Register(userData, dto.Password);
            return "worked";
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authService.Authenticate(dto.Email, dto.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }
            var userData = await _userService.GetUserByEmail(dto.Email);
            var jwt = _authService.GenerateJWTToken(user, userData);
            var reponse = new { jwt = jwt };
            return Ok(reponse);
        }

        [HttpPost]
        [Route("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
        {
            var user = await _authService.VerifyEmailAddress(dto.Email, dto.Token);
            return Ok(new { message = "Email Verified" });
        }

        [HttpPost]
        [Route("send-reset-email")]
        public async Task<IActionResult> SendPasswordResetEmail([FromBody] EmailDto dto)
        {
            // var user = await _aut

            return Ok(new { message = "Password Reset Email Sent" });
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] EmailDto dto)
        {

            // var code = await _authService.GetChangePasswordCode()
            return Ok("");
        }

        [HttpDelete("email/{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            try
            {
                await _userService.DeleteUserByEmail(email);
                await _authService.DeleteApplicationUser(email);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, "An error occurred while deleting the user."); // Internal Server Error
            }
        }
    }
}
