#nullable disable

using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.Business.Models
{
    //the model I will use for the category
    public class CategoryModel : Record
	{
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
    }
}

