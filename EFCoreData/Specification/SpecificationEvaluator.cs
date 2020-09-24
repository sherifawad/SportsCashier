using System.Linq;
using EFCoreData.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreData.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseDatabaseItem
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
