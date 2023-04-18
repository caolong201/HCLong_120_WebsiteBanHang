using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanhang.Context;

namespace WebsiteBanhang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        WebsiteBanhangEntities3 objWedBanHangEntities3 = new WebsiteBanhangEntities3();
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstoder = new List<Order>();
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
                lstoder = objWedBanHangEntities3.Order.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstoder = objWedBanHangEntities3.Order.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstoder = lstoder.OrderByDescending(n => n.Id).ToList();
            return View(lstoder.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Details(int id)

        {
            var objoder = objWedBanHangEntities3.Order.Where(n => n.Id == id).FirstOrDefault();
            return View(objoder);

        }

    }
}