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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void RefreshDataGridView()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from NewBook", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void ViewBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            
            cmd.CommandText = "select * from NewBook";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }
        int bid;
        //for update 
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bid=int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
               // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());//print the id

            }
            //show the second section when clicking on a book
            panel2.Visible = true;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select * from NewBook where bid="+bid+"";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ///in this ds we have one table that is 0 table and in this table 
            ///we have rows and columns
            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            txtBName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthor.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPublication.Text=  ds.Tables[0].Rows[0][3].ToString();
            txtBDate.Text = ds.Tables[0].Rows[0][4].ToString();
            txtPrice.Text = ds.Tables[0].Rows[0][5].ToString();
            txtQuantity.Text = ds.Tables[0].Rows[0][6].ToString();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; 
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if (txtBookName.Text != "")
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewBook where bName LIKE '"+txtBookName.Text+"%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewBook";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds); dataGridView1.DataSource = ds.Tables[0];
            }
        }
        private void btnRefresh_click(object sender, EventArgs e)
        {
            txtBookName.Clear();
            panel2.Visible = false;
        }

        private void update_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Data will Be Updated confirm?","Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                String bname = txtBName.Text;
                String bauthor = txtAuthor.Text;
                String publication = txtPublication.Text;
                String pdate = txtBDate.Text;
                Int64 price = Int64.Parse(txtPrice.Text);
                Int64 quan = Int64.Parse(txtQuantity.Text);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=DESKTOP-P4NI2GF\\SQLEXPRESS01;Database=library;Integrated Security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update  NewBook set bName='" + bname + "',bAuthor='" + bauthor + "',bPubl='" + publication + "',bPDate='" + pdate + "',bPrice=" + price + ",bQuan=" + quan + " where bid=" + rowid + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                RefreshDataGridView();
            }
           
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
                cmd.CommandText = "Delete from NewBook where bid=" + rowid + " ";
                cmd.ExecuteNonQuery();
                con.Close();
                RefreshDataGridView();
            }
        }
    }
}
