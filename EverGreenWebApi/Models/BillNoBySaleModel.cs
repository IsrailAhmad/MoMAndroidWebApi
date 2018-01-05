using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class BillNoBySaleModel
    {
        public string BillNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime BillDate { get; set; }
        public double Total { get; set; }
    }
}