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

namespace SignUp
{
    public partial class LogIn : Form
    {
        static SqlConnection conn = new SqlConnection("Data Source=DESKTOP-GOLQTE8;Initial Catalog=signUp;Integrated Security=True");
        static SqlCommand sqlcmd;

        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            Hide();
            SignUp signup = new SignUp();
            signup.ShowDialog();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            bool isvalidemail = false; bool isvalidpassword = false;

            if (!CheckInputField())
            {
                MessageBox.Show("Input field is Empty");
                return;
            }

            string query = "SELECT * FROM users WHERE EMAIL=@EMAIL";
            conn.Open();
            sqlcmd = new SqlCommand(query, conn);

            //Passing Parameters
            sqlcmd.Parameters.Add("@EMAIL", SqlDbType.VarChar);
            sqlcmd.Parameters["@EMAIL"].Value = EmailBox.Text;

            SqlDataReader reader = sqlcmd.ExecuteReader();

            if (reader.HasRows)
            {
                isvalidemail = true;    
            }
            conn.Close();

            conn.Open();
            query = "SELECT * FROM users WHERE EMAIL=@EMAIL AND PASSWORD=@PASSWORD";
            sqlcmd = new SqlCommand(query, conn);

            //Passing Parameters
            sqlcmd.Parameters.Add("@EMAIL", SqlDbType.VarChar);
            sqlcmd.Parameters["@EMAIL"].Value = EmailBox.Text;

            sqlcmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar);
            sqlcmd.Parameters["@PASSWORD"].Value = PassTBox.Text;

            reader = sqlcmd.ExecuteReader();

            if (reader.HasRows)
            {
                isvalidpassword = true;
            }
            conn.Close();

            //Ckeck User and Password both exists in Database
            if (isvalidemail == false)
            {
                MessageBox.Show("Email does'nt exist!");
            }
            else if(isvalidemail == true && isvalidpassword == false)
            {
                MessageBox.Show("Wrong Password Entered!");
            }
            else
            {
                Hide();
                MessageBox.Show("Welcome to our Website");
                Close();
            }
            conn.Close();
        }

        //To check whether input field is empty or not
        bool CheckInputField()
        {
            if (string.IsNullOrEmpty(EmailBox.Text) ||
               string.IsNullOrEmpty(PassTBox.Text)
                )
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
