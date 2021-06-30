using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsCashier.Common.Models
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<SportHistoryDto> Sports { get; set; } = new List<SportHistoryDto>();


        public List<SportHistoryDto> SportsToAlert
        {
            get
            {
                List<SportHistoryDto> result = new List<SportHistoryDto>();
                foreach (var sport in Sports)
                {
                    // if the alert time for sport alert is sooner than the rsult
                    // set the result value to the current sport alert
                    if (result.LastOrDefault() == null)
                    {
                        result.Add(sport);
                    }
                    else
                    {

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
                return result;
            }
        }
        public SportHistoryDto? AlertSport
        {
            get
            {
                SportHistoryDto? result = null;
                // loop through all sport alert
                foreach (var sport in Sports)
                {
                    if (result != null)
                    {
                        // if the alert time for sport  is sooner than the rsult
                        // set the result value to the current sport alert else
                        // if is the same day add to list
                        if (result.Alert > sport.Alert)
                        {
                            result = sport;
                        }
                    }
                    else
                    {
                        result = sport;
                    }
                }
                return result;

            }
        }

    }
}
