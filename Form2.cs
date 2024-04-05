using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace LoginFormOfDesktopApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Please fill the ID field!!");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Please fill the Name field!!");
            }
            else if (comboBox1.SelectedItem == null)
            {
                comboBox1.Focus();
                errorProvider3.SetError(this.comboBox1, "Please select a gender field!!");
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider4.SetError(this.textBox3, "Please fill the Address field!!");
            }
            else
            {
                try
                {
                    // Database connection string
                    string str = "Data Source=.;Initial Catalog=LogIn;Integrated Security=True;";

                    // SQL connection object
                    SqlConnection con = new SqlConnection(str);

                    string que = "select * from Customer_tbl where CustomerId=@cid";
                    SqlCommand cmd2 = new SqlCommand(que, con);
                   
                    cmd2.Parameters.AddWithValue("@cid", textBox1.Text);
                    con.Open();
                    SqlDataReader rd = cmd2.ExecuteReader();
                   
                    if (rd.HasRows)
                    {
                        MessageBox.Show($"{textBox1.Text} This CustomerId already exists in the customer table!!");
                       
                    }
                    else
                    {
                        con.Close();
                        // SQL query
                        string query = "insert into Customer_tbl values(@cid, @cname, @cgender, @caddress)";

                        // Create a command to execute the query
                        SqlCommand cmd = new SqlCommand(query, con);

                        // Set parameter values from text boxes
                        cmd.Parameters.AddWithValue("@cid", textBox1.Text);
                        cmd.Parameters.AddWithValue("@cname", textBox2.Text);
                        cmd.Parameters.AddWithValue("@cgender", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@caddress", textBox3.Text);
                        con.Open();
                        // Execute the command and return a row
                        int a = cmd.ExecuteNonQuery(); // Use this for insert, update, or delete operations
                        if (a >= 1)
                        {
                            MessageBox.Show("Data is inserted successfully!!");
                        }
                        else
                        {
                            MessageBox.Show("Data failed to insert");
                        }
                       
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    textBox1.Focus();

                    // Close the connection
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
               

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Please fill the ID field!!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Focus();
                errorProvider2.SetError(this.textBox2, "Please fill the Name field!!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null)
            {
                comboBox1.Focus();
                errorProvider3.SetError(this.comboBox1, "Please select a gender field!!");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.Focus();
                errorProvider4.SetError(this.textBox3, "Please fill the Address field!!");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) || ch == 8)
            {
                e.Handled = false;//it let go in text box;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetter(ch) || ch == 8 || ch == 32)
            {
                e.Handled = false;//it let go in text box;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            Program.ln.Show();//calling static var...
        }
    }
}
