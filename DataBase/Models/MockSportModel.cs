using System;
using System.ComponentModel;

namespace DataBase.Models
{
    public class MockSportModel : BaseModel
    {
        private DateTime receiteDate;

        public string Name { get; set; }
        public string Icon { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int ReceiteNumber { get; set; }
        public int Code { get; set; }
        public bool EditMode { get; set; }
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