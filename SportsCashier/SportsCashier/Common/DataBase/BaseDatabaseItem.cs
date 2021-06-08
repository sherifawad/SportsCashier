using SportsCashier.Services.MessagingService;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SportsCashier.DataBase
{
    public abstract class BaseDatabaseItem : IDatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }
}
