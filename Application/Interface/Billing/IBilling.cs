using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.BillingDTO;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.Billing
{
    public interface IBilling
    {
        Task<ResponseVM> GenerateBill(GenerateBillDTO model);
        Task<ResponseVM> GetAllBill();
        Task<ResponseVM> GetSpecificBill(int id);
        Task<ResponseVM> UpdateBillStatus(int id, UpdateStatusBillDTO model);
        Task<ResponseVM> DeleteBill(int id);
    }
}
