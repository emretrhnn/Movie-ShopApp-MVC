using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Business.Models;
using Movie.Business.Models.Cart;
using Movie.Business.Services;
using Newtonsoft.Json;

namespace MovieShopApp.WebUI.Controllers
{
    //only users will be able to make cart transactions
    [Authorize(Roles = "User")]
	public class CartController : Controller
	{
        //since we need the name of the product and the unit price via the product id,
        //I inject the product service via the constructor
        private readonly ISilverScreenService _silverScreenService;

        public CartController(ISilverScreenService silverScreenService)
        {
            _silverScreenService = silverScreenService;
        }

        int userId;//id of the logged in user
        const string SESSIONKEY = "cart";//the key we will use to store browser based data in session

        //action to list the elements in the cart
        public IActionResult GetCart()
        {
            userId = Convert.ToInt32(User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.Sid).Value);
            var cart = GetSession(userId);
            return View("CartGroupBy", GroupBy(cart));
        }

        //allows the cart element created using the userId with the product name and unit price via
        //the id sent as a parameter to be added to the cart.
        public IActionResult AddToCart(int id)
        {
            userId = Convert.ToInt32(User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.Sid).Value);

            SilverScreenModel movie = _silverScreenService.Query().SingleOrDefault(m => m.Id == id);

            var cart = GetSession(userId);
            var cartItem = new CartItemModel(id, userId, movie.Price, movie.Name);
            cart.Add(cartItem);
            SetSession(cart);
            TempData["Message"] = $"{cart.Count(c => c.Id == id)} \"{movie.Name}\" Added To Cart Successfully.";
            return RedirectToAction(nameof(GetCart));
        }

        //deletes product added to cart
        public IActionResult RemoveFromCart(int silverScreenId)
        {
            userId = Convert.ToInt32(User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.Sid).Value);
            var cart = GetSession(userId);
            var cartItem = cart.SingleOrDefault(c => c.Id == silverScreenId);

            if (cartItem is not null)
            {
                cart.Remove(cartItem);
                SetSession(cart);
                TempData["Message"] = $"\"{cartItem.SilverScreenName}\" Removed From Cart Successfully.";
            }

            return RedirectToAction(nameof(GetCart));
        }

        //cleans the cart
        public IActionResult ClearCart()
        {
            userId = Convert.ToInt32(User.Claims.SingleOrDefault(u => u.Type == ClaimTypes.Sid).Value);
            var cart = GetSession(userId);
            cart.RemoveAll(c => c.UserId == userId);
            SetSession(cart);
            TempData["Message"] = "Cart Cleared Successfully.";
            return RedirectToAction(nameof(GetCart));
        }

        //method that returns the list of elements in the user cart from session according to the userId parameter
        private List<CartItemModel> GetSession(int userId)
        {
            var cart = new List<CartItemModel>();
            var cartJson = HttpContext.Session.GetString(SESSIONKEY);

            if(!string.IsNullOrWhiteSpace(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItemModel>>(cartJson);
                cart = cart.Where(c => c.UserId == userId).ToList();
            }
            return cart;
        }

        private void SetSession(List<CartItemModel> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SESSIONKEY, cartJson);
        }


        //returns the list in the model type by grouping by product id user id and product name according
        //to the basket element list sent as a parameter
        private List<CartItemGroupByModel> GroupBy(List<CartItemModel> cart)
        {
            var cartGroupBy = (from c in cart group c by new { c.Id, c.UserId, c.SilverScreenName } into cGroupBy 

                select new CartItemGroupByModel() 
                {
                    SilverScreenId = cGroupBy.Key.Id, 

                    UserId = cGroupBy.Key.UserId, 
                    SilverScreenName = cGroupBy.Key.SilverScreenName, 

                    TotalUnitPriceValue = cGroupBy.Sum(cgb => cgb.Price), 

                    TotalUnitCountValue = cGroupBy.Count(), 

                    TotalPrice = cGroupBy.Sum(cgb => cgb.Price).ToString("C2"), 

                    TotalCount = cGroupBy.Count() + " " + (cGroupBy.Count() == 1 ? "unit" : "units") 
                }).ToList();

           
            var totalItem = new CartItemGroupByModel(); 

            totalItem.SilverScreenName = "Total"; 
            totalItem.TotalPrice = cartGroupBy.Sum(cgb => cgb.TotalUnitPriceValue).ToString("C2"); 

            totalItem.TotalCount = cartGroupBy.Sum(cgb => cgb.TotalUnitCountValue).ToString(); 

            if (totalItem.TotalCount == "0") 
                totalItem.TotalCount = "No units";
            else if (totalItem.TotalCount == "1")
                totalItem.TotalCount += " unit";
            else
                totalItem.TotalCount += " units";

            cartGroupBy.Add(totalItem); 

            return cartGroupBy; 
        }
    }
}


    

