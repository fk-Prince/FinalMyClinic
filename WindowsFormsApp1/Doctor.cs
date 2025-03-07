using System;


namespace WindowsFormsApp1
{
    public class Doctor
    {

        private int doctorID;
        private string doctorFirstName;
        private string doctorMiddleName;
        private string doctorLastName;
        private string doctorAge;
        private string pin;
        private string dateHired;
        private string gender;

        public Doctor(string doctorFirstName, string doctorMiddleName, string doctorLastName, string doctorAge, string pin, string dateHired, string gender)
        {

            this.doctorFirstName = doctorFirstName;
            this.doctorMiddleName = doctorMiddleName;
            this.doctorLastName = doctorLastName;
            this.doctorAge = doctorAge;
            this.pin = pin;
            this.dateHired = dateHired;
            this.gender = gender;
        }

        public Doctor(int doctorID, string doctorFirstName, string doctorMiddleName, string doctorLastName, string doctorAge, string pin, string dateHired, string gender)
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

        public int DoctorID { get => doctorID;  }
        public string DoctorFirstName { get => doctorFirstName;  }
        public string DoctorMiddleName { get => doctorMiddleName;  }
        public string DoctorLastName { get => doctorLastName; }
        public string DoctorAge { get => doctorAge;  }
        public string Pin { get => pin; set => pin = value; }
        public string DateHired { get => dateHired;}
        public string Gender { get => gender;  }
    }
}
