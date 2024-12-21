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

    public partial class salary : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");

        public salary()
        {
            InitializeComponent();
            GetEmployees();
            GetAttendance();
            GetBonus();
            ShowSalary();
        }
        private void Clear()
        {
            SName.Text = "";
            salalry.Text = "";
            SAbsence.Text = "";
            SExcused.Text = "";
            //Key = 0;
        }
        private void ShowSalary()
        {
            con.Open();
            String Query = "Select * from SalaryTb";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0]; con.Close();
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
            SId.ValueMember = "EmpId";
            SId.DataSource = dt; con.Close();
        }    
        private void GetBonus()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from Bonus", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader(); DataTable dt
            = new DataTable();
            dt.Columns.Add("Bname", typeof(String));
            dt.Load(Rdr);
            SBonus.ValueMember = "BName";
            SBonus.DataSource = dt; con.Close();
        }
        private void GetAttendance()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AttendanceTb1 where AtId = " + SId.SelectedValue.ToString() + "", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("AtId", typeof(int)); dt.Load(Rdr);
            SAtte.ValueMember = "AtId";
            SAtte.DataSource = dt;
            con.Close();
        }
        private void GetEmployeeName()
        {
             con.Open();
             String Query = " Select * from EmployeeTb1 where EmpId =" + SId.SelectedValue.ToString() + "";
             SqlCommand Cmd = new SqlCommand(Query, con);
             DataTable dt = new DataTable();
             SqlDataAdapter sda = new SqlDataAdapter(Cmd);
             sda.Fill(dt); 
             foreach (DataRow dr in dt.Rows)
             {
                 SName.Text = dr["EmpName"].ToString();
                 salalry.Text = dr["EmpBaseSalary"].ToString();

             }
             con.Close();
        }
        private void GetAttendanceDate()
        {
            con.Open();
            String Query = " Select * from AttendanceTb1 where AtId = " + SAtte.SelectedValue.ToString() + "";
            SqlCommand Cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable(); 
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            sda.Fill(dt); foreach (DataRow dr in dt.Rows)
            {
                SPresence.Text = dr["AtPresent"].ToString();
                SAbsence.Text = dr["AtAbsent"].ToString();
                SExcused.Text = dr["AtExcused"].ToString();
            }
            con.Close();
        }
        private void GetBAmount()
        {
            con.Open();
            String Query = " Select * from Bonus where BName='" + SBonus.SelectedValue.ToString() + "'";
            SqlCommand Cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bonuss.Text = dr["BAmount"].ToString();
                bonuss.Text = dr["BAmount"].ToString();
            }
            con.Close();
        }
        private void salary_Load(object sender, EventArgs e)
        {

        }
        private void label17_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Attendance obj = new Attendance();
            obj.Show();
        }
        private void label15_Click(object sender, EventArgs e)
        {
            Advancs obj = new Advancs();
            obj.Show();
        }
        private void label14_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
        }
        int DailyBase = 0, Total = 0, Pres = 0, Abs = 0, Exc = 0;
        double GrdTot = 0, TotTax = 0;
        private void btnCompute_Click(object sender, EventArgs e)
        {

            if (salalry.Text == "" || bonuss.Text == "" || SAdvance.Text == "")
             {
                 MessageBox.Show("Select the Employee");
             }
             else
             {
                 Pres = Convert.ToInt32(SPresence.Text);
                 Abs = Convert.ToInt32(SAbsence.Text);
                 Exc = Convert.ToInt32(SExcused.Text);
                 DailyBase = Convert.ToInt32(salalry.Text) / 28;
                 Total = ((DailyBase) * Pres) + ((DailyBase / 2) * Exc);
                 double Tax = Total * 0.16;
                 TotTax = Total + Tax;
                 GrdTot = TotTax + Convert.ToInt32(bonuss.Text);
                 GrdTot = TotTax + Convert.ToInt32(SAdvance.Text);
                 SBalancy.Text = "Rs" + GrdTot;
             }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SName.Text == "" || SPresence.Text == "" || SExcused.Text == "" || SAbsence.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                string Period = SPeriod.Value.Month + "-" + SPeriod.Value.Year; con.Open();
                SqlCommand cmd = new SqlCommand("insert into SalaryTb(EmpSId, EmpSName, EmpSBaseSalary, EmpSBonus, EmpSAdvance, EmpSBonusAm,EmpSBalance,EmpsPeriod)values(@SId, @SN, @SBS, @SB, @SA, @SBA, @SBal, @SPe)", con);
                cmd.Parameters.AddWithValue("@SId", SId.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@SN", SName.Text);
                cmd.Parameters.AddWithValue("@SBS", salalry.Text);
                cmd.Parameters.AddWithValue("@SB", SBonus.Text);
                cmd.Parameters.AddWithValue("@SA", SAdvance.Text);
                cmd.Parameters.AddWithValue("@SBA", bonuss.Text);
                cmd.Parameters.AddWithValue("@SBal", GrdTot);
                cmd.Parameters.AddWithValue("@SPe", Period);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Salary Saved");
                con.Close(); ShowSalary();
                Clear();
                try
                {
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }   
        }

        private void SBonus_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetBAmount();
        }

        private void SAtte_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetAttendanceDate();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (SName.Text == "" || SPresence.Text == "" || SExcused.Text == "" ||SAbsence.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open(); string Period = SPeriod.Value.Month+ "-" + SPeriod.Value.Year;
                    SqlCommand cmd = new SqlCommand("Update SalaryTb set EmpSId=@SId, EmpSName = @SN, EmpSBaseSalary = @SBS, EmpSBonus = @SB, EmpSAdvance = @SA,EmpSBonusAm = @SBA, EmpSBalance = @SBal, EmpsPeriod = @SPe where SalNum = @SalKey", con); cmd.Parameters.AddWithValue("@SId", 
                    SId.SelectedValue.ToString()); cmd.Parameters.AddWithValue("@SN",
                    SName.Text); cmd.Parameters.AddWithValue("@SBS", salalry.Text);
                    cmd.Parameters.AddWithValue("@SB", SBonus.Text);
                    cmd.Parameters.AddWithValue("@SA", SAdvance.Text);
                    cmd.Parameters.AddWithValue("@SBA", bonuss.Text);
                    cmd.Parameters.AddWithValue("@SBal", GrdTot);
                    cmd.Parameters.AddWithValue("@SPe", Period);
                    cmd.Parameters.AddWithValue("@SalKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Updated");
                    con.Close(); ShowSalary();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        
       
        private void btnPayRecipt_Click(object sender, EventArgs e)
        {
            Pay_recipt fm = new Pay_recipt();
            fm.Show();
        }

        int key = 0;
        private void SDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SId.SelectedValue = SDGV.SelectedRows[0].Cells[1].Value.ToString();
            SName.Text = SDGV.SelectedRows[0].Cells[2].Value.ToString();
            salalry.Text = SDGV.SelectedRows[0].Cells[3].Value.ToString();
            SBonus.Text = SDGV.SelectedRows[0].Cells[4].Value.ToString();
            SAdvance.Text = SDGV.SelectedRows[0].Cells[5].Value.ToString();
            SBonus.Text = SDGV.SelectedRows[0].Cells[6].Value.ToString();
            SBalancy.Text = SDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (SName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SDGV.SelectedRows[0].Cells[0].Value.ToString());
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
                    SqlCommand cmd = new SqlCommand("Delete from SalaryTb Where SalNum = @EmpKey", con); cmd.Parameters.AddWithValue("@EmpKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Deleted");
                    con.Close(); ShowSalary();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void SId_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetEmployeeName();
            GetAttendance();
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

