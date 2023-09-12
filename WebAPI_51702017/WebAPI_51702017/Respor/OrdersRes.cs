using CAIT.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_51702017.Const;
using WebAPI_51702017.Model;

namespace WebAPI_51702017.Respor
{
    public static class OrdersRes
    {
        public static List<OrderModel> getOrder(string orderID, int userID)
        {
            object[] value = { orderID, userID};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Orders_getAll_orderID", value);
            List<OrderModel> listPro = new List<OrderModel>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderModel or = new OrderModel();
                    or.orderId = dr["orderId"].ToString();
                    or.productId = dr["ProductID"].ToString();
                    or.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                    or.countProduct = string.IsNullOrEmpty(dr["countPro"].ToString()) ? 0 : int.Parse(dr["countPro"].ToString());
                    or.userName = dr["userName"].ToString();
                    or.name = dr["name"].ToString();
                    or.email = dr["email"].ToString();
                    or.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                    or.phone = dr["phone"].ToString();
                    or.address = dr["address"].ToString();
                    or.NameProduct = dr["NameProduct"].ToString();
                    or.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                    or.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                    or.Image = dr["Image"].ToString();
                    or.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());
                    listPro.Add(or);
                }
            }
            return listPro;
        }
        public static List<OrderModel> getOrderUserID(int UserID)
        {
            object[] value = { UserID };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Orders_getAll_userID", value);
            List<OrderModel> listPro = new List<OrderModel>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    OrderModel or = new OrderModel();
                    or.orderId = dr["orderId"].ToString();
                    or.productId = dr["ProductID"].ToString();
                    or.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                    or.countProduct = string.IsNullOrEmpty(dr["countPro"].ToString()) ? 0 : int.Parse(dr["countPro"].ToString());
                    or.userName = dr["userName"].ToString();
                    or.name = dr["name"].ToString();
                    or.email = dr["email"].ToString();
                    or.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                    or.phone = dr["phone"].ToString();
                    or.address = dr["address"].ToString();
                    or.NameProduct = dr["NameProduct"].ToString();
                    or.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                    or.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                    or.Image = dr["Image"].ToString();
                    or.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());

                    listPro.Add(or);
                }
            }
            return listPro;
        }
        public static bool CreateOrders(OrderModel order)
        {
            object[] value = { order.orderId , order.accountId, order.productId, order.countProduct };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Orders_create", value);
            if (connection.errorCode == 0 && connection.errorMessage == "")
            {
                return true;
            }
            return false;
        }
        public static bool DeleteOrders(string orderID , int userID)
        {
            object[] value = { orderID, userID };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Orders_delete", value);
            bool item1 = (bool)result.Rows[0][0];
            if (connection.errorCode == 0 && item1 == true)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteProductInOrder(string orderID, int userID,string idPro)
        {
            object[] value = { orderID, userID, idPro };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Orders_delete_idPro", value);
            bool item1 = (bool)result.Rows[0][0];
            if (connection.errorCode == 0 && item1 == true)
            {
                return true;
            }
            return false;
        }
    }
}
