using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Điền tiêu đề của sách")]
        [DisplayName("Tựa đề")]
        public string? Title { get; set; }
        [Required(ErrorMessage ="Điền mô tả sách")]
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        [Required(ErrorMessage ="Điền tên tác giả")]
        [DisplayName("Tác Giả")]
        public string? Author { get; set; }
        public double? ListPrice { get; set; }
        [Required(ErrorMessage ="Điền giá sách")]
        [DisplayName("Giá sách")]
        public double? Price { get; set; }
        [DisplayName("Mua sỉ 50+ ")]
        public double? Price50 { get; set; }
        [DisplayName("Mua sỉ 100+")]
        public double? Price100 { get; set; }
        public string? ImageUrl { get; set; }
        
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }


    }
}
