using System;

namespace WorldCities.Application.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() 
            : base() { }
    }
}
