using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.AddPatient
{



    public class PatientAppointment
    {

        private int roomNo;
        private Patient patient;
        private DateTime scheduleDate;
        private TimeSpan startTime;
        private TimeSpan endTime;

        public PatientAppointment(int roomNo, Patient patient, DateTime scheduleDate, TimeSpan startTime, TimeSpan endTime)
        {
            this.roomNo = roomNo;
            this.patient = patient;
            this.scheduleDate = scheduleDate;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public int RoomNo { get => roomNo; }
        public Patient Patient { get => patient; }
        public DateTime ScheduleDate { get => scheduleDate;  }
        public TimeSpan StartTime { get => startTime; }
        public TimeSpan EndTime { get => endTime;  }
    }
}
