#nullable disable

using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.DataAccess.Entities
{
	public class Category : Record
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public List<SilverScreenCategory> SilverScreenCategories { get; set; }//for many to many relationships
    }
}

