using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Models
{
    public class Payoutput
    {
        public string MemberShipCode { get; set; }
        public List<PlayerPayoutput> PlayerPayoutput { get; set; } = new List<PlayerPayoutput>();
    }

    public class PlayerPayoutput
    {
        public string Name { get; set; }
        public List<int> SportsCodeList { get; set; } = new List<int>();

        public List<PlayerSport> GetSports
        {
            get
            {
                List<PlayerSport> list = new List<PlayerSport>();
                foreach (var code in SportsCodeList)
                {
                    list.Add(new PlayerSport
                    {
                        Code = code
                    });
                }

                if (list.Count > 0)
                    return list;

                return null;
            }

        }

    }
}
