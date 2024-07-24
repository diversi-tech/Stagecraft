using Common;
using Microsoft.AspNetCore.Mvc;
using StagecraftDAL.Interface;
using StagecraftDAL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StagecraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentsController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("processPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentToken paymentToken)
        {
            var result = await _paymentService.ProcessPayment(paymentToken);
            if (result.IsSuccess)
            {
                return Ok(new { message = "Payment successful", transactionId = result.TransactionId });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPost("generateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] PaymentDetails paymentDetails)
        {
            var token = await _paymentService.GenerateToken(paymentDetails);
            if (token != null)
            {
                return Ok(new { token });
            }
            return BadRequest(new { message = "Token generation failed" });
        }
    }
    }
