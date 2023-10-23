#nullable disable

namespace Movie.Business.Models.Cart
{
	public class CartItemModel //we do not inherit from record because we will not do crud operations
    {
		public int Id { get; set; }

		public int UserId { get; set; }

		public double Price { get; set; }

		public string SilverScreenName { get; set; }

		public CartItemModel(int silverScreenId, int userId, double price, string silverScreenName)
		{
			Id = silverScreenId;
			UserId = userId;
			Price = price;
			SilverScreenName = silverScreenName;
		}
	}
}

