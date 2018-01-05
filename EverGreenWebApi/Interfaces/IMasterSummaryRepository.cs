using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Interfaces
{
    public interface IMasterSummaryRepository: IDisposable
    {
        IEnumerable<CashierBySaleModel> CashierBySale(SalesSummaryModel model);
        IEnumerable<CategoryBySaleModel> CategoryBySale(SalesSummaryModel model);
        IEnumerable<BillNoBySaleModel> BillNoBySale(SalesSummaryModel model);
        IEnumerable<TodayAndLastWeekSalesModel> TodayAndLastWeekSale(SalesSummaryModel model);
    }
}