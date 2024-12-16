﻿using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBlogService
    {
        Service Create(Blog blog);
        Service Update(Blog blog);
        Service Delete(int id);
        IQueryable<BlogModels> Query();
    }

    public class BlogService : Service, IBlogService
    {
        public BlogService(DB db) : base(db)
        {
        }

        // Create a new blog
        public Service Create(Blog blog)
        {
            try
            {
                _db.Blogs.Add(blog);
                _db.SaveChanges();
                return Success("Blog created successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error creating blog: {ex.Message}");
            }
        }

        // Update an existing blog
        public Service Update(Blog blog)
        {
            var existingBlog = _db.Blogs.Include(b => b.BlogTags).FirstOrDefault(b => b.Id == blog.Id);
            if (existingBlog == null)
            {
                return Error("Blog not found.");
            }

            try
            {
                _db.BlogTags.RemoveRange(existingBlog.BlogTags);
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.Rating = blog.Rating;
                existingBlog.PublishDate = blog.PublishDate;
                existingBlog.UserId = blog.UserId;
                existingBlog.BlogTags = blog.BlogTags;

                _db.Blogs.Update(existingBlog);
                _db.SaveChanges();
                return Success("Blog updated successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error updating blog: {ex.Message}");
            }
        }

        // Delete a blog by ID
        public Service Delete(int id)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return Error("Blog not found.");
            }

            try
            {
                _db.Blogs.Remove(blog);
                _db.SaveChanges();
                return Success("Blog deleted successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error deleting blog: {ex.Message}");
            }
        }

        // Query to get all blogs or filter as needed
        public IQueryable<BlogModels> Query()
        {
            return _db.Blogs
                .Include(b => b.User)
                .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
                .Select(b => new BlogModels { Record = b });
        }
    }
}