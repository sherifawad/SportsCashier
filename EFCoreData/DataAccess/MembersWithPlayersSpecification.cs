using EFCoreData.Models;
using EFCoreData.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EFCoreData.DataAccess
{
    public class MembersWithPlayersSpecification : BaseSpecification<Membership>
    {
        public MembersWithPlayersSpecification(int id) : base(x=> x.Id == id)
        {
            AddInclude(x => x.Players);
        }
    }
}
