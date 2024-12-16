#nullable disable

using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BLL.Models
{
    public class BlogModels
    {
        public Blog Record { get; set; }

        [DisplayName("Blog Title")]
        public string Title => Record.Title;

        [DisplayName("Content")]
        public string Content => Record.Content;

        [DisplayName("Rating")]
        public string? Rating => Record.Rating.HasValue ? Record.Rating.Value.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) : "";

        [DisplayName("Publish Date")]
        public string PublishDate => Record.PublishDate.ToString("dd.MM.yyyy");

        [DisplayName("Blog ID")]
        public int BlogId => Record.Id;

        [DisplayName("User")]
        public string UserName => Record.User?.UserName;

        [DisplayName("UserId")]
        public int? UserId => Record.UserId;

        public string Tags => string.Join("<br>", Record.BlogTags?.Select(bt => bt.Tag?.Name));


        [DisplayName("Tags")]
        public List<int> TagIds
        {
            get => Record.BlogTags?.Select(t => t.TagId).ToList();
            set => Record.BlogTags = value.Select(v => new BlogTag() { TagId = v }).ToList();
        }
    }
}