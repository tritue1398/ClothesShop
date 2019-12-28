using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Clothes_Shop.Models;

namespace Clothes_Shop.Controllers
{
    public class TrangChuController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        // GET: TrangChu
        public ActionResult Index(int? page = 1)
        {
            ViewBag.Trang = page;
            List<DanhMucLoc> dm = new List<DanhMucLoc>();
            dm.Add(new DanhMucLoc() { Id = 1, Name = "Quần jean", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 2, Name = "Quần Short", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 3, Name = "Áo sơ mi", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 4, Name = "Áo thun", IsChecked = false });

            DanhMucLocList dmlist = new DanhMucLocList();
            dmlist.loc = dm;
            Session["dml"] = dmlist;
            return View(dmlist);
        }

        /*[HttpPost]
        public ActionResult Index(DanhMucLocList dml)
        {
            TempData["md"] = dml;
            
            return View(dml);
        }*/
        /*[HttpPost]
        public ActionResult Index()
        {
            var sp = (from p in db.SANPHAMs orderby p.MASP descending select p).Take(2).ToList();
            return View();
        }*/
        /*public ActionResult ChiTietSanPham()
        {
        }*/
        public ActionResult ThanhToan()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            if (kh!=null)
            {
                string tenKH = kh.HOTEN;
                ViewBag.TenKH = tenKH;
                ViewBag.SoDT = kh.DIENTHOAI;
                ViewBag.DiaChi = kh.DIACHI;
                return View();
            }
            ViewBag.TenKH = "";
            return View();
        }
        
        
    }
}