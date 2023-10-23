#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.Business.Models
{
    public class SilverScreenModel : Record //models should also inherit from record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Description { get; set; }

        [Range(1900, 2024)]
        public string Year { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Director { get; set; }

        [Range(1, 10000, ErrorMessage = "{0} must be zero or positive!")]
        public double Price { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Actors { get; set; }

        [DisplayName("Category")]
        public List<CategoryModel> CategoriesDisplay { get; set; }

        [DisplayName("Category")]
        public string CategoryNameDisplay { get; set; }

        [DisplayName("Categories")]
        public List<int> CategoryIds { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }


    }
}

