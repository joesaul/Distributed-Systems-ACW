using DistSysACW.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DistSysACW.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Models.UserContext dbContext)
        {
            #region Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            //        Then set the correct roles for the User, using claims
            #endregion

            // Call the next delegate/middleware in the pipeline
            

            context.Request.Headers.TryGetValue("ApiKey", out var value);
            if(UserDatabaseAccess.checkUserByAPIKey(value) == true)
            {
                User user = dbContext.Users.Find(value);
                IList<Claim> claimCollection = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection);
                context.User.AddIdentity(claimsIdentity);
            }
            await _next(context);

        }

    }
}
