﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem
{
    public partial class DoctorClinic : Form
    {

        private Button lastButtonClicked;
        private Doctor dr;
        public DoctorClinic(Doctor dr)
        {
            this.dr = dr;
            InitializeComponent();
            Clock.Text = DateTime.Now.ToString("HH:mm:ss");
            Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DoctorHome home = new DoctorHome(dr);
            loadForm(home);
            lastButtonClicked = Home;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Clock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void DateTimer_Tick(object sender, EventArgs e)
        {
            Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void ViewPatientD_Click(object sender, EventArgs e)
        {

            DoctorViewPatient view = new DoctorViewPatient(dr);
            loadForm(view);
        }

        private void loadForm(Form f)
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

        private void Home_Click(object sender, EventArgs e)
        {
            DoctorHome home = new DoctorHome(dr);
            loadForm(home);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            UserLoginForm login = new UserLoginForm();
            login.Show();
            this.Hide();
        }
    }
}
