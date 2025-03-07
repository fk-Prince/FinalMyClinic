using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1.AddPatient
{
    public class DatabaseAddPatient
    {
        public DatabaseAddPatient() { }

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
                string query = "SELECT * FROM doctoroperation_tbl" +
                    " LEFT JOIN doctor_tbl " +
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
                        reader.GetInt32("Age").ToString(),
                        reader.GetString("Pin"),
                        reader.GetDateTime("DateHired").ToString("yyyy-MM-dd"),
                        reader.GetString("Gender")
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
            return "1";
        }

        public bool isScheduleAvailable(int id, DateTime date, TimeSpan start, TimeSpan end)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT * FROM patientoperationdetails_tbl " +
                           "WHERE DoctorID = @DoctorID " +
                           "AND DateSchedule = @DateSchedule " +
                           "AND (StartTime < @EndTime AND EndTime > @StartTime)";


                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DoctorID", id);
                command.Parameters.AddWithValue("@DateSchedule", date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@StartTime", start);
                command.Parameters.AddWithValue("@EndTime", end);

                MySqlDataReader reader = command.ExecuteReader();

           
                return reader.Read() ? false : true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error from checkSchedule DB" + ex.Message);
            }
            return true;
        }

        public int insertPatient(Patient patient)
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
                conn.Close();
                return Convert.ToInt32(result);

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error from insertPatient DB" + e.Message);
            }
            return 0;
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
                    query = "INSERT INTO patientoperationdetails_tbl(PatientOperationNo, DoctorID, OperationCode, DateSchedule, StartTime, EndTime) " +
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

        private const string driver = "server=localhost;username=root;pwd=root;database=myclinic_db";
    }
}
