using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Models
{
    public class PlayerSport
    {
        private int code;

        public string Name { get; private set; }
        public string Icon { get; private set; }
        public double Price { get; private set; }
        public int Code 
        {
            get => code;
            set 
            {
                code = value; 
                var sportData = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == value);
                Name = sportData?.Name;
                Icon = sportData?.Icon;
                Price = sportData.Price;

            }
        }
        public bool EditMode { get; set; }
    }
}
