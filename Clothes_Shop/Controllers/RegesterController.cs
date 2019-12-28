using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clothes_Shop.Controllers
{
    public class RegesterController : Controller
    {
        // GET: Regester
        ClotherShopEntities db = new ClotherShopEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            
            List<NHANVIEN> lstnv = db.NHANVIENs.ToList();
            List<KHACHHANG> lstkh = db.KHACHHANGs.ToList();
            KHACHHANG kh = new KHACHHANG();
            int count = db.KHACHHANGs.Count();
            if (count == 0)
                kh.MAKH = 1;
            else
            {
                kh.MAKH = db.KHACHHANGs.Max(n => n.MAKH) + 1;
            }
            kh.HOTEN = f["HoTen"].ToString();
            kh.TAIKHOAN = f["TaiKhoan"].ToString();
            kh.MATKHAU = f["MatKhau"].ToString();
            kh.EMAIL = f["Email"].ToString();
            kh.DIACHI = f["DiaChi"].ToString();
            kh.DIENTHOAI = f["DienThoai"].ToString();
            foreach(var item in lstnv)
            {
                if(kh.TAIKHOAN==item.TAIKHOAN)
                {
                    TempData["TonTai"] = "Tài khoản đã tồn tại";
                    ViewBag.ThongBao = "Tài khoản đã tồn tại!";
                    return View();
                }
            }
            foreach (var item in lstkh)
            {
                if (kh.TAIKHOAN == item.TAIKHOAN)
                {
                    TempData["TonTai"] = "Tài khoản đã tồn tại";
                    ViewBag.ThongBao = "Tài khoản đã tồn tại!";
                    return View();
                }
            }
            if(kh.HOTEN=="Admin")
            {
                TempData["TonTai"] = "Tài khoản đã tồn tại";
                ViewBag.ThongBao = "Tài khoản đã tồn tại!";
                return View();
            }
            db.KHACHHANGs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index", "TrangChu");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f, int ma=0)
        {
            string taiKhoan = f["txtTaiKhoan"].ToString();
            string matkhau = f.Get("txtMatKhau").ToString();
            
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN == taiKhoan && n.MATKHAU == matkhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.TAIKHOAN == taiKhoan && n.MATKHAU == matkhau);
                if (nv != null)
                {
                    if (nv.TAIKHOAN == "Admin")
                    {
                        Session["admin"] = nv;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Session["NhanVien"] = nv;
                        return RedirectToAction("QuanLySanPham", "NhanVien");
                    }
                }
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không chính xác!";
            return View();
        }
        public PartialViewResult RegesterPartial()
        {
            return PartialView();
        }

    }
}