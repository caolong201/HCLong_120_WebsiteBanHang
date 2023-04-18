using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanhang.Context;

namespace WebsiteBanhang.Controllers
{
    public class CategoryController : Controller
   
    {
        WebsiteBanhangEntities3 objwebsiteBanhangEntities3 = new WebsiteBanhangEntities3();
        // GET: Category
        public ActionResult Index()
        {
            var lstCatelogy = objwebsiteBanhangEntities3.Category.ToList();
            return View(lstCatelogy);
        }
        
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objwebsiteBanhangEntities3.Product.Where(n=>n.CategoryId == Id).ToList();
            return View(listProduct);
        }
    }
}