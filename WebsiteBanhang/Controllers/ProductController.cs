using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanhang.Context;

namespace WebsiteBanhang.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanhangEntities3 objwebsiteBanhangEntities3 = new WebsiteBanhangEntities3();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objwebsiteBanhangEntities3.Product.Where(n=>n.Id == Id).FirstOrDefault();


            return View(objProduct);
        }
    }
}