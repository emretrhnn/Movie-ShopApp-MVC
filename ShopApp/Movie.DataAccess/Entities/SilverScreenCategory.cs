#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.DataAccess.Entities
{
    //movie and category intermediate relationship entity
    public class SilverScreenCategory
	{
		[Key]
		[Column(Order = 0)]
		public int SilverScreenId { get; set; }

		public SilverScreen Movie { get; set; }

        [Key]
		[Column(Order = 1)]
        public int CategoryId { get; set; }

		public Category Category { get; set; }
	}
}

