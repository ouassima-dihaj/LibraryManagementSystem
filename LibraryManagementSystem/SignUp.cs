using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtEmail.Text) &&
                !String.IsNullOrWhiteSpace(txtUserName.Text) &&
                !String.IsNullOrWhiteSpace(txtPasword.Text) &&
                !String.IsNullOrWhiteSpace(txtPasswordConfirm.Text))
            {
                // Validate email format
                if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Invalid email format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate passwords match
                if (txtPasword.Text != txtPasswordConfirm.Text)
                {
                    MessageBox.Show("Passwords do not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hash the password
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(txtPasword.Text);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    // stuid,sname,enroll,dep,sem,contact,email
                    String username = txtUserName.Text;
                    String password = hashedPassword;
                    String email = txtEmail.Text;

                    try
                    {
                        // Connect to database
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                        conn.Open();

                        // Check if username already exists
                        SqlCommand cmdCheckUsername = new SqlCommand("SELECT COUNT(*) FROM signUpTable WHERE username = @username", conn);
                        cmdCheckUsername.Parameters.AddWithValue("@username", username);
                        int usernameCount = (int)cmdCheckUsername.ExecuteScalar();

                        if (usernameCount > 0)
                        {
                            MessageBox.Show("Username already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Check if email already exists
                        SqlCommand cmdCheckEmail = new SqlCommand("SELECT COUNT(*) FROM signUpTable WHERE email = @email", conn);
                        cmdCheckEmail.Parameters.AddWithValue("@email", email);
                        int emailCount = (int)cmdCheckEmail.ExecuteScalar();

                        if (emailCount > 0)
                        {
                            MessageBox.Show("Email already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Insert data into database
                        SqlCommand cmd = new SqlCommand("insert into signUpTable (username,pass,email) values(@username, @password, @email)", conn);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.ExecuteNonQuery();

                        // Close connection
                        conn.Close();
                        txtEmail.Clear();
                        txtPasword.Clear();
                        txtUserName.Clear();
                        txtPasswordConfirm.Clear();

                        // Show success message
                        MessageBox.Show("User created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                       
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void txtUserName_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUserName.Text == "Username")
            {
                txtUserName.Clear();
            }
        }

        private void textPasword_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void txtPasswordConfirm_MouseClick(object sender, MouseEventArgs e)
        {
            
                 if (txtPasswordConfirm.Text == "Confirm Password")
            {
                txtPasswordConfirm.Clear();
            }

        }

        private void txtPasword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPasword.Text == "Password")
            {
                txtPasword.Clear();
            }
        }

        private void txteEmail_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtEmail.Text == "Email Address")
            {
                txtEmail.Clear();
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email Address")
            {
                txtEmail.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void exit(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
