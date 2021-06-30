using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public class SportHistory : BaseModel
    {
        private DateTime receiteDate;

        public double Discount { get; set; }
        public double Price { get; set; }
        public int ReceiteNumber { get; set; }
        public int Code { get; set; }
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