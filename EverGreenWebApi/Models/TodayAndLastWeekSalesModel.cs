using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class TodayAndLastWeekSalesModel
    {
        public double TodayTotalSales { get; set; }
        public double LastWeekTotalSales { get; set; }
    }
}