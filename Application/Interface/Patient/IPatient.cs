using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.PatientDTO;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.Patient
{
    public interface IPatient
    {
        Task<ResponseVM> CreatePatient(RegisterPatientDTO patient);
        Task<ResponseVM> GetAllPatient();
        Task<ResponseVM> GetPatientbyId(int id);
        Task<ResponseVM> UpdatePatient(int id, RegisterPatientDTO patient);
        Task<ResponseVM> DeletePatient(int id);
    }
}
