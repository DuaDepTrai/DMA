﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWCFClient.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}