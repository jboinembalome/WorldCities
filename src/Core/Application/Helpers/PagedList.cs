using System;
using System.Collections.Generic;
using System.Linq;
using WorldCities.Application.Interfaces.Common;

namespace WorldCities.Application.Helpers
{
    /// <summary>
    /// Defines a paged list for an enumeration.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IEnumerable{T}"/>.</typeparam>
    /// <remarks>
    /// This class returns the elements for the specified page number for a given page size and
    /// also provides the calculated page count based on input parameters.
    /// </remarks>
    public class PagedList<T> : IPagedList<T>
    {
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="PagedList{T}"/>.
        /// </summary>
        /// <param name="source">Source enumeration.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="count">Total records in source if known.</param>
        /// <remarks>
        /// If count is known or can be calculated early, specify the value in the
        /// <paramref name="count"/> parameter as an optimization to prevent a full
        /// enumeration on <paramref name="source"/> to get a count.
        /// </remarks>
        public PagedList(IEnumerable<T> source, int page, int pageSize, int count = -1)
        {
            Source = source;
            Page = page < 0 ? 0 : page;
            PageSize = pageSize;
            TotalCount = count > -1 ? count : source.Count(); // For many enumeration sources, Count() will map to Length or Count
            PageCount = CalculatePageCount(pageSize, TotalCount);
        }

        #endregion

        #region Properties
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<T> Source { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int PageSize { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Calculate page count based on page size and total item count.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="totalCount">The total count of elements.</param>
        /// <returns></returns>
        private static int CalculatePageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            return int.Parse(Math.Ceiling((decimal)totalCount / pageSize).ToString());
        }

        #endregion
    }
}
