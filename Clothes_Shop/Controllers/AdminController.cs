using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Clothes_Shop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ClotherShopEntities db = new ClotherShopEntities();
        public ActionResult Index()
        {
            return RedirectToAction("QuanLyNhanVien");
        }
        #region Nhân viên
        public ActionResult QuanLyNhanVien(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.NHANVIENs.ToList().OrderBy(n => n.MANV).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChiTiet(int maNV)
        {
            NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.MANV == maNV);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nv);
        }

        [HttpGet]
        public ActionResult Xoa(int maNV)
        {
            NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.MANV == maNV);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nv);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int maNV)
        {

            NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.MANV == maNV);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NHANVIENs.Remove(nv);
            db.SaveChanges();
            return RedirectToAction("QuanLyNhanVien");
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ThemMoi(NHANVIEN nv)
        {
            int count = db.NHANVIENs.Count();
            if (count ==0)
                nv.MANV = 1;
            else
                nv.MANV = db.NHANVIENs.Max(n=>n.MANV)+1;
            
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            
            if (ModelState.IsValid)
            {
       
                db.NHANVIENs.Add(nv);
                db.SaveChanges();
            }
            return View();
        }
        #endregion

        #region khách hàng
        public ActionResult QuanLyKhachHang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region Thống kê hóa đơn
        public ActionResult DaThanhToan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.HOADONs.ToList().Where(n=>n.Status==true).OrderBy(n => n.MAHD).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChuaThanhToan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.HOADONs.ToList().Where(n => n.Status == false).OrderBy(n => n.MAHD).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChiTietHoaDon(int maHD)
        {
           
            List<ChiTietHD> lstCthd = db.ChiTietHDs.Where(n => n.MAHD == maHD).OrderBy(n => n.MASP).ToList();
            
            if (lstCthd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HOADON hd = db.HOADONs.SingleOrDefault(n => n.MAHD == maHD);
            if (hd.Status == true)
            {
                TempData["status"] = "true";
            }
            else
                TempData["status"] = "false"; 
            return View(lstCthd);
        }
       
        [HttpGet]
        public ActionResult XoaHoaDon(int maHD)
        {
            List<ChiTietHD> lstCthd = db.ChiTietHDs.Where(n => n.MAHD == maHD).OrderBy(n => n.MASP).ToList();
            if (lstCthd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lstCthd);
        }

        [HttpPost, ActionName("XoaHoaDon")]
        public ActionResult XacNhanXoaHoaDon(int maHD)
        {
            List<ChiTietHD> lstCthd = db.ChiTietHDs.Where(n => n.MAHD == maHD).ToList();
            if(lstCthd!=null)
            {
                foreach(var item in lstCthd)
                {
                    db.ChiTietHDs.Remove(item);
                    db.SaveChanges();
                }
            }
            HOADON hd = db.HOADONs.SingleOrDefault(n => n.MAHD == maHD);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.HOADONs.Remove(hd);
            db.SaveChanges();
            if(TempData["status"].ToString()=="true")
                return RedirectToAction("DaThanhToan");
            else
                return RedirectToAction("ChuaThanhToan");

        }
        #endregion

        #region Thống kê sản phẩm
        public ActionResult ThongKeSanPham()
        {
            List<ThongKeSanPham> lstSP = new List<ThongKeSanPham>();

            var result = db.ChiTietHDs.GroupBy(o => o.MASP)
                  .Select(g => new { masp = g.Key, total = g.Sum(i => i.SOLUONG) });

            foreach (var group in result)
            {
                SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == group.masp);
                ThongKeSanPham tk = new ThongKeSanPham();
                tk.maSP = group.masp;
                tk.tenSP = sp.TENSP;
                tk.soLuong =(int) group.total;
                tk.anhSP = sp.ANHSP;
                tk.size = sp.SIZE;
                tk.giaBan =(double) sp.GIABD;
                lstSP.Add(tk);
            }
            return View(lstSP.OrderByDescending(m=>m.soLuong).ToList());
        }
        #endregion

        public PartialViewResult Account()
        {
            NHANVIEN nv = Session["admin"] as NHANVIEN;

            return PartialView();
        }

        public ActionResult infor()
        {
            NHANVIEN nv = Session["admin"] as NHANVIEN;
            return View(nv);
        }

        [HttpGet]
        public ActionResult EditInfor()
        {
            NHANVIEN nv = Session["admin"] as NHANVIEN;
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT", nv.MAGT);
            return View(nv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditInfor(NHANVIEN n, FormCollection f)
        {

            NHANVIEN nv = Session["admin"] as NHANVIEN;
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(m => m.MANV == nv.MANV);
            if (f["HoTen"].ToString() != "")
                nhanvien.HOTEN = f["HoTen"].ToString();
            if (f["Email"].ToString() != "")
                nhanvien.EMAIL = f["Email"].ToString();
            if (f["DiaChi"].ToString() != "")
                nhanvien.DIACHI = f["DiaChi"].ToString();
            if (f["DienThoai"].ToString() != "")
                nhanvien.DIENTHOAI = f["DienThoai"].ToString();
            //nhanvien.MAGT = n.MAGT;
            db.SaveChanges();
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            Session["admin"] = nhanvien;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditPass()
        {
            NHANVIEN nv = Session["admin"] as NHANVIEN;

            return View(nv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPass(FormCollection f)
        {

            NHANVIEN nv = Session["admin"] as NHANVIEN;
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(m => m.MANV == nv.MANV);
            if (f["OldPass"].ToString() != nv.MATKHAU)
            {

                ViewBag.ThongBao = "Mật khẩu cũ không đúng.";
                return View();
            }
            nhanvien.MATKHAU = f["NewPass"].ToString();
            //nhanvien.MAGT = n.MAGT;
            db.SaveChanges();
            Session["admin"] = nhanvien;
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Remove("admin");
            return RedirectToAction("Index", "TrangChu");
        }
    }
}