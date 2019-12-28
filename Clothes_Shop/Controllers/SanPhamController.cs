using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Clothes_Shop.Controllers
{
    public class SanPhamController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        // GET: SanPham
        public PartialViewResult SanPhamPartial(int?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            List<SANPHAM> lstSanPham = db.SANPHAMs.ToList();
            return PartialView(lstSanPham.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ChiTietSanPham(int MaSP=0)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            return View(sp);
        }
        [HttpPost]
        public ActionResult ChiTietSanPham()
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MASP == 1);
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if (kh == null)
            {
                TempData["LoginMessage"] = "Bạn cần đăng nhập.";
                ViewBag.TK = "Bạn cần đăng nhập!";
                return View(sp);
            }
            return RedirectToAction("Index", "TrangChu");
        }
       
    }
}