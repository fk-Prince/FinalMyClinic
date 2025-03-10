using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicSystem.AddPatient;

namespace ClinicSystem
{
    public class PatientOperation
    {
        private Dictionary<Operation, Doctor> doctorOperation;
        private int roomNo;
        private int patientId;
        private double bill;
        private List<DoctorOperation> doctOperaiton;

       

        public PatientOperation(int roomNo, int patientId, List<DoctorOperation> doctOperaiton, double bill)
        {
            this.doctOperaiton = doctOperaiton;
            this.roomNo = roomNo;
            this.patientId = patientId;
            this.bill = bill;
        }

      
        public int RoomNo { get => roomNo; }
        public int PatientId { get => patientId;  }

        public double Bill { get => bill; }
        public List<DoctorOperation> DoctorOperation { get => doctOperaiton; }
    }
}
