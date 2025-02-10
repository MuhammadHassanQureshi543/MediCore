using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.Reports
{
    public interface IReports
    {
        Task<ResponseVM> GetPatientCount();
        Task<ResponseVM> GetFinancialReport();
    }
}
