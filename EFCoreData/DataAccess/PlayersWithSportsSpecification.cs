using EFCoreData.Models;
using EFCoreData.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EFCoreData.DataAccess
{
    public class PlayersWithSportsSpecification : BaseSpecification<Player>
    {
        public PlayersWithSportsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.SportPlayer);
        }
    }
}
