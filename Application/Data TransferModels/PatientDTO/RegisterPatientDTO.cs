using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.PatientDTO
{
    public class RegisterPatientDTO
    {

        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }

}
