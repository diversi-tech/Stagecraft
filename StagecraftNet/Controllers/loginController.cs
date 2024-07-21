using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;
using System.Net.Http;
using StagecraftApi.JwtManager;


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
                    var token = JwtTokenMiddleware.GenerateJwtToken(credentials.Code.ToString(), credentials.Email);
                    //return Ok(new { Token = token });
                    return Ok(userExists);
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
}



