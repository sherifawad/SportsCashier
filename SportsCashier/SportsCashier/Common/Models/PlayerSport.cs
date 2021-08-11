using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace SportsCashier.Common.Models
{
    public class PlayerSport : ObservableObject
    {
        private int code;
        private bool isChecked;
        private bool isPaid;

        public string Name { get; private set; }
        public string Icon { get; private set; }
        public decimal Price { get; private set; }
        public double Discount { get; internal set; }
        public decimal Total { get; internal set; }
        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }
        public bool IsPaid { get => isPaid; set => SetProperty(ref isPaid, value); }
        public int Code
        {
            get => code;
            set
            {
                code = value;
                var sportData = SportsData.GetSpoertsData.FirstOrDefault(x => x.Code == value);
                Name = sportData?.Name;
                Icon = sportData?.Icon;
                Price = sportData.Price;

            }
        }
        public bool EditMode { get; set; }
    }
}
