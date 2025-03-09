using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicSystem.ViewPatients
{
    public partial class ViewPatientForm : Form
    {
        public ViewPatientForm()
        {
            InitializeComponent();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomNo", typeof(string));
            dt.Columns.Add("PatientID", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("MiddleName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Doctor Assigned", typeof(string));
            dt.Columns.Add("Date Admitted", typeof(TimeSpan));
            dt.Columns.Add("Date Discharged", typeof(TimeSpan));

            dataGridView.DataSource = dt;
        }
    }
}
