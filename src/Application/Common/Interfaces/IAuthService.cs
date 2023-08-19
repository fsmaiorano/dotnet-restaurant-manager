namespace Application.Common.Interfaces;

public interface IAuthService
{
    // Task<string?> GenerateToken(UserAuthenticationDto user);
    Task<bool> ValidateToken(string token);
}
