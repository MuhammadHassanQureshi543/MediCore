using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data_TransferModels.UserDTO
{
    public class UserUpdateDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // "Admin", "Doctor", "Nurse", "Receptionist", "Patient"
    }
}
