
using Microsoft.AspNetCore.Identity;
using SupplyChain.DatabaseContext;

namespace SupplyChain.Entities
{
    public class AppUserRole:IdentityUserRole<Guid>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role{set;get;}

    }
}