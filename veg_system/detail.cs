using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace veg_system
{
    public partial class detail : Form
    {
        String queryfilter="";
        public detail()
        {
            InitializeComponent();
        }

        private void detail_Load(object sender, EventArgs e)
        {
            string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("SELECT code from itemlist", con);
            con.Open();
            MySqlDataReader sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox2.Items.Add(sqlReader["code"].ToString());
            }

            sqlReader.Close();
            con.Close();
            displaydata();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
            string query = "update transaction set status=1 where t_id=@id"; //do it change status here 
            MySqlConnection con = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, con);
            String id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            con.Open();
            cmd.Parameters.AddWithValue("@id", id);
      

            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Successfully");

            con.Close();
            displaydata();

           

        }
        private void displaydatafilter(String queryfilter)
        {
            try
            {
                //current users
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(queryfilter, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                dataGridView1.Columns["t_id"].Visible = false;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void displaydata()
        {
            try
            {
                //current users
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                string query = "SELECT * FROM transaction";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                dataGridView1.Columns["t_id"].Visible = false;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                queryfilter = "select * from transaction where mobileno = '" + textBox1.Text + "' ";
                displaydatafilter(queryfilter);
                textBox1.Text = "";
            }
            else if (!textBox2.Text.Equals(""))
            {
                queryfilter = "select * from transaction where address = '" + textBox2.Text + "' ";
                displaydatafilter(queryfilter);
                textBox2.Text = "";
            }
            else if (!comboBox2.Text.Equals("") || !comboBox2.Text.Equals("-"))
            {
                queryfilter = "select * from transaction where itemcode = '" + comboBox2.Text + "' ";
                displaydatafilter(queryfilter);

            }else if{
                queryfilter = "select * from transaction where status = '" + comboBox5.Text + "' ";
                displaydatafilter(queryfilter);
            }
                
            else {
                MessageBox.Show("plz select only one filter");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
