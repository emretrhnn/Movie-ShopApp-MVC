#nullable disable


using System.ComponentModel.DataAnnotations;
using Core.Records.Bases;

namespace Movie.DataAccess.Entities
{
    public class User : Record
	{
		[Required]
		[StringLength(20)]
		public string UserName { get; set; }

		[Required]
		[StringLength(10)]
		public string Password { get; set; }

		public string Email { get; set; }

		public bool IsActive { get; set; }

		public int RoleId { get; set; }//1 to many

		public Role Role { get; set; }
	}
}

