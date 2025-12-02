using Itzin.Core.Entities;

namespace Itzin.Core.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateEmailVerificationToken();
    string GeneratePasswordResetToken();
}
