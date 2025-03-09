using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ClinicSystem
{
    public class DataBaseOperation
    {
        private const string driver = "server=localhost;username=root;pwd=root;database=myclinic_db";

        public List<Operation> getOperations()
        {
            List<Operation> operations = new List<Operation>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT * FROM operation_tbl";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Operation operation = new Operation(
                        reader.GetString("operationcode"),
                        reader.GetString("name"),
                        reader.GetDateTime("dateadded"),
                        reader.GetString("description"),
                        reader.GetDouble("price"),
                        reader.GetTimeSpan("duration")
                    );
                    getDoctor(operation);
                    operations.Add(operation);
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR FROM GETOPERATIONS() DB " + ex.Message);
            }
            return operations;
        }

        private void getDoctor(Operation operation)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT * FROM doctoroperation_tbl " +
                    "LEFT JOIN doctor_tbl ON doctoroperation_tbl.DoctorId = doctor_tbl.doctorId " +
                    "WHERE OperationCode = @OperationCode";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@OperationCode", operation.OperationCode);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor(
                        reader.GetInt32("doctorid"),
                        reader.GetString("firstname"),
                        reader.GetString("middlename"),
                        reader.GetString("lastname"),
                        reader.GetInt32("age"),
                        reader.GetString("pin"),
                        reader.GetDateTime("datehired"),
                        reader.GetString("gender"),
                        reader.GetString("Address") );

                    operation.DoctorList.Add(doctor);
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("ERROR FROM GETDOCTOR() DB " + ex.Message);
            }
        }
        public bool insert(int frontDeskId, Operation operation)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "INSERT INTO operation_tbl(operationcode, name,frontdeskid, dateadded, description, price, duration) " +
                    "VALUES(@OperationCode, @Name, @frontdeskid, @DateAdded, @Description, @Price, @Duration)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OperationCode", operation.OperationCode);
                cmd.Parameters.AddWithValue("@Name", operation.OperationName);
                cmd.Parameters.AddWithValue("@DateAdded", operation.DateAdded);
                cmd.Parameters.AddWithValue("@Description", operation.Description);
                cmd.Parameters.AddWithValue("@Price", operation.Price);
                cmd.Parameters.AddWithValue("@frontdeskid", frontDeskId);
                cmd.Parameters.AddWithValue("@Duration", operation.Duration);
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR FROM INSERT() DB " + ex.Message);
            }
            return false;
        }


        public List<Operation> getOperationByDoctor(int id)
        {
            List<Operation> operations = new List<Operation>();
            try
            {
                MySqlConnection conn = new MySqlConnection(driver);
                conn.Open();
                string query = "SELECT doctoroperation_tbl.OperationCode, Operation_tbl.* " +
                    "FROM doctoroperation_tbl " +
                    "LEFT JOIN Operation_tbl " +
                    "ON Operation_tbl.OperationCode = doctoroperation_tbl.OperationCode " +
                    "WHERE DOCTORID = @DoctorID;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@DoctorID", id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Operation operation = new Operation(
                       reader.GetString("operationcode"),
                       reader.GetString("name"),
                       reader.GetDateTime("dateadded"),
                       reader.GetString("description"),
                       reader.GetDouble("price"),
                       reader.GetTimeSpan("duration")
                   );
                    operations.Add(operation);
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("ERROR FROM getOperationByDoctor() DB " + ex.Message);
            }

            return operations;
        }
    }
}