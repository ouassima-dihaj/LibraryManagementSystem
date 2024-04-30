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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to Exit? This will DELETE your Unsaved Data ", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
            txtDepartment.Clear();
            txtEmail.Clear();
            txtEnrollement.Clear();
            txtSemester.Clear();
            txtName.Clear();
            txtContact.Clear();
        }

        private void btnSaveInfo_Click(object sender, EventArgs e)
        {
            if (txtContact.Text != "" &&
            txtDepartment.Text != "" &&
            txtEmail.Text != "" &&

            txtEnrollement.Text != "" &&
            txtName.Text != "")
            {
               // stuid,sname,enroll,dep,sem,contact,email
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
                cmd.CommandText = "insert into NewStudent (sname,enroll,dep,sem,contact,email) values('" + sname + "','" + enroll + "','" + dep + "','" + sem + "'," + contact + ",'" + email + "') ";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data saved ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Clear();
                txtEnrollement.Clear();
                txtDepartment.Clear();
                //dateTimePicker1.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
            else
            {
                MessageBox.Show("Empty Field Box Not Allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
