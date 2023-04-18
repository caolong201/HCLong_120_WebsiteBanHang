using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanhang.Context;
using WebsiteBanhang.Models;

namespace WebsiteBanhang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanhangEntities3 objWedBanHangEntities3 = new WebsiteBanhangEntities3();
        // GET: Payment
        public ActionResult Index()
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWedBanHangEntities3.Order.Add(objOrder);
                objWedBanHangEntities3.SaveChanges();
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.OrderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWedBanHangEntities3.OrderDetail.AddRange(lstOrderDetail);
                objWedBanHangEntities3.SaveChanges();
            }
            return View();
        }
    }
}