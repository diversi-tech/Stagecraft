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
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("processPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            var result = await _paymentService.ProcessPayment(paymentDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var receiptPdf = await _paymentService.GenerateReceiptPdf(result.Receipt);
            // שמירת הקבלה בענן
            await _paymentService.SaveReceiptToCloud(result.Receipt, receiptPdf);

            return File(receiptPdf, "application/pdf", "receipt.pdf");
        }
    }
}