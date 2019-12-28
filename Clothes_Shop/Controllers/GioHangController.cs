using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clothes_Shop.Controllers
{

    public class GioHangController : Controller
    {
        // GET: GioHang
        ClotherShopEntities db = new ClotherShopEntities();
       
        #region Giỏ hàng
        public List<Gio> layGioHang()
        {
            List<Gio> lstGioHang = Session["GioHang"] as List<Gio>;
            if(lstGioHang==null)
            {
                lstGioHang = new List<Gio>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        
        
        [HttpPost]
        public ActionResult MultipleCommand( int ma, string sl, string Command)
        {
            if (Command == "Search")
            {
                return View();
                // do search here
            }
            else
              return View();
           
        }

        public ActionResult ThemGioHang(int ma, string url, FormCollection f, string command)
        {
            /*KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if(kh==null)
            {
                TempData["LoginMessage"] = "Vui lòng đăng nhập";
                ViewBag.DangNhap = "Quý khách vui lòng đăng nhập!";
                return RedirectToAction("ChiTietSanPham","SanPham",new { @masp=ma});
            }*/
            if (command == "Thêm vào giỏ hàng")
            {
                if (int.Parse(f["txtsl"].ToString()) == 0)
                {
                    return Redirect(url);
                }
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == ma);
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                int slt = (int)sp.SoLuongTon;

                List<Gio> lstGio = layGioHang();
                Gio sanpham = lstGio.Find(n => n.maSP == ma);
                if (sanpham == null)
                {


                    sanpham = new Gio(ma);
                    sanpham.soLuong = int.Parse(f["txtsl"].ToString());
                    TempData["SLT"] = slt - sanpham.soLuong;
                    lstGio.Add(sanpham);
                    return Redirect(url);
                }
                else
                {

                    sanpham.soLuong = sanpham.soLuong + int.Parse(f["txtsl"].ToString());
                    TempData["SLT"] = slt - sanpham.soLuong;
                    return Redirect(url);
                }
            }
            else
            {
                return RedirectToAction("MuaNgay", new { @ma = ma, @sl = f["txtsl"].ToString() });
            }
        }

        
        public ActionResult MuaNgay(int ma, string sl)
        {
            /*KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if(kh==null)
            {
                TempData["LoginMessage"] = "Vui lòng đăng nhập";
                ViewBag.DangNhap = "Quý khách vui lòng đăng nhập!";
                return RedirectToAction("ChiTietSanPham","SanPham",new { @masp=ma});
            }*/
            //int ma = mam;
            int slm = int.Parse(sl.ToString());
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == ma);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            int slt = (int)sp.SoLuongTon;

            List<Gio> lstGio = layGioHang();
            Gio sanpham = lstGio.Find(n => n.maSP == ma);
            if (sanpham == null)
            {
                sanpham = new Gio(ma);
                sanpham.soLuong = slm;
                TempData["SLT"] = slt - sanpham.soLuong;
                lstGio.Add(sanpham);
                return RedirectToAction("GioHang");
            }
            else
            {

                sanpham.soLuong = sanpham.soLuong + slm;
                TempData["SLT"] = slt - sanpham.soLuong;
                return RedirectToAction("GioHang");
            }
        }

       
        public ActionResult CapNhatGio(int ma, FormCollection f)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == ma);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Gio> lstGio = layGioHang();
            Gio sanpham = lstGio.Find(n => n.maSP == ma);
            if (sanpham != null)
            {
                sanpham.soLuong = sanpham.soLuong + int.Parse(f["txtsl"].ToString());
            }
            return View("GioHang");
        }
        public ActionResult XoaGioHang(int ma)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == ma);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Gio> lstGio = layGioHang();
            Gio sanpham = lstGio.Find(n => n.maSP == ma);
            if (sanpham != null)
            {
                lstGio.RemoveAll(n => n.maSP == sanpham.maSP);
                TempData["SLT"] = sp.SoLuongTon;
            }
            if(lstGio.Count==0)
            {
                return RedirectToAction("Index","TrangChu");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if (kh != null)
            {
                string tenKH = kh.HOTEN;
                ViewBag.TenKH = tenKH;
                ViewBag.SoDT = kh.DIENTHOAI;
                ViewBag.DiaChi = kh.DIACHI;
            }
           
            if (Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.TongTien = TongTien();
            List<Gio> lstGio = layGioHang();
            return View(lstGio);
        }
        private int TongSoLuong()
        {
            int tong = 0;
            List<Gio> lstGio = Session["GioHang"] as List<Gio>;
            if(lstGio!=null)
            {
                tong = lstGio.Sum(n => n.soLuong);
            }
            return tong;
        }
        private double TongTien()
        {
            double tongTien = 0;
            List<Gio> lstGio = Session["GioHang"] as List<Gio>;
            if (lstGio != null)
            {
                tongTien = lstGio.Sum(n => n.ThanhTien);
            }
            
            return tongTien;
        }
        public PartialViewResult GioHangPartial()
        {
            if(TongSoLuong()==0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }

        #endregion
        #region Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            if(Session["TaiKhoan"]==null||Session["TaiKhoan"].ToString()=="")
            {
                return RedirectToAction("Login", "Regester");
            }
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            int count = db.HOADONs.Count();
            HOADON hd = new HOADON();
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            List<Gio> gio = layGioHang();
            if(count==0)
            {
                hd.MAHD = 1;
            }
            else
            {
                hd.MAHD = db.HOADONs.Max(n => n.MAHD) + 1;
            }
           
            hd.MAKH = kh.MAKH;
            hd.NGAYDAT = DateTime.Now;
            hd.TONGTIEN = TongTien();
            hd.Status = false;
            db.HOADONs.Add(hd);

            foreach(var item in gio)
            {
                ChiTietHD cthd = new ChiTietHD();
                cthd.MAHD = hd.MAHD;
                cthd.MASP = item.maSP;
                cthd.SOLUONG = item.soLuong;
                cthd.DONGIA = item.donGia;

                SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == item.maSP);
                sp.SoLuongTon -= item.soLuong;
                db.ChiTietHDs.Add(cthd);
            }         
            db.SaveChanges();
            Session.Remove("GioHang");
            return RedirectToAction("Index", "TrangChu");
        }
        #endregion

        public PartialViewResult ChiTietGioHang()
        {
            List<Gio> lstGioHang = Session["GioHang"] as List<Gio>;
            return PartialView(lstGioHang);
        }


    }
}