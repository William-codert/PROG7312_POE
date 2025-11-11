using System.ComponentModel.DataAnnotations;

namespace MVC_POE.Models
{
    public class ReportIssuesForm
    {
        [Key]
        public Guid FormId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage ="Location is required")]
        public string Location {  get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string? MediaAttachment { get; set; }

    }
}
