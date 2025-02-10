using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.MedicalRecordDTO
{
    public class MedicalRecordDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
