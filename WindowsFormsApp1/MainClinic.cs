using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.AddPatient;
using WindowsFormsApp1.ViewPatients;

namespace WindowsFormsApp1
{
    public partial class Sidebar : Form
    {

        private Button lastButtonClicked;
        public Sidebar()
        {
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

        public void LoadForm(Form form)
        {
            if (mainpanel.Controls.Count > 0)
            {
                mainpanel.Controls.Clear();
            }
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            mainpanel.Controls.Add(form);
            mainpanel.Tag = form;
            form.Show();
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
            Environment.Exit(0);
        }

        private void AddPatientS_Click(object sender, EventArgs e)
        {
            AddPatientForm addPatientForm = new AddPatientForm();
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
    }
}
