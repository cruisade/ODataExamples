using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace Simple.Auth.Example
{
    public static class ODataExtension
    {
        public static IQueryable<TEntity> ApplyODataQuery<TEntity>(
            this IQueryable<TEntity> queryableIn,
            ODataQueryOptions queryOptions)
            where TEntity : class
        {
            var querySettings = new ODataQuerySettings();
            var queryableOut = queryOptions.ApplyTo(queryableIn, querySettings) as IQueryable<TEntity>;
            return queryableOut;
        }
    }
}