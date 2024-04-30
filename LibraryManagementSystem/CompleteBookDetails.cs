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
    public partial class CompleteBookDetails : Form
    {
        public CompleteBookDetails()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CompleteBookDetails_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Retrieve books with NULL return date (books yet to be returned)
                string queryNotReturned = "SELECT * FROM IRBook WHERE book_return_date IS NULL";
                SqlDataAdapter daNotReturned = new SqlDataAdapter(queryNotReturned, con);
                DataSet dsNotReturned = new DataSet();
                daNotReturned.Fill(dsNotReturned);
                dataGridView1.DataSource = dsNotReturned.Tables[0];

                // Retrieve books with non-NULL return date (returned books)
                string queryReturned = "SELECT * FROM IRBook WHERE book_return_date IS NOT NULL";
                SqlDataAdapter daReturned = new SqlDataAdapter(queryReturned, con);
                DataSet dsReturned = new DataSet();
                daReturned.Fill(dsReturned);
                dataGridView2.DataSource = dsReturned.Tables[0];
            }

            // Customize DataGridView appearance and behavior
            CustomizeDataGridView(dataGridView1);
            CustomizeDataGridView(dataGridView2);
        }

        private void CustomizeDataGridView(DataGridView dataGridView)
        {
            // Set visual properties of the DataGridView
            dataGridView.AutoGenerateColumns = true; // Automatically generate columns
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill columns to fit the width
            dataGridView.RowHeadersVisible = false; // Hide row headers
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Allow full row selection
            dataGridView.ReadOnly = true; // Make the DataGridView read-only
            dataGridView.AllowUserToAddRows = false; // Disable adding rows
            dataGridView.AllowUserToDeleteRows = false; // Disable deleting rows
            dataGridView.AllowUserToOrderColumns = false; // Disable column reordering
            dataGridView.AllowUserToResizeRows = false; // Disable resizing rows
            dataGridView.MultiSelect = false; // Disable multi-selection
            dataGridView.BorderStyle = BorderStyle.Fixed3D; // Add a fixed 3D border
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold); // Customize column header font
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10); // Customize cell font
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; // Customize column header background color
            dataGridView.EnableHeadersVisualStyles = false; // Disable visual styles for headers (use custom styles)
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
