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
        ///fill the combobox with books avai
        private void issueBook_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            try
            {
                con.Open();  
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
                ///3 code to count how many books has been issued for this enrollement no
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
        ///stop here 
        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                String enroll = txtEnrollement.Text;
                String sname = txtName.Text;
                String sdep = txtDepartment.Text;
                String sem = txtSemester.Text;
                Int64 contact = Int64.Parse(txtContact.Text);
                String email = txtEmail.Text;
                String bookName = comboBook.Text;
                String bookIssueDate = dateTimePicker1.Text;

                // Check if a book is selected
                if (comboBook.SelectedIndex != -1)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    try
                    {
                        con.Open();

                        // Check how many books have been issued to the student
                        cmd.CommandText = "SELECT COUNT(*) FROM IRBook WHERE std_enroll = '" + enroll + "' AND book_return_date IS NULL";
                        int issuedBooksCount = Convert.ToInt32(cmd.ExecuteScalar());

                        // Check if the maximum number of books allowed to be issued is not exceeded
                        if (issuedBooksCount <= 2) // Assuming max books allowed is 3
                        {
                            // Check the current quantity of the selected book
                            cmd.CommandText = "SELECT bQuan FROM NewBook WHERE bName = '" + bookName + "'";
                            int bookQuantity = Convert.ToInt32(cmd.ExecuteScalar());

                            // Check if the book quantity is > than zero
                            if (bookQuantity > 0)
                            {
                                // Insert record into IRBook table
                                cmd.CommandText = "INSERT INTO IRBook (std_enroll, std_name, std_dep, std_sem, std_contact, std_email, book_name, book_issue_date) " +
                                                   "VALUES ('" + enroll + "', '" + sname + "', '" + sdep + "', '" + sem + "', " + contact + ", '" + email + "', '" + bookName + "', '" + bookIssueDate + "')";
                                cmd.ExecuteNonQuery();

                                // Update NewBook table to decrease book quantity
                                cmd.CommandText = "UPDATE NewBook SET bQuan = bQuan - 1 WHERE bName = '" + bookName + "'";
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Book issued successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Book quantity is zero. Cannot issue this book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Maximum number of books issued (3) reached for this student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Enrolment Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
