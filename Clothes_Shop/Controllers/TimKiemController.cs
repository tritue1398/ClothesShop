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
    public class TimKiemController : Controller
    {
        ClotherShopEntities db = new ClotherShopEntities();
        
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string tuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = tuKhoa;
            List<SANPHAM> lstSP = db.SANPHAMs.Where(n => n.TENSP.Contains(tuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstSP.Count==0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào.";
                return View(lstSP.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
            }
           
            return View(lstSP.OrderBy(n=>n.TENSP).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(string tuKhoa, int? page)
        {
            ViewBag.TuKhoa = tuKhoa;
            List<SANPHAM> lstSP = db.SANPHAMs.Where(n => n.TENSP.Contains(tuKhoa)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            if (lstSP.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào.";
                return View(lstSP);
            }

            return View(lstSP.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
        }
    }
}