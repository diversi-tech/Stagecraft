//using Microsoft.AspNetCore.DataProtection;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace StagecraftApi.JwtManager
//{

//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        private Roles _role { get; set; }
//        public AuthorizeAttribute(Roles roles) {
//            _role = roles;
//        }


//        ////גישה לכולם
//        //public AuthorizeAttribute()
//        //{

//        //}


//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var userId = int.Parse(secretKey.Claims.First(x => x.Type == "code").Value);
//            //var userId = context.HttpContext.Items["UserId"];
//            if(_role == Roles.Admin)
//            {

//            }
//            if (userId == null )
//            {
//                // not logged in
//                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//            }
//        }
//    }

//    public enum Roles
//    {
//        Everyone,
//        User,
//        Admin,
//    }
//}

//using Microsoft.AspNetCore.DataProtection;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Linq;

//namespace StagecraftApi.JwtManager
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        private Roles _role { get; set; }

//        public AuthorizeAttribute(Roles role)
//        {
//            _role = role;
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//            if (token == null || !JwtTokenMiddleware.ValidateToken(token, out int userId, out Roles userRole))
//            {
//                // not logged in or token validation failed
//                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//                return;
//            }

//            if (_role != Roles.Everyone && userRole != _role)
//            {
//                // role not authorized
//                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
//                return;
//            }

//            // attach user to context on successful jwt validation
//            context.HttpContext.Items["UserId"] = userId;
//        }
//    }

//    public enum Roles
//    {
//        Everyone,
//        User,
//        Admin,
//    }
//}




//using Microsoft.AspNetCore.DataProtection;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace StagecraftApi.JwtManager
//{

//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        private Roles _role { get; set; }
//        public AuthorizeAttribute(Roles roles) {
//            _role = roles;
//        }


//        ////גישה לכולם
//        //public AuthorizeAttribute()
//        //{

//        //}


//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var userId = int.Parse(secretKey.Claims.First(x => x.Type == "code").Value);
//            //var userId = context.HttpContext.Items["UserId"];
//            if(_role == Roles.Admin)
//            {

//            }
//            if (userId == null )
//            {
//                // not logged in
//                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//            }
//        }
//    }

//    public enum Roles
//    {
//        Everyone,
//        User,
//        Admin,
//    }
//}

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace StagecraftApi.JwtManager
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private Roles _role { get; set; }

        public AuthorizeAttribute(Roles role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null || !JwtTokenMiddleware.ValidateToken(token, out int userId, out Roles userRole))
            {
                // not logged in or token validation failed
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (_role != Roles.Everyone && userRole != _role && userRole != Roles.Admin)
            {
                // role not authorized
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                return;
            }

            // attach user to context on successful jwt validation
            context.HttpContext.Items["UserId"] = userId;
        }
    }

    public enum Roles
    {
        Everyone,
        User,
        Admin,
    }
}







