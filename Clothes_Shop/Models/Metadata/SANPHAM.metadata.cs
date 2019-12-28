using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clothes_Shop.Models
{
    [MetadataTypeAttribute(typeof(SANPHAMMetadata))]
    public partial class SANPHAM
    {
        internal sealed class SANPHAMMetadata
        {
            [Display(Name = "Mã SP")]
            public int MASP { get; set; }
            [Display(Name = "Tên SP")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TENSP { get; set; }
            [Display(Name = "Ảnh")]
            
            public string ANHSP { get; set; }
            [Display(Name = "Size")]
            public string SIZE { get; set; }
            [Display(Name = "Màu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string MAU { get; set; }
            [Display(Name = "Thương hiệu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string THUONGHIEU { get; set; }
            [Display(Name = "Chất liệu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string CHATLIEU { get; set; }
            [Display(Name = "Mô tả")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string MOTA { get; set; }
            
            [Display(Name = "Giá BĐ")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<double> GIABD { get; set; }
            
            [Display(Name = "Ngày Đăng")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]        
            public Nullable<System.DateTime> NGAYDANG { get; set; }
           
            [Display(Name = "Loại SP")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<int> MALSP { get; set; }
            [Display(Name = "Giới tính")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<int> MAGIOITINH { get; set; }

            [Display(Name = "SL Tồn")]
            public Nullable<int> SoLuongTon { get; set; }
        }
    }
}