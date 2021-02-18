using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Barbuuuda.Core.Custom
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.User.Identity.Name))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
