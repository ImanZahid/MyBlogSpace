#nullable disable

using BLL.DAL;
using System.Collections.Generic;
using System.ComponentModel;

namespace BLL.Models
{
    public class UsersModels
    {
        public User Record { get; set; }

        [DisplayName("User ID")]
        public int UserId => Record.Id;

        [DisplayName("Username")]
        public string UserName => Record.UserName;

        [DisplayName("Password")]
        public string Password => Record.Password;

        [DisplayName("Is Active")]
        public bool IsActive => Record.IsActive;

        [DisplayName("Role ID")]
        public int? RoleId => Record.RoleId;

        [DisplayName("Role")]
        public string RoleName => Record.Role?.Name; 

        [DisplayName("Blogs")]
        public ICollection<BlogModels> Blogs
        {
            get
            {
                var blogModals = new List<BlogModels>();
                if (Record.Blogs != null)
                {
                    foreach (var blog in Record.Blogs)
                    {
                        blogModals.Add(new BlogModels { Record = blog });
                    }
                }
                return blogModals;
            }
        }
    }
}
