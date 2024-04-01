using System;
namespace VailInstructorWikiApi.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VailInstructorWikiApi.DTOs.User;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Repos;
using VailInstructorWikiApi.Services;

public interface IAuthService
{
    public string GenerateJWTToken(ApplicationUser aUser, User user);
    public Task<ApplicationUser> Register(User user, string password);
    public Task<ApplicationUser> Authenticate(string email, string password);
    public Task<string> GetEamilVerificationCode(ApplicationUser user);
    public Task<string> GetChangePasswordCode(ApplicationUser user);
    public Task<IdentityResult> VerifyEmailAddress(string email, string token);
    public Task<IdentityResult> UpdateUserPassword(ApplicationUser user, string token, string newPassword);
    public Task<Boolean> SendPasswordResetEmail(string email);
    public Task<bool> DeleteApplicationUser(string email);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly UserRepo _userRepo;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserService _userService;

    public AuthService(
        IConfiguration config,
        UserRepo userRepo,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IEmailService emailService,
        IUserService userService
    )
    {
        _emailService = emailService;
        _config = config;
        _userRepo = userRepo;
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
    }

    public string GenerateJWTToken(ApplicationUser aUser, User user)
    {
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("JWT").GetValue<string>("SecretKey")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("emailVerified", aUser.EmailConfirmed.ToString())
        };
        var token = new JwtSecurityToken(
            issuer: _config.GetSection("JWT").GetValue<string>("Issuer"),
            audience: _config.GetSection("JWT").GetValue<string>("Audience"),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ApplicationUser> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            return user;
        }
        return null;
    }

    public async Task<ApplicationUser> Register(User user, string password)
    {

        var emailExists = await _userManager.FindByEmailAsync(user.Email);
        if (emailExists != null)
        {
            throw new Exception("Email Already exists");
        }
        ApplicationUser applicationUser = new ApplicationUser()
        {
            Email = user.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = user.Email,
        };
        await _userService.CreateUser(user);
        IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, password);
        var code = await GetEamilVerificationCode(applicationUser);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        string actionUrl = _config.GetSection("ActionUrls").GetValue("VerifyEmail", "localhost") + $"{code}/{user.Email}";
        WelcomeEmailData welcomeEmailData = new WelcomeEmailData
        {
            Email = user.Email,
            Name = "Gamer",
            ActionUrl = actionUrl,
        };
        string email = _emailService.GetEmailTemplate("VerifyEmail", welcomeEmailData);
        MailData mailData = new MailData(
          new List<string> { user.Email },
          "Welcome to the MailKit Demo",
          email
        );
        await _emailService.SendAsync(mailData, new CancellationToken());
        return applicationUser;
    }

    public async Task DeleteUser(string email)
    {
        await _userService.DeleteUserByEmail(email);
        return;
    }

    public async Task<IdentityResult> VerifyEmailAddress(string email, string token)
    {
        var user = await GetApplicationUser(email);
        byte[] encodedBytes = WebEncoders.Base64UrlDecode(token);
        string decodedToken = Encoding.UTF8.GetString(encodedBytes);
        return await VerifyEmailAddress(user, decodedToken);
    }

    public async Task<bool> GetUserEmailVerificationStatus(string email) {
        var user = await GetApplicationUser(email);
        if (user == null) {
            return false;
        }
        return user.EmailConfirmed;
    }

    private async Task<ApplicationUser> GetApplicationUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User does not exist");
        }
        return user;
    }

    public async Task<string> GetEamilVerificationCode(ApplicationUser user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GetChangePasswordCode(ApplicationUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    private async Task<IdentityResult> VerifyEmailAddress(ApplicationUser user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<IdentityResult> UpdateUserPassword(ApplicationUser user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<bool> SendPasswordResetEmail(string email)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return true;
        }
        string token = await GetChangePasswordCode(user);
        string actionUrl = _config.GetSection("ActionUrls").GetValue("VerifyEmail", "localhost") + $"{token}/{user.Email}";
        PasswordResetEmailData passwordResetEmailData = new PasswordResetEmailData
        {
            Email = user.Email,
            ActionUrl = actionUrl,
        };
        string emailBody = _emailService.GetEmailTemplate("ResetPassword", passwordResetEmailData);
        MailData mailData = new MailData(
          new List<string> { "martin@skismolka.com" },
          "Welcome to the MailKit Demo",
          emailBody
        );
        await _emailService.SendAsync(mailData, new CancellationToken());

        return true;
    }

    public async Task<bool> DeleteApplicationUser(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return true;
        }
        await _userManager.DeleteAsync(user);
        return true;
    }
}
