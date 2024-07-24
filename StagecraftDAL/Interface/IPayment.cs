using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StagecraftDAL.Interface
{
    public interface IPayment
    {
        Task<PaymentResult> ProcessPayment(PaymentToken paymentToken);
        Task<string> GenerateToken(PaymentDetails paymentDetails);
    }

}
