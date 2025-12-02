using Itzin.Api.DTOs;
using Itzin.Core.Entities;
using Itzin.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Itzin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IUserRepository userRepository,
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _userRepository = userRepository;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return BadRequest(new { message = "Email already registered" });
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PreferredLanguage = request.PreferredLanguage,
            IsEmailVerified = false,
            EmailVerificationToken = _authService.GenerateEmailVerificationToken(),
            EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
        };

        await _userRepository.CreateAsync(user);

        _logger.LogInformation("User registered: {Email}", user.Email);

        var token = _authService.GenerateJwtToken(user);
        var refreshToken = _authService.GenerateRefreshToken();

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PreferredLanguage = user.PreferredLanguage,
                IsEmailVerified = user.IsEmailVerified
            }
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        _logger.LogInformation("User logged in: {Email}", user.Email);

        var token = _authService.GenerateJwtToken(user);
        var refreshToken = _authService.GenerateRefreshToken();

        return Ok(new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PreferredLanguage = user.PreferredLanguage,
                IsEmailVerified = user.IsEmailVerified
            }
        });
    }

    [HttpPost("verify-email")]
    public async Task<ActionResult> VerifyEmail([FromQuery] string token)
    {
        var user = await _userRepository.GetByEmailVerificationTokenAsync(token);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid or expired verification token" });
        }

        user.IsEmailVerified = true;
        user.EmailVerificationToken = null;
        user.EmailVerificationTokenExpiry = null;
        await _userRepository.UpdateAsync(user);

        _logger.LogInformation("Email verified for user: {Email}", user.Email);

        return Ok(new { message = "Email verified successfully" });
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPassword([FromBody] PasswordResetRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return Ok(new { message = "If the email exists, a password reset link has been sent" });
        }

        user.PasswordResetToken = _authService.GeneratePasswordResetToken();
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
        await _userRepository.UpdateAsync(user);

        _logger.LogInformation("Password reset requested for: {Email}", user.Email);

        return Ok(new { message = "If the email exists, a password reset link has been sent" });
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] PasswordResetConfirmDto request)
    {
        var user = await _userRepository.GetByPasswordResetTokenAsync(request.Token);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid or expired reset token" });
        }

        user.PasswordHash = _authService.HashPassword(request.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        await _userRepository.UpdateAsync(user);

        _logger.LogInformation("Password reset completed for: {Email}", user.Email);

        return Ok(new { message = "Password reset successfully" });
    }
}
