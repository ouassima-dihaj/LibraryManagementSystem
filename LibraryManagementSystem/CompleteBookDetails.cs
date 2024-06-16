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
                //avai books
                string queryNotReturned = "SELECT * FROM IRBook WHERE book_return_date IS NULL";
                SqlDataAdapter daNotReturned = new SqlDataAdapter(queryNotReturned, con);
                DataSet dsNotReturned = new DataSet();
                daNotReturned.Fill(dsNotReturned);
                dataGridView1.DataSource = dsNotReturned.Tables[0];
                
                string queryReturned = "SELECT * FROM IRBook WHERE book_return_date IS NOT NULL";
                SqlDataAdapter daReturned = new SqlDataAdapter(queryReturned, con);
                DataSet dsReturned = new DataSet();
                daReturned.Fill(dsReturned);
                dataGridView2.DataSource = dsReturned.Tables[0];
            }

            CustomizeDataGridView(dataGridView1);
            CustomizeDataGridView(dataGridView2);
        }

        private void CustomizeDataGridView(DataGridView dataGridView)
        {
           
            dataGridView.AutoGenerateColumns = true; 
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            dataGridView.RowHeadersVisible = false; 
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true; 
            dataGridView.AllowUserToAddRows = false; 
            dataGridView.AllowUserToDeleteRows = false; 
            dataGridView.AllowUserToOrderColumns = false; 
            dataGridView.AllowUserToResizeRows = false; 
            dataGridView.MultiSelect = false; 
            dataGridView.BorderStyle = BorderStyle.Fixed3D; 
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue; 
            dataGridView.EnableHeadersVisualStyles = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
