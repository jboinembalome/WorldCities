using System.Collections.Generic;
using WorldCities.Application.Helpers;
using WorldCities.Application.Interfaces.Common;

namespace WorldCities.Application.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Gets a <see cref="PagedList{T}"/> <paramref name="source"/> enumeration with
        /// a given <paramref name="page"/> and specified <paramref name="pageSize"/>.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IEnumerable{T}"/> to paginate.</typeparam>
        /// <param name="source">Source enumeration to paginate.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="count">Total records in source if known.</param>
        /// <returns>Set of items on a given <paramref name="page"/> for specified <paramref name="pageSize"/>.</returns>
        /// <remarks>
        /// If count is known or can be calculated early, specify the value in the
        /// <paramref name="count"/> parameter as an optimization to prevent a full
        /// enumeration on <paramref name="source"/> to get a count.
        /// </remarks>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page, int pageSize, int count = -1)
        {
            return new PagedList<T>(source, page, pageSize, count);
        }
    }
}
