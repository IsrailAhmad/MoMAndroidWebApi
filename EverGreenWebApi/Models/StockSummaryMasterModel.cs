using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class StockSummaryMasterModel
    {
        public int DepartmentId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public double TotalRecieved { get; set; }
        public double TotalIssue { get; set; }
        public double OpeningBalance { get; set; }
        public double TotalReceivedTill { get; set; }
        public double TotalIssuedTill { get; set; }
        public double ClosingBalance { get; set; }
        public double Price { get; set; }
        public string UOM { get; set; }
        public string GroupName { get; set; }

    }
}