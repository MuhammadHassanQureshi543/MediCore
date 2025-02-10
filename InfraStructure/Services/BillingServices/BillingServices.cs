using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.BillingDTO;
using Application.Data_TransferModels.MedicalRecordDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.Billing;
using Application.Interface.Patient;
using Azure;
using CommonOperations.Constants;
using Domain.Models.Entities.BillingEntities;
using Domain.Models.Entities.PatientEntities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Services.BillingServices
{
    public class BillingServices : IBilling
    {
        private readonly AppDbContext _dbContext;
        public BillingServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseVM> DeleteBill(int id)
        {
            ResponseVM response = ResponseVM.Instance;
            var bill = await _dbContext.billings.FirstOrDefaultAsync(x => x.Id == id);
            if(bill == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Bill Not Found with this Id";
                return response;
            }

            _dbContext.billings.Remove(bill);
            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Bill Got Deleted ";
            return response;
        }

        public async Task<ResponseVM> GenerateBill(GenerateBillDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == model.PatientId);
            if(patient == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Found with this Id";
                return response;
            }

            var Bill = new Billing
            {
                PatientId = model.PatientId,
                Amount = model.Amount,
                PaymentStatus = model.PaymentStatus,
                CreatedAt = DateTime.UtcNow
            };

            var billdto = new PrintBillDTO
            {
                PatientId = Bill.PatientId,
                PatientName = patient.FullName,
                Amount = Bill.Amount,
                PaymentStatus = Bill.PaymentStatus,
                CreatedAt = Bill.CreatedAt
            };

            await _dbContext.billings.AddAsync(Bill);
            await _dbContext.SaveChangesAsync();

            response.data = billdto;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Bill Generate Successfully";
            return response;
        }

        public async Task<ResponseVM> GetAllBill()
        {
            ResponseVM response = ResponseVM.Instance;

            var bills = await _dbContext.billings.ToListAsync();

            if(bills.Count == 0)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "No Bill Found";
                return response;
            }

            List<BillDTO> recordlist = new List<BillDTO>();

            foreach (var bill in bills)
            {
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == bill.PatientId);

                var billdto = new BillDTO
                {
                    Id = bill.Id,
                    PatientId = bill.PatientId,
                    PatientName = patient.FullName,
                    Amount = bill.Amount,
                    PaymentStatus = bill.PaymentStatus,
                    CreatedAt = bill.CreatedAt
                };

                recordlist.Add(billdto);
            }

            response.data = recordlist;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Bill Data Get Successfully";
            return response;
        }

        public async Task<ResponseVM> GetSpecificBill(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var bill = await _dbContext.billings.FirstOrDefaultAsync(x => x.Id == id);
            if(bill == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Bill Not Exsist with this Id";
                return response;
            }

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == bill.PatientId);
            
            var PrintBill = new BillDTO
            {
                Id = bill.Id,
                PatientId = bill.PatientId,
                PatientName = patient.FullName,
                Amount = bill.Amount,
                PaymentStatus = bill.PaymentStatus,
                CreatedAt = bill.CreatedAt
            };

            response.data = PrintBill;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Bill Data Get Successfully";
            return response;
        }

        public async Task<ResponseVM> UpdateBillStatus(int id,UpdateStatusBillDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var bill = await _dbContext.billings.FirstOrDefaultAsync(x => x.Id == id);
            if(bill == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Bill Not Exsist with this Id";
                return response;
            }

            bill.PaymentStatus = model.Status;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Bill Update Successfully";
            return response;
        }
    }
}
