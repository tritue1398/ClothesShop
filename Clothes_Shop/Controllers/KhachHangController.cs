using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clothes_Shop.Controllers
{
    
    public class KhachHangController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult infor()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            return View(kh);
        }

        [HttpGet]
        public ActionResult EditInfor()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditInfor( FormCollection f)
        {

            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(m => m.MAKH== kh.MAKH);
            khachhang.HOTEN = f["HoTen"].ToString();
            khachhang.EMAIL = f["Email"].ToString();
            khachhang.DIACHI = f["DiaChi"].ToString();
            khachhang.DIENTHOAI = f["DienThoai"].ToString();
            //KHACHHANG.MAGT = n.MAGT;
            db.SaveChanges();
            
            Session["TaiKhoan"] = khachhang;
            return RedirectToAction("Index","TrangChu");
        }

        [HttpGet]
        public ActionResult EditPass()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;

            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPass(FormCollection f)
        {

            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            KHACHHANG khachhang = db.KHACHHANGs.SingleOrDefault(m => m.MAKH == kh.MAKH);
            if (f["OldPass"].ToString() != kh.MATKHAU)
            {
                TempData["MKS"] = "Mật khẩu sai";
                ViewBag.ThongBao = "Mật khẩu cũ không đúng.";
                return View();
            }
            khachhang.MATKHAU = f["NewPass"].ToString();
            //KHACHHANG.MAGT = n.MAGT;
            db.SaveChanges();
            Session["TaiKhoan"] = khachhang;
            return RedirectToAction("Index","TrangChu");
        }

        public ActionResult Logout()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("Index", "TrangChu");
        }

        public PartialViewResult InforPartial()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            return PartialView(kh);
        }

        public PartialViewResult EditInforPartial()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            return PartialView(kh);
        }
        public PartialViewResult EditPassPartial()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            return PartialView(kh);
        }

        public ActionResult TinhTrangDonHang()
        {

            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if(kh==null)
            {
                return View("Index", "TrangChu");
            }
            if (kh != null)
            {
                string tenKH = kh.HOTEN;
                ViewBag.TenKH = tenKH;
                ViewBag.SoDT = kh.DIENTHOAI;
                ViewBag.DiaChi = kh.DIACHI;
            }
            List<HOADON> lsthd = db.HOADONs.Where(n => n.MAKH == kh.MAKH && n.Status==false).ToList();
            List<ChiTietHD> lstct = new List<ChiTietHD>();
            foreach(var item in lsthd)
            {
                List<ChiTietHD> lstCthd = db.ChiTietHDs.Where(n => n.MAHD == item.MAHD).OrderBy(n => n.MASP).ToList();
                foreach(var n in lstCthd)
                {
                    lstct.Add(n);
                }
                
            }
            ViewBag.TongTien = TongTien(lstct);
            return View(lstct);
        }
        public double TongTien(List<ChiTietHD> ct)
        {
            double thanhtien=0;
            double tongtien=0;
            foreach(var item in ct)
            {
                thanhtien =((double) item.SOLUONG )* ((double)item.SANPHAM.GIABD);
                tongtien += thanhtien;
            }
            return tongtien;
        }
    }
}