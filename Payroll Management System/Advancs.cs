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
    public partial class Advancs : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");
        public Advancs()
        {
            InitializeComponent();
            ShowAdvance();
        }

        private void Clear()
        {
            ANameTb.Text = "";
            AAmountTb.Text = "";
            Key = 0;
        }
        private void ShowAdvance()
        {
            con.Open();
            String Query = "Select * from AdvanceTb2";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); sda.Fill(ds);
            ADGV.DataSource = ds.Tables[0]; con.Close();
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

        private void label14_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
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

        private void Advancs_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || ANameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AdvanceTb2(AdName, AdAmount)values(@BN, @BA)", con);
                    cmd.Parameters.AddWithValue("@BN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", AAmountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Saved");
                    con.Close(); ShowAdvance();
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
                MessageBox.Show("Select The I Advance");
               
                }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AdvanceTb2 Where AID = @AKey", con); 
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Deleted");
                    con.Close(); ShowAdvance();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void ADGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ANameTb.Text = ADGV.SelectedRows[0].Cells[1].Value.ToString();
            AAmountTb.Text = ADGV.SelectedRows[0].Cells[2].Value.ToString();
            if (ANameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ADGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || ANameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
              
                 try
                 {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update AdvanceTb2 Set AdName = @BN, AdAmount = @BA Where AID = @AKey", con);
                    cmd.Parameters.AddWithValue("@BN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", AAmountTb.Text);
                    cmd.Parameters.AddWithValue("@AKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Updated");
                    con.Close(); ShowAdvance();
                    Clear();
                }
                 catch (Exception Ex)
                 {
                     MessageBox.Show(Ex.Message);
                 }

            }
        }
    }
}


