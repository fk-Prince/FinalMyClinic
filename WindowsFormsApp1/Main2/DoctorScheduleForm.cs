using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicSystem.AddPatient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ClinicSystem.Main2
{
    public partial class DoctorScheduleForm : Form
    {
        private List<PatientAppointment> patientAppointments;
        private ScheduleDatabase db = new ScheduleDatabase();
        public DoctorScheduleForm(Doctor dr)
        {
            InitializeComponent();
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            patientAppointments = db.getAppointments(dr);

            List<PatientAppointment> filtered = new List<PatientAppointment>(); 
            foreach (PatientAppointment pa in patientAppointments) {
                if (pa.Schedules.ScheduleDate == today)
                {
                    filtered.Add(pa);
                }
            }

            displaySchedules(filtered);
        }

        private void displaySchedules(List<PatientAppointment> patientAppointments)
        {
            flowPanel.Controls.Clear();
            foreach (PatientAppointment pa in patientAppointments)
            {
                Panel panel = new Panel();
                panel.Size = new Size(300, 290);
                panel.BackColor = Color.FromArgb(153, 180, 209);
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Margin = new Padding(15, 10, 10, 10);
                panel.Padding = new Padding(10, 10, 10, 10);

                Label label = createLabel("Room No", pa.RoomNo.ToString(), 10, 10);
                panel.Controls.Add(label);

                label = createLabel("Operation Code", pa.OperationCode, 10, 30);
                panel.Controls.Add(label);

                label = createLabel("Operation Name", pa.OperationName, 10, 50);
                panel.Controls.Add(label);

                label = createLabel("Schedule", pa.Schedules.ScheduleDate.ToString("yyyy-MM-dd"), 10, 70);
                panel.Controls.Add(label);  

                label = createLabel("Start-Time", pa.Schedules.StartTime.ToString(), 10, 90);
                panel.Controls.Add(label);

                label = createLabel("End-Time", pa.Schedules.EndTime.ToString(), 10,110);
                panel.Controls.Add(label);

                Panel panel2 = new Panel();
                panel2.BackColor = Color.Gray;
                panel2.Size = new Size(270, 2);
                panel2.Location = new Point(15, 150);
                panel.Controls.Add(panel2);

                label = createLabel("Patient ID", pa.Patient.Patientid.ToString(), 10, 170);
                panel.Controls.Add(label);

                string fullname = pa.Patient.FirstName + " " + pa.Patient.MiddleName + " " + pa.Patient.LastName;
                label = createLabel("Name", fullname, 10, 190);
                panel.Controls.Add(label);

                label = createLabel("Age", pa.Patient.Age.ToString(), 10, 210);
                panel.Controls.Add(label);

                label = createLabel("Gender", pa.Patient.Gender, 10, 230);
                panel.Controls.Add(label);

                label = createLabel("Contact Number", pa.Patient.ContactNumber, 10, 250);
                panel.Controls.Add(label);


                flowPanel.Controls.Add(panel);
            }
        }

        public Label createLabel(string title, string value, int x, int y)
        {
            Label label = new Label();
            label.Text = $"{title}:   {value}";
            label.MaximumSize = new Size(280, 30);
            label.AutoSize = true;
            label.Location = new Point(x, y);
            return label;
        }

        private void x_CheckedChanged(object sender, EventArgs e)
        {
            if (selection.Checked)
            {
                datePickDate.Visible = true;
            } else
            {
                datePickDate.Visible=false;
            }
            
        } 

        private void weekRadio_Check(object sender, EventArgs e)
        {
            DateTime week = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            List<PatientAppointment> filtered = new List<PatientAppointment>();
            foreach (PatientAppointment pa in patientAppointments)
            {
                if (week <= pa.Schedules.ScheduleDate && pa.Schedules.ScheduleDate < week.AddDays(7))
                {
                    filtered.Add(pa);
                }
            }

            displaySchedules(filtered);
        }

        private void monthRadio_Check(object sender, EventArgs e)
        {
            DateTime month = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime start = new DateTime(month.Year, month.Month, 1); 
            DateTime end = start.AddMonths(1).AddDays(-1); 

            List<PatientAppointment> filtered = new List<PatientAppointment>();

            foreach (PatientAppointment pa in patientAppointments)
            {
                if (pa.Schedules.ScheduleDate >= start && pa.Schedules.ScheduleDate <= end)
                {
                    filtered.Add(pa);
                }
            }

            displaySchedules(filtered);
        }

        private void allScheduleRadio_Check(object sender, EventArgs e)
        {
            displaySchedules(patientAppointments);
        }

        private void radioToday_Check(object sender, EventArgs e)
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            List<PatientAppointment> filtered = new List<PatientAppointment>();
            foreach (PatientAppointment pa in patientAppointments)
            {
                if (pa.Schedules.ScheduleDate == today)
                {
                    filtered.Add(pa);
                }
            }

            displaySchedules(filtered);
        }

        private void datePickDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = Convert.ToDateTime(datePickDate.Value.ToString("yyyy-MM-dd"));

            List<PatientAppointment> filtered = new List<PatientAppointment>();
            foreach (PatientAppointment pa in patientAppointments)
            {
                if (pa.Schedules.ScheduleDate == date)
                {
                    filtered.Add(pa);
                }
            }
            displaySchedules(filtered);
        }
    }
}
