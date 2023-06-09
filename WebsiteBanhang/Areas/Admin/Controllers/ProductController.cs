﻿
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebsiteBanhang.Context;
using static WebsiteBanhang.Common;

namespace WebsiteBanhang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        WebsiteBanhangEntities3 objWedBanHangEntities3 = new WebsiteBanhangEntities3();
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstProduct = objWedBanHangEntities3.Product.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objWedBanHangEntities3.Product.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }




        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [ValidateInput (false)]

         [HttpPost]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avartar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.Now;
                    objWedBanHangEntities3.Product.Add(objProduct);
                    objWedBanHangEntities3.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Details(int id)
           
        {
            var objProduct = objWedBanHangEntities3.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
           
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objWedBanHangEntities3.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWedBanHangEntities3.Product.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWedBanHangEntities3.Product.Remove(objProduct);
            objWedBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objWedBanHangEntities3.Product.Where(n => n.Id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product objProduct)
        {
            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + extension;
                objProduct.Avartar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
            }
            objProduct.UpdatedOnUtc = DateTime.Now;
            objWedBanHangEntities3.Entry(objProduct).State = EntityState.Modified;
            objWedBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var lstCat = objWedBanHangEntities3.Category.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);

            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "id", "Name");

            var lstBrand = objWedBanHangEntities3.Brand.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);

            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "id", "Name");

            List<ProductTypeId> lstProductType = new List<ProductTypeId>();

            ProductTypeId objProductType = new ProductTypeId();
            objProductType.Id = 1;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductTypeId();
            objProductType.Id = 2;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "id", "Name");
        }

    }


}