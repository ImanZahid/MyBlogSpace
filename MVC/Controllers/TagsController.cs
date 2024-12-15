#nullable disable
using Microsoft.AspNetCore.Mvc;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using System.Linq;

namespace MVC.Controllers
{
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

        // GET: Tags
        public IActionResult Index()
        {
            var list = _tagService.Query().ToList();
            return View(list);
        }

        // GET: Tags/Details/5
        public IActionResult Details(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound(); // 404 if not found
            }
            return View(item);
        }

        // Set ViewData for dropdown lists
        protected void SetViewData()
        {
            ViewData["BlogId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _blogService.Query().Select(b => b.Record).ToList(),
                "Id", "Title");
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Tags/Create
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

        // GET: Tags/Edit/5
        public IActionResult Edit(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound(); // 404 if not found
            }

            SetViewData();
            return View(item);
        }

        // POST: Tags/Edit
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

        // GET: Tags/Delete/5
        public IActionResult Delete(int id)
        {
            var item = _tagService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound(); // 404 if not found
            }

            return View(item);
        }

        // POST: Tags/Delete
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
