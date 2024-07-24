using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StagecraftDAL.Interface;

namespace StagecraftDAL.Services
{

    public class PaymentService : IPayment 
    {
        private readonly HttpClient _httpClient;
        private readonly string _growApiUrl;
        private readonly string _publicKey;
        private readonly string _secretKey;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _growApiUrl = configuration["GrowApi:Url"];
            _publicKey = configuration["GrowApi:PublicKey"];
            _secretKey = configuration["GrowApi:SecretKey"];
        }

        public async Task<PaymentResult> ProcessPayment(PaymentToken paymentToken)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(paymentToken), Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync($"{_growApiUrl}/payments", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var paymentResult = JsonConvert.DeserializeObject<PaymentResult>(responseContent);
                return paymentResult;
            }
            return new PaymentResult { IsSuccess = false, Message = "Payment failed" };
        }

        public async Task<string> GenerateToken(PaymentDetails paymentDetails)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(paymentDetails), Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync($"{_growApiUrl}/generateToken", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return tokenResponse.token;
            }
            return null;
        }
    }
}