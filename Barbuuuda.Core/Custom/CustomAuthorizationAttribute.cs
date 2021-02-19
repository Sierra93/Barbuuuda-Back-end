using Barbuuuda.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Barbuuuda.Core.Custom
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.User.Identity.Name))
            {
                throw new NoAuthorize(HttpStatusCode.Unauthorized, "Пользователь сервиса не авторизован. Пожалуйста, авторизуйтесь.");
            }
        }
    }
}
