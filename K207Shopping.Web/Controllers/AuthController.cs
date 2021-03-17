using K207Shopping.Web.Data;
using K207Shopping.Web.Models;
using K207Shopping.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<K207User> _userManager;
        private readonly SignInManager<K207User> _signManager;
        private readonly ShoppingContext _context;

        public AuthController(ShoppingContext context, SignInManager<K207User> signManager,
            UserManager<K207User> userManager)
        {
            _context = context;
            _signManager = signManager;
            _userManager = userManager;
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            K207User appUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Email yanlışdı");
                return View(loginVM);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await 
                _signManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Sifre yanlışdı");
                return View(loginVM);
            }

            return RedirectToAction("index","home");
        }



        public  IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            K207User newUser = new K207User()
            {
                UserName=registerVM.Email,
                Email = registerVM.Email,
                Firstname=registerVM.Firstname,
                Lastname=registerVM.Lastname

            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (result.Succeeded)
            {
                IdentityResult res = await _userManager.AddToRoleAsync(newUser, "User");
                return RedirectToAction(nameof(Login));
            }
            return View(registerVM);
        }
    }
}
