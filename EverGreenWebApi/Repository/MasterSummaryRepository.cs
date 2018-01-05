using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Web;
using System.IO;
using System.Drawing;
using EverGreenWebApi.DataAccess;

namespace EverGreenWebApi.Repository
{
    public class MasterSummaryRepository : IMasterSummaryRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashierBySaleModel> CashierBySale(SalesSummaryModel model)
        {
            List<CashierBySaleModel> data = new List<CashierBySaleModel>();
            MultipleDataBase dataAccess = new MultipleDataBase();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var user = context.userdatabasemasters.Where(u => u.DataBaseId == model.DataBaseId).FirstOrDefault();

                data = dataAccess.CashierBySale(model,user.DataBase).ToList();
            }

            return data;
        }

        public IEnumerable<CategoryBySaleModel> CategoryBySale(SalesSummaryModel model)
        {
            List<CategoryBySaleModel> data = new List<CategoryBySaleModel>();
            MultipleDataBase dataAccess = new MultipleDataBase();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var user = context.userdatabasemasters.Where(u => u.DataBaseId == model.DataBaseId).FirstOrDefault();

                data = dataAccess.CategoryBySale(model,user.DataBase).ToList();
            }

            return data;
        }

        public IEnumerable<BillNoBySaleModel> BillNoBySale(SalesSummaryModel model)
        {
            List<BillNoBySaleModel> data = new List<BillNoBySaleModel>();
            MultipleDataBase dataAccess = new MultipleDataBase();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var user = context.userdatabasemasters.Where(u => u.DataBaseId == model.DataBaseId).FirstOrDefault();

                data = dataAccess.BillNoBySale(model,user.DataBase).ToList();
            }

            return data;
        }

        public IEnumerable<TodayAndLastWeekSalesModel> TodayAndLastWeekSale(SalesSummaryModel model)
        {
            List<TodayAndLastWeekSalesModel> data = new List<TodayAndLastWeekSalesModel>();
            MultipleDataBase dataAccess = new MultipleDataBase();
            using (mom_androidEntities context = new mom_androidEntities())
            {
                var user = context.userdatabasemasters.Where(u => u.DataBaseId == model.DataBaseId).FirstOrDefault();

                data = dataAccess.TodayAndLastWeekSale(model, user.DataBase).ToList();
            }

            return data;
        }
    }
}