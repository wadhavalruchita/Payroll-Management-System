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
    public partial class Pay_recipt : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AM4BGAPB\\SQLEXPRESS;Initial Catalog=payrollmanagement;Integrated Security = True");

        public Pay_recipt()
        {
            InitializeComponent();
            ShowPay();
        }
        private void ShowPay()
        {
            con.Open();
            String Query = "Select * from SalaryTb";
            SqlDataAdapter sda = new
           SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new
            SqlCommandBuilder(sda); var ds = new
            DataSet(); sda.Fill(ds);
            SDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Tech Wizard Ltd", new Font("Averia", 10, FontStyle.Bold), Brushes.Red, new Point(160, 25));
            e.Graphics.DrawString("PayRoll Management System 1.0", new Font("Averia", 10, FontStyle.Bold), Brushes.Blue, new Point(125, 45));
            String SalNum = SDGV.SelectedRows[0].Cells[0].Value.ToString();
            String EmpSId = SDGV.SelectedRows[0].Cells[1].Value.ToString();
            String EmpSName = SDGV.SelectedRows[0].Cells[2].Value.ToString();
            String EmpSBaseSalary = SDGV.SelectedRows[0].Cells[3].Value.ToString();
            String EmpSBonus = SDGV.SelectedRows[0].Cells[4].Value.ToString();
            String EmpSAdvance = SDGV.SelectedRows[0].Cells[5].Value.ToString();
            String EmpSBalance = SDGV.SelectedRows[0].Cells[6].Value.ToString();
            String EmpsPeriod = SDGV.SelectedRows[0].Cells[7].Value.ToString();
            String EmpSBonusAm = SDGV.SelectedRows[0].Cells[8].Value.ToString();
            e.Graphics.DrawString("Salary Number : " + SalNum, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 100));

            e.Graphics.DrawString("Employee Id : " + EmpSId, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 150));

            e.Graphics.DrawString("Employee Name : " + EmpSName, new Font("Arial Rounded MT", 11, FontStyle.Bold), Brushes.Blue, new Point(200, 150));
            e.Graphics.DrawString("Base Salary : Rs " + EmpSBaseSalary, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 180));

            e.Graphics.DrawString("Bonus : Rs " + EmpSBonus, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 210));
            e.Graphics.DrawString("Advance On Salary : Rs " + EmpSAdvance, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 240));
            e.Graphics.DrawString("Bonus Amount : Rs " + EmpSBonusAm, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 270));

            e.Graphics.DrawString("Total : Rs " + EmpSBalance, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 300));

            e.Graphics.DrawString("Period : " + EmpsPeriod, new Font("Arial Rounded MT", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 330));

            e.Graphics.DrawString("Powered By Tech Wizard", new Font("Arial Rounded MT", 12, FontStyle.Bold), Brushes.Crimson, new Point(150, 420));

            e.Graphics.DrawString("***************version 1.0 * ****************", new Font("Arial Rounded MT", 12, FontStyle.Bold), Brushes.Crimson, new Point(100, 435));
        
        }

        private void SDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprm", 500, 800);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            salary fn = new salary();
            fn.Show();
        }
    }
}
