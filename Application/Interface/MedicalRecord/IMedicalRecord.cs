using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.MedicalRecordDTO;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.MedicalRecord
{
    public interface IMedicalRecord
    {
        Task<ResponseVM> CreateMedicalRecord(RegisterMedicalRecordDTO model);
        Task<ResponseVM> GetRecordofPatient(int id);
        Task<ResponseVM> GetSpecificRecord(int id);
        Task<ResponseVM> UpdateRecord(int id,UpdateMedicalRecordDTO record);
        Task<ResponseVM> DeleteRecord(int id);
    }
}
