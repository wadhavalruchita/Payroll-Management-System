using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text == "" || txtPassword.Text == "")
{
                MessageBox.Show("Missing Information");
            }else if (txtUserName.Text == "ruchita" && txtPassword.Text == "7061")
            {
                Dashboard ds = new Dashboard();
                ds.Show();
                this.Hide();
               
            
            }
            else
            {
                MessageBox.Show("Wrong Admin or Password");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

