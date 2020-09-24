using System.Collections.Generic;

namespace EFCoreData.Models
{
    public class Player : BaseDatabaseItem
    {

        public string PlayerName { get; set; }

        public double PlayerPayment { get; set; }


        public List<SportPlayer> SportPlayer { get; set; }
    }
}