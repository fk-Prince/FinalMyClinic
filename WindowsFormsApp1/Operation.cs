using System;

namespace WindowsFormsApp1
{
    public class Operation
    {
        private string operationCode;
        private string operationName;
        private DateTime dateAdded;
        private string description;
        private double price;
        private TimeSpan duration; 
        public string OperationCode => operationCode;
        public string OperationName => operationName;
        public DateTime DateAdded => dateAdded;
        public string Description => description;
        public double Price => price;
        public TimeSpan Duration => duration;

        public Operation(string operationCode, string operationName, DateTime dateAdded, string description, double price, TimeSpan duration)
        {
            this.operationCode = operationCode;
            this.operationName = operationName;
            this.dateAdded = dateAdded.Date;
            this.description = description;
            this.price = price;
            this.duration = duration;
        }

        public Operation(string operationName, DateTime dateAdded, string description, double price, TimeSpan duration)
        {
            this.operationName = operationName;
            this.dateAdded = dateAdded.Date;
            this.description = description;
            this.price = price;
            this.duration = duration;
        }
    }
}
