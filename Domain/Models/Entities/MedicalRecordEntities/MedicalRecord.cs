﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities.MedicalRecordEntities
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string Diagnosis { get; set; }
        [Required]
        public string Treatment { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}
