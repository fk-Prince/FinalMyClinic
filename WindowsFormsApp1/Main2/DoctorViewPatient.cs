using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ClinicSystem.AddPatient;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClinicSystem
{
    public partial class DoctorViewPatient : Form
    {
        private List<PatientAppointment> patientAppointments;
        private List<DoctorPatientSchedule> schedules;
        private DatabasePatient db = new DatabasePatient();
        private DataGridViewRow lastSelectedRow = null;
        private DataTable dt;
        private Doctor dr;
        private DoctorPatientSchedule lastSelectedSchedule;

        private int limitCharacter = 200;
        private int detailId;
        private TimeSpan duration;
        public DoctorViewPatient(Doctor dr)
        {
            this.dr = dr;
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("RoomNo", typeof(string));
            dt.Columns.Add("PatientID", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("MiddleName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Birth-Date", typeof(DateTime));
            patientAppointments = db.getPatients(dr.DoctorID);
            addRows(patientAppointments);

            patientGrid.DataSource = dt;
            patientGrid.Columns["Birth-Date"].DefaultCellStyle.Format = "yyyy-MM-dd";


            datePickerSchedule.Value = DateTimePicker.MinimumDateTime;
            datePickerBDay.Value = DateTimePicker.MinimumDateTime;
        }

        private void addRows(List<PatientAppointment> patientAppointments)
        {
            dt.Clear();
            foreach (PatientAppointment pa in patientAppointments)
            {
                bool duplicate = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["PatientID"]) == pa.Patient.Patientid)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    dt.Rows.Add(pa.RoomNo, pa.Patient.Patientid, pa.Patient.FirstName, pa.Patient.MiddleName, pa.Patient.LastName, pa.Patient.Gender, pa.Patient.Age, Convert.ToDateTime(pa.Patient.BirthDate.ToString("yyyy-MM-dd")));
                }
            }
        }

        private void patientClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = patientGrid.Rows[e.RowIndex];

                int patientID = Convert.ToInt32(row.Cells["PatientID"].Value);
                PatientAppointment selectedPatient = null;
                foreach (PatientAppointment pa in patientAppointments)
                {
                    if (pa.Patient.Patientid == patientID)
                    {
                        selectedPatient = pa;
                        break;
                    }
                }
                if (lastSelectedRow != null)
                {
                    lastSelectedRow.DefaultCellStyle.BackColor = Color.White;
                    lastSelectedRow.DefaultCellStyle.ForeColor = Color.Black;
                }

                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
                lastSelectedRow = row;


                if (selectedPatient != null)
                {
                    tabControl.SelectedIndex = 1;
                    clear();
                    patientDetails(selectedPatient);

                }

            }

        }

        private void clear()
        {
            comboOperations.Items.Clear();
            tbPatientId.Text = "";
            RoomNo.Text = "";
            tbFullName.Text = "";
            tbAddress.Text = "";
            tbAge.Text = "";
            tbGender.Text = "";
            tbContact.Text = "";
            datePickerSchedule.Value = DateTimePicker.MinimumDateTime;
            datePickerBDay.Value = DateTimePicker.MinimumDateTime;
            tbStartTime.Text = "";
            tbEndTime.Text = "";
        }

        private void patientDetails(PatientAppointment pa)
        {
            schedules = new List<DoctorPatientSchedule>();
            tbPatientId.Text = pa.Patient.Patientid.ToString();
            RoomNo.Text = pa.RoomNo.ToString();
            tbFullName.Text = pa.Patient.FirstName + "  " + pa.Patient.MiddleName + "  " + pa.Patient.LastName;
            tbAddress.Text = pa.Patient.Address;
            tbAge.Text = pa.Patient.Age.ToString();
            tbGender.Text = pa.Patient.Gender;
            tbContact.Text = pa.Patient.ContactNumber;
            datePickerBDay.Value = pa.Patient.BirthDate;
            foreach (PatientAppointment pas in patientAppointments)
            {
                if (pa.Patient.Patientid == pas.Patient.Patientid)
                {
                    schedules.Add(pas.Schedules);
                    comboOperations.Items.Add(pas.Schedules.OperationName);
                }
            }
        }

        private void searchPatient_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchPatient.Text))
            {
                addRows(patientAppointments);
            }
            else
            {
                string text = searchPatient.Text;
                List<PatientAppointment> filtered = new List<PatientAppointment>();
                if (int.TryParse(text, out int id))
                {
                    filtered = patientAppointments.Where(pa => pa.Patient.Patientid == id).ToList();
                }
                else
                {
                    filtered = patientAppointments.Where(pa =>
                    pa.Patient.FirstName.StartsWith(text) ||
                    pa.Patient.MiddleName.StartsWith(text) ||
                    pa.Patient.LastName.StartsWith(text)).ToList();
                }
                addRows(filtered);
            }
        }

        private void comboOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboOperations.SelectedIndex == -1)
            {
                tbStartTime.Text = "";
                tbEndTime.Text = "";
                detailId = 0;
                return;
            }
            string operationName = comboOperations.SelectedItem.ToString();
            foreach (DoctorPatientSchedule schedules in schedules)
            {
                if (schedules.OperationName.Equals(operationName))
                {
                    tbStartTime.Text = schedules.StartTime.ToString();
                    tbEndTime.Text = schedules.EndTime.ToString();
                    datePickerSchedule.Value = schedules.ScheduleDate;
                    duration = schedules.Duration;
                    detailId = schedules.DetailID;
                    tbDiagnosis.Text = schedules.Diagnosis;
                    lastSelectedSchedule = schedules;
                }
            }
        }

        private void TabChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void tbDiagnosis_TextChanged(object sender, EventArgs e)
        {
            string diagnosis = tbDiagnosis.Text;

            if (!string.IsNullOrWhiteSpace(diagnosis))
            {
                int total = limitCharacter - diagnosis.Length;
                if (total == 0)
                {
                    limit.Text = $"You reached limit 200 characters.";
                }
                else
                {
                    limit.Text = $"Up to {total.ToString()} characters.";
                }
            }
        }

        private bool isInputValid()
        {
            if (comboOperations.SelectedIndex == -1)
            {
                MessageBox.Show("Please select operation.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(tbStartTime.Text) ||
               string.IsNullOrWhiteSpace(tbEndTime.Text) )
            {
                MessageBox.Show("Please fill up all fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime selectedDate = datePickerSchedule.Value.Date;
 
            TimeSpan startTime;
            if (!TimeSpan.TryParseExact(tbStartTime.Text, @"hh\:mm\:ss", null, out startTime))
            {
                MessageBox.Show("Invalid Time, Please enter HH:MM:SS.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime currentDateTime = DateTime.Now;
            DateTime selectedStartDateTime = selectedDate.Add(startTime);
            if (selectedStartDateTime < currentDateTime)
            {
                MessageBox.Show("Time is already past", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool isScheduleAvailable = db.isScheduleAvailable(dr.DoctorID, selectedDate, startTime, TimeSpan.Parse(tbEndTime.Text));

            if (lastSelectedSchedule == null)
            {
                return false;
            }

            if (lastSelectedSchedule.StartTime == startTime && lastSelectedSchedule.ScheduleDate.Date == selectedDate.Date)
            {
                isScheduleAvailable = true;
            }
            if (!isScheduleAvailable)
            {
                MessageBox.Show("Schedule is not available", "Schedule Conflict1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void tbStartTime_TextChanged(object sender, EventArgs e)
        {
            TimeSpan startTime;
            if (TimeSpan.TryParseExact(tbStartTime.Text, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out startTime))
            {
                if (duration == null) return;
                TimeSpan endTime = startTime + duration;
                if (endTime.TotalHours >= 24)
                {
                    endTime = TimeSpan.FromHours(endTime.TotalHours % 24);
                }
                tbEndTime.Text = endTime.ToString();
            }        
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool validInput = isInputValid();
            if (!validInput)
            {
                return;
            }
            DialogResult output = MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (output != DialogResult.Yes)
            {
                return;
            }

            string diagnosis = tbDiagnosis.Text;
            DateTime scheduleDate = datePickerSchedule.Value.Date;
            TimeSpan startTime = TimeSpan.Parse(tbStartTime.Text);
            TimeSpan endTime = TimeSpan.Parse(tbEndTime.Text);
            string operationName = comboOperations.SelectedIndex.ToString();
            DoctorPatientSchedule updatedSchedule = new DoctorPatientSchedule(scheduleDate, startTime, endTime, operationName, detailId, duration, diagnosis);
            bool success = db.updateSchedule(updatedSchedule);
            if (success)
            {
                patientAppointments = db.getPatients(dr.DoctorID);
                MessageBox.Show("Appointment Updated", "Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
