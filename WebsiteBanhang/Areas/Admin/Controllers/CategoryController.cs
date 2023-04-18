using PagedList;
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebsiteBanhang.Context;

namespace WebsiteBanhang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBanhangEntities3 objWedBanHangEntities3 = new WebsiteBanhangEntities3();

        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
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
                lstCategory = objWedBanHangEntities3.Category.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objWedBanHangEntities3.Category.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var lstCategory = objWedBanHangEntities3.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(lstCategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWedBanHangEntities3.Category.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);

        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {

            var objCategory = objWedBanHangEntities3.Category.Where(n => n.Id == objCate.Id).FirstOrDefault();
            objWedBanHangEntities3.Category.Remove(objCategory);
            objWedBanHangEntities3.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            var objCategory = objWedBanHangEntities3.Category.Where(n => n.Id == id).FirstOrDefault();

            return View(objCategory);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Category objCategory)
        {
            if (ModelState.IsValid)
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + extension;
                    objCategory.Avartar = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                }
                objCategory.UpdatedOnUtc = DateTime.Now;
                objWedBanHangEntities3.Entry(objCategory).State = System.Data.Entity.EntityState.Modified;
                objWedBanHangEntities3.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avartar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objWedBanHangEntities3.Category.Add(objCategory);
                    objWedBanHangEntities3.SaveChanges();
                    return  RedirectToAction("Index");
                }

                catch (Exception)

                {
                    return RedirectToAction("Index");
                }
            }

            return View(objCategory);
        }

    }
}
    