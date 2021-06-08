using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsCashier.ViewModels
{
    public class SportsViewModel : BaseViewModel
    {
        public List<Sport> Sports { get; set; }

        public SportsViewModel()
        {
            Sports = new List<Sport>();
        }

        public override async Task InitializeAsync()
        {
            Sports = await _sportsRepository.GetItemsAsync();
        }
    }
}
