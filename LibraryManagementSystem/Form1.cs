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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_MouseEnter(object sender, EventArgs e)
        {
          
        }

        private void txtUserName_MouseEnter(object sender, EventArgs e)
        {
          
        }

        

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
                txtPassword.PasswordChar = '*';
            }
        }

        private void txtUserName_MouseClick_1(object sender, MouseEventArgs e)
        {
              if (txtUserName.Text == "Username")
                    {
                        txtUserName.Clear();
                    }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Connection string
            string connectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            // SQL query
            string query = "SELECT * FROM loginTable WHERE username = @username AND pass = @password";

            // Create a SqlConnection and SqlCommand
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters to prevent SQL injection
                cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                try
                {
                    con.Open();

                    // Create a new instance of SqlDataAdapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Create a new DataSet
                    DataSet ds = new DataSet();

                    // Fill the DataSet with data from the database
                    da.Fill(ds);

                    // Check if any rows were returned
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        // Login successful
                        //MessageBox.Show("Login successful!");

                        // Close the current login form
                        this.Hide(); // Hide the login form

                        // Open the Dashboard form
                       Dashboard dashboardForm = new Dashboard();
                        dashboardForm.Show(); // Show the dashboard form
                    }
                    else
                    {
                        // Invalid credentials
                        MessageBox.Show("Invalid username or password!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Display error message
                }
            }
        }

    }
}
