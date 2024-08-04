//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using System.Data;
//using System.Data.Common;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.WebRequestMethods;
//using Microsoft.Extensions.Configuration;

//namespace StagecraftApi.JwtManager
//{


//    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
//    public class JwtTokenMiddleware
//    {
//        private static string secretKey = "Stagecraft secret for generate Jwt Token"; // מפתח סודי לחתימת הטוקן
//                                                                                      //משתנה לריענון טוקן
//                                                                                      //public required string RefreshToken {  get; set; }
//        private readonly RequestDelegate _next;


//        public  JwtTokenMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }
//        // יצירת טוקן JWT
//        public static string GenerateJwtToken(string code, string email)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(secretKey);
//            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
//            string role= Roles.Everyone.ToString();

//            var claims = new[]
//            {
//            new Claim(ClaimTypes.NameIdentifier, code),
//            new Claim(ClaimTypes.Name, email),
//            new Claim(ClaimTypes.Role, role)
//            // ניתן להוסיף קליימים נוספים כפי שרלוונטי ליישום שלך
//        };

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddSeconds(30), // זמן תוקף של הטוקן
//                SigningCredentials = credentials
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }

//        // אימות טוקן JWT
//        public ClaimsPrincipal ValidateJwtToken(string token)
//        {

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Convert.FromBase64String(secretKey);

//            var validationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(key),
//                ValidateIssuer = false, // לפי הצורך שלך, ניתן לשנות
//                ValidateAudience = false, // לפי הצורך שלך, ניתן לשנות
//                ClockSkew = TimeSpan.Zero // אין דלאי בזמן ביצוע הטוקן
//            };

//            SecurityToken validatedToken;
//            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
//            return principal;
//        }
//        // אימות טוקן JWT
//        public static bool ValidateToken(string token, out int userId,out Roles userRole)
//        {
//            userId = 0;
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(secretKey);


//            try
//            {
//                tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ClockSkew = TimeSpan.Zero
//                },
//        out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value);
//                // שליפת הערכים

//                if (userId==111111)
//                    userRole= Roles.Admin;
//                else if(userId!=-1)
//                    userRole = Roles.User;
//                else userRole = Roles.Everyone;
//                //userRole = Enum.Parse<Roles>(jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value);
//                return true;
//            }
//            catch
//            {
//                userRole = Roles.Everyone;
//                // token validation failed
//                return false;
//            }
//        }

//            //נכנס לפה כל קריאה לאמת את הטוקן
//            //בודקת אם יש אותוריז
//            public Task Invoke(HttpContext httpContext)
//        {
//            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//            if (token != null)
//            {
//                // Example: Output token to console
//                Console.Out.WriteLineAsync($"Token received: {token}");
//            }

//            // Call the next delegate/middleware in the pipeline
//            return _next(httpContext);
//        }
//        //יוצר רפרש טוקן
//        public static string GenerateRefreshToken()
//        {
//            var randomNumber = new byte[32];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(randomNumber);
//                return Convert.ToBase64String(randomNumber);
//            }
//        }
//        // אימות
//        // Validate Refresh Token
//        public static bool ValidateRefreshToken(string token, string storedToken)
//        {
//            return token == storedToken;
//        }
//    }


//    //המחלקה הזו מספקת שיטת הרחבה המאפשרת להוסיף את ה-JwtTokenMiddleware
//    // Extension method used to add the middleware to the HTTP request pipeline.
//    public static class JwtTokenMiddlewareExtensions
//    {
//        public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<JwtTokenMiddleware>();
//        }
//    }
//}


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace StagecraftApi.JwtManager
{


    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtTokenMiddleware
    {
        private static string secretKey = "Stagecraft secret for generate Jwt Token"; // מפתח סודי לחתימת הטוקן
                                                                                      //משתנה לריענון טוקן
                                                                                      //public required string RefreshToken {  get; set; }
        private readonly RequestDelegate _next;


        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // יצירת טוקן JWT
        public static string GenerateJwtToken(string code, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            string role = Roles.Everyone.ToString();

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, code),
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
            // ניתן להוסיף קליימים נוספים כפי שרלוונטי ליישום שלך
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(30), // זמן תוקף של הטוקן
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // אימות טוקן JWT
        public ClaimsPrincipal ValidateJwtToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // לפי הצורך שלך, ניתן לשנות
                ValidateAudience = false, // לפי הצורך שלך, ניתן לשנות
                ClockSkew = TimeSpan.Zero // אין דלאי בזמן ביצוע הטוקן
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return principal;
        }
        // אימות טוקן JWT
        public static bool ValidateToken(string token, out int userId, out Roles userRole)
        {
            userId = 0;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);


            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
        out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value);
                //var aaa = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;

                // שליפת הערכים

                if (userId == 111111)
                    userRole = Roles.Admin;
                else if (userId != -1)
                    userRole = Roles.User;
                else userRole = Roles.Everyone;
                //userRole = Enum.Parse<Roles>(jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value);
                return true;
            }
            catch
            {
                userRole = Roles.Everyone;
                // token validation failed
                return false;
            }
        }

        //נכנס לפה כל קריאה לאמת את הטוקן
        //בודקת אם יש אותוריז
        public Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Example: Output token to console
                Console.Out.WriteLineAsync($"Token received: {token}");
            }

            // Call the next delegate/middleware in the pipeline
            return _next(httpContext);
        }
        //יוצר רפרש טוקן
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        // אימות
        // Validate Refresh Token
        public static bool ValidateRefreshToken(string token, string storedToken)
        {
            return token == storedToken;
        }




        //[HttpPost("refresh")]
        //public IActionResult RefreshToken([FromBody] TokenRequest tokenRequest)
        //{
        //    // נבדוק אם הטוקן לריענון חוקי
        //    if (JwtTokenMiddleware.ValidateRefreshToken(tokenRequest.RefreshToken, tokenRequest.StoredRefreshToken))
        //    {
        //        // אם חוקי, ניצור טוקן חדש
        //        var newToken = JwtTokenMiddleware.GenerateJwtToken(tokenRequest.Code, tokenRequest.Email);
        //        var newRefreshToken = JwtTokenMiddleware.GenerateRefreshToken();

        //        // ניתן להוסיף כאן לוגיקה לאחסון הטוקן החדש במאגר הנתונים

        //        return Ok(new TokenResponse
        //        {
        //            Token = newToken,
        //            RefreshToken = newRefreshToken
        //        });
        //    }

        //    return Unauthorized();
        //}
    }

    public class TokenRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public string StoredRefreshToken { get; set; }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}




////המחלקה הזו מספקת שיטת הרחבה המאפשרת להוסיף את ה-JwtTokenMiddleware
//// Extension method used to add the middleware to the HTTP request pipeline.
//public static class JwtTokenMiddlewareExtensions
//{
//    public static IApplicationBuilder UseJwtTokenMiddleware(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<JwtTokenMiddleware>();
//    }
//}




//}
