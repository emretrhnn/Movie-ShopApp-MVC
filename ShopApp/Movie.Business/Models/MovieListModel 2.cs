#nullable disable

using Movie.DataAccess.Entities;

namespace Movie.Business.Models
{
    //the model I will use for listing
    public class MovieListModel
	{
		public List<SilverScreen> silverScreens { get; set; }
	}
}

