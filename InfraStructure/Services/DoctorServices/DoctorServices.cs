using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.DoctorDTO;
using Application.Data_TransferModels.PatientDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.Doctor;
using Azure;
using CommonOperations.Constants;
using Domain.Models.Entities.DoctorEntities;
using Domain.Models.Entities.PatientEntities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Services.DoctorServices
{
    public class DoctorServices : IDoctor
    {
        private readonly AppDbContext _dbContext;
        public DoctorServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseVM> DeleteDoctor(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x=>x.Id == id);
            if(doctor == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctors Not Exsist";
                return response;
            }

            doctor.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Doctor Delted Successfully";
            return response;
        }

        public async Task<ResponseVM> GetAllDoctors()
        {
            ResponseVM response = ResponseVM.Instance;

            var doctors = await _dbContext.Doctors.Where(u => !u.IsDeleted).Select(doctor => new DoctorDTO
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialization = doctor.Specialization,
                ContactNumber = doctor.ContactNumber,
                Email = doctor.Email
            }).ToListAsync();

            if(doctors == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctors Not Exsist";
                return response;
            }

            response.data = doctors;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Get Successfully";
            return response;
        }

        public async Task<ResponseVM> GetDoctorbyID(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if(doctor == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctors Not Exsist with this Id";
                return response;
            }

            var Doctor = new DoctorDTO
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialization = doctor.Specialization,
                ContactNumber = doctor.ContactNumber,
                Email = doctor.Email
            };

            response.data = Doctor;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Doctor Add Successfully";

            return response;
        }

        public async Task<ResponseVM> RegisterDoctor(RegisterDoctorDTO Doctor)
        {
            ResponseVM response = ResponseVM.Instance;

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Email == Doctor.Email);

            if (doctor != null) 
            {
                response.responseCode = ResponseCode.BadRequest;
                response.responseMessage = "Doctor Alredy Exsist with This Email";
                return response;
            }

            var createDoctor = new Doctor
            {
                FullName = Doctor.FullName,
                Specialization = Doctor.Specialization,
                Email = Doctor.Email,
                ContactNumber = Doctor.ContactNumber,
                CreatedAt = DateTime.Now
            };

            await _dbContext.Doctors.AddAsync(createDoctor);
            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Doctor Add Successfully";

            return response;
        }

        public async Task<ResponseVM> UpdateDoctor(int id,DoctorUpdateDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var exdoctor = _dbContext.Doctors.FirstOrDefault(x=>x.Id == id);
            if (exdoctor == null) 
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Doctor Not Exsist With this Id";
                return response;
            }

            exdoctor.FullName = model.FullName;
            exdoctor.ContactNumber = model.ContactNumber;
            exdoctor.Specialization = model.Specialization;
            exdoctor.Email = model.Email;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Doctor Updated Successfully";

            return response;
        }
    }
}
