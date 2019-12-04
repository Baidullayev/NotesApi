using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gala.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gala.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> UserMgr { get; }
        private SignInManager<AppUser> SignInMgr { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            UserMgr = userManager;
            SignInMgr = signInManager;
        }
       
        public async Task<IActionResult> Logout()
        {
            await SignInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //var result = await SignInMgr.PasswordSignInAsync("TestUser", "Test123!", false, false);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("Index", "Home");

            //}
            //else
            //{
            //    ViewBag.Result = "result is: " + result.ToString();
            //}
            //return View();
            if (model.Password != null && model.Username != null)
            {
                if (ModelState.IsValid)
                {
                    var result = await SignInMgr.PasswordSignInAsync(model.Username,
                       model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else { ViewBag.Message = "Введен неверный логин или пароль"; }
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            
            return View(model);
        }
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { Username = model.Username };
                IdentityResult result = await UserMgr.CreateAsync(user, "Test123!");

                if (result.Succeeded)
                {
                    await SignInMgr.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}