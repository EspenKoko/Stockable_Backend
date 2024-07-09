using Stockable_Backend.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Data;

namespace Stockable_Backend.Factory
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public AppUserClaimsPrincipalFactory(UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {
        }

    }
}

