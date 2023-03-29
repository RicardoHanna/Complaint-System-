using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
