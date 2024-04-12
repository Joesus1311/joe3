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
    public partial class Teacher : Form
    {
        SqlConnection conn;
        public Teacher(string username)
        {
            InitializeComponent();
            conn = new SqlConnection("Server=LAPTOP-OLJP7E8C;Database=student_grading_system;Integrated Security=True;");
            userlb.Text = "User: " + username;
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            conn.Open();
           
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

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Do you want to edit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                string update = "UPDATE Assignment SET AssignmentGrade = @assignmentgrade WHERE CourseID = @courseid AND StudentID = @studentid";
                conn.Open();
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.Add("@assignmentgrade", SqlDbType.VarChar);
                cmd.Parameters["@assignmentgrade"].Value = gradetbx.Text;
                cmd.Parameters.Add("@courseid", SqlDbType.NVarChar, 10);
                cmd.Parameters["@courseid"].Value = coursetbx.Text;
                cmd.Parameters.Add("@studentid", SqlDbType.NVarChar, 10);
                cmd.Parameters["@studentid"].Value = studenttbx.Text;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    FillData();
                    MessageBox.Show(this, "Updated successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                coursetbx.Text = row.Cells["CourseID"].Value.ToString();
                studenttbx.Text = row.Cells["StudentID"].Value.ToString();
                gradetbx.Text = row.Cells["AssignmentGrade"].Value.ToString();
            }
        }

        private void coursecbx_SelectedIndexChanged(object sender, EventArgs e)
        {

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
