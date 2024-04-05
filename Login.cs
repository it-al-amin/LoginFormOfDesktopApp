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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           bool check = checkBox1.Checked;
           switch(check)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
                default:
                    textBox2.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void LogInbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please fill the both fields!!");
                return;
            }
            try
            {
                // Database connection string
                string str = "Data Source=.;Initial Catalog=LogIn;Integrated Security=True;";

                // SQL connection object
                SqlConnection con = new SqlConnection(str);
                con.Open();

                // SQL query
                string query = "SELECT * FROM SignUp WHERE (StuName=@user OR Email=@user) AND Password=@pass";

                // Create a command to execute the query
                SqlCommand cmd = new SqlCommand(query, con);

                // Set parameter values from text boxes
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                // Execute the command and return a row
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    MessageBox.Show("LOGIN SUCCESSFUL!!","SUCCESS",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Form2 fm = new Form2();
                    this.Hide();
                    fm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("LOGIN FAILED!!", "FAILURE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Close the connection
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Focus();
                errorProvider1.SetError(this.textBox1, "Please fill this field");
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
                errorProvider2.SetError(this.textBox2, "Please fill this field");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void Registerlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm sn = new SignUpForm();
            sn.ShowDialog();

        }
    }
}
