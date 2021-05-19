using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialHub.Application.Interfaces;
using SocialHub.Infrastructure.Database;
using SocialHub.Infrastructure.Services;
using System;

namespace SocialHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                && configuration.GetConnectionString("Postgres") is null)
            {
                serviceCollection.AddDbContext<ISocialHubDbContext, SocialHubDbContext>(options => options.UseInMemoryDatabase("SocialHub"));
            }
            else
            {
                serviceCollection.AddDbContext<ISocialHubDbContext, SocialHubDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("postgres")));
            }

            serviceCollection.AddSingleton<IJwtService, JwtService>();
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
            serviceCollection.AddTransient<IPostService, PostService>();
            serviceCollection.AddTransient<ICryptographyService, CryptographyService>(config =>
            {
                return new CryptographyService(new(iterations: 10_000));
            });

            return serviceCollection;
        }
    }
}
