using System;
using System.Collections.Generic;

namespace ClinicSystem
{
    public class Operation
    {
        private string operationCode;
        private string operationName;
        private DateTime dateAdded;
        private string description;
        private double price;
        private TimeSpan duration;
        private List<Doctor> doctorList = new List<Doctor>();
        public string OperationCode => operationCode;
        public string OperationName => operationName;
        public DateTime DateAdded => dateAdded;
        public string Description => description;
        public double Price => price;
        public TimeSpan Duration => duration;
        public List<Doctor> DoctorList => doctorList;




        public Operation(string operationCode, string operationName, DateTime dateAdded, string description, double price, TimeSpan duration)
        {
            this.operationCode = operationCode;
            this.operationName = operationName;
            this.dateAdded = dateAdded.Date;
            this.description = description;
            this.price = price;
            this.duration = duration;
        }

       
    }
}
