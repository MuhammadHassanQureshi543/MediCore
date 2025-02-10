using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.DoctorDTO;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.Doctor
{
    public interface IDoctor
    {
        Task<ResponseVM> RegisterDoctor(RegisterDoctorDTO Doctor);
        Task<ResponseVM> GetAllDoctors();
        Task<ResponseVM> GetDoctorbyID(int id);
        Task<ResponseVM> UpdateDoctor(int id, DoctorUpdateDTO model);
        Task<ResponseVM> DeleteDoctor(int id);
    }
}
