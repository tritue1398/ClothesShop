using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clothes_Shop.Models;

namespace Clothes_Shop.Models
{
    public class Gio
    {
        ClotherShopEntities db = new ClotherShopEntities();
        public int maSP { get; set; }
        public string tenSP { get; set; }
        public string image { get; set; }
        public double donGia { get; set; }
        public int soLuong { get; set; }
        public double ThanhTien
        {
            get { return soLuong * donGia; }
        }
        //tạo giỏ hàng
        public Gio(int masp)
        {
            maSP = masp;
            SANPHAM sp = db.SANPHAMs.Single(n => n.MASP == maSP);
            tenSP = sp.TENSP;
            image = sp.ANHSP;
            donGia = (double) sp.GIABD;
            soLuong = 1;
        }
    }
}