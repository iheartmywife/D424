using System.ComponentModel.DataAnnotations;

namespace helpmepickmymain.Models.ViewModels
{
    public class AdminLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
