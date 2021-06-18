using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SportsCashier.Models
{
    public class SportsData
    {
        public int Code { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public double Price { get; private set; }
        public string NamePath => $"{Name} - {Category}";

        public SportsData(int code, string name, string category, double price)
        {
            Code = code;
            Name = name;
            Category = category;
            Price = price;
        }

        public static List<SportsData> GetSpoertsData
        {
            get
            {
                return new List<SportsData>
                {
                    new SportsData(5002101, "Swimming", "Practising", 250),
                    new SportsData(5002102, "Swimming", "Schools", 250),
                    new SportsData(5002103, "Swimming", "Ready", 250),
                    new SportsData(5002104, "Swimming", "Team", 150),
                    new SportsData(5002105, "Swimming", "Learning old", 500),
                    new SportsData(5002106, "Swimming", "Free", 300),
                    new SportsData(5002107, "Swimming", "Private", 600),
                    new SportsData(5002108, "Swimming", "Private Group", 1625),
                };
            }
        }
    }
}
