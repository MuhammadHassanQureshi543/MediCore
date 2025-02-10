using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities.AppointmentEntities;
using Domain.Models.Entities.BillingEntities;
using Domain.Models.Entities.DoctorEntities;
using Domain.Models.Entities.MedicalRecordEntities;
using Domain.Models.Entities.PatientEntities;
using Domain.Models.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalRecord> medicalRecords { get; set; }
        public DbSet<Billing> billings { get; set; }
        public DbSet<Appointment> appointments { get; set; }
    }
}
