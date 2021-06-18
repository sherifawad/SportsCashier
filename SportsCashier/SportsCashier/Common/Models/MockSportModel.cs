using System;

namespace SportsCashier.Models
{
    public class MockSportModel
    {
        private DateTime receiteDate;

        public string Name { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int ReceiteNumber { get; set; }
        public int code { get; set; }
        public DateTime ReceiteDate
        {
            get => receiteDate;
            set
            {
                receiteDate = value;
                Alert = receiteDate.AddMonths(1);
            }
        }
        public DateTime Alert { get; set; }
    }
}