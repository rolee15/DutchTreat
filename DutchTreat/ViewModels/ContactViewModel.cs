using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(5)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Message too long")]
        public string? Message { get; set; }
    }
}
