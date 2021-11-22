using WorldCities.Application.Interfaces.Common;
using System;

namespace WorldCities.Infrastructure.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
