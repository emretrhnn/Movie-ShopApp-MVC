#nullable disable

using System.ComponentModel;

namespace Movie.Business.Models.Cart
{
    //The model we will use to group over the list of type cartitemmodel
    public class CartItemGroupByModel
	{
		public int SilverScreenId { get; set; }

		public int UserId { get; set; }

		[DisplayName("Movie Name")]
		public string SilverScreenName { get; set; }

		public string TotalUnitPrice { get; set; }

		public string TotalUnitCount { get; set; }

		//All totals
		public double TotalUnitPriceValue { get; set; }

		public int TotalUnitCountValue { get; set; }

		[DisplayName("Total Price")]
		public string TotalPrice { get; set; }

		[DisplayName("Total Count")]
		public string TotalCount { get; set; }
	}
}

