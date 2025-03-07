using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsApp1.AddPatient
{
    public partial class AddPatientForm : Form
    {
        private DatabaseAddPatient db = new DatabaseAddPatient();
        private List<Operation> operations;
        private List<string> rooms;
        private List<Doctor> doctors;
        private Stack<string> text = new Stack<string>();
        private Operation selectedOperation;
        private Doctor selectedDoctor;
        private Dictionary<Operation, Doctor> doctorOperation = new Dictionary<Operation, Doctor>();
        public AddPatientForm()
        {
            InitializeComponent();
            roomSettings();
            operationSettings();
            PatientOperationNo.Text = db.getLastOpeartionNo();
            Gender.Items.Add("Male");
            Gender.Items.Add("Female");
        }

        private void operationSettings()
        {
            operations = db.getOperations();
            if (operations != null && operations.Count != 0)
            {
                foreach (Operation operation in operations)
                {
                    comboOperation.Items.Add(operation.OperationName);
                }
            }
            else
            {
                comboRoom.Items.Add("No Operation Available");
            }
             comboOperation.SelectedIndex = -1;
        }

        private void roomSettings()
        {
            if (comboRoom != null) comboRoom.Items.Clear();
            rooms = db.getRoomNo();
            if (rooms != null && rooms.Count != 0)
            {
                foreach (String room in rooms)
                {
                    comboRoom.Items.Add(room);
                }
                comboRoom.SelectedIndex = 0;
            }
            else
            {
                comboRoom.Items.Add("No available rooms");
            }
        }

        private void comboOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboDoctor.Items.Clear();
            if (comboOperation == null || comboOperation.SelectedItem == null) return;
            string operationSelected = comboOperation.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(operationSelected))
            {
                return;
            }
            selectedOperation = null;
            foreach (Operation operation in operations)
            {
                if (operation.OperationName.Equals(operationSelected, StringComparison.OrdinalIgnoreCase))
                {
                    selectedOperation = operation;
                    break;
                }
            }

            doctors = db.getDoctors(selectedOperation);
            if (doctors != null && doctors.Count != 0)
            {
                foreach (Doctor doctor in doctors)
                {
                    selectedDoctor = doctor;
                    comboDoctor.Items.Add(doctor.DoctorID + ",   " + doctor.DoctorLastName + ", " + doctor.DoctorFirstName + " " + doctor.DoctorMiddleName);
                }
            }
            else
            {
                comboDoctor.Items.Add("No Doctor Available");
            }
            comboDoctor.SelectedIndex = 0;
            calculateEndTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboOperation.SelectedItem == null || string.IsNullOrWhiteSpace(comboOperation.SelectedItem.ToString()))
            {
                MessageBox.Show("No Operation Selected", "No Operation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            if (comboOperation.SelectedItem.Equals("No Operation Available"))
            {
                MessageBox.Show("No Operation Available", "No Operation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            if (comboDoctor.SelectedItem == null || string.IsNullOrWhiteSpace(comboDoctor.SelectedItem.ToString()))
            {
                MessageBox.Show("No Doctor Selected", "No Doctor", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            if (comboDoctor.SelectedItem.Equals("No Doctor Available"))
            {
                MessageBox.Show("No Doctor Available", "No Doctor", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }

            foreach (var pair in doctorOperation.ToList())
            {
                if (pair.Key.OperationCode.Equals(selectedOperation.OperationCode, StringComparison.OrdinalIgnoreCase)
                    && pair.Value.DoctorID == selectedDoctor.DoctorID)
                {
                    MessageBox.Show("This operation is already assigned", "Duplicate Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DateTime selectedDate = scheduleDate.Value.Date;
            DateTime currentDateTime = DateTime.Now;

            TimeSpan startTime;
            if (!TimeSpan.TryParseExact(StartTime.Text, @"hh\:mm\:ss", null, out startTime))
            {
                MessageBox.Show("Invalid Time, Please enter HH:MM:SS.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime selectedStartDateTime = selectedDate.Add(startTime);
            if (selectedStartDateTime < currentDateTime)
            {
                MessageBox.Show("Time is already past", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string scheduleD = scheduleDate.Value.ToString("yyyy-MM-dd");
            TimeSpan endTime = startTime + selectedOperation.Duration;
            if (endTime.TotalHours >= 24)
            {
                endTime = TimeSpan.FromHours(endTime.TotalHours % 24);
            }

            bool isScheduleAvailable = db.isScheduleAvailable(selectedDoctor.DoctorID, selectedDate, startTime, endTime);
            if (!isScheduleAvailable)
            {
                MessageBox.Show("Schedule is not available", "Schedule Conflict1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool conflictExists = tupleSchedule.Any(s =>
                s.Item1.DoctorID == selectedDoctor.DoctorID &&
                s.Item3 == selectedDate &&
                (startTime < s.Item5 && endTime > s.Item4)
            );

            if (conflictExists)
            {
                MessageBox.Show("Schedule is not available", "Schedule Conflict2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fullname = selectedDoctor.DoctorLastName + ", " + selectedDoctor.DoctorFirstName + " " + selectedDoctor.DoctorMiddleName;
            string o = "Operation Name: " + selectedOperation.OperationName + Environment.NewLine + "Doctor Assigned: " + fullname + Environment.NewLine
                + "Date Schedule: " + scheduleD + Environment.NewLine + "StartTime: " + startTime + Environment.NewLine + "EndTime: " + endTime + Environment.NewLine +
                "---------------------------------------------------------------------------------------------------------------" + Environment.NewLine;
            text.Push(o);
            tupleSchedule.Add(Tuple.Create(selectedDoctor,selectedOperation, selectedDate, startTime, endTime));

            doctorOperation[selectedOperation] = selectedDoctor;
            tbListOperation.Text += o;
            MessageBox.Show("Operation Added", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboDoctor.SelectedItem = null;
            comboDoctor.SelectedIndex = -1;
            comboOperation.SelectedIndex = -1;
            StartTime.Text = "";
            EndTime.Text = "";
            calculateTotalBill();
        }
        private List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>> tupleSchedule = new List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>>();


        private void calculateTotalBill()
        {
            double totalBill = 0;
            foreach (var pair in doctorOperation)
            {
                totalBill += pair.Key.Price;    
            }
            TotalBill.Text = totalBill.ToString("F2");
        }

        private void calculateEndTime()
        {
            TimeSpan startTime;
            if (!TimeSpan.TryParseExact(StartTime.Text, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out startTime))
            {
                if (checkEndTime.Checked)
                {
                    MessageBox.Show("Invalid Time, Please enter HH:MM:SS.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkEndTime.Checked = false;
                    EndTime.Text = "";
                    return;
                }
            }
            if (TimeSpan.TryParseExact(StartTime.Text, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out startTime))
            {
                if (selectedOperation == null) return;
                TimeSpan endTime = startTime + selectedOperation.Duration;
                if (endTime.TotalHours >= 24)
                {
                    endTime = TimeSpan.FromHours(endTime.TotalHours % 24);
                }
                EndTime.Text = endTime.ToString();
            }
        }
        private void StartTime_TextChanged(object sender, EventArgs e)
        {
            calculateEndTime();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (text == null || text.Count == 0) return;
            text.Pop();
            tbListOperation.Text = "";
            foreach (string t in text)
            {
                tbListOperation.Text += t;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkEmptyInputs();
        }
        public bool checkEmptyInputs()
        {
            string fname = FirstName.Text;
            string mname = MiddleName.Text;
            string lname = LastName.Text;
            string address = Address.Text;
            string age = Age.Text;
            DateTime bday = BirthDate.Value;
            string contact = ContactNo.Text;

            if (comboRoom == null || comboRoom.SelectedItem == null)
            {
                MessageBox.Show("No Room Selected", "No Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (Gender.SelectedItem == null)
            {
                MessageBox.Show("Choose gender", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false ;
            }

            string gender = Gender.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(fname) || 
                string.IsNullOrWhiteSpace(mname) || 
                string.IsNullOrWhiteSpace(lname) || 
                string.IsNullOrWhiteSpace(address) || 
                string.IsNullOrWhiteSpace(age) || 
                string.IsNullOrWhiteSpace(bday.ToString()) )
            {
                MessageBox.Show("Please fill up all fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int ageInt = 0;
            if (!int.TryParse(age, out ageInt))
            {
                MessageBox.Show("Invalid Age", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ageInt > 120 || ageInt < 0)
            {
                MessageBox.Show("Invalid Age", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (bday > DateTime.Now)
            {
                MessageBox.Show("Invalid Birthdate", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(contact) &&
             (!long.TryParse(contact, out _) || !Regex.IsMatch(contact, @"^09\d{9}$")))
            {
                MessageBox.Show("Invalid Contact Number", "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (doctorOperation.Count == 0)
            {
                MessageBox.Show("No Operation", "No Operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            double bill = 0;
            if (double.TryParse(TotalBill.Text, out _))
            {
                MessageBox.Show("Invalid Total Bill", "Invalid Total Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Patient patient = new Patient(fname, mname, lname, ageInt, gender, address, contact, bday.Date);
            int patientid = db.insertPatient(patient);

            int roomNo = int.Parse(comboRoom.SelectedItem.ToString());
            if (patientid == 0)
            {
                return false;
            }
            PatientOperation patientOperation = new PatientOperation(roomNo, patientid, tupleSchedule,bill);
            bool result = db.insertPatientOperation(patientOperation);
            if (result)
            {
                MessageBox.Show("Successfully Added Patient", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                roomSettings();
                text.Clear();
                tupleSchedule.Clear();
                selectedDoctor = null;
                selectedOperation = null;
                PatientOperationNo.Text = db.getLastOpeartionNo();
                FirstName.Text = "";
                MiddleName.Text = "";
                LastName.Text = "";
                Age.Text = "";
                Address.Text = "";
                ContactNo.Text = "";
                BirthDate.Value = DateTime.Now;
                TotalBill.Text = "";
                tbListOperation.Text = "";
                scheduleDate.Value = DateTime.Now;
                return true;
            }

            return false;
        }
    }
}
