using Microsoft.AspNetCore.Http;
using SocialHub.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccountService accountService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await jwtService.ValidateToken(token)
                    .IfSomeAsync(async jwtToken =>
                    {
                        var accountId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "sub").Value);
                        var acc = await accountService.GetAccountByIdAsync(accountId);

                        // attach user to context on successful jwt validation
                        acc.IfRight(acc => context.Items[jwtService.Key] = acc);
                    });
            }

            await _next(context);
        }
    }
}
