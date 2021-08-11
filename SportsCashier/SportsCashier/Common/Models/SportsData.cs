using SportsCashier.Common.Helpers;
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
        public decimal Price { get; private set; }
        public bool CanDiscounted { get; private set; }
        public string Icon { get; private set; } = "\ue80e";

        public string NamePath => $"{Name} - {Category}";

        public SportsData(int code, string name, string category, decimal price, bool canDiscounted, string icon)
        {
            Code = code;
            Name = name;
            Category = category;
            Price = price;
            CanDiscounted = canDiscounted;
            Icon = icon;
        }

        public static List<SportsData> GetSpoertsData
        {
            get
            {
                return new List<SportsData>
                {
                    // Swimming
                    new SportsData(5002101, "Swimming", "Practising", 250, true, IconFont.IcofontSwimmer),
                    new SportsData(5002102, "Swimming", "Schools", 250, true, IconFont.IcofontSwimmer),
                    new SportsData(5002103, "Swimming", "Ready", 250, true, IconFont.IcofontSwimmer),
                    new SportsData(5002104, "Swimming", "Team", 150, true, IconFont.IcofontSwimmer),
                    new SportsData(5002105, "Swimming", "Learning old", 500, true, IconFont.IcofontSwimmer),
                    new SportsData(5002106, "Swimming", "Free", 30, false, IconFont.IcofontSwimmer),
                    new SportsData(5002107, "Swimming", "Private", 600, false, IconFont.IcofontSwimmer),
                    new SportsData(5002108, "Swimming", "Private Group", 1625, false, IconFont.IcofontSwimmer),

                    //Gymnastics
                    new SportsData(5002001, "Gymnastics", "normal", 200, true, IconFont.IcofontJumping),
                    new SportsData(5002002, "Gymnastics", "Ayrobics", 300, true, IconFont.IcofontJumping),
                    new SportsData(5002003, "Gymnastics", "Private", 50, false, IconFont.IcofontJumping),

                    //Gym
                    new SportsData(5003101, "Gym(fitness)", "Normal", 200, true, IconFont.IcofontGym),
                    new SportsData(5003102, "Gym(fitness)", "Iron", 150, true, IconFont.IcofontGym),
                    new SportsData(5003103, "Gym(fitness)", "Private", 325, false, IconFont.IcofontGym),
                    new SportsData(5003104, "Gym(fitness)", "Sungle class", 40, false, IconFont.IcofontGym),

                    //Squash
                    new SportsData(5001801, "Squash", "Academy", 350, true, IconFont.IcofontTennis),
                    new SportsData(5001802, "Squash", "Schools", 350, true, IconFont.IcofontTennis),
                    new SportsData(5001803, "Squash", "Team", 400, true, IconFont.IcofontTennis),
                    new SportsData(5001804, "Squash", "Private", 75, false, IconFont.IcofontTennis),
                    new SportsData(5001805, "Squash", "Team Rent", 35, false, IconFont.IcofontTennis),
                    new SportsData(5001806, "Squash", "Normal Rent", 75, false, IconFont.IcofontTennis),   
                    new SportsData(5001505, "Squash", "Glass Rent", 80, false, IconFont.IcofontTennis),


                    //Athletics
                    new SportsData(5001400, "Athletics(fitness)", "Normal", 200, true, IconFont.IcofontRunner),
                    new SportsData(5001401, "Athletics(fitness)", "Private", 100, false, IconFont.IcofontRunner),

                    //Football
                    new SportsData(5000300, "Football", "Normal", 150, true, IconFont.IcofontFootball),
                    new SportsData(5000400, "Football", "Special", 250, true, IconFont.IcofontFootball),

                    
                    //Tennis
                    new SportsData(5001502, "Tennis", "Schools", 300, true, IconFont.IcofontTennis),
                    new SportsData(5001503, "Tennis", "Team", 450, true, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Morning Private Single", 60, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Evening Private Single", 90, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Morning Private Double", 70, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Evening Private Double", 130, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Morning Private Triple", 90, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Evening Private Triple", 135, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Morning Rent", 40, false, IconFont.IcofontTennis),
                    new SportsData(5001504, "Tennis", "Evening Rent", 100, false, IconFont.IcofontTennis),

                    

                    //Modern pentathlon
                    new SportsData(5004500, "Modern pentathlon", "Double", 200, true, IconFont.IcofontAim),
                    new SportsData(5004501, "Modern pentathlon", "Triple", 250, true, IconFont.IcofontAim),

                    //Othe Sports
                    new SportsData(5001200, "Taekwondo ", "", 150, true, IconFont.IcofontKarate),
                    new SportsData(5001300, "Jodo", "", 150, true, IconFont.IcofontKarate),
                    new SportsData(5001100, "Karate", "", 150, true, IconFont.IcofontKarate),
                    new SportsData(5002400, "BodyBuilding", "", 150, true, IconFont.IcofontKarate),
                    new SportsData(5002500, "Weight lifting", "", 150, true, IconFont.IcofontMuscleWeight),
                    new SportsData(5002300, "Boxing", "", 150, true, IconFont.IcofontKarate),
                    new SportsData(5000900, "Handball", "", 150, true, IconFont.IcofontAirBalloon),
                    new SportsData(5000500, "Basketball", "", 150, true, IconFont.IcofontBasketball),
                    new SportsData(5000700, "Vollyball", "", 150, true, IconFont.IcofontVolleyball),
                    new SportsData(5001600, "PingPong", "", 150, true, IconFont.IcofontTableTennis),


                };
            }
        }

    }
}
