using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.Common.Models
{
    public class SportHistoryDto : SportHistory
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool EditMode { get; set; }
    }
}
