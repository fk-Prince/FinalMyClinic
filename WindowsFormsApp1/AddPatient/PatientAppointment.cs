using System;
using System.Collections.Generic;

namespace ClinicSystem.AddPatient
{



    public class PatientAppointment
    {

        private int roomNo;
        private Patient patient;
        private DoctorPatientSchedule schedules;


        public PatientAppointment(int roomNo, Patient patient, DoctorPatientSchedule schedules)
        {
            this.roomNo = roomNo;
            this.patient = patient;
            this.schedules = schedules;
        }

        public DoctorPatientSchedule Schedules { get => schedules; }
        public int RoomNo { get => roomNo; }
        public Patient Patient { get => patient; }
  
    }
}
