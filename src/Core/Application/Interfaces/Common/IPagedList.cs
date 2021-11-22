using System.Collections.Generic;

namespace WorldCities.Application.Interfaces.Common
{
    /// <summary>
    /// Defines an interface for <see cref="Helpers.PagedList{T}"/> instances that is usable
    /// regardless of any specified type.
    /// </summary>
    public interface IPagedList<T>
    {
        /// <summary>
        /// The data source.
        /// </summary>
        IEnumerable<T> Source { get; }

        /// <summary>
        /// Gets total count of elements in enumeration.
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets calculated page count based on page size and total items count.
        /// </summary>
        int PageCount { get; }

        /// <summary>
        /// Gets current page number.
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Gets current page size.
        /// </summary>
        int PageSize { get; }
    }
}
