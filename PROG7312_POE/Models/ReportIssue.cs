using System.ComponentModel.DataAnnotations;

namespace PROG7312_POE.Models
{
    public class ReportIssue
    {
        public Guid Id { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "Please provide more details (at least 10 characters).")]
        public string Description { get; set; } = string.Empty;

        public IFormFile? AttachmentFile { get; set; }
        public string? AttachmentPath { get; set; }

        public DateTime ReportedAt { get; set; }
        public string Status { get; set; } = "Pending";
    }
}