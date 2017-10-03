using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public int product_code { get; set; }
        public string product_name { get; set; }
        public bool product_status { get; set; }
        public int count { get; set; }

    }
}
