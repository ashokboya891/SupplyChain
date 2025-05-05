using System.Security.Claims;
using SupplyChain.DTO;
using SupplyChain.DatabaseContext;

namespace SupplyChain.ServiceContracts
{
    public interface IJwtService
    {
        Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user);
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
