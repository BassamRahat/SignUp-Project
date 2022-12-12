using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignUp
{
    public partial class SignUp : Form
    {
        static SqlConnection conn=new SqlConnection("Data Source=DESKTOP-GOLQTE8;Initial Catalog=signUp;Integrated Security=True");
        static SqlCommand sqlcmd;

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            LogIn login = new LogIn();
            login.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //To Check that FirstName is less than 12 characters
            if (!FirstNameLength())
            {
                MessageBox.Show("FirstName should be less than 12 characters");
                return;
            }

            //To Check that LastName is less than 16 characters
            if (!LastNameLength())
            {
                MessageBox.Show("LastName should be less than 16 characters");
                return;
            }

            //To Check that email is valid or not
            if (!EmailIsValid(emailBox.Text))
            {
                MessageBox.Show("Please enter the valid email");
                return;
            }

            //To Check that passwords are matching
            if (!isPasswordMatching())
            {
                MessageBox.Show("Password not matching");
                return;
            }

            //To Check that password is valid or not
            if (!isValid(cPassBox.Text))
            {
                MessageBox.Show("Password is not in right format");
                return;
            }

            //To Check that Input Field is not Null
            if (!CheckInputField())
            {
                MessageBox.Show("Input Field is Empty");
                return;
            }


            string query = "INSERT INTO users VALUES(@FIRSTNAME,@LASTNAME,@EMAIL,@PASSWORD)";
            conn.Open();
            sqlcmd = new SqlCommand(query, conn);

            //Passing Parameters
            sqlcmd.Parameters.Add("@FIRSTNAME", SqlDbType.VarChar);
            sqlcmd.Parameters["@FIRSTNAME"].Value = FirstBox.Text;

            sqlcmd.Parameters.Add("@LASTNAME", SqlDbType.VarChar);
            sqlcmd.Parameters["@LASTNAME"].Value = LastBox.Text;

            sqlcmd.Parameters.Add("@EMAIL", SqlDbType.VarChar);
            sqlcmd.Parameters["@EMAIL"].Value = emailBox.Text;

            sqlcmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar);
            sqlcmd.Parameters["@PASSWORD"].Value = passBox.Text;

            sqlcmd.ExecuteNonQuery();
            conn.Close();


            MessageBox.Show("User Registred Successfully");
            Close();

        }

        //To check whether input field is empty or not
        bool CheckInputField()
        {
            if (string.IsNullOrEmpty(FirstBox.Text) ||
               string.IsNullOrEmpty(LastBox.Text) ||
               string.IsNullOrEmpty(emailBox.Text) ||
               string.IsNullOrEmpty(passBox.Text) ||
               string.IsNullOrEmpty(cPassBox.Text)
                )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //To check password is matching or not
        bool isPasswordMatching()
        {
            if (passBox.Text != cPassBox.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //To check that password is valid or not
        bool isValid(String password)
        {

            // for checking if password length
            // is between 8 and 15
            if (!((password.Length >= 8)
                && (password.Length <= 15)))
            {
                return false;
            }

            // to check space
            if (password.Contains(" "))
            {
                return false;
            }
            if (true)
            {
                int count = 0;

                // check digits from 0 to 9
                for (int i = 0; i <= 9; i++)
                {

                    // to convert int to string
                    String str1 = i.ToString();

                    if (password.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }

            // for special characters
            if (!(password.Contains("@") || password.Contains("#")
                || password.Contains("!") || password.Contains("~")
                || password.Contains("$") || password.Contains("%")
                || password.Contains("^") || password.Contains("&")
                || password.Contains("*") || password.Contains("(")
                || password.Contains(")") || password.Contains("-")
                || password.Contains("+") || password.Contains("/")
                || password.Contains(":") || password.Contains(".")
                || password.Contains(", ") || password.Contains("<")
                || password.Contains(">") || password.Contains("?")
                || password.Contains("|")))
            {
                return false;
            }

            if (true)
            {
                int count = 0;

                // checking capital letters
                for (int i = 65; i <= 90; i++)
                {

                    // type casting
                    char c = (char)i;

                    String str1 = c.ToString();
                    if (password.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }

            if (true)
            {
                int count = 0;

                // checking small letters
                for (int i = 97; i <= 122; i++)
                {

                    // type casting
                    char c = (char)i;
                    String str1 = c.ToString();

                    if (password.Contains(str1))
                    {
                        count = 1;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
            }

            // if all conditions fails
            return true;
        }

        //To check Email is valid or not
        bool EmailIsValid(string email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, string.Empty).Length == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //To check Length of First Name
        bool FirstNameLength()
        {
            if(FirstBox.Text.Length > 12)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //To check Length of Last Name
        bool LastNameLength()
        {
            if(LastBox.Text.Length > 16)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
}
}
