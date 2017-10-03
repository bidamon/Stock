using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockLibrary.Models;

namespace StockLibrary.DataAccess
{
    public interface IDataConnection
    {
        ProductModel CreateProduct(ProductModel model);
        ProductModel UpdateProduct(ProductModel model);
        ProductModel DeleteProduct(ProductModel model);
        ProductModel CheckProductExists(ProductModel model);
        List<ProductModel> GetProducts();
        LoginModel Login(LoginModel model);
    }
}
