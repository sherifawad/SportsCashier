using SportsCashier.DataBase;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.Models
{
    public class Invoice : BaseDatabaseItem
    {
        public long InvoiceNmber { get; set; }
        public long InvoiceGovNmber { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(typeof(Sport))]
        public int SportId { get; set; }

        [ForeignKey(typeof(PlayerModel))]
        public int PlayerId { get; set; }

        [ManyToOne]
        public PlayerModel Player { get; set; }

        [ManyToOne]
        public Sport Sport { get; set; }
    }
}
