using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem
{
    public class PatientOperation
    {
        private Dictionary<Operation, Doctor> doctorOperation;
        private int roomNo;
        private int patientId;
        private double bill;

        List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>> schedules = new List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>>();

        public PatientOperation(int roomNo, int patientId, List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>> schedules, double bill)
        {
            this.schedules = schedules;
            this.roomNo = roomNo;
            this.patientId = patientId;
            this.bill = bill;
        }

      
        public int RoomNo { get => roomNo; }
        public int PatientId { get => patientId;  }

        public double Bill { get => bill; }
        public List<Tuple<Doctor, Operation, DateTime, TimeSpan, TimeSpan>> Schedules { get => schedules; }
    }
}
