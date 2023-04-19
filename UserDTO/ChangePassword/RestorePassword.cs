using System.ComponentModel.DataAnnotations;

namespace Models.RestorePassword
{
    public class RestorePassword
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string RepeatPassword { get; set; }
    }
}
