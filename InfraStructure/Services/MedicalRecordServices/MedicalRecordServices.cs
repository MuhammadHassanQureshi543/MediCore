using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.AppointmentDTO;
using Application.Data_TransferModels.MedicalRecordDTO;
using Application.Data_TransferModels.ResponseModels;
using Application.Interface.MedicalRecord;
using Azure;
using CommonOperations.Constants;
using Domain.Models.Entities.DoctorEntities;
using Domain.Models.Entities.MedicalRecordEntities;
using Domain.Models.Entities.PatientEntities;
using InfraStructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Services.MedicalRecordServices
{
    public class MedicalRecordServices : IMedicalRecord
    {
        private readonly AppDbContext _dbContext;
        public MedicalRecordServices(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<ResponseVM> CreateMedicalRecord(RegisterMedicalRecordDTO model)
        {
            ResponseVM response = ResponseVM.Instance;

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == model.PatientId);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == model.DoctorId);
            if (patient == null || doctor == null) 
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "User Not Found with this Id";
                return response;
            }

            var MedicalRecord = new MedicalRecord
            {
                PatientId = model.PatientId,
                DoctorId = model.DoctorId,
                Diagnosis = model.Diagnosis,
                Treatment = model.Treatment,
                CreatedAt = DateTime.UtcNow,
            };

            await _dbContext.medicalRecords.AddAsync(MedicalRecord);
            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Medical Record Added Successfully";
            return response;

        }

        public async Task<ResponseVM> DeleteRecord(int id)
        {
            ResponseVM response = ResponseVM.Instance;
            var record = await _dbContext.medicalRecords.FirstOrDefaultAsync(x => x.Id == id);
            if(record == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Medical Record Not Found with this Id";
                return response;
            }

            record.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Medical Record Deleted Successfully";
            return response;
        }

        public async Task<ResponseVM> GetRecordofPatient(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var records = await _dbContext.medicalRecords.Where(x=>x.PatientId == id).ToListAsync();

            if(records.Count == 0)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Medical Record Not Found";
                return response;
            }

            List<MedicalRecordDTO> recordlist = new List<MedicalRecordDTO>();

            foreach (var record in records)
            {
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == record.PatientId);
                var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == record.DoctorId);

                var medicalrecorddto = new MedicalRecordDTO
                {
                    Id = record.Id,
                    PatientId = record.PatientId,
                    PatientName = patient.FullName,
                    DoctorId = record.DoctorId,
                    DoctorName = doctor.FullName,
                    Diagnosis = record.Diagnosis,
                    Treatment = record.Treatment,
                    CreatedAt = record.CreatedAt
                };

                recordlist.Add(medicalrecorddto);
            }

            response.data = recordlist;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Medical Record Added Successfully";
            return response;

        }

        public async Task<ResponseVM> GetSpecificRecord(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var record = await _dbContext.medicalRecords.FirstOrDefaultAsync(x => x.Id == id);
            if(record == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Medical Record Not Found with this Id";
                return response;
            }

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == record.PatientId);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == record.DoctorId);

            var medicalrecorddto = new MedicalRecordDTO
            {
                Id = record.Id,
                PatientId = record.PatientId,
                PatientName = patient.FullName,
                DoctorId = record.DoctorId,
                DoctorName = doctor.FullName,
                Diagnosis = record.Diagnosis,
                Treatment = record.Treatment,
                CreatedAt = record.CreatedAt
            };

            response.data = medicalrecorddto;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Medical Record Fetched Successfully";
            return response;
        }

        public async Task<ResponseVM> UpdateRecord(int id, UpdateMedicalRecordDTO record)
        {
            ResponseVM response = ResponseVM.Instance;

            var records = await _dbContext.medicalRecords.FirstOrDefaultAsync(x => x.Id == id);
            if(records == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Medical Record Not Found with this Id";
                return response;
            }

            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == records.PatientId);
            if(patient == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "Patient Not Found with this Id";
                return response;
            }

            records.PatientId = record.PatientId;
            records.Diagnosis = record.Diagnosis;
            records.Treatment = record.Treatment;

            await _dbContext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Medical Record Updated Successfully";
            return response;
        }
    }
}
