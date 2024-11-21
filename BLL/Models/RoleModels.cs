#nullable disable

using BLL.DAL;
using System.Collections.Generic;
using System.ComponentModel;

namespace BLL.Models
{
    public class RoleModels
    {
        public Role Record { get; set; }

        [DisplayName("Role ID")]
        public int RoleId => Record.Id;

        [DisplayName("Role Name")]
        public string RoleName => Record.Name;

        [DisplayName("Users")]
        public ICollection<UsersModels> Users
        {
            get
            {
                var usersList = new List<UsersModels>();
                if (Record.Users != null)
                {
                    foreach (var user in Record.Users)
                    {
                        usersList.Add(new UsersModels { Record = user });
                    }
                }
                return usersList;
            }
        }
    }
}
