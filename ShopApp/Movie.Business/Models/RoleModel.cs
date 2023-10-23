#nullable disable

using System.ComponentModel;
using Core.Records.Bases;

namespace Movie.Business.Models
{
	public class RoleModel : Record
	{
		[DisplayName("Role")]
		public string Name { get; set; }
	}
}

