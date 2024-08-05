using System.ComponentModel.DataAnnotations;

namespace BW2_Team6.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(30)]
        public required string Name { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
