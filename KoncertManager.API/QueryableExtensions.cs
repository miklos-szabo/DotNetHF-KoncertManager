using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoncertManager.API
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ToQueryable<T>(this T instance)
        {
            return new[] { instance }.AsQueryable();
        }
    }
}
