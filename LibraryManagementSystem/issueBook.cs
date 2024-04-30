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
    public partial class issueBook : Form
    {
        public issueBook()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void issueBook_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            try
            {
                con.Open();  // Open the connection here
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select bName from NewBook";

                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        comboBook.Items.Add(sdr.GetString(i));
                    }
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        int count; 
        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            //1 check if the txtEnrollement is null 
                if (txtEnrollement.Text != "")
                {
                String eid = txtEnrollement.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where enroll='" + eid + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ////////////////////////////////////
                ///3 code to count how many books has benn issued on this enrollement no
                cmd.CommandText = "select count(std_enroll) from IRBook where std_enroll='" + eid + "' and book_return_date IS NULL";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
                ////                
                /////////////////////////////////////////
                //2 remplir  les input avec les information du etudiant of enroll entred 
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();

                }
                else
                {
                    txtName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid Enrollement No ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
       //i was stopped here continue ....
        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (comboBook.SelectedIndex != -1 && count <= 2)
                {
                    String enroll = txtEnrollement.Text;
                    String sname = txtName.Text;
                    String sdep = txtDepartment.Text;
                    String sem = txtSemester.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookName = comboBook.Text;
                    String bookIssueDate = dateTimePicker1.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into IRBook (std_enroll,std_name ,std_dep ,std_sem ,std_contact,std_email,book_name ,book_issue_date ) " +
                        "values('" + enroll + "','" + sname + "','" + sdep + "','" + sem + "'," + contact + ",'" + email + "','" + bookName + "','" + bookIssueDate + "') ";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Data saved ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("DaSelect Book ,Or Maximum number of Book has been issued    ", "No Book Selected ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            else
            {
                MessageBox.Show(" Enter Valid Enrollement Number ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void txtEnrollement_StyleChanged(object sender, EventArgs e)
        {

        }

        private void txtEnrollement_TextChanged(object sender, EventArgs e)
        {
            if (txtEnrollement.Text == "")
            {
                txtName.Clear();
                txtDepartment.Clear();
                txtContact.Clear();
                txtSemester.Clear();
                txtEmail.Clear();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollement.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("Are you Sure ?  ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }

        }
    }
}
