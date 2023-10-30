using Core.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie.Business.Models;
using Movie.Business.Services;

namespace MovieShopApp.WebUI.Controllers
{
    [Authorize]//those who are logged into the system can call all actions
    public class SilverScreenController : Controller
	{
		private readonly ISilverScreenService _silverScreenService;//a service area is defined in the controller to perform product related tasks
        private readonly ICategoryService _categoryService;

        public SilverScreenController(ISilverScreenService silverScreenService, ICategoryService categoryService)
        {
            _silverScreenService = silverScreenService;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]//only admin can login
        public IActionResult Index()
        {
            return View(new MovieListModel()
            {
                silverScreens = _silverScreenService.GetAll()//all products
            });
            
        }

        public IActionResult List()//all users
        {
            return View(new MovieListModel()
            {
                silverScreens = _silverScreenService.GetAll()//all products
            });

        }

        [Authorize(Roles = "Admin")]//only admin can login
        public IActionResult Create()
        {
            //I send all categories to the listbox for the user with viewbag
            ViewBag.Categories = new MultiSelectList(_categoryService.Query().ToList(), "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Admin")]//only admin can login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SilverScreenModel screen)
        {
            if (ModelState.IsValid)//If there is no validation error in the information coming from the user, the insertion process
            {
                Result result = _silverScreenService.Add(screen);

                if (result.IsSuccessful)//our message if the insertion is successful
                {
                    TempData["Message"] = result.Message;

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            //If the addition is not successful, we will come back to the create view and we will get the categories again.
            ViewBag.Categories = new MultiSelectList(_categoryService.Query().ToList(), "Id", "Name", screen.CategoryIds);
            return View(screen);
        }

        [Authorize(Roles = "Admin")]//only admin can login
        public IActionResult Edit(int id)
        {
            //I send the information of the product selected according to id to the view
            SilverScreenModel movie = _silverScreenService.Query().FirstOrDefault(m => m.Id == id);

            if( movie is null)
                return View("_Error", "Movie not found!");

            //I send the categories according to their ids as selected
            ViewData["Categories"] = new MultiSelectList(_categoryService.GetList(), "Id", "Name", movie.CategoryIds);

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SilverScreenModel movie)
        {
            Result result = _silverScreenService.Update(movie);

            if (result.IsSuccessful)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
                
            ModelState.AddModelError("", result.Message);
            
            ViewData["Categories"] = new MultiSelectList(_categoryService.GetList(), "Id", "Name", movie.CategoryIds);
            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            //retrieve the product according to id with query method
            SilverScreenModel movie = _silverScreenService.Query().SingleOrDefault(m => m.Id == id);
            if( movie is null)
                return View("_Error", "Movie not found!");

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")] 
        public IActionResult DeleteConfirmed(int id)
        {
            //I'm deleting the product we pulled by id with the query method
            Result result = _silverScreenService.Delete(id); 

            TempData["Message"] = result.Message; 

            return RedirectToAction(nameof(Index)); 
        }

        public IActionResult Details(int id)
        {
            //Product details by id
            SilverScreenModel movie = _silverScreenService.Query().SingleOrDefault(b => b.Id == id); 
            if (movie is null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}

