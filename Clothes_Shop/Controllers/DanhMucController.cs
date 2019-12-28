using Clothes_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Clothes_Shop.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc
        ClotherShopEntities db = new ClotherShopEntities();
       
        public PartialViewResult DanhMucPartial()
        {
            var lstDM = db.LOAISANPHAMs.ToList();
            return PartialView(lstDM);
        }
        
        public ViewResult SanPhamTheoDM(int maDM=0, int maGT=0, int?page=1)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            LOAISANPHAM loaiSP = db.LOAISANPHAMs.SingleOrDefault(n => n.MALSP == maDM);
            if(loaiSP==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SANPHAM> lstSP = db.SANPHAMs.Where(n => n.MALSP == maDM && n.MAGIOITINH==maGT).OrderBy(n=>n.GIABD).ToList();
            if(lstSP.Count==0)
            {
                ViewBag.Sach = "Không có sản phẩm nào trong loại này.";
            }
            return View(lstSP.OrderBy(n => n.TENSP).ToPagedList(pageNumber, pageSize));
        }
        
        /*[HttpGet]
        public ActionResult LocSP()
        {
            List<DanhMucLoc> dm = new List<DanhMucLoc>();
            dm.Add(new DanhMucLoc() { Id = 1, Name = "Quần Jean", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 2, Name = "Quần Short", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 3, Name = "Áo sơ mi", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 4, Name = "Áo thun", IsChecked = false });

            DanhMucLocList dmlist = new DanhMucLocList();
            dmlist.loc = dm;
            return View(dmlist);
        }

        [HttpPost]
        public ActionResult LocSP(DanhMucLocList dml)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dml.loc)
            {
                if (item.IsChecked)
                {
                    sb.Append(item.Name + ",");
                }
            }
            ViewBag.select = sb.ToString();
            return View(dml);
        }*/
        /*[HttpGet]
        public ActionResult Loc()
        {
            List<DanhMucLoc> dm = new List<DanhMucLoc>();
            dm.Add(new DanhMucLoc() { Id = 1, Name = "Quần Jean", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 2, Name = "Quần Short", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 3, Name = "Áo sơ mi", IsChecked = false });
            dm.Add(new DanhMucLoc() { Id = 4, Name = "Áo thun", IsChecked = false });

            DanhMucLocList dmlist = new DanhMucLocList();
            dmlist.loc = dm;
            return View(dmlist);
        }*/
        
        [HttpPost]
        public ActionResult LocSP(DanhMucLocList dml, FormCollection f)
        {
            int giamin=0, giamax=0;

            if (f["giaMin"].ToString() != "")
            {
                giamin = Convert.ToInt32(f["giaMin"].ToString());
            }
            else giamin = 0;

            if (f["giaMax"].ToString() != "")
            {
                giamax = Convert.ToInt32(f["giaMax"].ToString());
            }
            else giamax=0;
            List<DanhMucLoc> lstLoc = Session["loc"] as List<DanhMucLoc>;
            List<SANPHAM> lstSP=new List<SANPHAM>();
            if(lstLoc==null)
            {
                lstLoc = new List<DanhMucLoc>();
                Session["loc"] = lstLoc;
            }
            foreach (var item in dml.loc)
            {
                if (item.IsChecked)
                {
                    List<LOAISANPHAM> lstLoaiSP = db.LOAISANPHAMs.Where(n => n.TENLSP == item.Name).ToList();
                    if (lstLoaiSP == null)
                    {
                        Response.StatusCode = 404;
                        return null;
                    }
                    if (lstLoaiSP.Count != 0)
                    {
                        foreach (LOAISANPHAM ls in lstLoaiSP)
                        {
                            List<SANPHAM> lstSP1 = db.SANPHAMs.Where(n => n.MALSP == ls.MALSP).OrderBy(n => n.GIABD).ToList();
                            foreach (SANPHAM s in lstSP1)
                            {
                                lstSP.Add(s);
                            }
                        }
                    }
                }
            }
            if(giamax>0)
            {
                if (lstSP.Count != 0)
                {
                    lstSP = lstSP.Where(n => n.GIABD >= giamin && n.GIABD <= giamax).OrderBy(n => n.GIABD).ToList();
                }
                else
                {
                    lstSP=db.SANPHAMs.Where(n => n.GIABD >= giamin && n.GIABD <= giamax).OrderBy(n => n.GIABD).ToList();
                }
            }
            Session["SP"] = lstSP;
            return View(dml);
        }

        public PartialViewResult SPTheoDMPartial()
        {
            //List<SANPHAM> lstSP = db.SANPHAMs.Where(n => n.MALSP == 1).OrderBy(n => n.GIABD).ToList();
            List<SANPHAM> lstSP = Session["SP"] as List<SANPHAM>;
            return PartialView(lstSP);
        }

        public PartialViewResult DMLoc()
        {
            DanhMucLocList dml = Session["dml"] as DanhMucLocList;

            return PartialView(dml);
        }
    }

}