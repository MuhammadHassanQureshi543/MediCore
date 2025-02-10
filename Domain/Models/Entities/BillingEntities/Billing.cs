using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities.BillingEntities
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // Paid, Unpaid
        [Required]
        public DateTime CreatedAt { get; set; }
    }

}
