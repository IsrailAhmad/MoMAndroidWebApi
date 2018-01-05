using EverGreenWebApi.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.DataAccess
{
    public class MultipleDataBase
    {
        public MultipleDataBase()
        {

        }

        public IEnumerable<LocationMasterModel> GetAllLocationList(string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";


            List<LocationMasterModel> data = new List<LocationMasterModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = "Select COMPANY_NM,Location_id from tblcompany order by COMPANY_NM";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        LocationMasterModel singledata = new LocationMasterModel();
                        singledata.LocationId = myRdr["Location_id"].ToString();
                        singledata.LocationName = myRdr["COMPANY_NM"].ToString();
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<DepartmentMasterModel> GetAllDepartmentList(string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<DepartmentMasterModel> data = new List<DepartmentMasterModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = "SELECT Department_Code,Department_Name FROM tbldepartment Order By Department_Name";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        DepartmentMasterModel singledata = new DepartmentMasterModel();
                        singledata.DepartmentId = Convert.ToInt16(myRdr["Department_Code"].ToString());
                        singledata.DepartmentName = myRdr["Department_Name"].ToString();
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<CashierBySaleModel> CashierBySale(SalesSummaryModel model, string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<CashierBySaleModel> data = new List<CashierBySaleModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = " Select USER_NAME,CASH,SUM(AMOUNT_N) AS TOTAL FROM tbltotalsales " +
                                    " where LOCATION = '" + model.Location + "' AND CASH != 'CANCEL' AND CASH != 'NC' AND CASH != 'CH' " +
                                    " AND(INV_DT >= '" + model.FromDate.ToString("yyyy-MM-dd") + "' AND INV_DT <= '" + model.ToDate.ToString("yyyy-MM-dd") + "') " +
                                    " GROUP BY USER_NAME,CASH" +
                                    " Order By USER_NAME,CASH";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        CashierBySaleModel singledata = new CashierBySaleModel();
                        singledata.UserName = myRdr["USER_NAME"].ToString();
                        singledata.CashType = myRdr["CASH"].ToString();
                        singledata.Amount = Convert.ToDouble(myRdr["TOTAL"].ToString());
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<CategoryBySaleModel> CategoryBySale(SalesSummaryModel model, string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<CategoryBySaleModel> data = new List<CategoryBySaleModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = " Select CATAGORY,PRODUCT_NAME,SUM(QTY) as TotalCount,SUM(AMOUNT) as TOTAL FROM tbltotalsales " +
                                    " where LOCATION = '" + model.Location + "' " +
                                    " AND(INV_DT >= '" + model.FromDate.ToString("yyyy-MM-dd") + "' AND INV_DT <= '" + model.ToDate.ToString("yyyy-MM-dd") + "') " +
                                    " GROUP BY CATAGORY,PRODUCT_NAME" +
                                    " Order by CATAGORY,PRODUCT_NAME";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        CategoryBySaleModel singledata = new CategoryBySaleModel();
                        singledata.CategoryName = myRdr["CATAGORY"].ToString();
                        singledata.ProductName = myRdr["PRODUCT_NAME"].ToString();
                        singledata.Quantity = Convert.ToDouble(myRdr["TotalCount"].ToString());
                        singledata.Amount = Convert.ToDouble(myRdr["TOTAL"].ToString());
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<TodayAndLastWeekSalesModel> TodayAndLastWeekSale(SalesSummaryModel model, string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<TodayAndLastWeekSalesModel> data = new List<TodayAndLastWeekSalesModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {

                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = " Select " +
                                    " (Select  SUM(AMOUNT_N)  from tbltotalsales " +
                                    " where LOCATION = '" + model.Location + "'  AND(INV_DT >= '" + model.FromDate.ToString("yyyy-MM-dd") + "' AND INV_DT <= '" + model.FromDate.ToString("yyyy-MM-dd") + "') " +
                                    " AND CASH != 'CANCEL') as TODAYTOTALSALE, " +
                                    " (Select  SUM(AMOUNT_N)  from tbltotalsales " +
                                    " where LOCATION = '" + model.Location + "'  AND(INV_DT >= DATE('" + model.FromDate.ToString("yyyy-MM-dd") + "') - INTERVAL 7 DAY AND INV_DT <= '" + model.FromDate.ToString("yyyy-MM-dd") + "') " +
                                    " AND CASH != 'CANCEL') as LASTWEEKSALE;";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        TodayAndLastWeekSalesModel singledata = new TodayAndLastWeekSalesModel();
                        singledata.TodayTotalSales =Convert.ToDouble( myRdr["TODAYTOTALSALE"].ToString());
                        singledata.LastWeekTotalSales = Convert.ToDouble(myRdr["LASTWEEKSALE"].ToString());                        
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<BillNoBySaleModel> BillNoBySale(SalesSummaryModel model, string databasename)
        {
            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<BillNoBySaleModel> data = new List<BillNoBySaleModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.Text;
                myCmd.CommandText = " Select INV_DT,INV_NO,CASH,SUM(AMOUNT_N) as TOTAL FROM tbltotalsales " +
                                    " where LOCATION = '" + model.Location + "' " +
                                    " AND(INV_DT >= '" + model.FromDate.ToString("yyyy-MM-dd") + "' AND INV_DT <= '" + model.ToDate.ToString("yyyy-MM-dd") + "') " +
                                    " AND CASH != 'CANCEL' AND CASH != 'NC' AND CASH != 'CH' " +
                                    " GROUP BY INV_DT,INV_NO,CASH" +
                                    " Order by INV_DT,INV_NO,CASH";
                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        BillNoBySaleModel singledata = new BillNoBySaleModel();
                        singledata.BillDate = Convert.ToDateTime(myRdr["INV_DT"].ToString());
                        singledata.BillNo = myRdr["INV_NO"].ToString();
                        singledata.PaymentType = myRdr["CASH"].ToString();
                        singledata.Total = Convert.ToDouble(myRdr["TOTAL"].ToString());
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }
        public IEnumerable<StockSummaryMasterModel> StockSummary(StockMasterModel model, string databasename)
        {

            string strConnection = "";
            strConnection = "Server=localhost;user id=root;password=pari@123;Database=" + databasename + "";

            List<StockSummaryMasterModel> data = new List<StockSummaryMasterModel>();
            MySqlConnection myConn = new MySqlConnection();
            MySqlCommand myCmd = new MySqlCommand();
            MySqlDataReader myRdr = null;

            try
            {
                myConn.ConnectionString = strConnection;
                myConn.Open();
                myCmd.Connection = myConn;
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.CommandText = "STOCKSUMM_Android";

                MySqlParameter ParamdToDate = new MySqlParameter();
                ParamdToDate.DbType = System.Data.DbType.DateTime;
                ParamdToDate.Direction = ParameterDirection.Input;
                ParamdToDate.ParameterName = "@VRDATE";
                ParamdToDate.Value = model.ToDate;

                MySqlParameter ParamdFromDate = new MySqlParameter();
                ParamdFromDate.DbType = System.Data.DbType.DateTime;
                ParamdFromDate.Direction = ParameterDirection.Input;
                ParamdFromDate.ParameterName = "@VRDATE_TILL";
                ParamdFromDate.Value = model.FromDate;


                MySqlParameter ParamdDepartmentId = new MySqlParameter();
                ParamdDepartmentId.DbType = System.Data.DbType.Int32;
                ParamdDepartmentId.Direction = ParameterDirection.Input;
                ParamdDepartmentId.ParameterName = "@COSTCODE";
                ParamdDepartmentId.Value = model.DepartmentId;

                MySqlParameter ParamdLocationName = new MySqlParameter();
                ParamdLocationName.DbType = System.Data.DbType.String;
                ParamdLocationName.Direction = ParameterDirection.Input;
                ParamdLocationName.ParameterName = "@Locationname";
                ParamdLocationName.Value = model.Location;


                myCmd.Parameters.Add(ParamdToDate);
                myCmd.Parameters.Add(ParamdFromDate);
                myCmd.Parameters.Add(ParamdDepartmentId);
                myCmd.Parameters.Add(ParamdLocationName);



                myRdr = myCmd.ExecuteReader();

                if (myRdr.HasRows)
                {
                    while (myRdr.Read())
                    {
                        StockSummaryMasterModel singledata = new StockSummaryMasterModel();
                        singledata.DepartmentId = Convert.ToInt32(myRdr["COSTCENTER_CODE"].ToString());
                        singledata.ItemName = myRdr["item_name"].ToString();
                        singledata.ItemCode = myRdr["Item_code"].ToString();
                        singledata.TotalRecieved = Convert.ToDouble(myRdr["tot_recd"].ToString());
                        singledata.TotalIssue = Convert.ToDouble(myRdr["tot_ISSUE"].ToString());
                        singledata.OpeningBalance = Convert.ToDouble(myRdr["opening_balance"].ToString());
                        singledata.TotalReceivedTill = Convert.ToDouble(myRdr["Total_received_Till"].ToString());
                        singledata.TotalIssuedTill = Convert.ToDouble(myRdr["Total_Issued_Till"].ToString());
                        singledata.ClosingBalance = Convert.ToDouble(myRdr["Closing_Balance"].ToString());
                        singledata.Price = Convert.ToDouble(myRdr["PRICE"].ToString());
                        singledata.UOM = myRdr["uom"].ToString();
                        singledata.GroupName = myRdr["GRNAME"].ToString();
                        data.Add(singledata);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (myRdr != null)
                {
                    myRdr.Close();
                    myRdr.Dispose();
                    myRdr = null;
                }

                if (myCmd != null)
                {
                    myCmd.Dispose();
                    myCmd = null;
                }
                if (myConn != null)
                {
                    myConn.Close();
                    myConn.Dispose();
                    myConn = null;
                }
            }
            return data;
        }

    }
}