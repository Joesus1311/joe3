using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fptgrading
{
    public partial class student : Form
    {
        SqlConnection conn;
        public student(string username)
        {
            InitializeComponent();
            conn = new SqlConnection("Server=LAPTOP-OLJP7E8C;Database=student_grading_system;Integrated Security=True;");
            userlb.Text = "User: " + username;
        }

        private void student_Load(object sender, EventArgs e)
        {
            conn.Open();
            MessageBox.Show(this, "Successful connection!", "Result", MessageBoxButtons.OK, MessageBoxIcon.None);
            FillData();
        }
        public void FillData()
        {
            string query = "select * from Assignment";

            DataTable tbl = new DataTable();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);

            ad.Fill(tbl);

            dataGridView1.DataSource = tbl;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Form1 = new Login();
            Form1.ShowDialog();
            this.Dispose();
        }
    }
}
