using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ClinicSystem.AddPatient;
using static System.Windows.Forms.LinkLabel;

namespace ClinicSystem
{
    public partial class DoctorViewPatient : Form
    {
        private List<PatientAppointment> patientAppointments;
        private DatabasePatient db = new DatabasePatient();
        private DataGridViewRow lastSelectedRow = null;
        private DataGridViewCell lastSelectedCell = null;
        private DataTable dt;
        public DoctorViewPatient(Doctor dr)
        {
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
        }

        private void addRows(List<PatientAppointment> patientAppointments)
        {
            dt.Clear();
            foreach (PatientAppointment pa in patientAppointments)
            {
                bool dupli = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToInt32(row["PatientID"]) == pa.Patient.Patientid)
                    {
                        dupli = true;
                        break;
                    }
                }
                if (!dupli)
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
                PatientAppointment selectedPatient = null ;
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
                    patientDetails(selectedPatient);
                }

            }

        }


        private void patientDetails(PatientAppointment pa)
        {
            tbPatientId.Text = pa.Patient.Patientid.ToString(Name);
            RoomNo.Text = pa.RoomNo.ToString(Name);
            tbFullName.Text = pa.Patient.FirstName + "  " + pa.Patient.MiddleName + "  " + pa.Patient.LastName;
            tbAddress.Text = pa.Patient.Address;
            tbAge.Text = pa.Patient.Age.ToString();
            tbGender.Text = pa.Patient.Gender;
            tbContact.Text = pa.Patient.ContactNumber;
            datePickerBDay.Value = pa.Patient.BirthDate;

            

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

  
    }
}
