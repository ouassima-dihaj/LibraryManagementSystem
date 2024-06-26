﻿using System;
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
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtEnterEnroll.Text == "")
            {
                panel2.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            // Establish connection
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            // Construct the SQL query with proper usage of txtEnterEnroll.Text
            string enrollNumber = txtEnterEnroll.Text.Trim(); // Get the enrollment number from TextBox
            string query = "SELECT * FROM IRBook WHERE std_enroll = @enroll AND book_return_date IS NULL";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@enroll", enrollNumber); // Add parameter to the command

            try
            {
                con.Open();

                // Execute query and fill dataset
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                // Check if any rows are returned
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Bind dataset to DataGridView
                    dataGridView1.DataSource = ds.Tables[0];
                }
                else
                {
                    // Clear DataGridView if no rows found
                    dataGridView1.DataSource = null;
                    MessageBox.Show("No records found for this enrollment number.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close(); // Always close the connection
            }
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            txtEnterEnroll.Clear();

        }
        String bName;
        String bDate;
        Int64 rowId;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                rowId = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bName = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bDate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtBookName.Text = bName;
                txtBookIssueDate.Text = bDate;

            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            try
            {
                con.Open();

                // update book return date 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE IRBook SET book_return_date = '" + dateTimePicker1.Text + "' WHERE std_enroll = '" + txtEnterEnroll.Text + "' AND id = " + rowId;
                cmd.ExecuteNonQuery();

                // nom apartir de IRBook
                SqlCommand cmdGetBookName = new SqlCommand();
                cmdGetBookName.Connection = con;
                cmdGetBookName.CommandText = "SELECT book_name FROM IRBook WHERE id = " + rowId;
                string bookName = cmdGetBookName.ExecuteScalar().ToString();

                // update book quantite
                SqlCommand cmdUpdateBookQuantity = new SqlCommand();
                cmdUpdateBookQuantity.Connection = con;
                cmdUpdateBookQuantity.CommandText = "UPDATE NewBook SET bQuan = bQuan + 1 WHERE bName = '" + bookName + "'";
                cmdUpdateBookQuantity.ExecuteNonQuery();

                MessageBox.Show("Book Returned successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnBook_Load(this, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
              
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnterEnroll.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
