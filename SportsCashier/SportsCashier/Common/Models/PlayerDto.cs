using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Models
{
    public class PlayerDto
    {
        private bool isChecked;

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Hide { get; set; }
        public string Image { get; set; }
        public bool IsChecked 
        {
            get => isChecked;
            set
            {
                isChecked = value;
                foreach (var item in Sports)
                {
                    item.IsChecked = value;
                }
            }
        }
        public List<PlayerSport> Sports { get; set; } = new List<PlayerSport>();
        public List<HistoryDto> Histories { get; set; } = new List<HistoryDto>();
        public List<SportHistoryDto> SportsToAlert
        {
            get
            {
                List<SportHistoryDto> result = new List<SportHistoryDto>();
                foreach (var sport in Histories.SelectMany(x => x.SportsToAlert))
                {
                    if (result.LastOrDefault() == null)
                    {
                        result.Add(sport);
                    }
                    else
                    {

                        // if the alert time for sport  is sooner than the rsult
                        // set the result value to the current sport alert else
                        // if is the same day add to list
                        if (result.Last().Alert.Date > sport.Alert.Date)
                        {
                            result.Clear();
                            result.Add(sport);
                        }
                        else if (result.Last().Alert.Date == sport.Alert.Date)
                        {
                            result.Add(sport);
                        }
                    }
                }
                if (result.Count > 0)
                {
                    AlertSport = result.Last();
                }
                else
                {
                    AlertSport = null;
                }
                return result;
            }
        }

        public SportHistoryDto? AlertSport { get; set; }
    }
}
