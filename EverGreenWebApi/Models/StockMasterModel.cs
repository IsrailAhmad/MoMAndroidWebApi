using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Models
{
    public class StockMasterModel
    {
        public string Location { get; set; }
        public int DepartmentId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DataBaseId { get; set; }
        public int LoginID { get; set; }


    }
}