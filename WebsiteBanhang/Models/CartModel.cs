﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanhang.Context;

namespace WebsiteBanhang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}