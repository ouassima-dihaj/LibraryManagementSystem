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

namespace LibraryManagementSystem
{
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }
        private void RefreshDataGridView()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from NewStudent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void txtSearchEnrollement_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchEnrollement.Text != "")
            {
                label9.Visible = false;
                Image image = Image.FromFile("C:/Users/n/source/repos/images/Liberay Management System/search1.gif");
                pictureBox1.Image = image;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where enroll LIKE '" + txtSearchEnrollement.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            
            else
            {

                label9.Visible = true;
                Image image = Image.FromFile("C:/Users/n/source/repos/images/Liberay Management System/search.gif");
                pictureBox1.Image = image;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds); dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewStudent";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchEnrollement.Clear();
            panel2.Visible = false;
        }
        int bid;
        //for update 
        Int64 rowid;
        //cell click is and event 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            //show the second section when clicking on a student
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewStudent where stuid=" + bid + "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ///in this ds we have one table that is 0 table and in this table 
            ///we have rows and columns
            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtEnrollement.Text = ds.Tables[0].Rows[0][2].ToString();
            txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
            txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
            txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; 
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will Be Deleted confirm?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "Delete from NewStudent where stuid=" + rowid + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                RefreshDataGridView();
            }
        }

        private void update_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Data will Be Updated confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
              //  stuid,sname,enroll,dep,sem,contact,email
                String sname = txtName.Text;
                String enroll = txtEnrollement.Text;
                String dep = txtDepartment.Text;
                String sem = txtSemester.Text;
                Int64 contact = Int64.Parse(txtContact.Text);
                String email = txtEmail.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update  NewStudent set sname='" + sname + "',enroll='" + enroll + "',dep='" + dep + "',sem='" + sem + "',contact=" + contact + ",email='" + email + "' where stuid=" + rowid + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                RefreshDataGridView();
            }

        }
    }
}
