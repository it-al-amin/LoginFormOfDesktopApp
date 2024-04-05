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
using System.Text.RegularExpressions;//for email validation pattern
namespace LoginFormOfDesktopApp
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        private void id_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id.Text))
            {
                id.Focus();
                errorProvider1.SetError(this.id, "Please fill the id!");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch)||ch==8)
            {
                e.Handled = false;//it let go in text box;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void name_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(name.Text))
            {
                name.Focus();
                errorProvider2.SetError(this.name, "Please fill the student name!");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsLetter(ch) || ch == 8||ch==32)
            {
                e.Handled = false;//it let go in text box;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void FatherName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FatherName.Text))
            {
                FatherName.Focus();
                errorProvider3.SetError(this.FatherName, "Please fill the father name!");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void FatherName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void SurName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SurName.Text))
            {
                SurName.Focus();
                errorProvider4.SetError(this.SurName, "Please fill the sur name!");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void SurName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void gender_Leave(object sender, EventArgs e)
        {
            if (gender.SelectedItem == null)
            {
                gender.Focus();
                errorProvider5.SetError(this.gender, "Please select a gender field!!");
            }
            else
            {
                errorProvider5.Clear();
            }
        }

        private void Standard_Leave(object sender, EventArgs e)
        {
            if(Standard.Value==0)
            {
                Standard.Focus();
                errorProvider6.SetError(this.Standard, "Please select a class!");
            }
            else
            {
                errorProvider6.Clear();
            }
        }
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string pass = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
        private void email_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(email.Text, pattern))
            {
                email.Focus();
                errorProvider7.SetError(this.email, "Pease fill the valid email!");
            }
            else
            {
                errorProvider7.Clear();
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(password.Text, pass))
            {
                password.Focus();
                errorProvider8.SetError(this.password, "Pease fill the valid password!");
            }
            else
            {
                errorProvider8.Clear();
            }
        }

        private void confirmPassword_Leave(object sender, EventArgs e)
        {
            if (password.Text!=confirmPassword.Text)
            {
                confirmPassword.Focus();
                errorProvider9.SetError(this.confirmPassword, "Pease fill the valid password!");
            }
            else
            {
                errorProvider9.Clear();
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(id.Text))
            {
                id.Focus();
                errorProvider1.SetError(this.id, "Please fill the id!");
            }
            else if (string.IsNullOrEmpty(name.Text))
            {
                name.Focus();
                errorProvider2.SetError(this.name, "Please fill the student name!");
            }
            else if (string.IsNullOrEmpty(FatherName.Text))
            {
                FatherName.Focus();
                errorProvider3.SetError(this.FatherName, "Please fill the father name!");
            }
            else if (string.IsNullOrEmpty(SurName.Text))
            {
                SurName.Focus();
                errorProvider4.SetError(this.SurName, "Please fill the sur name!");
            }
            else if (gender.SelectedItem == null)
            {
                gender.Focus();
                errorProvider5.SetError(this.gender, "Please select a gender field!!");
            }
            else if (Standard.Value == 0)
            {
                Standard.Focus();
                errorProvider6.SetError(this.Standard, "Please select a class!");
            }
            else if (!Regex.IsMatch(email.Text, pattern))
            {
                email.Focus();
                errorProvider7.SetError(this.email, "Pease fill the valid email!");
            }
            else if (!Regex.IsMatch(password.Text, pass))
            {
                password.Focus();
                errorProvider8.SetError(this.password, "Pease fill the valid password!");
            }
            else if (password.Text != confirmPassword.Text)
            {
                confirmPassword.Focus();
                errorProvider9.SetError(this.confirmPassword, "Pease fill the valid password!");
            }
            else
            {
                try
                {
                    // Database connection string
                    string str = "Data Source=.;Initial Catalog=LogIn;Integrated Security=True;";

                    // SQL connection object
                    SqlConnection con = new SqlConnection(str);

                    string que = "select * from SignUp where Id=@cid";
                    SqlCommand cmd2 = new SqlCommand(que, con);

                    cmd2.Parameters.AddWithValue("@cid", id.Text);
                    con.Open();
                    SqlDataReader rd = cmd2.ExecuteReader();
                    if(rd.HasRows)
                    {
                        MessageBox.Show($"{id.Text} This Id already exists in theSignUp table!!");

                    }
                    else
                    {
                        con.Close();
                        // SQL query
                        string query = "insert into SignUp values(@cid, @cname, @cfname,@csurname,@cstandard,@cgender, @cemail,@cpass)";

                        // Create a command to execute the query
                        SqlCommand cmd = new SqlCommand(query, con);

                        // Set parameter values from text boxes
                        cmd.Parameters.AddWithValue("@cid", id.Text);
                        cmd.Parameters.AddWithValue("@cname", name.Text);
                        cmd.Parameters.AddWithValue("@cfname", FatherName.Text);
                        cmd.Parameters.AddWithValue("@csurname", SurName.Text);
                        cmd.Parameters.AddWithValue("@cgender", gender.SelectedItem);
                        cmd.Parameters.AddWithValue("@cstandard", Standard.Value);
                        cmd.Parameters.AddWithValue("@cemail", email.Text);
                        cmd.Parameters.AddWithValue("@cpass", password.Text);
                       
                        con.Open();
                        // Execute the command and return a row
                        int a = cmd.ExecuteNonQuery(); // Use this for insert, update, or delete operations
                        if (a >= 1)
                        {
                            MessageBox.Show("Registered Successfully!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            this.Hide();
                            Login ln = new Login();
                            ln.Show();
                        }
                        else
                        {
                            MessageBox.Show("Registeration is failed!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    id.Focus();

                    // Close the connection
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            id.Text = "";
            id.Focus();
            name.Text = "";
            FatherName.Text = "";
            SurName.Text = "";
            gender.Text ="";
            Standard.Value = 0;
            email.Text = "";
            password.Text = "";
            confirmPassword.Text = "";

        }
    }
}
