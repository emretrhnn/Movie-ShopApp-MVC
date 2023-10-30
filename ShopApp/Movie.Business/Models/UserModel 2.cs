#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.Business.Models
{
    //the model I will use for user
    public class UserModel : Record
	{
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(250, ErrorMessage = "{0} must be maximum {1} characters!")]
        [EmailAddress(ErrorMessage = "{0} must be in e-mail format!")]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Role")]
        public int RoleId { get; set; }

        public RoleModel Role { get; set; } = new RoleModel();
    }
}

