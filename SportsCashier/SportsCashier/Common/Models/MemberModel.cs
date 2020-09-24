using SportsCashier.DataBase;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.Models
{
    [Table("Members")]
    public class MemberModel : BaseDatabaseItem
    {
        public string MemberShipCode { get; set; }
        public string MemberShipYear { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<PlayerModel> MembershipNPlayers { get; set; }
    }
}
