using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clothes_Shop.Models
{
    public class ThongKeSanPham
    {
        [Display(Name = "Mã sản phẩm")]
        public int maSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string tenSP { get; set; }
                
        [Display(Name = "Size")]
        public string size { get; set; }

        [Display(Name = "Giá bán")]
        public double giaBan { get; set; }

        [Display(Name = "Ảnh")]
        public string anhSP { get; set; }

        [Display(Name = "Số lượng")]
        public int soLuong { get; set; }

        
    }
}