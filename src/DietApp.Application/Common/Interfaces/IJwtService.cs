using DietApp.Domain.Entities.Identity;

namespace DietApp.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    (Guid userId, Guid? tenantId)? ValidateAccessToken(string token);
}
