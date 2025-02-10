using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.AppointmentDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.Appointment;
using Azure;
using CommonOperations.Constants;
using Domain.Models.Entities.AppointmentEntities;
using Domain.Models.Entities.PatientEntities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Services.AppointmentServices
{
    public class AppointmentServices : IAppointment
    {
        private readonly AppDbContext _dbContext;
        public AppointmentServices(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task<ResponseVM> BookAppointment(RegisterAppointmentDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == model.DoctorId);
            if(doctor == null || doctor.IsDeleted == true)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctor Not Exsist with this ID or Not Exsist";
                return response;
            }

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == model.PatientId);
            if (patient == null || patient.IsDeleted == true)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Exsist with this ID or Not Exsist";
                return response;
            }

            if(model.AppointmentDate <  DateTime.UtcNow)
            {
                response.responseCode = ResponseCode.BadRequest;
                response.responseMessage = "Please Provide Valid Date and Time";
                return response;
            }

            var allAppointment = await _dbContext.appointments.Where(x => x.DoctorId == model.DoctorId).ToListAsync();
            foreach(var a in allAppointment)
            {
                if(a.AppointmentDate == model.AppointmentDate)
                {
                    response.responseCode = ResponseCode.BadRequest;
                    response.responseMessage = "Doctor is alredy Booked on this DateTime";
                    return response;
                }
            }

            var newAppointment = new Appointment
            {
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                AppointmentDate = model.AppointmentDate,
                Status = "Pending"
            };

            await _dbContext.appointments.AddAsync(newAppointment);
            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Appointment Booked Successfully";
            return response;
        }

        public async Task<ResponseVM> GetAllAppointment()
        {
            ResponseVM response = ResponseVM.Instance;
            var Appointments = await _dbContext.appointments.ToListAsync();

            if (Appointments.Count == 0)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "No Appointment Found";
                return response;
            }

            List<AppointmentDTO> appointmentList = new List<AppointmentDTO>();

            foreach (var appointment in Appointments)
            {
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == appointment.PatientId);
                var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == appointment.DoctorId);

                var appointmentDTO = new AppointmentDTO
                {
                    Id = appointment.Id,
                    PatientId = appointment.PatientId,
                    PatientName = patient.FullName,
                    DoctorId = appointment.DoctorId,
                    DoctorName = doctor.FullName,
                    AppointmentDate = appointment.AppointmentDate,
                    status = appointment.Status
                };

                appointmentList.Add(appointmentDTO);
            }

            response.data = appointmentList;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Get Successfully";
            return response;
        }

        public async Task<ResponseVM> GetAppointmentbyId(int id)
        {
            ResponseVM response = ResponseVM.Instance;
            var Appointments = await _dbContext.appointments.FirstOrDefaultAsync(x => x.Id == id);
            if(Appointments == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Appointment Not Found with this Id";
                return response;
            }
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == Appointments.PatientId);
            if(patient == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "User Not Found with this Id";
                return response;
            }
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == Appointments.DoctorId);
            if (doctor == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctor Not Found with this Id";
                return response;
            }
            var appointment = new AppointmentDTO
            {
                Id = Appointments.Id,
                PatientId = Appointments.PatientId,
                PatientName = patient.FullName,
                DoctorId = Appointments.DoctorId,
                DoctorName = doctor.FullName,
                AppointmentDate = Appointments.AppointmentDate,
                status = Appointments.Status
            };

            response.data = appointment;
            response.responseCode = ResponseCode.NotFound;
            response.responseMessage = "Doctor Not Found with this Id";
            return response;
        }

        public async Task<ResponseVM> UpdateStatusofAppointment(int id, AppointmentStatusUpdateDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var Appointments = await _dbContext.appointments.FirstOrDefaultAsync(x => x.Id == id);
            if(Appointments == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Appointment Not Found with this Id";
                return response;
            }

            Appointments.Status = model.status;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Status Update Successfully";
            return response;
        }

        public async Task<ResponseVM> CancelAppointment(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var appointments = await _dbContext.appointments.FirstOrDefaultAsync(x => x.Id == id);
            if(appointments == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Appointment Not Found with this Id";
                return response;
            }

            _dbContext.appointments.Remove(appointments);
            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Appointment Delte Successfully";
            return response;
        }
    }
}
