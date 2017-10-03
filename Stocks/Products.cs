using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockLibrary;
using StockLibrary.Models;

namespace Stocks
{
    public partial class Products : Form
    {
        List<ProductModel> products;
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            cbStatus.SelectedIndex = 0;
            PopulateGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Valide form values
            if (FormValid())
            {
                ProductModel product = new ProductModel();

                product.product_code = Convert.ToInt32(productCodeValue.Text);
                product.product_name = productNameValue.Text;
                product.product_status = Status(cbStatus.SelectedIndex);
                if (!productExists())
                {
                    GlobalConfig.connection.CreateProduct(product);
                }
                else
                {
                    product.id = (int)productID;
                    GlobalConfig.connection.UpdateProduct(product);
                }

                //Clear the textboxes after the record is created
                ClearEntries();
                PopulateGrid();
            }
            else
            {
                MessageBox.Show("Please Fill in the required information", "Error");
            }
        }
        private int? productID;

        private bool productExists()
        {
            if (productID == null)
                return false;
            else
                return true;
        }
        private void PopulateGrid()
        {
            DataTable dt = new DataTable();
            products = GlobalConfig.connection.GetProducts();

            dt.Columns.Add("id");
            dt.Columns.Add("Product Code");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Status");
            foreach (var item in products)
            {
                var row = dt.NewRow();
                row["id"] = item.id;
                row["Product Code"] = item.product_code;
                row["Product Name"] = item.product_name;
                row["Status"] = item.product_status;

                dt.Rows.Add(row);
            }
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["id"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Product Code"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Product Name"].ToString();
                if (Convert.ToBoolean(item["Status"]))
                {
                    dataGridView1.Rows[n].Cells[3].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[3].Value = "Inactive";
                }
            }
            
        }

        private bool FormValid()
        {
            bool isValid = true;
            bool productCodeValid = productCodeValue.Text.Length > 0;
            bool productNameValid = productNameValue.Text.Length > 0;

            if (!productCodeValid)
            {
               
                isValid = false;
            }
            if (!productNameValid)
            {
                isValid = false;
            }
            return isValid;
        }
        private bool Status(int value)
        {
            bool statusValue = true;
            switch (value)
            {
                case 0:
                    statusValue = true;
                    break;
                case 1:
                    statusValue = false;
                    break;
            }
            return statusValue;
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            productCodeValue.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            productNameValue.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString() == "Active")
            {
                cbStatus.SelectedIndex = 0;
            }
            else
            {
                cbStatus.SelectedIndex = 1;
            }
            productID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (FormValid())
            {
                ProductModel product = new ProductModel();
                product.id = (int)productID;
                product = GlobalConfig.connection.CheckProductExists(product);
                if(product.count == 1)
                    GlobalConfig.connection.DeleteProduct(product);
                
                ClearEntries();
                PopulateGrid();
            }
            else
            {
                MessageBox.Show("Sorry there is no product selected!");
            }
        }
        private void ClearEntries()
        {
            productCodeValue.Text = "";
            productNameValue.Text = "";
        }



    }
}
