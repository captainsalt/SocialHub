using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Services;
using SocialHub.Infrastructure.Database;
using SocialHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace SocialHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // TODO: Add ability to configure from IConfiguration
            serviceCollection.AddDbContext<ISocialHubDbContext, SocialHubDbContext>(options => options.UseInMemoryDatabase("SocialHub"));

            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<ICryptographyService, CryptographyService>();
            serviceCollection.AddTransient<IJwtService, JwtService>();

            return serviceCollection;
        }
    }
}
