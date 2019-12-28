using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clothes_Shop.Models
{
    [MetadataTypeAttribute(typeof(KHACHHANGMetadata))]
    public partial class KHACHHANG
    {
        internal sealed class KHACHHANGMetadata
        {
            public int MAKH { get; set; }
            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HOTEN { get; set; }
            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TAIKHOAN { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string MATKHAU { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string EMAIL { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string DIACHI { get; set; }
            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string DIENTHOAI { get; set; }
        }
    }
}