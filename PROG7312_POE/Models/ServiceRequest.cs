using System;
using System.ComponentModel.DataAnnotations;

namespace PROG7312_POE.Models
{
    public class ServiceRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ReferenceCode { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";

        public int Priority { get; set; } 

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
