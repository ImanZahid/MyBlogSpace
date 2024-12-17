#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class UsersController : MvcController
    {
        private readonly IUsersService _userService;
        private readonly IRoleService _roleService;

        public UsersController(
            IUsersService userService,
            IRoleService roleService
        )
        {
            _userService = userService;
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            var list = _userService.Query().ToList();
            return View(list);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsersModels user)
        {
            if (ModelState.IsValid)
            {
                var model = _userService.Query()
                    .FirstOrDefault(u => u.Record.UserName == user.UserName
                                      && u.Record.Password == user.Password
                                      && u.Record.IsActive);

                if (model is not null)
                {
                    List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Record.UserName),
                new Claim(ClaimTypes.Role, model.Record.Role.Name)
            };

                    ClaimsIdentity identity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }


        public IActionResult Details(int id)
        {
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Record.Id", "Name");
        }

        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsersModels user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Create(user.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsersModels user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Update(user.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
