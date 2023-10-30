#nullable disable


using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.DataAccess.Entities
{
	public class SilverScreen : Record
	{
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(1900, 2024)]
        public string Year { get; set; }

        [StringLength(50)]
        public string Director { get; set; }

        public string ImageUrl { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string Actors { get; set; }

        public List<SilverScreenCategory> SilverScreenCategories { get; set; }//for many to many relationships
    }
}

