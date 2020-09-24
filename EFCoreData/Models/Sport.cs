using System.Collections.Generic;

namespace EFCoreData.Models
{
    public class Sport : BaseDatabaseItem
    {
        public string SportType { get; set; }
        public double SportPrice { get; set; }

        public string SportName { get; set; }

        public List<SportPlayer> Players { get; set; }
    }
}