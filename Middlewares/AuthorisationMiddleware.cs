using LibraryAPI.Classes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Middlewares
{
    public class AuthorisationMiddleware
    {
        private readonly RequestDelegate _next;
        private string? token1;

        public AuthorisationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Path.StartsWithSegments("/api/LogIn"))
            {
                await _next(context);
                token1 = context.Request.Headers["authorisation"].FirstOrDefault()?.Split(" ").Last();
                return;
            }
            if (!context.Request.Headers.ContainsKey("authorisation"))
            {
                if (token1 == null || !TokenStore.ValidateToken(token1)) 
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized - No authorisation Header or Invalid");
                    return;
                }
                context.Request.Headers.Append("authorisation", token1);
            }




            await _next(context);
        }
    }
}
