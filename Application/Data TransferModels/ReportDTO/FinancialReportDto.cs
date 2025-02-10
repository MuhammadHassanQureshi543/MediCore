using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.ReportDTO
{
    public class FinancialReportDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalPendingPayments { get; set; }
        public List<MonthlyEarningsDto> MonthlyEarnings { get; set; }
        public List<PendingInvoiceDto> PendingInvoices { get; set; }
    }

    public class MonthlyEarningsDto
    {
        public string Month { get; set; }
        public decimal Total { get; set; }
    }

    public class PendingInvoiceDto
    {
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }

}
