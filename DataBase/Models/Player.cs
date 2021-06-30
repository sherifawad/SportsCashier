using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DataBase.Models
{
    public class Player : BaseModel
    {
        public string Name { get; set; }
        public bool Hide { get; set; }
        public string Image { get; set; }
        public List<int> Sports { get; set; } = new List<int>();
        public List<History> Histories { get; set; } = new List<History>();

        //public List<SportHistory> SportsToAlert
        //{
        //    get
        //    {
        //        List<SportHistory> result = new List<SportHistory>();
        //        foreach (var sport in Histories.SelectMany(x => x.SportsToAlert))
        //        {
        //            if (result.LastOrDefault() == null)
        //            {
        //                result.Add(sport);
        //            }
        //            else
        //            {

        //                // if the alert time for sport  is sooner than the rsult
        //                // set the result value to the current sport alert else
        //                // if is the same day add to list
        //                if (result.Last().Alert.Date > sport.Alert.Date)
        //                {
        //                    result.Clear();
        //                    result.Add(sport);
        //                }
        //                else if (result.Last().Alert.Date == sport.Alert.Date)
        //                {
        //                    result.Add(sport);
        //                }
        //            }
        //        }
        //        if (result.Count > 0)
        //        {
        //            AlertSport = result.Last();
        //        }
        //        else
        //        {
        //            AlertSport = null;
        //        }
        //        return result;
        //    }
        //}

        //public SportHistory? AlertSport { get; set; }

        //public MockSportModel? AlertSport
        //{
        //    get
        //    {
        //        MockSportModel? result = null;
        //        // loop through all sport alert
        //        foreach (var alertSport in Histories.Select(x => x.AlertSport))
        //        {
        //            if (result != null)
        //            {
        //                // if the alert time for sport alert is sooner than the rsult and within 45 days
        //                // set the result value to the current sport alert
        //                if (alertSport != null && result.Alert > alertSport.Alert.AddDays(45))
        //                {
        //                    result = alertSport;
        //                }
        //            }
        //            else
        //            {
        //                result = alertSport;
        //            }
        //        }
        //        return result;
        //    }
        //}
    }
}