using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace StagecraftDAL.Interface
{
    public interface IPayment
    {
        Task<(bool Success, string Message, ReceiptDto Receipt)> ProcessPayment(PaymentDto paymentDto);
        Task<byte[]> GenerateReceiptPdf(ReceiptDto receipt);
        Task SaveReceiptToCloud(ReceiptDto receipt, byte[] pdfData);
    }
}