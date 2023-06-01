using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppExam.Models;
using WebAppExam.ViewModels;

namespace WebAppExam.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "member" });
            return Json("Done");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if(!ModelState.IsValid) return View();
            AppUser user = new AppUser()
            {
                FullName = registerVm.FullName, 
                UserName = registerVm.Username,
                Email = registerVm.Email
            };
            var result = await _userManager.CreateAsync(user,registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description); return View();
                }
            }
            await _userManager.AddToRoleAsync(user, "member");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) return View();
            if (loginVm.UserNameOrEmail.Contains("@"))
            {
                var user = await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
                if (user == null) return NotFound();

                var result =  await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid Credentails");
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var user = await _userManager.FindByNameAsync(loginVm.UserNameOrEmail);
                if (user == null) return NotFound();

                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid Credentails");
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
