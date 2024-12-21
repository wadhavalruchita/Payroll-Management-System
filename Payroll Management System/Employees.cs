using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Payroll_Management_System
{
    public partial class Employees : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");
        public Employees()
        {
            InitializeComponent();
            ShowEmployee();

        }

        private void label15_Click(object sender, EventArgs e)
        {
            salary Obj = new salary();
            Obj.Show();
        }
        private void Clear()
        {
            EmpName.Text = "";
            EmpAddress.Text = "";
            EmpNumber.Text = "";
            EmpBaseSalary.Text = "";
            EmpGender.SelectedIndex = 0;
            EmpPosition.SelectedIndex = 0;
            EmpQuali.SelectedIndex = 0;
            
        }
        private void ShowEmployee()
        {
            con.Open();
            String Query = "Select * from EmployeeTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmpDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }
        private void label14_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Attendance obj = new Attendance();
            obj.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Advancs obj = new Advancs();
            obj.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "" || EmpNumber.Text == "" ||EmpGender.SelectedIndex == -1 || EmpAddress.Text == "" ||EmpBaseSalary.Text == "" || EmpQuali.SelectedIndex == -1 ||EmpPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into EmployeeTb1(EmpName, EmpGender, EmpDOB, EmpPhoneNumber, EmpAdd,EmpPosition, EmpJoinDate, EmpQuali, EmpBaseSalary) values(@EN, @EG, @ED, @EP, @EA, @EPos, @EJD, @EQ, @EBS)", con);
                cmd.Parameters.AddWithValue("@EN", EmpName.Text);
                cmd.Parameters.AddWithValue("@EG", EmpGender.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                cmd.Parameters.AddWithValue("@EP", EmpNumber.Text);
                cmd.Parameters.AddWithValue("@EA", EmpAddress.Text);
                cmd.Parameters.AddWithValue("@EPos", EmpPosition.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EJD", EmpJoinDate.Value.Date);
                cmd.Parameters.AddWithValue("@EQ", EmpQuali.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@EBS", EmpBaseSalary.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Saved");
                con.Close();
                ShowEmployee();
                Clear();
               /* try
                {
                    
                }
                catch //(Exception Ex)
                {
                    //MessageBox.Show(Ex.Message);
                }*/
            }
        }
        int key = 0;
        private void EmpDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpName.Text =EmpDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpGender.SelectedItem =EmpDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text =EmpDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpNumber.Text =EmpDGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpAddress.Text =EmpDGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpPosition.SelectedItem =EmpDGV.SelectedRows[0].Cells[6].Value.ToString();
            EmpJoinDate.Text =EmpDGV.SelectedRows[0].Cells[7].Value.ToString();
            EmpQuali.SelectedItem =EmpDGV.SelectedRows[0].Cells[8].Value.ToString();
            EmpBaseSalary.Text =EmpDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (EmpName.Text == "")
            {
                key = 0;
            }
            else
            {
                key =Convert.ToInt32(EmpDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (EmpName.Text == "" || EmpNumber.Text == "" ||EmpGender.SelectedIndex == -1 || EmpAddress.Text == "" ||EmpBaseSalary.Text == "" || EmpQuali.SelectedIndex == -1 ||EmpPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTb1 Set EmpName = @EN, EmpGender = @EG, EmpDOB = @ED,EmpPhoneNumber = @EP, EmpAdd = @EA, EmpPosition = @EPos, EmpJoinDate = @EJD,EmpQuali = @EQ, EmpBaseSalary = @EBS where EmpId = @EmpKey" , con);
                    cmd.Parameters.AddWithValue("@EN",EmpName.Text);
                    cmd.Parameters.AddWithValue("@EG",EmpGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED",EmpDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@EP",EmpNumber.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddress.Text);
                    cmd.Parameters.AddWithValue("@EPos",EmpPosition.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EJD",EmpJoinDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EQ",EmpQuali.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EBS",EmpBaseSalary.Text);
                    cmd.Parameters.AddWithValue("@EmpKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated");
                    con.Close();
                    ShowEmployee();
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
            if (key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTb1 Where EmpId = @EmpKey", con);
                    
                     cmd.Parameters.AddWithValue("@EmpKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted");
                    con.Close();
                    ShowEmployee();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

    }
}
    

    
    

