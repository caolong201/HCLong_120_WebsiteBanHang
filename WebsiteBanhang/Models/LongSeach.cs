using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanhang.Context;

namespace WebsiteBanhang.Models
{
    public class LongSeach
    {
        WebsiteBanhangEntities3 objWedBanHangEntities3 = new WebsiteBanhangEntities3();
        public List<Product> SearchByKey(string key)
        {
            return objWedBanHangEntities3.Product.SqlQuery("Select * From Product Where Name like '%" + key + "%'").ToList();
        }
    }
}