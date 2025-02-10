using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.MedicalRecordDTO
{
    public class UpdateMedicalRecordDTO
    {
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}
