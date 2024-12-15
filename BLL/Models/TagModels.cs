#nullable disable

using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class TagModels
    {
        public Tag Record { get; set; }

        [DisplayName("Tag ID")]
        public int TagId => Record.Id;

        [DisplayName("Tag Name")]
        public string TagName => Record.Name;
    }
}
