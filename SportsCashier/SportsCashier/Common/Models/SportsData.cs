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
        public bool CanDiscounted { get; private set; }
        public string NamePath => $"{Name} - {Category}";

        public SportsData(int code, string name, string category, double price, bool canDiscounted)
        {
            Code = code;
            Name = name;
            Category = category;
            Price = price;
            CanDiscounted = canDiscounted;
        }

        public static List<SportsData> GetSpoertsData
        {
            get
            {
                return new List<SportsData>
                {
                    // Swimming
                    new SportsData(5002101, "Swimming", "Practising", 250, true),
                    new SportsData(5002102, "Swimming", "Schools", 250, true),
                    new SportsData(5002103, "Swimming", "Ready", 250, true),
                    new SportsData(5002104, "Swimming", "Team", 150, true),
                    new SportsData(5002105, "Swimming", "Learning old", 500, true),
                    new SportsData(5002106, "Swimming", "Free", 30, false),
                    new SportsData(5002107, "Swimming", "Private", 600, false),
                    new SportsData(5002108, "Swimming", "Private Group", 1625, false),

                    //jump
                    new SportsData(5002001, "jump", "normal", 200, true),
                    new SportsData(5002002, "jump", "Ayrobics", 300, true),
                    new SportsData(5002003, "jump", "Private", 50, false),

                    //Gymanisctics
                    new SportsData(5003101, "Gym", "Normal", 200, true),
                    new SportsData(5003102, "Gym", "Iron", 150, true),
                    new SportsData(5003103, "Gym", "Private", 325, false),
                    new SportsData(5003104, "Gym", "Sungle class", 40, false),

                    //Squash
                    new SportsData(5001801, "Squash", "Academy", 350, true),
                    new SportsData(5001802, "Squash", "Schools", 350, true),
                    new SportsData(5001803, "Squash", "Team", 400, true),
                    new SportsData(5001804, "Squash", "Private", 75, false),
                    new SportsData(5001805, "Squash", "Team Rent", 35, false),
                    new SportsData(5001806, "Squash", "Normal Rent", 75, false),   
                    new SportsData(5001505, "Squash", "Glass Rent", 80, false),


                    //Tennis
                    new SportsData(5001502, "Tennis", "Schools", 300, true),
                    new SportsData(5001503, "Tennis", "Team", 450, true),
                    new SportsData(5001504, "Tennis", "Morning Private Single", 60, false),
                    new SportsData(5001504, "Tennis", "Evening Private Single", 90, false),
                    new SportsData(5001504, "Tennis", "Morning Private Double", 70, false),
                    new SportsData(5001504, "Tennis", "Evening Private Double", 130, false),
                    new SportsData(5001504, "Tennis", "Morning Private Triple", 90, false),
                    new SportsData(5001504, "Tennis", "Evening Private Triple", 135, false),
                    new SportsData(5001504, "Tennis", "Morning Rent", 40, false),
                    new SportsData(5001504, "Tennis", "Evening Rent", 100, false),

                    //Track Sports
                    new SportsData(5001400, "Track Sports", "Normal", 200, true),
                    new SportsData(5001401, "Track Sports", "Private", 100, false),

                    //New Five
                    new SportsData(5004500, "New Five", "Double", 200, true),
                    new SportsData(5004501, "New Five", "Triple", 250, true),

                    //Football
                    new SportsData(5000300, "Football", "Normal", 150, true),
                    new SportsData(5000400, "Football", "Special", 250, true),

                    //Othe Sports
                    new SportsData(5001200, "Tikwendo", "", 150, true),
                    new SportsData(5001300, "Jodo", "", 150, true),
                    new SportsData(5001100, "Karate", "", 150, true),
                    new SportsData(5002400, "Butify Body", "", 150, true),
                    new SportsData(5002500, "Heavy Lefting", "", 150, true),
                    new SportsData(5002300, "Boxing", "", 150, true),
                    new SportsData(5000900, "Handball", "", 150, true),
                    new SportsData(5000500, "Basketball", "", 150, true),
                    new SportsData(5000700, "Vollyball", "", 150, true),
                    new SportsData(5001600, "PingPong", "", 150, true),


                };
            }
        }
    }
}
