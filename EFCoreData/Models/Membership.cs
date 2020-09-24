using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreData.Models
{
    public class Membership : BaseDatabaseItem
    {
        public string MemberShipCode { get; set; }
        public string MemberShipYear { get; set; }

        public List<Player> Players { get; set; }
    }
}
