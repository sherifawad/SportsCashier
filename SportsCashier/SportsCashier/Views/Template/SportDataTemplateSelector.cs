using DataBase.Models;
using SportsCashier.Common.Models;
using SportsCashier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SportsCashier.Views.Template
{
    public class SportDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NoramlSportTemplate { get; set; }
        public DataTemplate EditSportTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var template = ((SportHistoryDto)item).EditMode ? EditSportTemplate : NoramlSportTemplate;
            return template;
        }
    }
}