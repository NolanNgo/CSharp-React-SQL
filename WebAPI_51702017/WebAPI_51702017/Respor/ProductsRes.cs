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
    public static class ProductsRes
    {
        public static List<ProductsModel> GetAllProduct()
        {
            object[] value = {};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Product_getall", value);
            List<ProductsModel> listPro = new List<ProductsModel>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    ProductsModel pr = new ProductsModel();
                    pr.ProductID = dr["ProductID"].ToString();
                    pr.NameProduct = dr["NameProduct"].ToString();
                    pr.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                    pr.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                    pr.description = dr["description"].ToString();
                    pr.Image = dr["Image"].ToString();
                    pr.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());

                    listPro.Add(pr);
                }
            }
            return listPro;

        }
        public static List<ProductsModel> getProductWithType(string ProductTypeID)
        {
            object[] value = { ProductTypeID };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Product_getall_type", value);
            List<ProductsModel> listPro = new List<ProductsModel>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    ProductsModel pr = new ProductsModel();
                    pr.ProductID = dr["ProductID"].ToString();
                    pr.NameProduct = dr["NameProduct"].ToString();
                    pr.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                    pr.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                    pr.description = dr["description"].ToString();
                    pr.Image = dr["Image"].ToString();
                    pr.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());

                    listPro.Add(pr);
                }
            }
            return listPro;
        }
        public static ProductsModel getDetail(string id)
        {
            object[] value = {id};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Products_getDetail", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var dr = result.Rows[0];
                ProductsModel pr = new ProductsModel();
                pr.ProductID = dr["ProductID"].ToString();
                pr.NameProduct = dr["NameProduct"].ToString();
                pr.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                pr.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                pr.description = dr["description"].ToString();
                pr.Image = dr["Image"].ToString();
                pr.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());
                return pr;
            }
            return new ProductsModel();
        }
        public static ProductsModel CreateProducts(ProductsModel pro)
        {
            object[] value = { pro.ProductID, pro.NameProduct , pro.Prices , pro.Storage , pro.Image,pro.ProductTypeID , pro.description};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Products_createProducts", value);
            if(connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var dr = result.Rows[0];
                ProductsModel pr = new ProductsModel();
                pr.ProductID = dr["ProductID"].ToString();
                pr.NameProduct = dr["NameProduct"].ToString();
                pr.Prices = string.IsNullOrEmpty(dr["Prices"].ToString()) ? 0 : int.Parse(dr["Prices"].ToString());
                pr.Storage = string.IsNullOrEmpty(dr["storage"].ToString()) ? 0 : int.Parse(dr["storage"].ToString());
                pr.description = dr["description"].ToString();
                pr.Image = dr["Image"].ToString();
                pr.ProductTypeID = string.IsNullOrEmpty(dr["ProductTypeID"].ToString()) ? 0 : int.Parse(dr["ProductTypeID"].ToString());
                return pr;
            }
            return new ProductsModel();
        }
        public static bool DeleteProducts(string id)
        {
            object[] value = {id};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Products_deleteProducts", value);
            bool item1 = (bool)result.Rows[0][0];
            if (connection.errorCode == 0 && item1 == true)
            {
                return true;
            }
            return false;
        }
        public static bool EditProducts(ProductsModel pro)
        {
            object[] value = { pro.ProductID, pro.NameProduct, pro.Prices, pro.Storage, pro.Image, pro.ProductTypeID, pro.description };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Products_editProducts", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
