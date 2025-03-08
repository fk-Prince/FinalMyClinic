﻿using System;
using System.Collections.Generic;


namespace WindowsFormsApp1
{
    public class Doctor
    {

        private int doctorID;
        private string doctorFirstName;
        private string doctorMiddleName;
        private string doctorLastName;
        private int doctorAge;
        private string pin;
        private DateTime dateHired;
        private string gender;


        public Doctor(int doctorID, string doctorFirstName, string doctorMiddleName, string doctorLastName, int doctorAge, string pin, DateTime dateHired, string gender)
        {
            this.doctorID = doctorID;
            this.doctorFirstName = doctorFirstName;
            this.doctorMiddleName = doctorMiddleName;
            this.doctorLastName = doctorLastName;
            this.doctorAge = doctorAge;
            this.pin = pin;
            this.dateHired = dateHired;
            this.gender = gender;
        }

        public int DoctorID { get => doctorID; }
        public string DoctorFirstName { get => doctorFirstName;  }
        public string DoctorMiddleName { get => doctorMiddleName; }
        public string DoctorLastName { get => doctorLastName; }
        public int DoctorAge { get => doctorAge;  }
        public string Pin { get => pin; set => pin = value; }
        public DateTime DateHired { get => dateHired;}
        public string Gender { get => gender;  }
    }
}
