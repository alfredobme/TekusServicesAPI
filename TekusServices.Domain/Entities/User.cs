using System.ComponentModel.DataAnnotations;

namespace TekusServices.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string Password { get; set; } = string.Empty;
    }
}
