using SportsCashier.Common.Models;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Helpers
{
    public class CodeToSportDataHelpers
    {
        public static PlayerSport ConvertToSport(int code)
        {
            var sport = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == code);
            return new PlayerSport { Code = code};
        }
        public static List<PlayerSport> ConvertToSportList(List<int> codeList)
        {
            List<PlayerSport> list = new List<PlayerSport>();
            foreach (var item in codeList)
            {
                var sport = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == item);
                list.Add(new PlayerSport { Code = item });
            }

            return list;
        }
    }
}
