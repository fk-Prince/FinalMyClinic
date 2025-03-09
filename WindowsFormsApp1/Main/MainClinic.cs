using System;
using System.Drawing;
using System.Windows.Forms;
using ClinicSystem.ViewPatients;

namespace ClinicSystem
{
    public partial class MainClinic : Form
    {

        private Button lastButtonClicked;
        private FrontDesk FrontDesk;
        public MainClinic(FrontDesk frontDesk)
        {
            this.FrontDesk = frontDesk;
            InitializeComponent();
            Clock.Text = DateTime.Now.ToString("HH:mm:ss");
            Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lastButtonClicked = DashboardS;
        }


        private void ViewPatientSide_Click(object sender, EventArgs e)
        {
            ViewPatientForm viewPatientForm = new ViewPatientForm();
            LoadForm(viewPatientForm);

        }

        public void LoadForm(Form f)
        {
            if (mainpanel.Controls.Count > 0)
            {
                mainpanel.Controls.Clear();
            }
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            mainpanel.Controls.Add(f);
            mainpanel.Tag = f;
            f.Show();
        }


        private void mouseClicked(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != lastButtonClicked)
            {
                btn.BackColor = Color.FromArgb(119, 136, 152);
                btn.ForeColor = Color.White;
                lastButtonClicked.BackColor = Color.FromArgb(255, 255, 255);
                lastButtonClicked.ForeColor = Color.Black;
                lastButtonClicked = btn;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserLoginForm login = new UserLoginForm();
            login.Show();
        }

        private void AddPatientS_Click(object sender, EventArgs e)
        {
            AddPatientForm addPatientForm = new AddPatientForm(FrontDesk);
            LoadForm(addPatientForm);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Clock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OperationForm operationForm = new OperationForm(FrontDesk);
            LoadForm(operationForm);
        }
    }
}
