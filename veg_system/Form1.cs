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
    public partial class Form1 : Form
    {
        string queryfilter = "";
        public Form1()
        {
            InitializeComponent();
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displaydata();
            //need to do it with one query !
            //yeah w8 need to store the values in w8 w8
            /*try
            {
                //item name
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                string query = "select name,code,price from itemlist";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                comboBox1.DataSource = DS.Tables[0];
                comboBox1.ValueMember = "name";
                comboBox1.DisplayMember = "name";
                comboBox2.DataSource = DS.Tables[0];
                comboBox2.ValueMember = "code";
                comboBox2.DisplayMember = "code";
                comboBox3.DataSource = DS.Tables[0];
                comboBox3.ValueMember = "price";
                comboBox3.DisplayMember = "price";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
            /*try
            {
                //item code
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                string query = "select code from itemlist";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                comboBox2.DataSource = DS.Tables[0];
                comboBox2.ValueMember = "code";
                comboBox2.DisplayMember = "code";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                //item price
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                string query = "select price from itemlist";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                comboBox3.DataSource = DS.Tables[0];
                comboBox3.ValueMember = "price";
                comboBox3.DisplayMember = "price";
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void displaydata()
        {
            try
            {
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                string query = "select * from itemlist";
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                con.Close();
                comboBox1.Items.Clear();//Edit Item Code
                comboBox2.Items.Clear();//Filter Item Code
                comboBox3.Items.Clear();//Filter Item Name
                comboBox1.Items.Add("-");
                comboBox3.Items.Add("-");
                for (int i = 0; i <= DS.Tables[0].Rows.Count - 1; i++)
                {   
                    comboBox2.Items.Add(DS.Tables[0].Rows[i].ItemArray[0].ToString());
                    comboBox1.Items.Add(DS.Tables[0].Rows[i].ItemArray[0].ToString());
                    comboBox3.Items.Add(DS.Tables[0].Rows[i].ItemArray[1].ToString());
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detail d = new detail();
            d.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //price update
            //wait be table ma name su 6?
            if (comboBox2.Items.ToString() == "" )
            {
                MessageBox.Show("plz select item code !");
            }
            else
            {
                try
                {
                    //item price
                    string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                    string query = "update itemlist set price=@price where code=@code";
                    MySqlConnection con = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    //take value from combo and we dont need name to update right? yeah uniqu
                    con.Open();
                    cmd.Parameters.AddWithValue("@price", textBox3.Text);
                    cmd.Parameters.AddWithValue("@code", comboBox2.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Updated Successfully");
                    
                    con.Close();
                    displaydata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("-") && comboBox3.Text.Equals("-")) {
                displaydata();
            }
            try
            {
                string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                                
                MySqlConnection con = new MySqlConnection(connection);
                MySqlDataAdapter cmd = new MySqlDataAdapter(queryfilter, con);
                DataSet DS = new DataSet();
                cmd.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Please enter values for adding new item!");
            }
            else
            {
                try
                {
                    //item price
                    string connection = "datasource = 182.50.133.82; port = 3306; username = kp_lp; password = kanulp@512; database=converse";
                    string query = "insert into itemlist (code,name,price) values (@code,@name,@price)";
                    MySqlConnection con = new MySqlConnection(connection);
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    con.Open();
                    cmd.Parameters.AddWithValue("@price", textBox4.Text);
                    cmd.Parameters.AddWithValue("@code", textBox1.Text);
                    cmd.Parameters.AddWithValue("@name", textBox2.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Inserted Successfully");

                    con.Close();
                    displaydata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("-"))
            {
                comboBox3.Visible = true;
            }
            else
            {
                comboBox3.Visible = false;
            }
            queryfilter = "select * from itemlist where code = '" + comboBox1.Text + "'";

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Equals("-"))
            {
                comboBox1.Visible = true;
            }
            else {
                comboBox1.Visible = false;
            }
            queryfilter = "select * from itemlist where name = '" + comboBox3.Text + "'";

        }
    }
}
