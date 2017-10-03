using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using StockLibrary.Models;

namespace StockLibrary.DataAccess
{
    public class SqlConnector:IDataConnection
    {
        private const string db = "Stocks";
        public Models.ProductModel CreateProduct(Models.ProductModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                var product = new DynamicParameters();
                product.Add("@productCode", model.product_code);
                product.Add("@productName", model.product_name);
                product.Add("@productStatus", model.product_status);
                product.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spProduct_Insert", product, commandType: CommandType.StoredProcedure);
                model.id = product.Get<int>("id");
                return model;
            }

        }


        public Models.LoginModel Login(Models.LoginModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                var login = new DynamicParameters();
                login.Add("@username", model.username);
                login.Add("@password", model.password);
                login.Add("@count", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spLogin", login, commandType: CommandType.StoredProcedure);

                model.count = login.Get<int>("@count");
                return model;
            }
        }


        public List<Models.ProductModel> GetProducts()
        {
            List<ProductModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                output = connection.Query<ProductModel>("dbo.spProducts_GetAll").ToList();
            }
            return output;
        }


        public ProductModel UpdateProduct(ProductModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                var product = new DynamicParameters();
                product.Add("@productCode", model.product_code);
                product.Add("@productName", model.product_name);
                product.Add("@productStatus", model.product_status);
                product.Add("@id", model.id);
                connection.Execute("dbo.spProduct_Update", product, commandType: CommandType.StoredProcedure);

                return model;
            }

        }


        public ProductModel DeleteProduct(ProductModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                var product = new DynamicParameters();
                product.Add("@id", model.id);

                connection.Execute("dbo.spProduct_Delele", product, commandType: CommandType.StoredProcedure);

                return model;
            }
        }


        public ProductModel CheckProductExists(ProductModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.connecString(db)))
            {
                var product = new DynamicParameters();
                product.Add("@id", model.id);
                product.Add("@count", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spProduct_GetAProduct", product, commandType: CommandType.StoredProcedure);

                model.count = product.Get<int>("@count");
                return model;
            }
           
        }
    }
}
