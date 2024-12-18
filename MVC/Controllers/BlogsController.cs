#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    public class BlogsController : MvcController
    {
        private readonly IBlogService _blogService;
        private readonly ITagService _TagService;
        private readonly IUsersService _userService;

        public BlogsController(
            IBlogService blogService
            , ITagService TagService
            , IUsersService userService
        )
        {
            _blogService = blogService;
            _TagService = TagService;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var list = _blogService.Query().ToList();
            return View(list);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        protected void SetViewData()
        {
            ViewData["UserId"] = new SelectList(
                _userService.Query().Select(u => u.Record).ToList(),
                "Id", "UserName");
            var tags = _TagService.Query().ToList();
            var multiSelect = new MultiSelectList(tags, "TagId", "TagName");
            ViewBag.TagIds = multiSelect;
        }

        [Authorize(Roles = "admin,user")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,user")]
        public IActionResult Create(BlogModels blog)
        {
            if (blog?.Record == null)
            {
                ModelState.AddModelError("", "Blog data is required.");
                SetViewData();
                return View(blog);
            }

            if (ModelState.IsValid)
            {
                var result = _blogService.Create(blog.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            SetViewData();
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            SetViewData();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(BlogModels blog)
        {
            if (blog?.Record == null)
            {
                ModelState.AddModelError("", "Blog data is required.");
                SetViewData();
                return View(blog);
            }

            if (ModelState.IsValid)
            {
                var result = _blogService.Update(blog.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = blog.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            SetViewData();
            return View(blog);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var item = _blogService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _blogService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
