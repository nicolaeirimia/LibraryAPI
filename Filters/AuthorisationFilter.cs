using LibraryAPI.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;


namespace LibraryAPI.Filters
{


    public class AuthorisationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("authorisation"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = context.HttpContext.Request.Headers["authorisation"].ToString().Split(" ").LastOrDefault();
            if (string.IsNullOrEmpty(token) || !TokenStore.ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }


    }

}
