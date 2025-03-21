﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem
{
    public class Patient
    {
        private int patientid;
        private string firstName;
        private string middleName;
        private string lastName;
        private int age;
        private string gender;
        private string address;
        private string contactNumber;
        private DateTime birthDate;


        public Patient(int id, string fname, string mname, string lname, int age, string gender, string contactNumber)
        {
            patientid = id;
            firstName = fname;
            middleName = mname;
            lastName = lname;
            this.age = age;
            this.gender = gender;
            this.contactNumber = contactNumber;
        }

        public Patient(string firstName, string middleName, string lastName, int age, string gender, string address, string contactNumber, DateTime birthDate)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.age = age;
            this.gender = gender;
            this.address = address;
            this.contactNumber = contactNumber;
            this.birthDate = birthDate.Date;
        }
        public Patient(int patientid ,string firstName, string middleName, string lastName, int age, string gender, string address, string contactNumber, DateTime birthDate)
        {
            this.patientid = patientid;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.age = age;
            this.gender = gender;
            this.address = address;
            this.contactNumber = contactNumber;
            this.birthDate = birthDate.Date;
        }

        public string FirstName { get => firstName; }
        public string MiddleName { get => middleName;}
        public string LastName { get => lastName;  }
        public int Age { get => age; }
        public int Patientid { get => patientid; }
        public string Gender { get => gender;  }
        public string Address { get => address;  }
        public string ContactNumber { get => contactNumber;  }
        public DateTime BirthDate { get => birthDate;  }



    }
}
