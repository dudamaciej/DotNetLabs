using System;
using System.Collections.Generic;
using System.Linq;
using MainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MainProject.ViewModel;

namespace MainProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMngr, SignInManager<IdentityUser> signInMngr)
        {

            userManager = userMngr;
            signInManager = signInMngr;

            IdentitySeedData.EnsurePopulated(userMngr).Wait();

        }

        [AllowAnonymous]
        public ViewResult Login(string returnedURL)
        {

            return View(new LoginModel
            {
                ReturnedURL = returnedURL
            });

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(userName: loginModel.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnedURL ?? "/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Wrong nickname or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnedURL = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnedURL);
        }
    }

}
