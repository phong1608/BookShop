using Bulky.Models;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage ="Điền thông tin của bộ lọc")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Điền thứ tụ hiển thị")]
        public int? DisplayOrder { get; set; }
        
    }
}
