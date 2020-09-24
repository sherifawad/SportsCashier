using SportsCashier.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.Models
{
    [Table("Players")]
    public class PlayerModel : BaseDatabaseItem
    {
        public string PlayerName { get; set; }

        public double PlayerPayment { get; set; }

        [ForeignKey(typeof(MemberModel))]
        public int MemberModelId { get; set; }

        [ManyToMany(typeof(PlayerSport), CascadeOperations = CascadeOperation.All)]
        public List<Sport> Sports { get; set; }
    }
}
