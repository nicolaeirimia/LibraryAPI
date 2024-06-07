using LibraryAPI.Classes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Middlewares
{
    public class AuthorisationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorisationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/LogIn"))
            {
                await _next(context);
                return;
            }
            if (!context.Request.Headers.ContainsKey("authorisation"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized - No authorisation Header");
                return;
            }
            var token = context.Request.Headers["authorisation"].FirstOrDefault()?.Split(" ").Last();
            if (token == null || !TokenStore.ValidateToken(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized - Invalid Token");
                return;
            }
            await _next(context);
        }
    }
}
