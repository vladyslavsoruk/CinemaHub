using cineHubBackend.Models;
using System.Security.Claims;

namespace cineHubBackend.Interfaces
{
    public interface IJwtService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken(User user);
    }
}