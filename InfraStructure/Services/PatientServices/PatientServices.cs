using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.PatientDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.Patient;
using Domain.Models.Entities.PatientEntities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.Data_TransferModels.ResponseModels;
using CommonOperations.Constants;
using Azure;

namespace InfraStructure.Services.PatientServices
{
    public class PatientServices : IPatient
    {
        private readonly AppDbContext _dbcontext;
        public PatientServices(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<ResponseVM> CreatePatient(RegisterPatientDTO patient)
        {
            ResponseVM response = ResponseVM.Instance;
            var register = new Patient
            {
                FullName = patient.FullName,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
                ContactNumber = patient.ContactNumber,
                Address = patient.Address,
                CreatedAt = DateTime.Now
            };

            await _dbcontext.Patients.AddAsync(register);
            await _dbcontext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Patient Register Successful";

            return response;

        }

        public async Task<ResponseVM> GetAllPatient()
        {
            ResponseVM response = ResponseVM.Instance;

            var Patient = await _dbcontext.Patients.Select(patient => new PatientDTO
            {
                Id = patient.Id,
                FullName = patient.FullName,
                Gender = patient.Gender,
                Address = patient.Address,
                ContactNumber = patient.ContactNumber,
                DateOfBirth = patient.DateOfBirth,
            }).ToListAsync();

            response.data = Patient;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Fetched Successful";

            return response;
        }

        public async Task<ResponseVM> GetPatientbyId(int id)
        {
            ResponseVM response = ResponseVM.Instance;
            var Patient = await _dbcontext.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
            if (Patient == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Found with this Id";
                return response;
            }

            var data = new PatientDTO
            {
                Id = Patient.Id,
                FullName = Patient.FullName,
                Gender = Patient.Gender,
                Address = Patient.Address,
                ContactNumber = Patient.ContactNumber,
                DateOfBirth = Patient.DateOfBirth,
            };

            response.data = data;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Fetched Successful";

            return response;
        }

        public async Task<ResponseVM> UpdatePatient(int id, RegisterPatientDTO patient)
        {
            ResponseVM response = ResponseVM.Instance;

            var Patient = await _dbcontext.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (Patient == null) 
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Found with this Id";
                return response;
            }

            Patient.FullName = patient.FullName;
            Patient.Gender = patient.Gender;
            Patient.Address = patient.Address;
            Patient.ContactNumber = patient.ContactNumber;
            Patient.DateOfBirth = patient.DateOfBirth;

            await _dbcontext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Patient Update Successfuly";

            return response;
        }

        public async Task<ResponseVM> DeletePatient(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var Patient = await _dbcontext.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (Patient == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Found with this Id";
                return response;
            }

            Patient.IsDeleted = true;

            await _dbcontext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Patient Delte Successfully";
            return response;
        }
    }
}
