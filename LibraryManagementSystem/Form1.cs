using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        ///close button ==> red 
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

        
        //to clear the textBox ==> mouseClick
        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
                //password 
                txtPassword.PasswordChar = '*';
            }
        }
        //to clear the textBox ==> mouseClick
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
            string query = "SELECT * FROM signUpTable WHERE username = @username";

            // Create a SqlConnection and SqlCommand
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameter to prevent SQL injection
                cmd.Parameters.AddWithValue("@username", txtUserName.Text);

                try
                {
                    con.Open();

                    // Execute the query and get the DataReader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if any rows were returned
                        if (reader.Read())
                        {
                            // Hash the entered password
                            string enteredPassword = txtPassword.Text;
                            string hashedPassword = HashPassword(enteredPassword);

                            // Compare the hashed entered password with the stored hashed password
                            if (hashedPassword == reader["pass"].ToString())
                            {
                                // Login successful
                                this.Hide(); // Hide the login form

                                // Open the Dashboard interface
                                Dashboard dashboardForm = new Dashboard();
                                dashboardForm.Show(); 
                            }
                            else
                            {
                                // Invalid password
                                MessageBox.Show("Invalid password!");
                            }
                        }
                        else
                        {
                            // Invalid username
                            MessageBox.Show("Invalid username or password!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Display error message
                }
            }
        }

        private string HashPassword(string password)
        {
            // Hash the password using SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }
    }
}
