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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            usernameValue.Text = "";
            passwordValue.Text = "";
            usernameValue.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Validate username and password
            if (FormValid())
            {
                LoginModel login = new LoginModel();
                login.username = usernameValue.Text;
                login.password = passwordValue.Text;

                //Verify that the query returns only one row
                //Then login is successful otherwise not successful
                if (GlobalConfig.connection.Login(login).count == 1)
                {
                    this.Hide();
                    MainWindow form = new MainWindow();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username & Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnClear_Click(sender, e);
                }
            }
           
        }

        private bool FormValid()
        {
            bool isValid = true;
            bool isUsername = usernameValue.Text.Length > 0;
            bool isPassword = passwordValue.Text.Length > 0;
            if (!isUsername)
            {
                isValid = false;
            }
            if (!isPassword)
            {
                isValid = false;
            }
            return isValid;
        }

      
    }
}
