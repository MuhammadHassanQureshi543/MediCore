using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.ReportDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.Reports;
using CommonOperations.Constants;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Services.ReportsServices
{
    public class ReportsServices : IReports
    {
        private readonly AppDbContext _dbContext;
        public ReportsServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseVM> GetFinancialReport()
        {
            ResponseVM response = ResponseVM.Instance;

            var totalRevenue = await _dbContext.billings.Where(b => b.PaymentStatus == "Paid").SumAsync(b => b.Amount);
            
            var totalPendingPayments = await _dbContext.billings
            .Where(b => b.PaymentStatus == "Unpaid")
            .SumAsync(b => b.Amount);

            var monthlyEarnings = await _dbContext.billings
            .Where(b => b.PaymentStatus == "Paid")
            .GroupBy(b => new { Month = b.CreatedAt.Month })
            .Select(g => new MonthlyEarningsDto
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                Total = g.Sum(b => b.Amount)
            })
            .ToListAsync();


            var pendingInvoices = await _dbContext.billings
            .Where(b => b.PaymentStatus == "Unpaid")
            .Select(b => new PendingInvoiceDto
            {
                PatientId = b.PatientId,
                Amount = b.Amount,
                DueDate = b.CreatedAt.AddDays(30) 
            })
            .ToListAsync();

            var report = new FinancialReportDto
            {
                TotalRevenue = totalRevenue,
                TotalPendingPayments = totalPendingPayments,
                MonthlyEarnings = monthlyEarnings,
                PendingInvoices = pendingInvoices
            };

            response.data = report;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Patient Data Get Successfully";

            return response;

        }

        public async Task<ResponseVM> GetPatientCount()
        {
            ResponseVM response = ResponseVM.Instance;

            var totalPatients = await _dbContext.Patients.IgnoreQueryFilters().CountAsync();
            var activePatients = await _dbContext.Patients.CountAsync(p => !p.IsDeleted);
            var deletedPatients = await _dbContext.Patients.IgnoreQueryFilters().CountAsync(p => p.IsDeleted);

            var report = new PatientCountDto
            {
                TotalPatients = totalPatients,
                ActivePatients = activePatients,
                DeletedPatients = deletedPatients
            };

            response.data = report;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Patient Data Get Successfully";

            return response;
        }
    }
}
