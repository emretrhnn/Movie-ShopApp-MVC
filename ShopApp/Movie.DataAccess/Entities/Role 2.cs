#nullable disable


using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.DataAccess.Entities
{
	public class Role : Record
	{
		[Required]
        [StringLength(10)]
        public string Name { get; set; }

		public List<User> Users { get; set; } //for 1 to many relationships

    }
}

