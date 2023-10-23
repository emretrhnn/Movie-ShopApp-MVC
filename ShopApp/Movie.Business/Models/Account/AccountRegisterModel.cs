#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Movie.Business.Models.Account
{
    //I created for the register view Model
    public class AccountRegisterModel
	{
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(15, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }



        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        [Compare("ConfirmPassword", ErrorMessage = "Password and Confirm Password must be the same!")] //can compare this property with another specified property in the database
        public string Password { get; set; }



        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(10, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}

