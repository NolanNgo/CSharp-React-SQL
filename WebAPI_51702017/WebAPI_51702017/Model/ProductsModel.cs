using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_51702017.Model
{
    public class ProductsModel
    {

        public ProductsModel(string productID, string nameProduct, int prices, int storage, string image, int productTypeID, string description1)
        {
            ProductID = productID;
            NameProduct = nameProduct;
            Prices = prices;
            Storage = storage;
            Image = image;
            description = description1;
            ProductTypeID = productTypeID;
            
        }
        public ProductsModel()
        {
            ProductID = "";
            NameProduct = "";
            Prices = 0;
            Storage = 0;
            Image = "";
            description = "";
            ProductTypeID = 0;
        }
        public string ProductID { get; set; }
        public string NameProduct { get; set; }
        public string description { get; set; }
        public int Prices { get; set; }
        public int Storage { get; set; }
        public string Image { get; set; }
        public int ProductTypeID { get; set; }
        

    }
}
