using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SocialHub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddIniFile($"appSettings.{env.EnvironmentName}.ini", optional: true, reloadOnChange: true)
                        .AddIniFile("appSettings.local.ini", optional: true, reloadOnChange: true);

                    if (env.IsProduction())
                    {
                        var pgUsername = File.ReadAllText(Environment.GetEnvironmentVariable("POSTGRES_USER_FILE")) ?? throw new ArgumentNullException("POSTGRES_USER_FILE cannot be null");
                        var pgPassword = File.ReadAllText(Environment.GetEnvironmentVariable("POSTGRES_PASSWORD_FILE")) ?? throw new ArgumentNullException("POSTGRES_PASSWORD_FILE cannot be null");
                        var secretString = File.ReadAllText(Environment.GetEnvironmentVariable("SECRET_STRING_FILE")) ?? throw new ArgumentNullException("POSTGRES_PASSWORD_FILE cannot be null");

                        config.AddInMemoryCollection(new Dictionary<string, string>()
                        {
                            { "POSTGRES_USER", pgUsername },
                            { "POSTGRES_PASSWORD", pgPassword },
                            { "SECRET_STRING", secretString }
                        });
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
