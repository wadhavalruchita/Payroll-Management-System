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

    public partial class Attendance : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");

        public Attendance()
        {
            InitializeComponent();
            ShowAttendance();
            GetEmployees();
        }
        private void Clear()
        {
            AtNameTb.Text = "";
            AtAbsentTb.Text = "";
            AtPresentTb.Text = "";
            AtExcusedTb.Text = "";
            Key = 0;
        }
        private void ShowAttendance()
        {
            con.Open();
            String Query = "Select * from AttendanceTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); sda.Fill(ds);
            AtDGV.DataSource = ds.Tables[0]; con.Close();
        }
        private void GetEmployees()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from EmployeeTb1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader(); DataTable
            dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            AtEmpIdTb.ValueMember = "EmpId";
            AtEmpIdTb.DataSource = dt; con.Close();
        }
        private void GetEmployeeName()
        {
            con.Open();
            String Query = " Select * from EmployeeTb1 where EmpId=" +
            AtEmpIdTb.SelectedValue.ToString() + "";
            SqlCommand Cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(Cmd); sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                AtNameTb.Text = dr["EmpName"].ToString();
            }
            con.Close();
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
        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AtNameTb.Text == "" || AtPresentTb.Text == "" || AtEmpIdTb.SelectedIndex == -1 || AtAbsentTb.Text == "" ||AtExcusedTb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AttendanceTb1(AtId, AtName, AtPresent, AtAbsent, AtExcused,AtPeriod)values(@AtI, @AtN, @AtP, @AtA, @AtE, @AtPe)", con); 
                    cmd.Parameters.AddWithValue("@AtN", AtNameTb.Text);
                    cmd.Parameters.AddWithValue("@AtI", AtEmpIdTb.Text);
                    cmd.Parameters.AddWithValue("@AtP", AtPresentTb.Text);
                    cmd.Parameters.AddWithValue("@AtA", AtAbsentTb.Text);
                    cmd.Parameters.AddWithValue("@AtE", AtExcusedTb.Text);
                    cmd.Parameters.AddWithValue("@AtPe", AtPeriodTb.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Saved");
                    con.Close(); ShowAttendance();
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
            if (AtNameTb.Text == "" || AtPresentTb.Text == "" || AtEmpIdTb.SelectedIndex == -1 || AtAbsentTb.Text == "" ||AtExcusedTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update AttendanceTb1 Set AtId = @ATI,AtName = @AtN, AtPresent = @AtP, AtAbsent = @AtA, AtExcused = @AtE,AtPeriod = @AtPe where AtNum = @AtKey", con);
                cmd.Parameters.AddWithValue("@AtN", AtNameTb.Text);
                cmd.Parameters.AddWithValue("@AtI", AtEmpIdTb.Text);
                cmd.Parameters.AddWithValue("@AtP", AtPresentTb.Text);
                cmd.Parameters.AddWithValue("@AtA", AtAbsentTb.Text);
                cmd.Parameters.AddWithValue("@AtE", AtExcusedTb.Text);
                cmd.Parameters.AddWithValue("@AtPe", AtPeriodTb.Value.Date);
                cmd.Parameters.AddWithValue("@AtKey", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Attendance Updated");
                con.Close(); ShowAttendance();
                Clear();
               /* try
                {
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }*/
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AttendanceTb1 Where AtNum = @AtKey", con); 
                    cmd.Parameters.AddWithValue("@AtKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Deleted");
                    con.Close(); ShowAttendance();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }
        int Key = 0;
        private void AtDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            
                AtEmpIdTb.SelectedItem = AtDGV.SelectedRows[0].Cells[1].Value.ToString();
                AtNameTb.Text = AtDGV.SelectedRows[0].Cells[2].Value.ToString();
                AtPresentTb.Text = AtDGV.SelectedRows[0].Cells[3].Value.ToString();
                AtAbsentTb.Text = AtDGV.SelectedRows[0].Cells[4].Value.ToString();
                AtExcusedTb.Text = AtDGV.SelectedRows[0].Cells[5].Value.ToString();
                AtPeriodTb.Text = AtDGV.SelectedRows[0].Cells[6].Value.ToString();
                if (AtNameTb.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key =
                    Convert.ToInt32(AtDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            
        }

        private void AtEmpIdTb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetEmployeeName();
        }
    }
}


