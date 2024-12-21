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
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True"); 
        public Dashboard()
        {
            InitializeComponent();
            CountEmployee();
            CountManagers();
            SumSalary();
            SumBonus();
        }
        public void CountEmployee()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTb1", con); 
            DataTable dt = new DataTable(); sda.Fill(dt);
            Emplb.Text = dt.Rows[0][0].ToString(); con.Close();
        }
        public void CountManagers()
        {
            String Pos = "Manager";
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTb1 where EmpPosition = '"+Pos+"'", con); 
            DataTable dt = new DataTable(); sda.Fill(dt);
            Manalb.Text = dt.Rows[0][0].ToString(); con.Close();
        }
        public void SumSalary()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(EmpSBalance) from SalaryTb", con); 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SalaryLb.Text = "Rs" + dt.Rows[0][0].ToString(); con.Close();
        }
        public void SumBonus()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(EmpSBonusAm) from SalaryTb", con); 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            bonuslb.Text = "Rs" + dt.Rows[0][0].ToString(); con.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            salary Obj = new salary();
            Obj.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            bonus obj = new bonus();
            obj.Show();
           
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Advancs obj = new Advancs();
            obj.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Attendance obj = new Attendance();
            obj.Show();
        }
    }
}
