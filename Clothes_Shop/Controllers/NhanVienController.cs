using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Clothes_Shop.Controllers
{
    public class NhanVienController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        // GET: QuanLi
        public ActionResult SanPham()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult QuanLySanPham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.SANPHAMs.ToList().OrderBy(n=>n.MASP).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.ToList(), "MALSP", "TENLSP");
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ThemMoi(SANPHAM sp,HttpPostedFileBase fileImg)
        {
            int count = db.SANPHAMs.Count();
            if (count == 0)
                sp.MASP = 1;
            else 
                sp.MASP = db.SANPHAMs.Max(n=>n.MASP)+1;
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.ToList(), "MALSP", "TENLSP");
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            if (fileImg==null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            if(ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileImg.FileName);
                var path = Path.Combine(Server.MapPath("~/ImageClothes"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileImg.SaveAs(path);
                }
                sp.ANHSP = fileImg.FileName;
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChinhSua(int maSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TempData["fileimg"] = sp.ANHSP;
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.ToList(), "MALSP", "TENLSP",sp.MALSP);
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT",sp.MAGIOITINH);
            ViewBag.Ngay = sp.NGAYDANG;
            return View(sp);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(SANPHAM sp, HttpPostedFileBase fileImg)
        {         
            if (ModelState.IsValid)
            {
                if (fileImg != null)
                {
                    var fileName = Path.GetFileName(fileImg.FileName);
                    var path = Path.Combine(Server.MapPath("~/ImageClothes"), fileName);
                    if (System.IO.File.Exists(path))
                    { }
                    else
                    {
                        fileImg.SaveAs(path);
                    }
                    sp.ANHSP = fileImg.FileName;
                }
                else
                {
                    sp.ANHSP =(string) TempData["fileimg"];
                }
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaLSP = new SelectList(db.LOAISANPHAMs.ToList(), "MALSP", "TENLSP");
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            return RedirectToAction("QuanLySanPham");
        }

        public ActionResult HienThi(int  maSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult Xoa(int maSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(int maSP)
        {

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == maSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
        }

        public ActionResult QuanLyKhachHang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MAKH).ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult Account()
        {
            NHANVIEN nv=Session["NhanVien"] as NHANVIEN;

            return PartialView();
        }

        public ActionResult infor()
        {
            NHANVIEN nv = Session["NhanVien"] as NHANVIEN;
            return View(nv);
        }

        [HttpGet]
        public ActionResult EditInfor()
        {
            NHANVIEN nv = Session["NhanVien"] as NHANVIEN;
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT",nv.MAGT);
            return View(nv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditInfor(NHANVIEN n,FormCollection f)
        {
            
            NHANVIEN nv = Session["NhanVien"] as NHANVIEN;
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(m => m.MANV == nv.MANV);
            if(f["HoTen"].ToString()!="")
                nhanvien.HOTEN = f["HoTen"].ToString();
            if(f["Email"].ToString()!="")
                nhanvien.EMAIL = f["Email"].ToString();
            if(f["DiaChi"].ToString()!="")
                nhanvien.DIACHI = f["DiaChi"].ToString();
            if(f["DienThoai"].ToString()!="")
                nhanvien.DIENTHOAI = f["DienThoai"].ToString();
            //nhanvien.MAGT = n.MAGT;
            db.SaveChanges();
            ViewBag.MaGioiTinh = new SelectList(db.GioiTinhs.ToList(), "MaGT", "GT");
            Session["NhanVien"] = nhanvien;
            return RedirectToAction("QuanLySanPham");
        }

        [HttpGet]
        public ActionResult EditPass()
        {
            NHANVIEN nv = Session["NhanVien"] as NHANVIEN;
            
            return View(nv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPass( FormCollection f)
        {

            NHANVIEN nv = Session["NhanVien"] as NHANVIEN;
            NHANVIEN nhanvien = db.NHANVIENs.SingleOrDefault(m => m.MANV == nv.MANV);
            if(f["OldPass"].ToString()!=nv.MATKHAU)
            {
                
                ViewBag.ThongBao = "Mật khẩu cũ không đúng.";
                return View();
            }
            nhanvien.MATKHAU = f["NewPass"].ToString();
            //nhanvien.MAGT = n.MAGT;
            db.SaveChanges();
            Session["NhanVien"] = nhanvien;
            return RedirectToAction("QuanLySanPham");
        }

        public ActionResult Logout()
        {
            Session.Remove("NhanVien");
            return RedirectToAction("Index", "TrangChu");
        }

        
        public ActionResult XacNhanHoaDon(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.HOADONs.ToList().Where(n=>n.Status==false).OrderBy(n => n.MAHD).ToPagedList(pageNumber, pageSize));
        }

        
        public ActionResult XacNhanThanhToan(int maHD)
        {
            
            HOADON hd = db.HOADONs.SingleOrDefault(n => n.MAHD == maHD);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            hd.Status = true;
            db.SaveChanges();
            return RedirectToAction("XacNhanHoaDon");
        }

        public ActionResult ChiTietHoaDon(int maHD)
        {

            List<ChiTietHD> lstCthd = db.ChiTietHDs.Where(n => n.MAHD == maHD).OrderBy(n => n.MASP).ToList();
            if (lstCthd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lstCthd);
        }
    }
}