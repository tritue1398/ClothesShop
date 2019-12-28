using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clothes_Shop.Models
{
    [MetadataTypeAttribute(typeof(NHANVIENMetadata))]
    public partial class NHANVIEN
    {
        internal sealed class NHANVIENMetadata
        {
            [Display(Name = "Mã nhân viên")]
            public int MANV { get; set; }

            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string HOTEN { get; set; }

            [Display(Name = "Giới tính")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public Nullable<int> MAGT { get; set; }

            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string TAIKHOAN { get; set; }

            [Display(Name = "Mật khẩu")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string MATKHAU { get; set; }

            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string EMAIL { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string DIACHI { get; set; }

            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống!")]
            public string DIENTHOAI { get; set; }
        }
    }
}