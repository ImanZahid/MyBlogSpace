using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BLL.Services
{
    public interface ITagService
    {
        Service Create(Tag tag);
        Service Update(Tag tag);
        Service Delete(int id);
        IQueryable<TagModels> Query();
    }

    public class TagService : Service, ITagService
    {
        public TagService(DB db) : base(db)
        {
        }

        public Service Create(Tag tag)
        {
            try
            {
                _db.Tags.Add(tag);
                _db.SaveChanges();
                return Success("Tag created successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error creating tag: {ex.Message}");
            }
        }

        public Service Update(Tag tag)
        {
            var existingTag = _db.Tags.FirstOrDefault(t => t.Id == tag.Id);
            if (existingTag == null)
            {
                return Error("Tag not found.");
            }

            try
            {
                existingTag.Name = tag.Name;

                _db.SaveChanges();
                return Success("Tag updated successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error updating tag: {ex.Message}");
            }
        }

        public Service Delete(int id)
        {
            var tag = _db.Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return Error("Tag not found.");
            }

            try
            {
                _db.Tags.Remove(tag);
                _db.SaveChanges();
                return Success("Tag deleted successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error deleting tag: {ex.Message}");
            }
        }

        public IQueryable<TagModels> Query()
        {
            return _db.Tags
                .Select(t => new TagModels { Record = t });
        }
    }
}
