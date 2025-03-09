using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace ClinicSystem
{
    public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
         
            Placeholder(Username, "Username                   ");
            Placeholder(Password, "Password                   ");
        }


        private void label3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = Username.Text.Trim();
            string password = Password.Text.Trim();

            try
            {
                if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Password.Text) || Username.Text == "User Name" || Password.Text == "Password")
                {
                    MessageBox.Show("Empty space, provide credentials", "Incomplete Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string driver = "server=localhost;username=root;password=root;database=myclinic_db";
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();

                string query = "SELECT Username, Password, FrontDeskID, Type FROM FRONTDESK_TBL WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";
                MySqlCommand command = new MySqlCommand(query, conn);

                command.Parameters.AddWithValue("@USERNAME", username);
                command.Parameters.AddWithValue("@PASSWORD", password);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    MessageBox.Show("Wrong Username or Password", "Incorrect Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                while (reader.Read())
                {
                    FrontDesk frontDesk = new FrontDesk(reader["Username"].ToString(), reader["Password"].ToString(), int.Parse(reader["FrontDeskID"].ToString()), reader["Type"].ToString());
                    MessageBox.Show("Successfully Login", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MainClinic mainClinic = new MainClinic(frontDesk);
                    mainClinic.Show();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Doctor_Click(object sender, EventArgs e)
        {
            this.Hide();
            DateTime date = Convert.ToDateTime("2011-11-12");
            Doctor dr = new Doctor(2,"Aeyc","Aba","Doe",54,"0928",date,"Male","Roxas");
            DoctorClinic doc = new DoctorClinic(dr);
            doc.Show();
        }

        private void ExitButton(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
            {
                Show();
            }
        }

        private void Placeholder(TextBox text, string placeholdler)
        {
            if (string.IsNullOrEmpty(text.Text))
            {
                text.Text = placeholdler;
                text.ForeColor = Color.Gray;
            }
        }

        private void checkPassword_CheckedChanged(object sender, MouseEventArgs e)
        {
            Password.UseSystemPasswordChar = !checkPassword.Checked;
        }

        private void EnterUsername(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;

            if (text != null)
            {

                if (text.Text == "Username                   ")
                {
                    text.Text = "";
                    text.ForeColor = Color.Black;
                }

            }
        }

        private void LeaveUsername(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;


            if (text != null)
            {
                if (string.IsNullOrEmpty(text.Text))
                {
                    text.Text = "Username                   ";
                    text.ForeColor = Color.Gray;
                }
                else
                {
                    text.ForeColor = Color.Black;
                }
            }

        }

        private void LeavePassword(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;

            if (string.IsNullOrEmpty(text.Text))
            {
                text.Text = "Password                   ";
                text.UseSystemPasswordChar = false;
                text.ForeColor = Color.Gray;
                checkPassword.Checked = false;
            }
            else
            {
                text.ForeColor = Color.Black;
                text.UseSystemPasswordChar = true;
            }
        }

        private void EnterPassword(object sender, EventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text != null)
            {
                if (text.Text.Equals("Password                   "))
                {
                    text.UseSystemPasswordChar = true;
                    text.Text = "";
                    text.ForeColor = Color.Black;
                }
            }
        }

        private bool x = true;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (x)
            {
               panel1.Focus();
                x = false;
            }
        }
    }
}