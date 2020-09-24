using SportsCashier.Models;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.DataBase
{
    public class PlayerSport : BaseDatabaseItem
    {
        [ForeignKey(typeof(PlayerModel))]
        public int PlayerModelId { get; set; }

        [ForeignKey(typeof(Sport))]
        public int SportId { get; set; }
    }
}
