using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;
using System.Net.Http;
using StagecraftApi.JwtManager;
using Microsoft.AspNetCore.Identity.Data;
using System.Net;


namespace StagecraftNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {

        private readonly ILogin _loginService;

        public loginController(ILogin loginService)
        {
            _loginService = loginService;
        }



        [HttpPost("CheckUserExistence")]
        public ActionResult CheckUserExistence([FromBody] Users credentials)
        {
            try
            {


                var userExists = _loginService.CheckUserExistence(credentials);

                //var refreshToken = JwtTokenMiddleware.GenerateRefreshToken();


                // Store the refresh token securely (for demonstration purposes, using an in-memory dictionary)
                //refreshTokenStore["1234"] = refreshToken;
                if (userExists != -1)
                {
                    var token = JwtTokenMiddleware.GenerateJwtToken(credentials.Password.ToString(), credentials.Email);
                    return Ok(new { Token = token, userExists = Ok(userExists) });
                    //return Ok(userExists);
                }
                //לזכור לשנות שיחזיר את האוקי ולא את הטוקן!!!!!!!!!!!
                else
                    return Ok(userExists);
                //return Ok(new { Token = token, RefreshToken = refreshToken });

                //return Ok(userExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(500, "Internal server error: " + ex.Message);

            }

        }

        [HttpGet()]
        [Route("GetUserById/{userId}")]
        public ActionResult GetUserById(int userId)
        {
            try
            {
                var t = _loginService.GetUserById(userId);
                return Ok(t[0]);
            }

            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginRequest loginRequest)
        {
            if (ValidateCredentials(loginRequest.Email, loginRequest.Password))
            {
                var newToken = JwtTokenMiddleware.GenerateJwtToken(loginRequest.Email, loginRequest.Password);
                return Ok(new TokenResponse
                {
                    Token = newToken
                });
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        private bool ValidateCredentials(string e, string p)
        {
            // כאן יש להוסיף לוגיקה לאימות האישורים
            //Users credentials = new Users("string", e, 0, true, "string", p, "2024-07-28T12:16:50.083Z", "2024-07-28T12:16:50.083Z");

            Users credentials = new Users();
            credentials.Email = e;
            credentials.Password = p;
            var userExists = _loginService.CheckUserExistence(credentials);
            if (userExists != -1)
            {
                return true;
            }
            return false;
        }
    }
    //[HttpPost]
    //[Route("RefreshToken")]
    //public IActionResult RefreshToken([FromBody] TokenRequest request)
    //{
    //    // Validate the refresh token
    //    if (refreshTokenStore.TryGetValue("1234", out var storedRefreshToken) && JwtTokenMiddleware.ValidateRefreshToken(request.RefreshToken, storedRefreshToken))
    //    {
    //        var newToken = JwtTokenMiddleware.GenerateJwtToken("1234", "Rivka");
    //        string newRefreshToken = JwtTokenMiddleware.GenerateRefreshToken();

    //        // Update the stored refresh token
    //        refreshTokenStore["1234"] = newRefreshToken;

    //        return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
    //    }

    //    return Unauthorized();
    //}
}
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class TokenResponse
{
    public string Token { get; set; }
}




