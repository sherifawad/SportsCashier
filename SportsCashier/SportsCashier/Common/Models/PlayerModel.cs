using SportsCashier.DataBase;
using SportsCashier.Services.MessagingService;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SportsCashier.Models
{
    [Table("Players")]
    public class PlayerModel : BaseDatabaseItem
    {
        public string PlayerName { get; set; }
        [Ignore]
        public double PlayerPayment { get; set; }

        [ForeignKey(typeof(MemberModel))]
        public int MemberModelId { get; set; }

        [ManyToMany(typeof(PlayerSport), CascadeOperations = CascadeOperation.All)]
        public List<Sport> Sports { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Invoice> Invoices { get; set; }

    }
}
