using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicSystem.AddPatient;
using MySql.Data.MySqlClient;

namespace ClinicSystem.Main2
{
    public class ScheduleDatabase
    {
        private const string driver = "server=localhost;username=root;pwd=root;database=myclinic_db";
        public List<PatientAppointment> getAppointments(Doctor dr)
        {
           List<PatientAppointment> pa = new List<PatientAppointment>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = 
                    "SELECT appointment_tbl.*, patientoperation_tbl.*, operation_tbl.*, patient_tbl.* FROM appointment_tbl " +
                    "LEFT JOIN patientoperation_tbl on patientoperation_tbl.patientoperationNo = appointment_tbl.patientoperationNo " +
                    "LEFT JOIN operation_tbl ON operation_tbl.OperationCode = appointment_tbl.OperationCode " +
                    "LEFT JOIN patient_tbl ON patientoperation_tbl.PatientID = patient_tbl.PatientID " +
                    "WHERE DoctorID = @DoctorID";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DoctorID", dr.DoctorID);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("PatientID");
                    string fname = reader.GetString("FirstName");
                    string mname = reader.GetString("MiddleName");
                    string lname = reader.GetString("LastName");
                    int age = reader.GetInt32("Age");
                    string gender = reader.GetString("Gender");
                    string contactNumber = reader.GetString("ContactNumber");
                    if (string.IsNullOrWhiteSpace(contactNumber))
                    {
                        contactNumber = "Not Provided.";
                    }

                    Patient patient = new Patient(id,fname,mname,lname,age,gender,contactNumber);
                    int roomNo = reader.GetInt32("RoomNo");

                    DateTime dateSchedule = reader.GetDateTime("DateSchedule");
                    TimeSpan startTime = reader.GetTimeSpan("StartTime");
                    TimeSpan endTime = reader.GetTimeSpan("EndTime");
                    string opName = reader.GetString("Name");
                    string opCode = reader.GetString("OperationCode");
                    DoctorPatientSchedule schedule = new DoctorPatientSchedule(dateSchedule,startTime,endTime);

                    PatientAppointment appointment = new PatientAppointment(roomNo,patient,schedule,opName,opCode);
                    pa.Add(appointment);
                }

                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error From getAppointments() db" + ex.Message);
            }
           return pa;
        }
    }
}
