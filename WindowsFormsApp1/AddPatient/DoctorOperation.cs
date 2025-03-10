using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.AddPatient
{
    public class DoctorOperation
    {
        private Doctor doctor;
        private Operation operation;
        private DoctorPatientSchedule schedule ;

        public DoctorOperation(Doctor doctor, Operation operation, DoctorPatientSchedule schedule)
        {
            this.doctor = doctor;
            this.operation = operation;
            this.schedule = schedule;
        }

        public Doctor Doctor { get => doctor; }
        public Operation Operation { get => operation;  }
        public DoctorPatientSchedule Schedule { get => schedule; }
    }
}
