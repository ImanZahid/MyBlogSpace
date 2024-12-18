#nullable disable
using Microsoft.AspNetCore.Mvc;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class TagsController : MvcController
    {
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;

        public TagsController(
            ITagService tagService,
            IBlogService blogService)
        {
            _tagService = tagService;
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var list = _tagService.Query().ToList();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        protected void SetViewData()
        {
            ViewData["BlogId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _blogService.Query().Select(b => b.Record).ToList(),
                "Id", "Title");
        }

        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagModels tag)
        {
            if (tag?.Record == null)
            {
                ModelState.AddModelError("", "Tag data is required.");
                SetViewData();
                return View(tag);
            }

            if (ModelState.IsValid)
            {
                var result = _tagService.Create(tag.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = tag.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            SetViewData();
            return View(tag);
        }

        public IActionResult Edit(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            SetViewData();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagModels tag)
        {
            if (tag?.Record == null)
            {
                ModelState.AddModelError("", "Tag data is required.");
                SetViewData();
                return View(tag);
            }

            if (ModelState.IsValid)
            {
                var result = _tagService.Update(tag.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = tag.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            SetViewData();
            return View(tag);
        }

        public IActionResult Delete(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _tagService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
