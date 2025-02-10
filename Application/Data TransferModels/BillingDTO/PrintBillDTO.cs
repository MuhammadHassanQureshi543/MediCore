using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.BillingDTO
{
    public class PrintBillDTO
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // Paid, Unpaid
        public DateTime CreatedAt { get; set; }
    }
}
