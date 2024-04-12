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
    public partial class admincs : Form
    {
        SqlConnection conn;
        public admincs(string username)
        {
            InitializeComponent();
            conn = new SqlConnection("Server=LAPTOP-OLJP7E8C;Database=student_grading_system;Integrated Security=True;");
            userlb.Text = "User: " + username;
        }
        public void FillData1()
        {
            string query = "select * from Student";

            DataTable tbl = new DataTable();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);

            ad.Fill(tbl);

            dataGridView1.DataSource = tbl;

            conn.Close();
        }
        public void FillData2()
        {
            string query = "select * from Teacher";

            DataTable tbl = new DataTable();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);

            ad.Fill(tbl);

            dataGridView2.DataSource = tbl;

            conn.Close();
        }

        public void FillData3()
        {
            string query = "select * from Course";

            DataTable tbl = new DataTable();

            SqlDataAdapter ad = new SqlDataAdapter(query, conn);

            ad.Fill(tbl);

            dataGridView3.DataSource = tbl;

            conn.Close();
        }
        private void admincs_Load(object sender, EventArgs e)
        {
            conn.Open();

            if (tabControl1.SelectedTab == tabPage1)
            {
                FillData1();
                FillData2();
                FillData3();
                
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login Form1 = new Login();
            Form1.ShowDialog();
            this.Dispose();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int error = 0;
            string studentid = studenttbx1.Text;
            if (studentid.Equals(""))
            {
                error = error + 1;
                errorlb1.Text = "ID can't be blank";
            }
            else
                errorlb1.Text = "";
            string userid = usertbx1.Text;
            if (userid.Equals(""))
            {
                error = error + 1;
                errorlb2.Text = "ID can't be blank";
            }
            else
                errorlb2.Text = "";
            string studentname = nametbx.Text;
            if (studentname.Equals(""))
            {
                error = error + 1;
                errorlb3.Text = "Name can't be blank";
            }
            else
                errorlb3.Text = "";
            string studentemail = emailtbx.Text;
            if (studentemail.Equals(""))
            {
                error = error + 1;
                errorlb4.Text = "Email can't be blank";
            }
            else
            {
                string query = "Select * from Student where StudentID = @studentid";
                conn.Open();
                SqlCommand cmdcheck = new SqlCommand(query, conn);
                cmdcheck.Parameters.Add("@studentid", SqlDbType.NVarChar, 10);
                cmdcheck.Parameters["@studentid"].Value = studentid;

                SqlDataReader reader = cmdcheck.ExecuteReader();
                if (reader.Read())
                {
                    error++;
                    errorlb1.Text = "ID already exist, please choose another";

                }
                else
                {

                    errorlb1.Text = "";
                }
                conn.Close();
            }
            if (error == 0)
            {
                string insert = "insert into Student values (@studentid,@UserID,@StudentName,@StudentEmail)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.Add("@studentid", SqlDbType.VarChar);
                cmd.Parameters["@studentid"].Value = studentid;
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar);
                cmd.Parameters["@UserID"].Value = userid;
                cmd.Parameters.Add("@StudentName", SqlDbType.VarChar);
                cmd.Parameters["@StudentName"].Value = studentname;
                cmd.Parameters.Add("@StudentEmail", SqlDbType.VarChar);
                cmd.Parameters["@StudentEmail"].Value = studentemail;
                cmd.ExecuteNonQuery();

                FillData1();
                MessageBox.Show(this, "Inserted sucessfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to edit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string update = "UPDATE Student SET StudentName = @StudentName, StudentEmail = @StudentEmail WHERE StudentID = @StudentID";
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(update, conn, transaction);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar, 10).Value = studenttbx1.Text;
                cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = nametbx.Text;
                cmd.Parameters.Add("@StudentEmail", SqlDbType.VarChar).Value = emailtbx.Text;

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    transaction.Commit();

                    if (i > 0)
                    {
                        FillData1();
                        MessageBox.Show(this, "Updated successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(this, "An error occurred during the update operation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Do you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Assignment WHERE StudentID = @StudentID; DELETE FROM Student WHERE StudentID = @StudentID";

                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@StudentID", studenttbx1.Text);
                cmd.ExecuteNonQuery();

                conn.Close();
                FillData1();
                MessageBox.Show(this, "Deleted successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                studenttbx1.Text = row.Cells["StudentID"].Value.ToString();
                usertbx1.Text = row.Cells["UserID"].Value.ToString();
                nametbx.Text = row.Cells["StudentName"].Value.ToString();
                emailtbx.Text = row.Cells["StudentEmail"].Value.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int error = 0;
            string teacherid = teachertbx1.Text;
            if (teacherid.Equals(""))
            {
                error = error + 1;
                lberror1.Text = "ID can't be blank";
            }
            else
                lberror1.Text = "";
            string userid = usertbx2.Text;
            if (userid.Equals(""))
            {
                error = error + 1;
                lberror2.Text = "ID can't be blank";
            }
            else
                lberror2.Text = "";
            string teachername = nametbx2.Text;
            if (teachername.Equals(""))
            {
                error = error + 1;
                lberror3.Text = "Name can't be blank";
            }
            else
                lberror3.Text = "";
            string teacheremail = emailtbx2.Text;
            if (teacheremail.Equals(""))
            {
                error = error + 1;
                lberror4.Text = "Email can't be blank";
            }
            else
                lberror4.Text = "";
            string teacherdepartment = departmenttbx1.Text;
            if (teacherdepartment.Equals(""))
            {
                error = error + 1;
                lberror5.Text = "Department can't be blank";
            }
            {
                string query = "Select * from Teacher where TeacherID = @TeacherID";
                conn.Open();
                SqlCommand cmdcheck = new SqlCommand(query, conn);
                cmdcheck.Parameters.Add("@TeacherID", SqlDbType.NVarChar, 10);
                cmdcheck.Parameters["@TeacherID"].Value = teacherid;

                SqlDataReader reader = cmdcheck.ExecuteReader();
                if (reader.Read())
                {
                    error++;
                    errorlb1.Text = "ID already exist, please choose another";

                }
                else
                {

                    errorlb1.Text = "";
                }
                conn.Close();
            }
            if (error == 0)
            {
                string insert = "insert into Teacher values (@TeacherID,@UserID,@TeacherName,@TeacherEmail,@TeacherDepartment)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.Add("@TeacherID", SqlDbType.VarChar);
                cmd.Parameters["@TeacherID"].Value = teacherid;
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar);
                cmd.Parameters["@UserID"].Value = userid;
                cmd.Parameters.Add("@TeacherName", SqlDbType.VarChar);
                cmd.Parameters["@TeacherName"].Value = teachername;
                cmd.Parameters.Add("@TeacherEmail", SqlDbType.VarChar);
                cmd.Parameters["@TeacherEmail"].Value = teacheremail;
                cmd.Parameters.Add("@TeacherDepartment", SqlDbType.VarChar);
                cmd.Parameters["@TeacherDepartment"].Value = teacherdepartment;
                cmd.ExecuteNonQuery();

                FillData2();
                MessageBox.Show(this, "Inserted sucessfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to edit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string update = "UPDATE Teacher SET TeacherName = @TeacherName, TeacherEmail = @TeacherEmail, TeacherDepartment = @TeacherDepartment WHERE TeacherID = @TeacherID";
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(update, conn, transaction);
                cmd.Parameters.Add("@TeacherID", SqlDbType.NVarChar, 10).Value = teachertbx1.Text;
                cmd.Parameters.Add("@TeacherName", SqlDbType.VarChar).Value = nametbx2.Text;
                cmd.Parameters.Add("@TeacherEmail", SqlDbType.VarChar).Value = emailtbx2.Text;
                cmd.Parameters.Add("@TeacherDepartment", SqlDbType.VarChar).Value = departmenttbx1.Text;
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    transaction.Commit();

                    if (i > 0)
                    {
                        FillData2();
                        MessageBox.Show(this, "Updated successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(this, "An error occurred during the update operation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show(this, "Do you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                conn.Open();
                string deleteQuery = "DELETE FROM TeacherCourse WHERE TeacherID = @TeacherID; DELETE FROM Teacher WHERE TeacherID = @TeacherID";

                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                cmd.Parameters.AddWithValue("@TeacherID", teachertbx1.Text);
                cmd.ExecuteNonQuery();

                conn.Close();
                FillData2();
                MessageBox.Show(this, "Deleted successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                teachertbx1.Text = row.Cells["TeacherID"].Value.ToString();
                usertbx2.Text = row.Cells["UserID2"].Value.ToString();
                nametbx2.Text = row.Cells["TeacherName"].Value.ToString();
                emailtbx2.Text = row.Cells["TeacherEmail"].Value.ToString();
                departmenttbx1.Text = row.Cells["TeacherDepartment"].Value.ToString();

            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                Coursetbx2.Text = row.Cells["CourseID"].Value.ToString();

                nametbx3.Text = row.Cells["CourseName"].Value.ToString();
                titletbx.Text = row.Cells["CourseTitle"].Value.ToString();
                departmenttbx2.Text = row.Cells["CourseDepartment"].Value.ToString();

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int error = 0;
            string courseid = Coursetbx2.Text;
            if (courseid.Equals(""))
            {
                error = error + 1;
                errorb1.Text = "ID can't be blank";
            }
            else
                errorb1.Text = "";
            string coursename = nametbx3.Text;
            if (coursename.Equals(""))
            {
                error = error + 1;
                errorb2.Text = "Name can't be blank";
            }
            else
                errorb2.Text = "";
            string coursetitle = titletbx.Text;
            if (coursetitle.Equals(""))
            {
                error = error + 1;
                errorb3.Text = "Title can't be blank";
            }
            else
                errorb3.Text = "";
            string coursedepartment = departmenttbx2.Text;
            if (coursedepartment.Equals(""))
            {
                error = error + 1;
                errorb4.Text = "Department can't be blank";
            }

            {
                string query = "Select * from Course where CourseID = @CourseID";
                conn.Open();
                SqlCommand cmdcheck = new SqlCommand(query, conn);
                cmdcheck.Parameters.Add("@CourseID", SqlDbType.NVarChar, 10);
                cmdcheck.Parameters["@CourseID"].Value = courseid;

                SqlDataReader reader = cmdcheck.ExecuteReader();
                if (reader.Read())
                {
                    error++;
                    errorb1.Text = "ID already exist, please choose another";

                }
                else
                {

                    errorb1.Text = "";
                }
                conn.Close();
            }
            if (error == 0)
            {
                string insert = "insert into Course values (@CourseID,@CourseName,@CourseTitle,@CourseDepartment)";
                conn.Open();
                SqlCommand cmd = new SqlCommand(insert, conn);
                cmd.Parameters.Add("@CourseID", SqlDbType.VarChar);
                cmd.Parameters["@CourseID"].Value = courseid;
                cmd.Parameters.Add("@CourseName", SqlDbType.VarChar);
                cmd.Parameters["@CourseName"].Value = coursename;
                cmd.Parameters.Add("@CourseTitle", SqlDbType.VarChar);
                cmd.Parameters["@CourseTitle"].Value = coursetitle;
                cmd.Parameters.Add("@CourseDepartment", SqlDbType.VarChar);
                cmd.Parameters["@CourseDepartment"].Value = coursedepartment;
                cmd.ExecuteNonQuery();

                FillData3();
                MessageBox.Show(this, "Inserted sucessfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Do you want to edit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string update = "UPDATE Course SET CourseName = @CourseName, CourseTitle = @CourseTitle, CourseDepartment = @CourseDepartment WHERE CourseID = @CourseID";
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(update, conn, transaction);
                cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar, 10).Value = Coursetbx2.Text;
                cmd.Parameters.Add("@CourseName", SqlDbType.VarChar).Value = nametbx3.Text;
                cmd.Parameters.Add("@CourseTitle", SqlDbType.VarChar).Value = titletbx.Text;
                cmd.Parameters.Add("@CourseDepartment", SqlDbType.VarChar).Value = departmenttbx2.Text;

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    transaction.Commit();

                    if (i > 0)
                    {
                        FillData3();
                        MessageBox.Show(this, "Updated successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(this, "An error occurred during the update operation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            conn.Open();
            string deleteQuery = "DELETE FROM TeacherCourse WHERE CourseID = @CourseID; DELETE FROM Assignment WHERE CourseID = @CourseID; DELETE FROM Course WHERE CourseID = @CourseID";

            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
            cmd.Parameters.AddWithValue("@CourseID", Coursetbx2.Text);
            cmd.ExecuteNonQuery();

            conn.Close();
            FillData3();
            MessageBox.Show(this, "Deleted successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

      
    }
}
