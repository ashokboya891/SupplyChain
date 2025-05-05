using Microsoft.AspNetCore.Identity;

namespace SupplyChain.DatabaseContext
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? PersonName { set; get; }
        public string? RefreshToken { set; get; }

        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
