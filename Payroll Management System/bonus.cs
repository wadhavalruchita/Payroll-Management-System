using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Payroll_Management_System
{
    public partial class bonus : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");

        public bonus()
        {
            InitializeComponent();
            ShowBonus();
        }
        private void Clear()
        {
            BNameTb.Text = "";
            BAmountTb.Text = "";
            Key = 0;
        }
        private void ShowBonus()
        {
            con.Open();
            String Query = "Select * from Bonus";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); sda.Fill(ds);
            BonusDGV.DataSource = ds.Tables[0]; con.Close();
        }
        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Advancs obj = new Advancs();
            obj.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Attendance obj = new Attendance();
            obj.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            salary obj = new salary();
            obj.Show();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Bonus(Bname, BAmount)values(@BN, @BA)", con); 
                    cmd.Parameters.AddWithValue("@BN", BNameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", BAmountTb.Text);
                    cmd.ExecuteNonQuery(); MessageBox.Show("Bonus Saved"); 
                    con.Close(); ShowBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open(); SqlCommand cmd = new SqlCommand("Update Bonus Set Bname = @BN, BAmount = @BA Where BId = @BKey", con); 
                    cmd.Parameters.AddWithValue("@BN", BNameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", BAmountTb.Text);
                    cmd.Parameters.AddWithValue("@BKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Updated");
                    con.Close(); ShowBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The I Bonus");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from Bonus Where BId = @BKey", con); 
                    cmd.Parameters.AddWithValue("@BKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Deleted");
                    con.Close(); ShowBonus();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
        }
        int Key = 0;
        private void BonusDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            BNameTb.Text = BonusDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAmountTb.Text = BonusDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (BNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BonusDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
    }
    

