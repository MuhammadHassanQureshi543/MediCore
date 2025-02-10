using System.Runtime.CompilerServices;
using Application.Interface.Appointment;
using Application.Interface.Billing;
using Application.Interface.Doctor;
using Application.Interface.MedicalRecord;
using Application.Interface.Patient;
using Application.Interface.Reports;
using Application.Interface.User;
using InfraStructure.Services.AppointmentServices;
using InfraStructure.Services.BillingServices;
using InfraStructure.Services.DoctorServices;
using InfraStructure.Services.MedicalRecordServices;
using InfraStructure.Services.PatientServices;
using InfraStructure.Services.ReportsServices;
using InfraStructure.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
namespace UserSphere.InjectionServices
{
    public static class CustomServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserServices>();
            services.AddScoped<IPatient, PatientServices>();
            services.AddScoped<IDoctor, DoctorServices>();
            services.AddScoped<IAppointment, AppointmentServices>();
            services.AddScoped<IMedicalRecord, MedicalRecordServices>();
            services.AddScoped<IBilling, BillingServices>();
            services.AddScoped<IReports, ReportsServices>();
        }
    }
}
