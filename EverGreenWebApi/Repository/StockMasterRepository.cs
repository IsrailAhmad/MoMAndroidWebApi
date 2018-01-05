using EverGreenWebApi.DataAccess;
using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class StockMasterRepository : IStockMasterRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockSummaryMasterModel> StockSummary(StockMasterModel model)
        {
            List<StockSummaryMasterModel> data = new List<StockSummaryMasterModel>();
            MultipleDataBase dataAccess = new MultipleDataBase();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var user = context.userdatabasemasters.Where(u => u.DataBaseId == model.DataBaseId).FirstOrDefault();

                data = dataAccess.StockSummary(model, user.DataBase).ToList();
            }

            return data;
        }
    }
}