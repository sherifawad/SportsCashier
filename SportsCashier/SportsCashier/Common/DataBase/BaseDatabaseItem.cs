using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsCashier.DataBase
{
    public abstract class BaseDatabaseItem : IDatabaseItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
