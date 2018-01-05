using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class CategoryBySaleModel
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public double Quantity { get; set; }
    }
}