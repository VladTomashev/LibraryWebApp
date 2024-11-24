using Library.Core.Enums;
using System.Security.Claims;

namespace Library.WebApi.Extensions
{
    public static class PolicySetup
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, nameof(Role.Admin)));
                options.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, nameof(Role.User)));
            });

            return services;
        }
    }
}
