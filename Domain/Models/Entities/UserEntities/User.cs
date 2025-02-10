using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities.UserEntities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength (100)]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } // Admin, Doctor, Nurse, Receptionist, Patient
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

}
