using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Movie.Business.Models;
using Movie.Business.Models.Account;
using Movie.Business.Services;

namespace MovieShopApp.WebUI.Controllers;

public class HomeController : Controller
{
    //Add service injections here
    private readonly IAccountService _accountService;

    public HomeController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(AccountRegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var result = _accountService.Register(model);
            if (result.IsSuccessful)
                return RedirectToAction(nameof(Index));//redirect to index if registration is successful

            ModelState.AddModelError("", result.Message);
        }

        return View(model);
    }

    //the user will be redirected back to the page from which they came to Login
    public IActionResult Login(string? returnUrl)
    {
        AccountLoginModel model = new AccountLoginModel()
        {
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountLoginModel model)
    {
        if (ModelState.IsValid)
        {
            UserModel userResult = new UserModel()
            {
                Role = new RoleModel()
            };

            //check the username and password in the model for activation status
            var result = _accountService.Login(model, userResult);

            //if there are active users
            if (result.IsSuccessful)
            {
                List<Claim> claims = new List<Claim>()
                {
                    //Filling the data in the desired properties of the userResult object filled in the login method into the claim list
                    new Claim(ClaimTypes.Name, userResult.UserName),

                    new Claim(ClaimTypes.Role, userResult.Role.Name),

                    new Claim(ClaimTypes.Sid, userResult.Id.ToString())//for use in basket work
                };

                //Create an identity with cookie authentication defaults over the claim list we created
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //a principal that we will use for authentication in MVC over the identity we created.
                var principal = new ClaimsPrincipal(identity);

                //Identity login process in MVC with cookie authentication defaults via principal
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        //removes the cookie created by the login action
        await HttpContext.SignOutAsync();

        return RedirectToAction(nameof(Login));

    }

    //if the user enters the unauthorized page
    public IActionResult AccessDenied()
    {
        return View("_Error", "Access is denied to this page!");
    }
    
}

