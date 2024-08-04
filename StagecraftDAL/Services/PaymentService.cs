using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StagecraftDAL.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document; // Alias להגדרת iTextSharp.text.Document

namespace StagecraftDAL.Services
{
    public class PaymentService : IPayment
    {


        public async Task<(bool Success, string Message, ReceiptDto Receipt)> ProcessPayment(PaymentDto paymentDto)
        {
            // יצירת קבלה חדשה
            var receipt = new ReceiptDto
            {
                FullName = paymentDto.FullName,
                Email = paymentDto.Email,
                PhoneNumber = paymentDto.PhoneNumber,
                Address = paymentDto.Address,
                City = paymentDto.City,
                Country = paymentDto.Country,
                PostalCode = paymentDto.PostalCode,
                CardNumber = paymentDto.CardNumber,
                ExpiryDate = paymentDto.ExpiryDate,
                CVV = paymentDto.CVV,
                Amount = paymentDto.Amount,
                Date = DateTime.Now
            };

            // שמירת הקבלה במסד נתונים באמצעות הפונקציה ExecuteStoredProcedure
            var parameters = new SqlParameter[]
            {
            new SqlParameter("@FullName", receipt.FullName),
            new SqlParameter("@Email", receipt.Email),
            new SqlParameter("@PhoneNumber", receipt.PhoneNumber),
            new SqlParameter("@Address", receipt.Address),
            new SqlParameter("@City", receipt.City),
            new SqlParameter("@Country", receipt.Country),
            new SqlParameter("@PostalCode", receipt.PostalCode),
            new SqlParameter("@CardNumber", receipt.CardNumber),
            new SqlParameter("@ExpiryDate", receipt.ExpiryDate),
            new SqlParameter("@CVV", receipt.CVV),
            new SqlParameter("@Amount", receipt.Amount),
            new SqlParameter("@Date", receipt.Date)
            };

            SQLDataAccess.ExecuteStoredProcedure("SaveReceipt", parameters);

            return (true, "Payment processed successfully", receipt);
        }

        public async Task<byte[]> GenerateReceiptPdf(ReceiptDto receipt)
        {
            using (var ms = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();
                document.Add(new Paragraph("Receipt"));
                document.Add(new Paragraph($"Full Name: {receipt.FullName}"));
                document.Add(new Paragraph($"Email: {receipt.Email}"));
                document.Add(new Paragraph($"Phone Number: {receipt.PhoneNumber}"));
                document.Add(new Paragraph($"Address: {receipt.Address}"));
                document.Add(new Paragraph($"City: {receipt.City}"));
                document.Add(new Paragraph($"Country: {receipt.Country}"));
                document.Add(new Paragraph($"Postal Code: {receipt.PostalCode}"));
                document.Add(new Paragraph($"Card Number: {receipt.CardNumber}"));
                document.Add(new Paragraph($"Expiry Date: {receipt.ExpiryDate}"));
                document.Add(new Paragraph($"CVV: {receipt.CVV}"));
                document.Add(new Paragraph($"Amount: {receipt.Amount}"));
                document.Add(new Paragraph($"Date: {receipt.Date}"));
                document.Close();
                return ms.ToArray();
            }
        }

        public async Task SaveReceiptToCloud(ReceiptDto receipt, byte[] pdfData)
        {
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(pdfData), "file", "receipt.pdf");
                var response = await client.PostAsync("https://api.vangus.co.il/upload", content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
