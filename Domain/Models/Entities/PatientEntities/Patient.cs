using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities.PatientEntities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [Phone]
        public string ContactNumber { get; set; }
        [Required, MaxLength(250)]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
