using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.AppointmentDTO;
using Application.Data_TransferModels.ResponseModels;

namespace Application.Interface.Appointment
{
    public interface IAppointment
    {
        Task<ResponseVM> BookAppointment(RegisterAppointmentDTO model);
        Task<ResponseVM> GetAllAppointment();
        Task<ResponseVM> GetAppointmentbyId(int id);
        Task<ResponseVM> UpdateStatusofAppointment(int id, AppointmentStatusUpdateDTO model);
        Task<ResponseVM> CancelAppointment(int id);
    }
}
