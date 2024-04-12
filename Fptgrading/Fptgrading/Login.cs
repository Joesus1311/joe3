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
    public partial class Login : Form
    {
        SqlConnection conn;
        public Login()
        {
            InitializeComponent();
            conn = new SqlConnection("Server=LAPTOP-OLJP7E8C;Database=student_grading_system;Integrated Security=True;");
        }

        private void usertbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string username = usertbx.Text;
            string password = passtbx.Text;
            string query = "select * from Userlogin where Username =@username and UserPassword =@password";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", SqlDbType.VarChar);
            cmd.Parameters["@username"].Value = username;
            cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar);
            cmd.Parameters["@password"].Value = password;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string role = reader["UserRole"].ToString();
                if (role.Equals("teacher"))
                {
                    MessageBox.Show(this, "Login successful! ", "Result", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Hide();
                    Teacher p = new Teacher(username);
                    p.ShowDialog();
                    this.Dispose();
                }
                else if (role.Equals("student"))
                {
                    MessageBox.Show(this, "Login successful! ", "Result", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Hide();
                    student vp = new student(username);
                    vp.ShowDialog();
                    this.Dispose();
                }
                else if (role.Equals("admin"))
                {
                    MessageBox.Show(this, "Login successful! ", "Result", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Hide();
                    admincs vp = new admincs(username);
                    vp.ShowDialog();
                    this.Dispose();
                }
                else
                    label3.Text = "You are not allowed to accesss";

            }
            else
            {
                label3.Text = "Wrong username or password";
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Do you want exit?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            passtbx.PasswordChar = '*';
        }
    }
}
