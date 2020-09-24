using SportsCashier.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.Models
{
    public class SportModel
    {
        public string SportName { get; set; }
        public List<SportCaegory> Caegories { get; set; }
    }

    public class SportCaegory
    {
        public int Id { get; set; }
        public string SportType { get; set; }
        public double SportPrice { get; set; }
    }

    [Table("Sports")]
    public class Sport : BaseDatabaseItem
    {
        private SportCaegory sportCaegory;

        public string SportName { get; set; }

        [Ignore]
        public SportCaegory SportCaegory 
        {
            get 
            {
                return sportCaegory = new SportCaegory
                {
                    Id = Id,
                    SportPrice = SportPrice,
                    SportType = SportType
                };
            }
            set
            {
                sportCaegory = value;
                if (sportCaegory == null)
                    return;
                Id = sportCaegory.Id;
                SportType = sportCaegory.SportType;
                SportPrice = sportCaegory.SportPrice;
            }
        }

        public string SportType { get; set; }
        public double SportPrice { get; set; }

        [ManyToMany(typeof(PlayerSport), CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public List<PlayerModel> Players { get; set; }

    }

}
