using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Models
{
    public class History : BaseModel
    {
        public DateTime Date { get; set; }
        public List<MockSportModel> Sports { get; set; } = new List<MockSportModel>();

        public List<MockSportModel> SportsToAlert
        {
            get
            {
                List<MockSportModel> result = new List<MockSportModel>();
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
        public MockSportModel? AlertSport
        {
            get
            {
                MockSportModel? result = null;
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