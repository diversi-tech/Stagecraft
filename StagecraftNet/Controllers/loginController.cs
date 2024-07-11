using Microsoft.AspNetCore.Mvc;
using Common;
using StagecraftDAL.Interface;
using System.Net.Http;


namespace StagecraftNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {

        private readonly ILogin _loginService;
        //private readonly IHttpClientFactory _httpClientFactory;

        //public loginController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        public loginController(ILogin loginService)
        {
            _loginService = loginService;
        }

        //[HttpPost("fetchUserData")]
        //public async Task<ActionResult<bool>> FetchUserData([FromBody] Users user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("User object is null");
        //    }

        //    // המר את המידע למחרוזת שאילתה
        //    var queryString = $"id={user.Email}&name={user.Name}&email={user.Email}";
        //    var url = $"https://example.com/api?{queryString}";

        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient();
        //        var response = await client.GetAsync(url);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // קרא את התשובה מהשרת
        //            var responseData = await response.Content.ReadAsStringAsync();
        //            // המרת התשובה ל-bool (בהנחה שהתשובה היא "true" או "false")
        //            if (bool.TryParse(responseData, out bool result))
        //            {
        //                return result;
        //            }
        //            else
        //            {
        //                // אם לא הצלחנו להמיר את התשובה ל-bool
        //                return BadRequest("Invalid response from external server");
        //            }
        //        }
        //        else
        //        {
        //            // אם הבקשה לשרת החיצוני נכשלה
        //            return StatusCode((int)response.StatusCode, "Error fetching data from external server");
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        // טיפול בשגיאה
        //        Console.WriteLine($"Request error: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpPost("CheckUserExistence")]
        public ActionResult CheckUserExistence([FromBody] Users credentials)
        {
            try
            {


                var userExists = _loginService.CheckUserExistence(credentials);

                return Ok(userExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}



