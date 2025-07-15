using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> values)
        {
            return values.ElementAt(Utils.Random.Next(values.Count()));
        }
    }
}
