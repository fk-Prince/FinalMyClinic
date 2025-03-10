using System;
using System.Collections.Generic;

namespace ClinicSystem.AddPatient
{



    public class PatientAppointment
    {

        private int roomNo;
        private Patient patient;
        private DoctorPatientSchedule schedules;
        private string operationName;
        private string operationCode;


        public PatientAppointment(int roomNo, Patient patient, DoctorPatientSchedule schedules)
        {
            this.roomNo = roomNo;
            this.patient = patient;
            this.schedules = schedules;
        }

        public PatientAppointment(int roomNo, Patient patient, DoctorPatientSchedule schedules, string operationName, string operationCode) : this(roomNo, patient, schedules)
        {
            this.roomNo = roomNo;
            this.patient = patient;
            this.schedules = schedules;
            this.operationName = operationName;
            this.operationCode = operationCode;
        }

        public DoctorPatientSchedule Schedules { get => schedules; }
        public int RoomNo { get => roomNo; }
        public Patient Patient { get => patient; }
        public string OperationName { get => operationName; }
        public string OperationCode { get => operationCode;  }
    }
}
