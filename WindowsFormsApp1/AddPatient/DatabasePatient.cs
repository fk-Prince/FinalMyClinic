using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicSystem.AddPatient;
using MySql.Data.MySqlClient;

namespace ClinicSystem
{
    public class DatabasePatient
    {
        public DatabasePatient() { }

        public List<string> getRoomNo()
        {
            List<string> roomNo = new List<string>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                MySqlCommand command = new MySqlCommand(
                    "SELECT RoomNo FROM Rooms_tbl WHERE Occupation != 'Occupied'",
                    conn
                );
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roomNo.Add(reader["RoomNo"].ToString());
                }
                conn.Close();
                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from getRoomNo DB" + e.Message);
            }
            return roomNo;
        }

        public List<Operation> getOperations()
        {
            List<Operation> operations = new List<Operation>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Operation_Tbl", conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Operation operation = new Operation(
                        reader.GetString("OperationCode"),
                        reader.GetString("Name"),
                        reader.GetDateTime("DateAdded"),
                        reader.GetString("Description"),
                        reader.GetDouble("Price"),
                        reader.GetTimeSpan("Duration")
                    );
                    operations.Add(operation);
                }
                conn.Close();
                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from getOperations DB" + e.Message);
            }
            return operations;
        }

        public List<Doctor> getDoctors(Operation operation)
        {
            List<Doctor> doctorList = new List<Doctor>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT *, doctor_tbl.* FROM doctoroperation_tbl " +
                    "LEFT JOIN doctor_tbl " +
                    "ON doctoroperation_tbl.DoctorID = doctor_tbl.DoctorID " +
                    "WHERE operationcode = @operationcode";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@operationcode", operation.OperationCode);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor(
                        reader.GetInt32("DoctorID"),
                        reader.GetString("FirstName"),
                        reader.GetString("MiddleName"),
                        reader.GetString("LastName"),
                        reader.GetInt32("Age"),
                        reader.GetString("Pin"),
                        reader.GetDateTime("DateHired"),
                        reader.GetString("Gender"),
                        reader.GetString("Address")
                     );
                    doctorList.Add(doctor);
                }
                conn.Close();
                reader.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from getDoctors DB" + e.Message);
            }
            return doctorList;
        }

        public string getLastOpeartionNo()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT PatientOperationNo FROM PatientOperation_Tbl " +
                    "ORDER BY PatientOperationNo DESC LIMIT 1 ", conn);
                MySqlDataReader reader = command.ExecuteReader();
                int id = reader.Read() ? int.Parse(reader["PatientOperationNo"].ToString()) + 1 : 1;
                return id.ToString();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from getLastOperationNo DB" + e.Message);
            }
            return "0";
        }

        public bool isScheduleAvailable(int doctorId, DateTime dateSchedule, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT * FROM appointment_tbl " +
                           "WHERE DoctorID = @DoctorID " +
                           "AND DateSchedule = @DateSchedule " +
                           "AND (StartTime < @EndTime AND EndTime > @StartTime)";


                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                command.Parameters.AddWithValue("@DateSchedule", dateSchedule.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@StartTime", startTime);
                command.Parameters.AddWithValue("@EndTime", endTime);
                MySqlDataReader reader = command.ExecuteReader();
                return reader.Read() ? false : true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error from checkSchedule DB" + ex.Message);
            }
            return true;
        }

        public int insertPatient(Patient patient,int frontDeskId)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "INSERT INTO patient_tbl(FirstName, MiddleName, LastName, Age, Gender, Address, ContactNumber, BirthDate) " +
                    "VALUES(@FirstName, @MiddleName, @LastName, @Age, @Gender, @Address, @ContactNumber, @BirthDate); " + "SELECT LAST_INSERT_ID();";
                
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@MiddleName", patient.MiddleName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@Age", patient.Age);
                command.Parameters.AddWithValue("@Gender", patient.Gender);
                command.Parameters.AddWithValue("@Address", patient.Address);
                if (patient.ContactNumber != null)
                {
                    command.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
                }
                command.Parameters.AddWithValue("@BirthDate", patient.BirthDate.ToString("yyyy-MM-dd"));
                object result = command.ExecuteScalar();
                int patientid = Convert.ToInt32(result);
                conn.Close();

                insertPatientFrontDesk(patientid,frontDeskId);
                return patientid;

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from insertPatient DB" + e.Message);
            }
            return 0;
        }

        private void insertPatientFrontDesk(int patientid, int frontDeskId)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "INSERT INTO frontdeskpatient_tbl ( FrontDeskID, PatientId) VALUES (@FrontDeskID, @PatientID)";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@FrontDeskID", frontDeskId);
                command.Parameters.AddWithValue("@PatientID", patientid);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from insertPatientFrontdek() DB" + e.Message);
            }
        }

        public bool insertPatientOperation(PatientOperation patientOperation)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                MySqlTransaction trans = conn.BeginTransaction();
                string query = "INSERT INTO patientoperation_tbl(PatientID, RoomNo, Bill ) " +
                    "VALUES(@PatientID, @RoomNo, @Bill); " + "SELECT LAST_INSERT_ID();";
                MySqlCommand command = new MySqlCommand(query, conn,trans);
                command.Parameters.AddWithValue("@PatientID", patientOperation.PatientId);
                command.Parameters.AddWithValue("@RoomNo", patientOperation.RoomNo);
                command.Parameters.AddWithValue("@Bill", patientOperation.Bill);
                roomSetting(patientOperation.RoomNo);
                object result = command.ExecuteScalar();
                int patientOperationNo = Convert.ToInt32(result);

                foreach (var record in patientOperation.Schedules)
                {
                    query = "INSERT INTO appointment_tbl(PatientOperationNo, DoctorID, OperationCode, DateSchedule, StartTime, EndTime) " +
                        "VALUES(@PatientOperationNo, @DoctorID, @OperationCode, @DateSchedule, @StartTime, @EndTime)";
                    MySqlCommand detailcommand = new MySqlCommand(query, conn,trans);
                    detailcommand.Parameters.AddWithValue("@PatientOperationNo", patientOperationNo);
                    detailcommand.Parameters.AddWithValue("@DoctorID", record.Item1.DoctorID);
                    detailcommand.Parameters.AddWithValue("@OperationCode", record.Item2.OperationCode);
                    detailcommand.Parameters.AddWithValue("@DateSchedule", record.Item3.ToString("yyyy-MM-dd"));
                    detailcommand.Parameters.AddWithValue("@StartTime", record.Item4);
                    detailcommand.Parameters.AddWithValue("@EndTime", record.Item5);
                    detailcommand.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();

        
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error from insertPatientOperation DB" + ex.Message);
            }
            return false;
        }
        private void roomSetting(int roomNo)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "UPDATE rooms_tbl SET Occupation = 'Occupied' WHERE RoomNo = @RoomNo";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@RoomNo", roomNo);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("Error from roomSetting DB" + ex.Message);
            }
        }

        public List<PatientAppointment> getPatients(int doctorid)
        {
            List<PatientAppointment> patientAppointments = new List<PatientAppointment>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query =
                    "SELECT appointment_tbl.*, patientoperation_tbl.*,patient_tbl.*, operation_tbl.Name, operation_tbl.Duration " +
                    "FROM appointment_tbl " +
                    "LEFT JOIN patientoperation_tbl ON patientoperation_tbl.PatientOperationNo = appointment_tbl.PatientOperationNo " +
                    "LEFT JOIN patient_tbl on patientoperation_tbl.PatientID = patient_tbl.PatientId " +
                    "LEFT JOIN operation_tbl on appointment_tbl.OperationCode = operation_tbl.OperationCode " +  
                    "WHERE DoctorID = @DoctorID";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DoctorID", doctorid);
                MySqlDataReader reader = command.ExecuteReader(); 
                while (reader.Read())
                {

                    int roomNo = reader.GetInt32("RoomNo");
                    string diagnosis = reader["Diagnosis"] != DBNull.Value ? reader.GetString("Diagnosis") : "";
                    Patient patient = new Patient(
                        reader.GetInt32("PatientID"),
                        reader.GetString("FirstName"),
                        reader.GetString("MiddleName"),
                        reader.GetString("LastName"),
                        reader.GetInt32("Age"),
                        reader.GetString("Gender"),
                        reader.GetString("Address"),
                        reader.GetString("ContactNumber"),
                        reader.GetDateTime("BirthDate")
                     );

                    DateTime dateScheduled = reader.GetDateTime("DateSchedule");
                    TimeSpan startTime = reader.GetTimeSpan("StartTime");
                    TimeSpan endTime = reader.GetTimeSpan("EndTime");
                    TimeSpan duration = reader.GetTimeSpan("Duration");
                    string operationName = reader.GetString("Name");
                    int detailid = reader.GetInt32("DetailID");
                    DoctorPatientSchedule schedule = new DoctorPatientSchedule(dateScheduled, startTime, endTime,operationName, detailid, duration, diagnosis);


                    PatientAppointment patientAppointment = new PatientAppointment(roomNo, patient, schedule);
                    patientAppointments.Add(patientAppointment);
                }


                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error from getPatients DB" + ex.Message);
            }

            return patientAppointments;
        }

        public bool updateSchedule(DoctorPatientSchedule schedule)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "UPDATE appointment_tbl SET DateSchedule = @DateSchedule, StartTime = @StartTime, EndTime = @EndTime, Diagnosis = @Diagnosis WHERE DetailID = @DetailID";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DateSchedule", schedule.ScheduleDate);
                command.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                command.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                command.Parameters.AddWithValue("@Diagnosis", schedule.Diagnosis);
                command.Parameters.AddWithValue("@DetailID", schedule.DetailID);
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error from updateSchedule DB" + ex.Message);
            }
            return false;
        }

        private const string driver = "server=localhost;username=root;pwd=root;database=myclinic_db";
    }
}
