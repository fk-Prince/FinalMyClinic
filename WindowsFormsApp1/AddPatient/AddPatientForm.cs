using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ClinicSystem.AddPatient;

namespace ClinicSystem
{
    public partial class AddPatientForm : Form
    {
        private DatabasePatient db = new DatabasePatient();
        private List<Operation> operations;
        private List<string> rooms;
        private List<Doctor> doctors;
        private Stack<string> text = new Stack<string>();
        private Operation selectedOperation;
        private Doctor selectedDoctor;
        private Dictionary<Operation, Doctor> doctorOperation = new Dictionary<Operation, Doctor>();
        private List<DoctorOperation> TemporaryStorage;
        private Operation lastSelected;
        private FrontDesk frontDesk;

        public AddPatientForm(FrontDesk frontDesk)
        {
            this.frontDesk = frontDesk;
            InitializeComponent();
            roomSettings();
            operationSettings();
            PatientOperationNo.Text = db.getLastOpeartionNo();
            Gender.Items.Add("Male");
            Gender.Items.Add("Female");
        }
        private void roomSettings()
        {
            if (comboRoom != null) comboRoom.Items.Clear();
            rooms = db.getRoomNo();
            if (rooms != null && rooms.Count != 0)
            {
                foreach (string room in rooms)
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

        private bool isComboValid()
        {
            if (comboOperation.SelectedItem == null || string.IsNullOrWhiteSpace(comboOperation.SelectedItem.ToString()))
            {
                MessageBox.Show("No Operation Selected", "No Operation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (comboOperation.SelectedItem.Equals("No Operation Available"))
            {
                MessageBox.Show("No Operation Available", "No Operation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (comboDoctor.SelectedItem == null || string.IsNullOrWhiteSpace(comboDoctor.SelectedItem.ToString()))
            {
                MessageBox.Show("No Doctor Selected", "No Doctor", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (comboDoctor.SelectedItem.Equals("No Doctor Available"))
            {
                MessageBox.Show("No Doctor Available", "No Doctor", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

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

        private void BirthDate_ValueChanged(object sender, EventArgs e)
        {

            if (DateTime.TryParse(BirthDate.Text, out DateTime birthDate))
            {
                DateTime todayDate = DateTime.Now;
                int age = todayDate.Year - birthDate.Year;

                if (todayDate.Month < birthDate.Month || (todayDate.Month == birthDate.Month && todayDate.Day < birthDate.Day))
                {
                    age--;  
                }
                Age.Text = age.ToString();
            }
        }

        private void RemoveStack_Click(object sender, EventArgs e)
        {
            if (text == null || text.Count == 0) return;
            text.Pop();
            tbListOperation.Text = "";
            foreach (string t in text)
            {
                tbListOperation.Text += t;
            }


            if (lastSelected != null && doctorOperation.ContainsKey(lastSelected))
            {
                var lastAdded = TemporaryStorage.FirstOrDefault(x => x.Operation == lastSelected);
                if (lastAdded != null) TemporaryStorage.Remove(lastAdded);
                
                doctorOperation.Remove(lastSelected);
                if (double.TryParse(TotalBill.Text, out double bill)){
                    bill -= lastSelected.Price;
                    TotalBill.Text = bill.ToString("F2");
                }
                if (text.Count == 0)
                {
                    double zeroB = 0;
                    TotalBill.Text = zeroB.ToString("F2");
                }
            }
        }

        private void AddPatient_Click(object sender, EventArgs e)
        {
            string fname = FirstName.Text;
            string mname = MiddleName.Text;
            string lname = LastName.Text;
            string address = Address.Text;
            string age = Age.Text;
            DateTime bday = BirthDate.Value;
            string contact = ContactNo.Text;

            if (string.IsNullOrWhiteSpace(fname) ||
              string.IsNullOrWhiteSpace(mname) ||
              string.IsNullOrWhiteSpace(lname) ||
              string.IsNullOrWhiteSpace(address) ||
              string.IsNullOrWhiteSpace(age) ||
              string.IsNullOrWhiteSpace(bday.ToString()))
            {
                MessageBox.Show("Please fill up all fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            if (comboRoom == null || comboRoom.SelectedItem == null)
            {
                MessageBox.Show("No Room Selected", "No Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }


            if (Gender.SelectedItem == null)
            {
                MessageBox.Show("Choose gender", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            string gender = Gender.SelectedItem.ToString();

            if (bday > DateTime.Now)
            {
                MessageBox.Show("Invalid Birthdate", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            int ageInt = 0;
            if (!int.TryParse(age, out ageInt))
            {
                MessageBox.Show("Invalid Age", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            if (ageInt > 120 || ageInt < 0)
            {
                MessageBox.Show("Invalid Age", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }


            if (!string.IsNullOrWhiteSpace(contact) &&
             (!long.TryParse(contact, out _) || !Regex.IsMatch(contact, @"^09\d{9}$")))
            {
                MessageBox.Show("Invalid Contact Number", "Invalid Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            if (doctorOperation.Count == 0)
            {
                MessageBox.Show("No Operation", "No Operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            double bill = 0;
            if (!double.TryParse(TotalBill.Text, out bill))
            {
                MessageBox.Show("Invalid Total Bill", "Invalid Total Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
            int roomNo = int.Parse(comboRoom.SelectedItem.ToString());


            Patient patient = new Patient(fname, mname, lname, ageInt, gender, address, contact, bday.Date);
            int patientid = db.insertPatient(patient, frontDesk.getId());
            if (patientid == 0)
            {
                return ;
            }
            PatientOperation patientOperation = new PatientOperation(roomNo, patientid, TemporaryStorage, bill);
            bool result = db.insertPatientOperation(patientOperation);
            if (result)

            {
                MessageBox.Show("Successfully Added Patient", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                roomSettings();
                text.Clear();
                selectedDoctor = null;
                doctorOperation.Clear();
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
                return ;
            }
        }

        private void AddStack_Click(object sender, EventArgs e)
        {
            bool valid = isComboValid();
            if (!valid)
            {
                return;
            }
            bool duplicate = isAlreadyAdded();
            if (duplicate)
            {
                return;
            }

          

            DateTime selectedDate = this.scheduleDate.Value.Date;
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

            string scheduleDate = this.scheduleDate.Value.ToString("yyyy-MM-dd");
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


            TemporaryStorage = new List<DoctorOperation>();
            DoctorOperation docOperation = new DoctorOperation(selectedDoctor, selectedOperation, new DoctorPatientSchedule(selectedDate, startTime, endTime));
        

            foreach (DoctorOperation docOp in TemporaryStorage){
                if (docOp.Doctor.DoctorID == selectedDoctor.DoctorID && docOp.Schedule.ScheduleDate == selectedDate && (startTime < docOp.Schedule.EndTime && endTime > docOp.Schedule.StartTime))
                {
                    MessageBox.Show("Schedule is not available", "Schedule Conflict2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            TemporaryStorage.Add(docOperation);
            doctorOperation[selectedOperation] = selectedDoctor;


            displayOperationAdded(scheduleDate,startTime, endTime);

            lastSelected = selectedOperation;
            MessageBox.Show("Operation Added", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            comboDoctor.SelectedItem = null;
            comboDoctor.SelectedIndex = -1;
            comboOperation.SelectedIndex = -1;
            StartTime.Text = "";
            EndTime.Text = "";
            calculateTotalBill();
        }

        private void displayOperationAdded(string scheduleD, TimeSpan startTime, TimeSpan endTime)
        {
            string fullname = selectedDoctor.DoctorLastName + ", " + selectedDoctor.DoctorFirstName + " " + selectedDoctor.DoctorMiddleName;
            string displayText = "Operation Name: " + selectedOperation.OperationName + Environment.NewLine + "Doctor Assigned: " + fullname + Environment.NewLine
                + "Date Schedule: " + scheduleD + Environment.NewLine + "StartTime: " + startTime + Environment.NewLine + "EndTime: " + endTime + Environment.NewLine +
                "---------------------------------------------------------------------------------------------------------------" + Environment.NewLine;
            tbListOperation.Text += displayText;
            text.Push(displayText);
        }

        private bool isAlreadyAdded()
        {
            if (TemporaryStorage != null && TemporaryStorage.Count != 0)
            {
                foreach (DoctorOperation vb in TemporaryStorage)
                {
                    if (vb.Operation.OperationCode.Equals(selectedOperation.OperationCode, StringComparison.OrdinalIgnoreCase) && vb.Doctor.DoctorID == selectedDoctor.DoctorID)
                    {
                        MessageBox.Show("This operation is already added.", "Duplicate Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }
            }

            return false;
        }

        private void comboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDoctor.SelectedIndex == -1) return;
            string[] doc = comboDoctor.SelectedItem.ToString().Split(',');
            if (doctors != null && doctors.Count != 0)
            {
                foreach (Doctor doctor in doctors)
                {
                    if (int.Parse(doc[0])  == doctor.DoctorID)
                    {
                        selectedDoctor = doctor;
                    }
                }
            }
        }
    }
}
