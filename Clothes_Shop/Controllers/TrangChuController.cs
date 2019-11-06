using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clothes_Shop.Models;

namespace Clothes_Shop.Controllers
{
    public class TrangChuController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        // GET: TrangChu
        public ActionResult Index()
        {
            var sp = (from p in db.SANPHAMs orderby p.MASP descending select p).Take(2).ToList();
            return View();
        }

        public ActionResult ChiTietSanPham()
        {
            return View();
        }
    }
}